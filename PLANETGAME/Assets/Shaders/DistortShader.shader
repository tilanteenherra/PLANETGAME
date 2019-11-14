Shader "Metropolia/DistortMask"
{
	Properties
	{
		_NoiseTex("Noise Tex", 2D) = "black" {}
		_MaskTex ("Mask Tex", 2D) = "black" {}
		_NoiseAmount("Noise amount", float) = 0
	}

	SubShader
	{
		Tags{ "Queue" = "Transparent" }

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 spos : TEXCOORD1;
				float2 maskuv : TEXCOORD2;
			};

			sampler2D _CameraOpaqueTexture;
			sampler2D _NoiseTex;
			sampler2D _MaskTex;
			float4 _NoiseTex_ST;
			float _NoiseAmount;

			v2f vert(appdata i)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(i.pos);
				o.maskuv = i.uv;
				o.uv = TRANSFORM_TEX(i.uv, _NoiseTex);
				o.spos = ComputeScreenPos(o.pos);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float3 n = (tex2D(_NoiseTex, i.uv).rgb - 0.5) * 2.0;
				float m = pow(1 - tex2D(_MaskTex, i.maskuv).a, 2);
				float2 sp = i.spos.xy / i.spos.w;
				fixed4 c = tex2D(_CameraOpaqueTexture, sp + n * _NoiseAmount * m);
				return c;
			}

			ENDCG
		}
	}
}