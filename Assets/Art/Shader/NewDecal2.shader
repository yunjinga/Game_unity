Shader "Custom/NewDecal2"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Decal Texture", 2D) = "white" {}
	//_BumpMap("Normal Map", 2D) = "bump" {}
	//_BumpScale("Bump Scale", Float) = 1.0
	//_NormalClipThreshold("NormalClip Threshold", Range(0,0.3)) = 0.1
	_AlphaScale("Alpha Scale", Range(0,1)) = 1
		//_GlossinessTexture("Roughness Texture",2D) = "black" {}
		_MetallicTexture("Metallic Texture", 2D) = "white" {}
		_Normal("Normal Map", 2D) = "bump"{}
		_Glossiness("Smoothness", Range(0,1)) = 1.0
		_Metallic("Metallic", Range(0,1)) = 1.0
		_Emission("Emission",Color) = (0,0,0,1)
	}

		SubShader
		{
			Tags{ "Queue" = "Geometry+1" "LightMode" = "ForwardBase"}

			Pass
			{
				ZWrite Off
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma target 3.0
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#pragma multi_compile_fwdbase
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "UnityPBSLighting.cginc"
				#include "AutoLight.cginc"

				fixed4 _Color;
				sampler2D _MainTex;
				float4 _MainTex_ST;
				//sampler2D _BumpMap;
				//float4 _BumpMap_ST;
				sampler2D _MetallicTexture;
				float4 _MetallicTexture_ST;
				//sampler2D _GlossinessTexture;
				//float4 _GlossinessTexture_ST;
				sampler2D _Normal;
				fixed _Metallic;
				fixed _Glossiness;
				//sampler2D_float _CameraDepthTexture;
				sampler2D _NormalCopyRT;
				//float _NormalClipThreshold;
				sampler2D _CameraDepthTexture;
				float _AlphaScale;
				half3 _Emission;
				//half _Glossiness;
				//half _Metallic;
				/*
				struct a2v
				{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					float4 tangent : TANGENT;
					float4 texcoord : TEXCOORD;
				};
				*/
				struct v2f
				{
					float4 pos : SV_POSITION;
					float4 screenUV : TEXCOORD0;
					float3 ray : TEXCOORD1;
					float4 uv : TEXCOORD2;
					float3 worldPos : TEXCOORD3;

					float3 tSpace0:TEXCOORD4;
					float3 tSpace1:TEXCOORD5;
					float3 tSpace2:TEXCOORD6;
					//用于将xyz立方体物体空间转化到世界空间
					/*
					float3 xDir : TEXCOORD2;
					float3 yDir : TEXCOORD3;
					float3 zDir : TEXCOORD4;
					*/

					UNITY_FOG_COORDS(7)
					UNITY_SHADOW_COORDS(8)
	#if UNITY_SHOULD_SAMPLE_SH
					half3 sh: TEXCOORD9; // SH
	#endif
				};

				v2f vert(appdata_full v)
				{
					v2f o;
					UNITY_INITIALIZE_OUTPUT(v2f, o);
					o.pos = UnityObjectToClipPos(v.vertex);
					o.screenUV = ComputeScreenPos(o.pos);
					o.ray = UnityObjectToViewPos(v.vertex).xyz * float3(-1,-1,1);

					o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					float3 worldNormal = UnityObjectToWorldNormal(v.normal);
					half3 worldTangent = UnityObjectToWorldDir(v.tangent);
					half3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w * unity_WorldTransformParams.w;

					o.tSpace0 = float3(worldTangent.x, worldBinormal.x, worldNormal.x);
					o.tSpace1 = float3(worldTangent.y, worldBinormal.y, worldNormal.y);
					o.tSpace2 = float3(worldTangent.z, worldBinormal.z, worldNormal.z);
					//将xyz立方体物体空间转化到世界空间
					/*
					o.xDir = mul((float3x3)unity_ObjectToWorld, float3(1, 0, 0));
					o.yDir = mul((float3x3)unity_ObjectToWorld, float3(0, 1, 0));
					o.zDir = mul((float3x3)unity_ObjectToWorld, float3(0, 0, 1));
					*/
	#ifndef LIGHTMAP_ON
	#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
					o.sh = 0;
					// Approximated illumination from non-important point lights
					//如果有顶点光照的情况（超出系统限定的灯光数或者被设置为non-important灯光）
	#ifdef VERTEXLIGHT_ON
					o.sh += Shade4PointLights(
						unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
						unity_LightColor[0].rgb, unity_LightColor[1].rgb,
						unity_LightColor[2].rgb, unity_LightColor[3].rgb,
						unity_4LightAtten0, o.worldPos, worldNormal);
	#endif
					//球谐光照计算（光照探针，超过顶点光照数量的球谐灯光）
					o.sh = ShadeSHPerVertex(worldNormal, o.sh);
	#endif
	#endif // !LIGHTMAP_ON
					UNITY_TRANSFER_LIGHTING(o, v.texcoord1.xy);

					UNITY_TRANSFER_FOG(o, o.pos);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{

					float2 uv = i.screenUV.xy / i.screenUV.w;
					float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv);
					//float linear01Depth = Linear01Depth(depth);
					i.ray = i.ray * (_ProjectionParams.z / i.ray.z);

					// 要转换成线性的深度值 
					depth = Linear01Depth(depth);

					float4 vpos = float4(i.ray * depth,1);
					//转化为世界空间坐标
					float3 wpos = mul(unity_CameraToWorld, vpos).xyz;
					//转化为物体空间坐标
					float3 opos = mul(unity_WorldToObject, float4(wpos,1)).xyz;
					//剔除掉在立方体外面的部分
					clip(float3(0.5,0.5,0.5) - abs(opos.xyz));

					/*
					//GBuffer拷贝的NormalRT取世界空间法线方向
					float3 worldNormal = tex2D(_NormalCopyRT, uv).rgb * 2.0 - 1.0;
					//根据法线方向剔除与xz面垂直的面上投影的内容
					float3 yDir = normalize(i.yDir);
					clip(dot(yDir, worldNormal) - _NormalClipThreshold);
					*/

					// 转换到 [0,1] 区间，使用物体空间的xz坐标作为采样uv
					float2 texUV = opos.xz + 0.5;
					//outCol = tex2D(_MainTex, texUV);
					half3 normalTex = UnpackNormal(tex2D(_Normal, i.uv));
					half3 worldNormal = half3(dot(i.tSpace0, normalTex), dot(i.tSpace1, normalTex),
						dot(i.tSpace2, normalTex));
					worldNormal = normalize(worldNormal);
					fixed3 lightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
					float3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));


					SurfaceOutputStandard o;
					UNITY_INITIALIZE_OUTPUT(SurfaceOutputStandard, o);
					fixed4 AlbedoColorSampler = tex2D(_MainTex, texUV) * _Color;
					o.Albedo = AlbedoColorSampler.rgb;
					//自发光
					o.Emission = _Emission;
					fixed4 MetallicSmoothnessSampler = tex2D(_MetallicTexture, texUV);
					o.Metallic = MetallicSmoothnessSampler.r * _Metallic;
					o.Smoothness = MetallicSmoothnessSampler.a * _Glossiness;
					o.Alpha = _AlphaScale * AlbedoColorSampler.a;
					o.Normal = worldNormal;
					UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos)

					UnityGI gi;//声明变量
					UNITY_INITIALIZE_OUTPUT(UnityGI, gi);//初始化归零
					gi.indirect.diffuse = 0;//indirect部分先给0参数，后面需要计算出来。这里只是示意
					gi.indirect.specular = 0;
					gi.light.color = _LightColor0.rgb;//unity内置的灯光颜色变量
					gi.light.dir = lightDir;//赋予之前计算的灯光方向。	

					UnityGIInput giInput;
					UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);//初始化归零
					giInput.light = gi.light;//之前这个light已经给过，这里补到这个结构体即可。
					giInput.worldPos = i.worldPos;//世界坐标
					giInput.worldViewDir = worldViewDir;//摄像机方向
					giInput.atten = atten;//在之前的光照衰减里面已经被计算。其中包含阴影的计算了。

					//球谐光照和环境光照输入（已在顶点着色器里的计算，这里只是输入）
	#if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
					giInput.ambient = i.sh;
	#else//假如没有做球谐计算，这里就归零
					giInput.ambient.rgb = 0.0;
	#endif

					//反射探针相关
					giInput.probeHDR[0] = unity_SpecCube0_HDR;
					giInput.probeHDR[1] = unity_SpecCube1_HDR;
	#if defined(UNITY_SPECCUBE_BLENDING) || defined(UNITY_SPECCUBE_BOX_PROJECTION)
					giInput.boxMin[0] = unity_SpecCube0_BoxMin; // .w holds lerp value for blending
	#endif
	#ifdef UNITY_SPECCUBE_BOX_PROJECTION
					giInput.boxMax[0] = unity_SpecCube0_BoxMax;
					giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
					giInput.boxMax[1] = unity_SpecCube1_BoxMax;
					giInput.boxMin[1] = unity_SpecCube1_BoxMin;
					giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
	#endif
					LightingStandard_GI(o, giInput, gi);
					fixed4 c = 0;
					// realtime lighting: call lighting function
					//PBS计算
					c += LightingStandard(o, worldViewDir, gi);

					//叠加雾效
					UNITY_EXTRACT_FOG(i);
					UNITY_APPLY_FOG(_unity_fogCoord, c);
					return c;
				}
				ENDCG
			}
		}
			Fallback Off
}
