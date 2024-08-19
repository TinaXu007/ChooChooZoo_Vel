Shader"Custom/PostProcess"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VRadius("Vignette Radius", Range(0.0, 1.0)) = 1.0
        _VSoft("Vignette Softness",Range(0.0, 1.0))= 0.5
        _Color("Color",Color) = (1,1,1,1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _VRadius;
            float _VSoft;
            float4 _Color;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float distFromCenter = distance(i.uv.xy, float2(0.5, 0.5));
                float vingette = smoothstep(_VRadius, _VRadius - _VSoft, distFromCenter);
                col = saturate(col * vingette);
                col = col * _Color;
                return col;
}
            ENDCG
        }
    }
}
