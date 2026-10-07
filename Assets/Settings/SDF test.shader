Shader "Custom/SDF test"
{   
    Properties
    {
        _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        _Startpos("Start Position", Vector) = (1,1, 0, 0)
        _EndPos("End Position", Vector) = (1,1,0,0)
        _Radi("Radius", Float) = 1
        
    }
    SubShader
    {
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
        ENDHLSL

        Tags { "RenderType"="Transparent" }
        LOD 100
        ZWrite Off Cull Off
        Pass
        {
            Name "SDF test"

            HLSLPROGRAM
            CBUFFER_START(post)
            float4 _BaseColor;
            float2 _Startpos;
            float2 _EndPos;
            float _Radi;
            CBUFFER_END
            
            #pragma vertex Vert
            #pragma fragment Frag

            

            float4 Frag (Varyings input) : SV_Target
            {
                float4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord).rgba;
                float2 pixelPos = input.positionCS.xy/_ScaledScreenParams*1000.0;
                float2 pa = pixelPos - _Startpos;
                float2 ba = _EndPos - _Startpos;
                float midpos = clamp(dot(pa, ba) / dot(ba, ba), 0.0, 1.0);
                float2 closest = _Startpos + midpos * ba;
                float dist = length(pixelPos - closest) - _Radi;
                float aa = fwidth(dist);

                float mask = (1.0 - smoothstep(-aa, aa, dist)) * _BaseColor.a;;
                return lerp(color,_BaseColor, mask);
            };
            
            ENDHLSL
        }
    }
}
