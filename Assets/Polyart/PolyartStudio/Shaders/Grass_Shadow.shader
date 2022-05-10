// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "DJC/Grass_Shadow"
{
	Properties
	{
		_ColorTop("Color Top", Color) = (0,0,0,0)
		_Cutoff( "Mask Clip Value", Float ) = 0.59
		_ColorTopVariation("Color Top Variation", Color) = (0,0,0,0)
		_ColorBottom("Color Bottom", Color) = (0,0,0,0)
		_ColorBottomLevel("Color Bottom Level", Float) = 0
		_ColorBottomMaskFade("Color Bottom Mask Fade", Range( -1 , 1)) = 0
		_FoliageTexture("Foliage Texture", 2D) = "white" {}
		_ColorWave("Color Wave", Color) = (0,0,0,0)
		_WaveScale("Wave Scale", Float) = 33
		_VariationMapScale("Variation Map Scale", Float) = 15
		_WaveSpeed("Wave Speed", Color) = (0.1320755,0.1320755,0.1320755,0)
		_PanningWaveTexture("Panning Wave Texture", 2D) = "white" {}
		_VariationMask("Variation Mask", 2D) = "white" {}
		_InteractionAlpha("Interaction Alpha", Float) = 1
		_WindNoiseTexture("Wind Noise Texture", 2D) = "white" {}
		_WindNoiseSmallMultiply("Wind Noise Small Multiply", Range( -10 , 10)) = 0
		_WindNoiseLargeMultiply("Wind Noise Large Multiply", Range( -10 , 10)) = 1
		_PivotLockPower("Pivot Lock Power", Range( 0 , 10)) = 2
		_WindNoiseLarge("Wind Noise Large", Float) = 20
		_WindNoiseSmall("Wind Noise Small", Float) = 20
		_Roughness("Roughness", Range( -1 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float4 _ActorPosition;
		uniform float _InteractionStrength;
		uniform float _InteractionRadius;
		uniform float _InteractionAlpha;
		uniform sampler2D _WindNoiseTexture;
		uniform float _WindNoiseSmall;
		uniform float _WindNoiseSmallMultiply;
		uniform float _WindNoiseLarge;
		uniform float _WindNoiseLargeMultiply;
		uniform float _PivotLockPower;
		uniform sampler2D _FoliageTexture;
		uniform float4 _FoliageTexture_ST;
		uniform float4 _ColorWave;
		uniform float4 _ColorTop;
		uniform float4 _ColorTopVariation;
		uniform sampler2D _VariationMask;
		uniform float _VariationMapScale;
		uniform float4 _ColorBottom;
		uniform float _ColorBottomLevel;
		uniform float _ColorBottomMaskFade;
		uniform sampler2D _PanningWaveTexture;
		uniform float4 _WaveSpeed;
		uniform float _WaveScale;
		uniform float _Roughness;
		uniform float _Cutoff = 0.59;


		struct Gradient
		{
			int type;
			int colorsLength;
			int alphasLength;
			float4 colors[8];
			float2 alphas[8];
		};


		Gradient NewGradient(int type, int colorsLength, int alphasLength, 
		float4 colors0, float4 colors1, float4 colors2, float4 colors3, float4 colors4, float4 colors5, float4 colors6, float4 colors7,
		float2 alphas0, float2 alphas1, float2 alphas2, float2 alphas3, float2 alphas4, float2 alphas5, float2 alphas6, float2 alphas7)
		{
			Gradient g;
			g.type = type;
			g.colorsLength = colorsLength;
			g.alphasLength = alphasLength;
			g.colors[ 0 ] = colors0;
			g.colors[ 1 ] = colors1;
			g.colors[ 2 ] = colors2;
			g.colors[ 3 ] = colors3;
			g.colors[ 4 ] = colors4;
			g.colors[ 5 ] = colors5;
			g.colors[ 6 ] = colors6;
			g.colors[ 7 ] = colors7;
			g.alphas[ 0 ] = alphas0;
			g.alphas[ 1 ] = alphas1;
			g.alphas[ 2 ] = alphas2;
			g.alphas[ 3 ] = alphas3;
			g.alphas[ 4 ] = alphas4;
			g.alphas[ 5 ] = alphas5;
			g.alphas[ 6 ] = alphas6;
			g.alphas[ 7 ] = alphas7;
			return g;
		}


		float4 SampleGradient( Gradient gradient, float time )
		{
			float3 color = gradient.colors[0].rgb;
			UNITY_UNROLL
			for (int c = 1; c < 8; c++)
			{
			float colorPos = saturate((time - gradient.colors[c-1].w) / ( 0.00001 + (gradient.colors[c].w - gradient.colors[c-1].w)) * step(c, (float)gradient.colorsLength-1));
			color = lerp(color, gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), gradient.type));
			}
			#ifndef UNITY_COLORSPACE_GAMMA
			color = half3(GammaToLinearSpaceExact(color.r), GammaToLinearSpaceExact(color.g), GammaToLinearSpaceExact(color.b));
			#endif
			float alpha = gradient.alphas[0].x;
			UNITY_UNROLL
			for (int a = 1; a < 8; a++)
			{
			float alphaPos = saturate((time - gradient.alphas[a-1].y) / ( 0.00001 + (gradient.alphas[a].y - gradient.alphas[a-1].y)) * step(a, (float)gradient.alphasLength-1));
			alpha = lerp(alpha, gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), gradient.type));
			}
			return float4(color, alpha);
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			Gradient gradient49 = NewGradient( 0, 2, 2, float4( 0, 0, 0, 0 ), float4( 1, 1, 1, 1 ), 0, 0, 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 normalizeResult22 = normalize( ( _ActorPosition - float4( ase_worldPos , 0.0 ) ) );
			float temp_output_11_0 = ( _InteractionStrength * 0.1 );
			float3 appendResult16 = (float3(temp_output_11_0 , 0.0 , temp_output_11_0));
			float clampResult21 = clamp( ( distance( _ActorPosition , float4( ase_worldPos , 0.0 ) ) / _InteractionRadius ) , 0.0 , 1.0 );
			float4 vertexToFrag82 = ( SampleGradient( gradient49, v.texcoord.xy.y ) * -( ( ( normalizeResult22 * float4( appendResult16 , 0.0 ) ) * ( 1.0 - clampResult21 ) ) * _InteractionAlpha ) );
			float4 color79 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
			float4 lerpResult73 = lerp( ( tex2Dlod( _WindNoiseTexture, float4( ( ( float2( 0,0.2 ) * _Time.y ) + ( (ase_worldPos).xz / _WindNoiseSmall ) ), 0, 0.0) ) * _WindNoiseSmallMultiply ) , ( tex2Dlod( _WindNoiseTexture, float4( ( ( float2( 0,0.1 ) * _Time.y ) + ( (ase_worldPos).xz / _WindNoiseLarge ) ), 0, 0.0) ) * _WindNoiseLargeMultiply ) , 0.5);
			Gradient gradient53 = NewGradient( 0, 2, 2, float4( 0, 0, 0, 0 ), float4( 1, 1, 1, 1 ), 0, 0, 0, 0, 0, 0, float2( 1, 0 ), float2( 1, 1 ), 0, 0, 0, 0, 0, 0 );
			float4 temp_cast_4 = (_PivotLockPower).xxxx;
			float4 lerpResult86 = lerp( color79 , lerpResult73 , pow( SampleGradient( gradient53, v.texcoord.xy.y ) , temp_cast_4 ));
			v.vertex.xyz += ( vertexToFrag82 + lerpResult86 ).rgb;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_FoliageTexture = i.uv_texcoord * _FoliageTexture_ST.xy + _FoliageTexture_ST.zw;
			float4 tex2DNode71 = tex2D( _FoliageTexture, uv_FoliageTexture );
			float3 ase_worldPos = i.worldPos;
			float4 lerpResult60 = lerp( _ColorTop , _ColorTopVariation , tex2D( _VariationMask, (( ase_worldPos / _VariationMapScale )).xz ));
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 lerpResult78 = lerp( lerpResult60 , _ColorBottom , saturate( ( ( ase_vertex3Pos.y + _ColorBottomLevel ) * ( _ColorBottomMaskFade * 2 ) ) ));
			float2 panner90 = ( _Time.y * (_WaveSpeed).rg + (( ase_worldPos / _WaveScale )).xz);
			float4 lerpResult99 = lerp( ( tex2DNode71 * _ColorWave ) , ( lerpResult78 * tex2DNode71 ) , tex2D( _PanningWaveTexture, panner90 ));
			o.Albedo = lerpResult99.rgb;
			o.Smoothness = _Roughness;
			o.Alpha = 1;
			clip( tex2DNode71.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
638;143;1714;1114;3763.127;2065.9;2.826483;True;False
Node;AmplifyShaderEditor.CommentaryNode;1;-3689.757,-2132.847;Inherit;False;2587.448;615.8922;Allows the foliage to interact with actors that have the interaction script attached.;22;82;77;66;61;54;49;44;39;36;32;31;22;21;16;12;11;9;6;5;4;3;2;Actor Interaction;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;103;-3227.972,625.2068;Inherit;False;2133.215;1279.717;;2;102;105;Wind;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;2;-3639.757,-2082.847;Inherit;False;Global;_ActorPosition;_ActorPosition;11;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-3632.297,-1908.192;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;105;-3182.161,675.4502;Inherit;False;1890.47;670.058;Wind Small;14;73;62;58;48;47;40;37;35;29;23;19;17;13;10;;0.2216981,1,0.9921367,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;102;-3181.836,1363.455;Inherit;False;1598.144;507.6038;Wind Large;11;63;50;45;38;33;28;20;18;15;14;8;;0.9711054,0.495283,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-3309.867,-1922.436;Inherit;False;Global;_InteractionStrength;_InteractionStrength;11;1;[HideInInspector];Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;4;-3311.268,-1840.255;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-3376.268,-1728.255;Inherit;False;Global;_InteractionRadius;_InteractionRadius;11;1;[HideInInspector];Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;10;-3132.161,987.0536;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleDivideOpNode;9;-3076.268,-1745.255;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleNode;11;-3018.622,-1918.594;Inherit;False;0.1;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;-3340.298,-2077.193;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WorldPosInputsNode;8;-3127.493,1674.691;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;7;-3991.51,-1482.241;Inherit;False;2888.247;1209.859;Comment;22;85;81;78;74;71;65;64;60;59;57;56;52;46;43;42;41;34;30;27;26;25;24;Cloud Tint;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-3920.794,-1015.787;Inherit;False;Property;_VariationMapScale;Variation Map Scale;9;0;Create;True;0;0;0;False;0;False;15;30;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;13;-2992.346,877.6636;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;23;-2932.161,982.0536;Inherit;False;True;False;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;20;-2927.493,1671.691;Inherit;False;True;False;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WorldPosInputsNode;24;-3921.588,-1156.182;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleTimeNode;14;-2986.677,1565.301;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-2930.493,1755.691;Inherit;False;Property;_WindNoiseLarge;Wind Noise Large;18;0;Create;True;0;0;0;False;0;False;20;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;16;-2814.419,-1918.964;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;22;-3162.297,-2076.193;Inherit;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ClampOpNode;21;-2923.268,-1745.255;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2935.161,1068.053;Inherit;False;Property;_WindNoiseSmall;Wind Noise Small;19;0;Create;True;0;0;0;False;0;False;20;40;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;18;-2984.677,1436.3;Inherit;False;Constant;_Vector1;Vector 1;14;0;Create;True;0;0;0;False;0;False;0,0.1;0,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;19;-2989.346,748.6633;Inherit;False;Constant;_Vector0;Vector 0;15;0;Create;True;0;0;0;False;0;False;0,0.2;0,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;55;-2626.929,-227.8195;Inherit;False;1528.561;709.4982;;10;99;95;90;84;83;80;76;72;70;67;Color;0.6900728,1,0.5801887,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-2750.346,754.6633;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;35;-2637.161,989.0536;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-3668.276,-809.4718;Inherit;False;Property;_ColorBottomLevel;Color Bottom Level;4;0;Create;True;0;0;0;False;0;False;0;-0.66;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-2753.677,1441.3;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;28;-2652.493,1672.691;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PosVertexDataNode;27;-3667.192,-952.0598;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;34;-3692.659,-724.8413;Inherit;False;Property;_ColorBottomMaskFade;Color Bottom Mask Fade;5;0;Create;True;0;0;0;False;0;False;0;-1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;30;-3603.482,-1156.888;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;31;-2758.269,-1745.255;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-2591.296,-2075.519;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;67;-2559.325,168.2155;Inherit;False;Property;_WaveScale;Wave Scale;8;0;Create;True;0;0;0;False;0;False;33;30;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-2456.129,-1665.155;Inherit;False;Property;_InteractionAlpha;Interaction Alpha;13;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-2427.346,754.6633;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WorldPosInputsNode;70;-2571.326,22.21537;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-2391.269,-1770.255;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-3393.082,-855.5629;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;40;-2640.155,1152.508;Inherit;True;Property;_WindNoiseTexture;Wind Noise Texture;14;0;Create;True;0;0;0;False;0;False;f3a1f84b44733e44e9e06b47e364540b;f3a1f84b44733e44e9e06b47e364540b;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.ComponentMaskNode;43;-3462.98,-1161.188;Inherit;False;True;False;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;38;-2422.677,1442.3;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleNode;42;-3425.195,-722.9641;Inherit;False;2;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;104;-1081.131,823.4559;Inherit;False;912.9565;352;;4;75;69;53;51;Vertical Gradient;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;56;-2944.457,-1375.815;Inherit;False;Property;_ColorTop;Color Top;0;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.4353558,0.5849056,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;46;-2958.486,-1206.923;Inherit;False;Property;_ColorTopVariation;Color Top Variation;2;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.4353558,0.5849056,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-3243.917,-859.6506;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-2127.017,916.7533;Inherit;False;Property;_WindNoiseSmallMultiply;Wind Noise Small Multiply;15;0;Create;True;0;0;0;False;0;False;0;-0.7;-10;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;48;-2149.175,725.4502;Inherit;True;Property;_TextureSample1;Texture Sample 1;16;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;72;-2576.929,274.6788;Inherit;False;Property;_WaveSpeed;Wave Speed;10;0;Create;True;0;0;0;False;0;False;0.1320755,0.1320755,0.1320755,0;0.3207546,0.0257209,0.0257209,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;76;-2317.325,21.21537;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GradientNode;53;-998.1316,874.162;Inherit;False;0;2;2;0,0,0,0;1,1,1,1;1,0;1,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-2186.72,-1769.955;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;57;-3250.628,-1182.814;Inherit;True;Property;_VariationMask;Variation Mask;12;0;Create;True;0;0;0;False;0;False;-1;None;e3adc16eb3921de48a178ce82c92cbd1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;44;-2370.308,-1973.021;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GradientNode;49;-2337.309,-2048.021;Inherit;False;0;2;2;0,0,0,0;1,1,1,1;1,0;1,1;0;1;OBJECT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;51;-1031.131,949.162;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;45;-2144.506,1413.086;Inherit;True;Property;_TextureSample2;Texture Sample 2;16;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;50;-2115.349,1608.391;Inherit;False;Property;_WindNoiseLargeMultiply;Wind Noise Large Multiply;16;0;Create;True;0;0;0;False;0;False;1;0.2;-10;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;80;-2322.93,273.6793;Inherit;False;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;84;-2285.027,377.8788;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;59;-2942.164,-1036.12;Inherit;False;Property;_ColorBottom;Color Bottom;3;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.5454459,0.8018868,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;65;-2953.67,-714.7073;Inherit;True;Property;_FoliageTexture;Foliage Texture;6;0;Create;True;0;0;0;False;0;False;None;aa3f5c93e1b271345a875c5ef4ebf6f9;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.NegateNode;66;-1961.309,-1772.021;Inherit;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;83;-2172.324,16.21537;Inherit;False;True;False;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GradientSampleNode;69;-778.1316,874.162;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;64;-2938.237,-858.885;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-1745.577,863.9596;Inherit;False;Constant;_Float1;Float 1;19;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GradientSampleNode;61;-2117.309,-2048.021;Inherit;True;2;0;OBJECT;;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;68;-750.1737,1060.455;Inherit;False;Property;_PivotLockPower;Pivot Lock Power;17;0;Create;True;0;0;0;False;0;False;2;0.95;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;60;-2688.833,-1223.043;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-1753.018,729.7533;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-1748.348,1417.39;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;71;-2726.193,-711.9462;Inherit;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;73;-1475.692,732.1826;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;75;-429.1738,873.4559;Inherit;True;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;90;-1895.128,19.87869;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;79;-466.1788,603.7953;Inherit;False;Constant;_Color2;Color2;7;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;74;-2640.818,-493.4831;Inherit;False;Property;_ColorWave;Color Wave;7;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.07332758,0.2547169,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-1764.309,-1796.021;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;78;-2442.166,-901.1198;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;-1812.666,-897.95;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-2323.815,-510.4832;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;86;-149.4877,706.7823;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;95;-1702.371,-10.61057;Inherit;True;Property;_PanningWaveTexture;Panning Wave Texture;11;0;Create;True;0;0;0;False;0;False;-1;None;e3adc16eb3921de48a178ce82c92cbd1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexToFragmentNode;82;-1606.311,-1796.021;Inherit;False;False;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;107;-732.3794,-787.9401;Inherit;False;Property;_Roughness;Roughness;20;0;Create;True;0;0;0;False;0;False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;106;-646.7381,-212.9763;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;99;-1287.57,-122.1105;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-269.362,-895.8494;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;DJC/Grass_Shadow;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.59;True;False;0;False;TransparentCutout;;AlphaTest;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;2;0
WireConnection;4;1;3;0
WireConnection;9;0;4;0
WireConnection;9;1;5;0
WireConnection;11;0;6;0
WireConnection;12;0;2;0
WireConnection;12;1;3;0
WireConnection;23;0;10;0
WireConnection;20;0;8;0
WireConnection;16;0;11;0
WireConnection;16;2;11;0
WireConnection;22;0;12;0
WireConnection;21;0;9;0
WireConnection;29;0;19;0
WireConnection;29;1;13;0
WireConnection;35;0;23;0
WireConnection;35;1;17;0
WireConnection;33;0;18;0
WireConnection;33;1;14;0
WireConnection;28;0;20;0
WireConnection;28;1;15;0
WireConnection;30;0;24;0
WireConnection;30;1;25;0
WireConnection;31;0;21;0
WireConnection;32;0;22;0
WireConnection;32;1;16;0
WireConnection;37;0;29;0
WireConnection;37;1;35;0
WireConnection;39;0;32;0
WireConnection;39;1;31;0
WireConnection;41;0;27;2
WireConnection;41;1;26;0
WireConnection;43;0;30;0
WireConnection;38;0;33;0
WireConnection;38;1;28;0
WireConnection;42;0;34;0
WireConnection;52;0;41;0
WireConnection;52;1;42;0
WireConnection;48;0;40;0
WireConnection;48;1;37;0
WireConnection;76;0;70;0
WireConnection;76;1;67;0
WireConnection;54;0;39;0
WireConnection;54;1;36;0
WireConnection;57;1;43;0
WireConnection;45;0;40;0
WireConnection;45;1;38;0
WireConnection;80;0;72;0
WireConnection;66;0;54;0
WireConnection;83;0;76;0
WireConnection;69;0;53;0
WireConnection;69;1;51;2
WireConnection;64;0;52;0
WireConnection;61;0;49;0
WireConnection;61;1;44;2
WireConnection;60;0;56;0
WireConnection;60;1;46;0
WireConnection;60;2;57;0
WireConnection;58;0;48;0
WireConnection;58;1;47;0
WireConnection;63;0;45;0
WireConnection;63;1;50;0
WireConnection;71;0;65;0
WireConnection;73;0;58;0
WireConnection;73;1;63;0
WireConnection;73;2;62;0
WireConnection;75;0;69;0
WireConnection;75;1;68;0
WireConnection;90;0;83;0
WireConnection;90;2;80;0
WireConnection;90;1;84;0
WireConnection;77;0;61;0
WireConnection;77;1;66;0
WireConnection;78;0;60;0
WireConnection;78;1;59;0
WireConnection;78;2;64;0
WireConnection;81;0;78;0
WireConnection;81;1;71;0
WireConnection;85;0;71;0
WireConnection;85;1;74;0
WireConnection;86;0;79;0
WireConnection;86;1;73;0
WireConnection;86;2;75;0
WireConnection;95;1;90;0
WireConnection;82;0;77;0
WireConnection;106;0;82;0
WireConnection;106;1;86;0
WireConnection;99;0;85;0
WireConnection;99;1;81;0
WireConnection;99;2;95;0
WireConnection;0;0;99;0
WireConnection;0;4;107;0
WireConnection;0;10;71;4
WireConnection;0;11;106;0
ASEEND*/
//CHKSM=A13E4AB3D95C24FCB78A2CC19FC7973984D9AEC7