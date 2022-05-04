Shader "Custom/UnderwaterEffect" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Amplitude ("Amplitude", Float) = 1
        _Frequency ("Frequency", Float) = 1
        _ZoomAmount ("Zoom Amount", Float) = 0
    }
    
    SubShader {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _Color;
            float _Amplitude;
            float _Frequency;
            float _ZoomAmount;

            fixed4 frag (v2f i) : SV_Target {
                // calculate modified position from wave function
                const float2 pos = (i.uv * _ZoomAmount) + ((1 - _ZoomAmount) / 2.0f);
                const float offset = sin(_Frequency * (_Time[0] + pos[0])) * _Amplitude;

                // get color
                fixed4 col = tex2D(_MainTex, pos + offset) * _Color;
                return col;
            }
            ENDCG
        }
    }
}
