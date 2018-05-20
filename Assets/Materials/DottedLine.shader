Shader "Unlit/Test"
{
	Properties
	{

		_TintColor ("Tint Color", Color) = (0.0, 0.0, 0.0, 0.0)
		_MainTex ("Texture", 2D) = "white" {}
		_Offset ("UV Offset", Vector) = (0.0, 0.0, 0.0, 0.0)

	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _TintColor;
			float4 _MainTex_ST;
			float2 _Offset;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv + _Offset, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed alpha = tex2D(_MainTex, i.uv).a * _TintColor.a;
				return fixed4(_TintColor.rgb, alpha);
			}
			ENDCG
		}
	}
}
