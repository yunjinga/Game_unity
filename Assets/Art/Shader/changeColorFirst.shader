Shader "Custom/changeColorFirst"
{
    Properties{
    _Color("Color", Color) = (0,1,0,1)  //��ʼΪ��ɫ
    _Color1("Color1", Color) = (1,1,0,0) //��ɫ
    _Color2("Color2", Color) = (1,0,0,0) //��ɫ
    _MainTex("Albedo (RGB)", 2D) = "white" {}
    _AlphaMap("Alpha Map", 2D) = "white" {}
    _AlphaScale("Alpha Scale", Range(0,1)) = 1
    _Glossiness("Smoothness", Range(0,1)) = 0.5
    _Metallic("Metallic", Range(0,1)) = 0.0
    _Clip("Clip", float) = 0.0 //3f��ȫ��ɫ��-->0f��ȫ����ɫ��
    _Clip2("Clip2", float) = 0.0 //3f��ȫ��ɫ��-->0f��ȫ����ɫ��

    //father thunder
    _Hardness("Hardness", Range(0,1)) = 0.5
    _Offset("Offset", Range(0,1)) = 0.6
    _Density("Density", float) = 5.0

        //[KeywordEnum(None, Left, Up, Forward)]_Mode("Mode", Float) = 0//���ң��ϵ��£�ǰ����
    }
        SubShader{
        Tags{ "RenderType" = "Transparent" }
        LOD 200
        CGPROGRAM
        // ��������ı�׼����ģ�ͣ����������й������͵���Ӱ
        #pragma surface surf Standard fullforwardshadows alpha vertex:vert

        // ʹ����ɫ��ģ��3.0Ŀ�꣬�Ի�ø��õ�����
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _AlphaMap;
        float4 _AlphaMap_ST;
        //float4 _MainTex_ST;
       struct Input {
        float2 uv_MainTex;
        float4 localPos;
       };

       half _AlphaScale;
       half _Glossiness;
       half _Metallic;
       float4 _Color;
       float _Clip;
       float _Clip2;

       float _Hardness;
       float _Offset;
       float _Density;

       //float _Mode;
       float4 _Color1;
       float4 _Color2;
       void vert(inout appdata_full i, out Input o)
       {
        UNITY_INITIALIZE_OUTPUT(Input, o);
        o.localPos = i.vertex;
        //o.uv_MainTex = i.texcoord.xy * _AlphaMap_ST.xy + _AlphaMap_ST.zw;
       }

       void surf(Input i, inout SurfaceOutputStandard o) {
           //albedo��һ����ɫ����
           fixed4 c = tex2D(_MainTex, i.uv_MainTex) * _Color;
           fixed4 alphaMap = tex2D(_AlphaMap, i.uv_MainTex);
           if (_Clip == 3.0f && _Clip2 == 0.0f)
           {
               o.Albedo = _Color1;
           }
           else if (_Clip2 == 3.0f && _Clip == 3.0f)
           {
               o.Albedo = _Color2;
           }
           else
           {
               o.Albedo = c.rgb;
           }

           // �����к�ƽ���Ȼ�������
           o.Metallic = _Metallic;
           o.Smoothness = _Glossiness;
           //o.Alpha = _AlphaScale*alphaMap.a;

           float o_dist = distance(i.localPos, float3(.0, .0, .0));
           float t1 = _Offset - _Hardness;
           float t2 = _Offset + _Hardness;
           o.Alpha = _AlphaScale * smoothstep(t1, t2, abs(frac(o_dist * _Density) - 0.5));
          }
          ENDCG
    }
        FallBack "Diffuse"
}
