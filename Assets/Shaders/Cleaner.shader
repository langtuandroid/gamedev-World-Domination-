Shader "Custom Render Texture Effect/Cleaner"
{
    Properties
    {
    }

    SubShader
    {
        Lighting Off
        Blend One Zero
        ZTest Always
        Pass
        {
            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0
            
            float4 _DrawPosition;
            float4 _DrawColor;
            float4 colors[6] = {
                float4(1,0,0,1),
                float4(1,1,0,1),
                float4(1,0,0,1),
                float4(1,0,1,1),
                float4(1,1,0,1),
                float4(1,0,0,1)
            };
            float4 get(v2f_customrendertexture IN, int x, int y) : COLOR
            {
                fixed2 uv = IN.localTexcoord.xy + fixed2(x/_CustomRenderTextureWidth, -y/_CustomRenderTextureHeight);
                
                if (uv.y < 0)
                    return float4(0,0,0,0);
                else if (uv.y > 1)
                    return float4(0,0,0,0);
            
                return tex2D(_SelfTexture2D, uv);
            }
            
            float4 frag(v2f_customrendertexture IN) : COLOR
            {

                

                float4 color = get(IN, 0, 0);
                
                if (distance(_DrawPosition, IN.localTexcoord.xy) < 0.03)
                {
                    // Если этот пиксель - близко к курсору, заливаем его текущим цветом рисования
                    color = float4(color.r, _DrawColor.g, color.b, color.a);
                }
                else if (color.r > 0)
                {

                    color.a = 1;
                }
                
                return color;
            }
            ENDCG
        }
    }
}
