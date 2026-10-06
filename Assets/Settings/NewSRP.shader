Shader "Custom/NewSRP"
{   
    Properties
    {
        _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        _BaseMap("Base Map", 2D) = "white" {}
        
    }
    SubShader
    {
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        ENDHLSL

        Tags { "RenderType"="Opaque" }
        LOD 100
        ZWrite Off Cull Off
        Pass
        {
            Name "NewSRP"

            HLSLPROGRAM

            CBUFFER_START(Post)
            float4 _BaseColor;
            CBUFFER_END
            
            #pragma vertex Vert
            #pragma fragment Frag

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            float4 Frag (Varyings input) : SV_Target
            {
                float4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord).rgba;
                float4 colour = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.texcoord).rgba;
                float3 HSV = RgbToHsv(colour.rgb);
                float4 outcolour = lerp(color, 1-color, HSV.z);
                return outcolour;
            }
            
            ENDHLSL
        }
    }
}
