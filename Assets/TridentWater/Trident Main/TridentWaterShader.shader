Shader "Custom/TridentWater" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Gradient ("Gradient", 2D) = "white" {}
        _WorleyNoiseA ("Worley Noise A", 2D) = "white" {}
        _WorleyNoiseB ("Worley Noise B", 2D) = "white" {}
        _PerlinNoise ("Perlin Noise", 2D) = "white" {}
        
        
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    
    SubShader {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "DisableBatching"="True" }
        Blend One OneMinusSrcAlpha
        ZWrite Off
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert alpha:fade addshadow

        #pragma target 5.0

        sampler2D _CameraDepthTexture;

        sampler2D _Gradient;
        sampler2D _WorleyNoiseA;
        float4 _WorleyNoiseA_ST;
        sampler2D _WorleyNoiseB;
        sampler2D _PerlinNoise;

        float _WaveMasterIntensity;
        float4 _WorleySpeeds;
        float4 _WorleyScales;
        float4 _WorleyIntensities;
        float _PerlinNoiseScale;
        float _PerlinNoiseIntensity;
        float _WaveTime;
        

        struct Input {
            float2 uv_WorleyNoiseB;
            float4 screenPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;


        float sampleChannel(sampler2D tex, int channel, float2 uv) {
            return tex2Dlod(tex, float4(uv, 0, 0))[channel];
        }

        float sampleWaveHeight(sampler2D tex, float2 uv) {
            float sampleTotal = 0;
            float intensityTotal = 0;
            for (int i = 0; i < 4; i++) {
                float speedOffset = _WorleySpeeds[i] * _WaveTime;// * float2(1, 1);
                sampleTotal += sampleChannel(tex, i, (uv + speedOffset) *_WorleyScales[i]) * _WorleyIntensities[i];
                intensityTotal += _WorleyIntensities[i];
            }
            return sampleTotal / intensityTotal;
        }

        fixed4 sampleGradient(float intensity) {
            return tex2D(_Gradient, float2(intensity, 0));
        }

        
        

        void vert (inout appdata_full v) {
            float2 uv = TRANSFORM_TEX(v.texcoord, _WorleyNoiseA);
            float waveHeight = sampleWaveHeight(_WorleyNoiseA, uv) * _WaveMasterIntensity;
            v.vertex += float4(0, waveHeight, 0, 0);
        }

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // float depth = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos));
            // depth = LinearEyeDepth(depth);

            float waveHeight = sampleWaveHeight(_WorleyNoiseB, IN.uv_WorleyNoiseB);
            fixed4 c = sampleGradient(waveHeight);

            o.Albedo = c.rgb;
            
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic * waveHeight;
            o.Smoothness = _Glossiness * waveHeight;
            o.Normal = tex2Dlod(_PerlinNoise, float4(IN.uv_WorleyNoiseB, 0, 0) * _PerlinNoiseScale) * _PerlinNoiseIntensity;
            o.Alpha = c.a;

            // o.Albedo = tex2Dlod(_PerlinNoise, float4(IN.uv_WorleyNoiseB, 0, 0) * _PerlinNoiseScale);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
