Shader "Custom/InvertWave"
{   
    Properties
    {
        _Extents ("Extents", VECTOR) = (1,1,0,0)
        _Radi ("Radius", FLOAT) = 1
        _Const("const", FLOAT) = 1
        
    }
    SubShader
    {
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
        ENDHLSL

        Tags { "RenderType"="Opaque" }
        LOD 100
        ZWrite Off Cull Off
        Pass
        {
            Name "InvertWave"

            HLSLPROGRAM

            CBUFFER_START(post)
            float2 _Extents;
            float _Radi;
            float _Const;
            CBUFFER_END
            
            #pragma vertex Vert
            #pragma fragment Frag

            

            float4 Frag (Varyings input) : SV_Target
            {
                float2 pixelPos = input.texcoord;
                float dist = length(pixelPos - _Extents) - _Radi;
                float hole = length(pixelPos - _Extents) - _Const;
                float aa = fwidth(dist);

                float mask = (1.0 - smoothstep(-aa, aa, dist)) - (1.0 - smoothstep(-aa, aa, hole));
                float4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord).rgba;
                float4 outcolor = lerp(color, 1- color, mask);

                return outcolor;
            }
            
            ENDHLSL
        }
    }
}
