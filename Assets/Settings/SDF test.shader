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
        ENDHLSL

        Tags { "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
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

               struct Attributes
            {
            float3 positionOS : POSITION;
            };



            struct Varyings
            {
            float4 positionCS : SV_POSITION;
            float3 positionWS : TEXCOORD0;
            
            };
            
            Varyings Vert(Attributes inp)
            {
            Varyings output;
            output.positionWS = TransformObjectToWorld(inp.positionOS);
            output.positionCS = TransformWorldToHClip(output.positionWS);
            return output;
            }

            

            float4 Frag (Varyings input) : SV_Target
            {
                
                float2 pixelPos = input.positionWS.xy;
                float2 pa = pixelPos - _Startpos;
                float2 ba = _EndPos - _Startpos;
                float midpos = clamp(dot(pa, ba) / dot(ba, ba), 0.0, 1.0);
                float2 closest = _Startpos + midpos * ba;
                float dist = length(pixelPos - closest) - _Radi;
                float aa = fwidth(dist);

                float mask = (1.0 - smoothstep(-aa, aa, dist)) * _BaseColor.a;
                return float4 (_BaseColor.rgb, mask);
            }
            
            ENDHLSL
        }
    }
}
