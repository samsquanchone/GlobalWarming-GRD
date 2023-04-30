Shader "Custom/Outline" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0.001, 0.1)) = 0.01
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}

        Pass {
            Cull Off
            ZWrite Off
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float _OutlineWidth;
            float4 _OutlineColor;
            sampler2D _MainTex;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 outline = float4(0,0,0,0);
                
                // Sample texture four times to get adjacent pixels
                outline += tex2D(_MainTex, i.uv + float2(_OutlineWidth, 0));
                outline += tex2D(_MainTex, i.uv + float2(0, _OutlineWidth));
                outline += tex2D(_MainTex, i.uv - float2(_OutlineWidth, 0));
                outline += tex2D(_MainTex, i.uv - float2(0, _OutlineWidth));

                // Average the four samples to get the outline color
                outline /= 4.0;
                
                // Calculate alpha based on distance from edge
                float alpha = 1.0 - smoothstep(_OutlineWidth - 0.001, _OutlineWidth + 0.001, length(outline.rgb));
                alpha = saturate(alpha);

                // Blend outline color with original color
                fixed4 finalColor = lerp(col, _OutlineColor, alpha);

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}