// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/screenChange"
{
	Properties
	{
		_Color("Main Color", Color) = (1, 1, 1, 1)
		_Float1("Change Color", Range(0, 1)) = 1.0
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
		SubShader
		{
		Pass
		{
		CGPROGRAM
#pragma vertex vert_img         //vert_img��UnityCG.cginc����Ԥ����ĺ����壬������ͨ�Ķ��㴦����  
#pragma fragment frag             
#include "UnityCG.cginc"  
		fixed4 _Color;
		sampler2D _MainTex;             //����������һ��Ҫ��CGPROGRAM�ж���һ�Σ���Ҫ��Properties���оͲ�д��  
		float1 _Float1;
		float4 frag(v2f_img i) : COLOR
		{
			float4 col = tex2D(_MainTex, i.uv) * _Float1;    //�򵥵�����float4��ˣ��պó˰�ɫ���䣬�˴��ھͺ�͸����һ���Ǵ����Թ�ϵ������Ч��û����  
			return col;
		}
		ENDCG
		}
		}
		Fallback off
}
