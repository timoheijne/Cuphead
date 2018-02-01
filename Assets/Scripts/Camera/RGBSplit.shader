Shader "Custom/RGBSplit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

            uniform sampler2D _MainTex;
            uniform float _offset;
            
            float4 frag(v2f_img i) : COLOR 
            {
                float2 coords = i.uv.xy;
                
                _offset /= 300;
                
                float4 red = tex2D(_MainTex, coords.xy + _offset);
                float4 green = tex2D(_MainTex, coords.xy);
                float4 blue = tex2D(_MainTex, coords.xy - _offset);
                
                return float4(red.r, green.g, blue.b, 1.0f);
            }

			ENDCG
		}
	}
}
