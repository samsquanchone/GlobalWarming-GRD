Shader "Custom/URP/SimpleColorShader" {
    Properties {
        _Color("Color", Color) = (1, 1, 1, 1)
    }

    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert
        #pragma target 3.0
        #pragma shader_feature _ALPHATEST_ON
        #pragma shader_feature _ALPHABLEND_ON
        #pragma shader_feature _ALPHAPREMULTIPLY_ON

        sampler2D _MainTex;
        float4 _Color;

        struct Input {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            o.Albedo = _Color.rgb;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
