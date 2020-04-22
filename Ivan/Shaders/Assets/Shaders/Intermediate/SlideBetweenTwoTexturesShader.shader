﻿Shader "Shader Demo/Slide Between Two Textures Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_SecondaryTex ("Secondary Texture", 2D) = "black" {}
		_Slider ("Slider", Range(0,1)) = 0
    }
    SubShader
    {
		Tags
		{
			"Queue" = "Transparent"
		}

        Pass
        {
			Blend SrcAlpha OneMinusSrcAlpha

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			sampler2D _SecondaryTex;
			float _Slider;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainColor = tex2D(_MainTex, i.uv);
				fixed4 secondColor = tex2D(_SecondaryTex, i.uv);
				mainColor = (1 - _Slider) * mainColor + _Slider * secondColor;
				//mainColor = lerp(mainColor, secondColor, _Slider);
                return mainColor;
            }
            ENDCG
        }
    }
}
