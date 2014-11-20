Shader "Unlit/Transparent Colored (Packed) (SoftClip)"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
	}

	SubShader
	{
		LOD 200

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Offset -1, -1
			Fog { Mode Off }
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			half4 _MainTex_ST;
			float2 _ClipSharpness = float2(20.0, 20.0);

			struct appdata_t
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 worldPos : TEXCOORD1;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = v.texcoord;
				o.worldPos = TRANSFORM_TEX(v.vertex.xy, _MainTex);
				return o;
			}

			half4 frag (v2f IN) : COLOR
			{
				half4 mask = tex2D(_MainTex, IN.texcoord);
				half4 mixed = saturate(ceil(IN.color - 0.5));
				half4 col = saturate((mixed * 0.51 - IN.color) / -0.49);
				float2 factor = (float2(1.0, 1.0) - abs(IN.worldPos)) * _ClipSharpness;
				
				mask *= mixed;
				col.a *= clamp( min(factor.x, factor.y), 0.0, 1.0);
				col.a *= mask.r + mask.g + mask.b + mask.a;
				return col;
			}
			ENDCG
		}
	}
	Fallback Off
}
