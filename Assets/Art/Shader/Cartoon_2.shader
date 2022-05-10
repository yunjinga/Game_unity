// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DJC/Cartoon"
{
	Properties
	{
		_shadowsoftness("shadowsoftness", Range( 0 , 1)) = 0
		_shadowoffest("shadowoffest", Range( -1 , 1)) = 0
		[HDR]_RimColor("Rim Color", Color) = (0,1,0.8758622,0)
		_RimPower("Rim Power", Range( 0 , 10)) = 0.5
		_shadowcolor("shadowcolor", Color) = (0.2267711,0.3006141,0.5283019,0)
		_RimOffset("Rim Offset", Range( 0 , 10)) = 0.24
		_maincolor("maincolor", Color) = (0.4156863,0.672353,0.7176471,1)
		_Basecolor("Basecolor", 2D) = "white" {}
		_Color0("Color 0", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ }
		Cull Front
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Outline nofog  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 
		
		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float cameraDepthFade30 = (( -UnityObjectToViewPos( v.vertex.xyz ).z -_ProjectionParams.y - 0.0 ) / 1.0);
			float lerpResult35 = lerp( 0.05 , 0.006 , saturate( ( cameraDepthFade30 / 2.0 ) ));
			float outlineVar = lerpResult35;
			v.vertex.xyz += ( v.normal * outlineVar );
		}
		inline half4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return half4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			o.Emission = _Color0.rgb;
		}
		ENDCG
		

		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Off
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
			float3 worldPos;
			float3 viewDir;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform float4 _shadowcolor;
		uniform sampler2D _Basecolor;
		uniform float4 _Basecolor_ST;
		uniform float4 _maincolor;
		uniform float _RimOffset;
		uniform float _RimPower;
		uniform float4 _RimColor;
		uniform float _shadowoffest;
		uniform float _shadowsoftness;
		uniform float4 _Color0;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += 0;
			v.vertex.w = 1;
		}

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			#ifdef UNITY_PASS_FORWARDBASE
			float ase_lightAtten = data.atten;
			if( _LightColor0.a == 0)
			ase_lightAtten = 0;
			#else
			float3 ase_lightAttenRGB = gi.light.color / ( ( _LightColor0.rgb ) + 0.000001 );
			float ase_lightAtten = max( max( ase_lightAttenRGB.r, ase_lightAttenRGB.g ), ase_lightAttenRGB.b );
			#endif
			#if defined(HANDLE_SHADOWS_BLENDING_IN_GI)
			half bakedAtten = UnitySampleBakedOcclusion(data.lightmapUV.xy, data.worldPos);
			float zDist = dot(_WorldSpaceCameraPos - data.worldPos, UNITY_MATRIX_V[2].xyz);
			float fadeDist = UnityComputeShadowFadeDistance(data.worldPos, zDist);
			ase_lightAtten = UnityMixRealtimeAndBakedShadows(data.atten, bakedAtten, UnityComputeShadowFade(fadeDist));
			#endif
			float2 uv_Basecolor = i.uv_texcoord * _Basecolor_ST.xy + _Basecolor_ST.zw;
			#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float4 temp_output_24_0 = ( tex2D( _Basecolor, uv_Basecolor ) * _maincolor * float4( ase_lightColor.rgb , 0.0 ) );
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult75 = dot( ase_worldNormal , ase_worldlightDir );
			float dotResult57 = dot( ase_worldNormal , i.viewDir );
			float4 temp_output_70_0 = ( saturate( ( ( ase_lightAtten * dotResult75 ) * pow( ( 1.0 - saturate( ( dotResult57 + _RimOffset ) ) ) , _RimPower ) ) ) * ( _RimColor * ase_lightColor ) );
			float dotResult3 = dot( ase_worldlightDir , ase_worldNormal );
			float4 lerpResult7 = lerp( ( _shadowcolor * temp_output_24_0 ) , ( temp_output_24_0 + temp_output_70_0 ) , saturate( ( ( _shadowoffest + dotResult3 ) / _shadowsoftness ) ));
			float4 temp_output_71_0 = ( lerpResult7 + temp_output_70_0 );
			c.rgb = temp_output_71_0.rgb;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			float2 uv_Basecolor = i.uv_texcoord * _Basecolor_ST.xy + _Basecolor_ST.zw;
			#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float4 temp_output_24_0 = ( tex2D( _Basecolor, uv_Basecolor ) * _maincolor * float4( ase_lightColor.rgb , 0.0 ) );
			float3 ase_worldNormal = i.worldNormal;
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult75 = dot( ase_worldNormal , ase_worldlightDir );
			float dotResult57 = dot( ase_worldNormal , i.viewDir );
			float4 temp_output_70_0 = ( saturate( ( ( 1 * dotResult75 ) * pow( ( 1.0 - saturate( ( dotResult57 + _RimOffset ) ) ) , _RimPower ) ) ) * ( _RimColor * ase_lightColor ) );
			float dotResult3 = dot( ase_worldlightDir , ase_worldNormal );
			float4 lerpResult7 = lerp( ( _shadowcolor * temp_output_24_0 ) , ( temp_output_24_0 + temp_output_70_0 ) , saturate( ( ( _shadowoffest + dotResult3 ) / _shadowsoftness ) ));
			float4 temp_output_71_0 = ( lerpResult7 + temp_output_70_0 );
			o.Albedo = temp_output_71_0.rgb;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting keepalpha fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = worldViewDir;
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
3041;191;1714;1114;3878.777;2388.135;3.094956;True;False
Node;AmplifyShaderEditor.CommentaryNode;53;-2846.713,-717.4487;Inherit;False;507.201;385.7996;Comment;3;57;55;54;N . V;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldNormalVector;54;-2798.713,-669.4487;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;55;-2750.713,-509.4485;Float;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;56;-2245.678,-734.6992;Inherit;False;1617.938;553.8222;;14;70;69;68;67;66;65;64;63;62;61;60;59;58;76;Rim Light;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-2165.678,-462.6991;Float;False;Property;_RimOffset;Rim Offset;6;0;Create;True;0;0;0;False;0;False;0.24;0.7;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;57;-2494.713,-589.4484;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;72;-2864.474,-1153.49;Inherit;False;540.401;320.6003;Comment;3;75;74;73;N . L;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;59;-1957.679,-574.6989;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;60;-1797.679,-574.6989;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;74;-2752.474,-1105.49;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;73;-2800.474,-945.4897;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DotProductOpNode;75;-2464.474,-1041.49;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LightAttenuation;76;-1848.999,-695.8714;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;42;-1316.755,-75.97667;Inherit;False;475.6997;387.103;标准光照预置;3;1;3;2;;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;61;-1621.679,-574.6989;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-1733.679,-446.699;Float;False;Property;_RimPower;Rim Power;4;0;Create;True;0;0;0;False;0;False;0.5;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;63;-1429.679,-574.6989;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;1;-1266.755,-25.97665;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;2;-1266.675,128.1263;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;-1445.679,-686.6992;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-425.1843,-148.6441;Inherit;False;Property;_shadowoffest;shadowoffest;1;0;Create;True;0;0;0;False;0;False;0;-0.28;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;3;-993.0549,46.32332;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;40;-368.3661,1267.636;Inherit;False;691.0009;301.9999;根据摄影机控制线条;4;31;30;32;34;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;41;-624.5691,-1806.023;Inherit;False;373.5698;988.2584;基础颜色参数调整;4;16;15;20;23;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-1189.679,-606.699;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;66;-1269.679,-462.6991;Float;False;Property;_RimColor;Rim Color;2;1;[HDR];Create;True;0;0;0;False;0;False;0,1,0.8758622,0;0,1.0348,2.670157,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LightColorNode;67;-1157.679,-286.6994;Inherit;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.SaturateNode;68;-997.6791,-606.699;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-601.0752,382.2386;Float;False;Property;_shadowsoftness;shadowsoftness;0;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-112.0199,-120.7587;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;20;-574.569,-1556.107;Inherit;True;Property;_Basecolor;Basecolor;8;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CameraDepthFade;30;-306.3658,1317.637;Inherit;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-318.3658,1453.637;Inherit;False;Constant;_Cameradepth;Cameradepth;9;0;Create;True;0;0;0;False;0;False;2;0;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.LightColorNode;23;-436.2818,-1173.564;Inherit;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-965.6791,-478.6991;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;15;-496.4032,-1357.625;Inherit;False;Property;_maincolor;maincolor;7;0;Create;True;0;0;0;False;0;False;0.4156863,0.672353,0.7176471,1;0.3301886,0.2045766,0.119927,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;16;-513.7034,-1756.023;Inherit;False;Property;_shadowcolor;shadowcolor;5;0;Create;True;0;0;0;False;0;False;0.2267711,0.3006141,0.5283019,0;0.6226414,0.4480709,0.3964934,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;32;3.634747,1393.636;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-805.6792,-606.699;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;328.0208,-787.4411;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;4;59.10577,-120.9922;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;39;-28.47079,1008.315;Inherit;False;351;245.9998;线条调整参数;2;37;36;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;36;22.52926,1058.316;Inherit;False;Constant;_farlinewidth;farlinewidth;8;0;Create;True;0;0;0;False;0;False;0.05;0;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;654.5866,-730.2722;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;80;552.5306,-515.3593;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;34;157.6349,1393.636;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;21.52926,1138.316;Inherit;False;Constant;_nearlinewidth;nearlinewidth;8;0;Create;True;0;0;0;False;0;False;0.006;0.4117647;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;6;214.7309,-116.1201;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;7;814.1468,-482.9772;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;47;305.6265,171.7285;Inherit;False;Property;_Color0;Color 0;10;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;35;397.9306,1090.619;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;71;1154.614,-315.4298;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-539.7064,657.7838;Inherit;False;Property;_Float0;Float 0;3;0;Create;True;0;0;0;False;0;False;0;0.07;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;51;100.2091,690.3079;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;29;-692.2606,775.3765;Inherit;False;Property;_outlinecolor;outlinecolor;9;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;50;-55.41599,685.4359;Inherit;False;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;49;-226.5423,685.6694;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;45;-476.3398,785.3183;Inherit;True;Property;_TextureSample0;Texture Sample 0;11;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OutlineNode;27;1110.381,152.9283;Inherit;False;0;True;None;0;0;Front;True;True;True;True;0;False;-1;3;0;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;52;269.227,652.2802;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1556.855,-250.6647;Float;False;True;-1;2;ASEMaterialInspector;0;0;CustomLighting;DJC/Cartoon;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;1.62;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;57;0;54;0
WireConnection;57;1;55;0
WireConnection;59;0;57;0
WireConnection;59;1;58;0
WireConnection;60;0;59;0
WireConnection;75;0;74;0
WireConnection;75;1;73;0
WireConnection;61;0;60;0
WireConnection;63;0;61;0
WireConnection;63;1;62;0
WireConnection;64;0;76;0
WireConnection;64;1;75;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;65;0;64;0
WireConnection;65;1;63;0
WireConnection;68;0;65;0
WireConnection;9;0;10;0
WireConnection;9;1;3;0
WireConnection;69;0;66;0
WireConnection;69;1;67;0
WireConnection;32;0;30;0
WireConnection;32;1;31;0
WireConnection;70;0;68;0
WireConnection;70;1;69;0
WireConnection;24;0;20;0
WireConnection;24;1;15;0
WireConnection;24;2;23;1
WireConnection;4;0;9;0
WireConnection;4;1;5;0
WireConnection;17;0;16;0
WireConnection;17;1;24;0
WireConnection;80;0;24;0
WireConnection;80;1;70;0
WireConnection;34;0;32;0
WireConnection;6;0;4;0
WireConnection;7;0;17;0
WireConnection;7;1;80;0
WireConnection;7;2;6;0
WireConnection;35;0;36;0
WireConnection;35;1;37;0
WireConnection;35;2;34;0
WireConnection;71;0;7;0
WireConnection;71;1;70;0
WireConnection;51;0;50;0
WireConnection;50;0;49;0
WireConnection;50;1;45;0
WireConnection;49;0;48;0
WireConnection;27;0;47;0
WireConnection;27;1;35;0
WireConnection;52;2;51;0
WireConnection;0;0;71;0
WireConnection;0;13;71;0
WireConnection;0;11;27;0
ASEEND*/
//CHKSM=6D5367A975F1BBA7957080B3BBFE9291A530D6FA