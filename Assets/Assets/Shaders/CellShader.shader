Shader "Custom/CellShader" 
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_IsBomb ("Is a bomb cell", Range(0,1)) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
			float _IsBomb;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				float speed = 2;

				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				c.rgb *= c.a;
				if(_IsBomb == 1)
				{
					//c.rgb *= .8 + (abs(sin(_Time.z)/2));
					c.r = pow(c.r, 2 * (abs(sin(_Time.z))));
					c.g = pow(c.g, 2 * (abs(sin(_Time.z))));
					c.b = pow(c.b, 2 * (abs(sin(_Time.z))));
				}

				if(IN.color.r == 1 && IN.color.g == 1 && IN.color.b == 1)
				{
					float sint = (_SinTime.z*speed);
					float cost = (_CosTime.z*speed);

					c.r = c.r * ( max( cos(IN.texcoord.y + .5 + sint) , sin(IN.texcoord.x + .5 + cost)));
					c.g = c.g * ( max( sin(IN.texcoord.x + .5 + cost) , sin(IN.texcoord.y + .5 + sint)));
					c.b = c.b * ( max( cos(IN.texcoord.y + .5 + cost) , cos(IN.texcoord.x + .5 + sint)));
				}

				return c;
			}
		ENDCG
		}
	}
}
