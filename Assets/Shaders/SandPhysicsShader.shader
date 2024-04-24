Shader "Custom Render Texture Effect/SandPhysics"
{
    Properties
    {
        _Stamp("Stamp", 2D) = "white" {}
        _StartScale("Start Scale", Float) = 0
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
            float _StartScale;
            sampler2D _Stamp;
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
            { float4 color = get(IN, 0, 0);
            float4 color0 = color;// get(IN, 0, 0);
            float a = color.a;
            /*
            int clean = 0;


            if (color.r > 0 && color.a < .21) {
                if (color.a < .01)
                    clean = 1;
                else
                    clean = 2;
            }
            */
            half3 delta = abs(color0.rgb - _DrawColor);


            if ((delta.r + delta.g + delta.b) > .01){
                fixed2 uv = IN.localTexcoord.xy;
                fixed2 stampUv = float4(((-IN.localTexcoord.xy + _DrawPosition) * 14 + fixed2(.5,.5)), 0, 0);
                if (tex2D(_Stamp, stampUv).a > 0) {

                    float4 c = tex2D(_Stamp, stampUv);
                    color = c.a * _DrawColor * color.a;// float4(color.r, _DrawColor.g, color.b, color.a);
                    delta = abs(color0.rgb - float3(0.651, 0.639, 0.651));

                    // if (distance(color.rgb, float3(0.651, 0.639, 0.651) < .01)) {
                   // if ((delta.r + delta.g + delta.b) < .01) {

                        if (length(c.rgb) == 0) {
                            color = float4(1, 1, 1, a);
                        }
                   // }
                   // else {

                   // }
                }
            }
            if (_StartScale > .1 && distance(_DrawPosition, IN.localTexcoord.xy) < 0.1 * _StartScale) {
            
                color = float4(_DrawColor.r, _DrawColor.g, _DrawColor.b, color.a);

                if (distance(_DrawPosition, IN.localTexcoord.xy) > 0.098 * _StartScale) {
                    color = float4(1, 1, 1, color.a);
                }
            }
            
            /*else if (distance(_DrawPosition, IN.localTexcoord.xy) < 0.01)
                {
                    // Если этот пиксель - близко к курсору, заливаем его текущим цветом рисования
                    color = float4(color.r, color.g, color.b, a);
                }
              */  





               
                return color;
            }
            ENDCG
        }
    }
}
