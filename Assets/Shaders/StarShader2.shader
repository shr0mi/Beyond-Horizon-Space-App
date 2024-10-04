Shader "Universal Render Pipeline/Unlit/StarShader2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (0.0, 0.0, 0.0, 1.0)
        _EmissionColor ("Emission Color", Color) = (0.0, 0.0, 0.0, 1.0)
        _Size ("Size", float) = 1.0
    }
    SubShader
    {
        Blend SrcAlpha One
        ZTest Off
        Tags { 
            "Queue"="Transparent"
            "RenderType" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
             }
        LOD 100

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            //#include "UnityCG.cginc"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            CBUFFER_START(UnityPerMaterial)
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Size;
            half4 _Color; 
            float4 _EmissionColor;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings o;
                o.vertex = TransformObjectToHClip(IN.vertex * _Size);
                o.uv = TRANSFORM_TEX(IN.uv, _MainTex); // Problematic
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            
            

            half4 frag (Varyings i) : SV_Target
            {
                // Center the UV coordinates -1 -> +1
                float distance_from_centre = length((2 * i.uv) - 1);
                // Desmos designed function to give punchy star drop off.
                float inverse_dist = saturate((0.2 / distance_from_centre) - 0.2);
                float4 emission = _EmissionColor * inverse_dist;
                half4 col = float4(_Color.r, _Color.g , _Color.b , inverse_dist);
                col.rgb += emission.rgb * 3.0f;
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDHLSL

            // Enable depth writing
            ZWrite On
            // Use standard depth testing
            ZTest LEqual
        }
    }
}