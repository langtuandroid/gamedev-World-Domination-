Shader "World" {
  Properties {
		
		_MainTex("Top", 2D) = "white" {}
		
	
		
		_ExtraBrightness ("Brightness", Float) = .1
			_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
	}
	
	SubShader {
		Tags {
			"Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout"
			//"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"
			//"Queue"="Geometry"
			//"IgnoreProjector"="False"
			//"RenderType"="Opaque"
		}

		//Cull Off
		//ZWrite On
		   Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma surface surf NoLighting  noambient alphatest:_Cutoff addshadows
		#pragma exclude_renderers flash

	
			sampler2D _MainTex;
		float _SideScale, _TopScale;
		fixed _ExtraBrightness;
		float4 colors[10];/* = {
			 float4(.6,.85,1,1),
			 float4(1,.65,0.55,1),
			 float4(1,0,0,1),
			 float4(1,0,1,1),
			 float4(1,1,0,1),
			 float4(1,0,0,1)
		};*/
		struct Input {
		
			  float2 uv_MainTex;
		};
			 fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten) {
             return fixed4(s.Albedo*saturate(atten), s.Alpha);
         }


		void surf (Input IN, inout SurfaceOutput o) {
		
			float4 col = tex2D(_MainTex, IN.uv_MainTex);

			o.Albedo = col.rgb;
			o.Alpha = col.a;
		} 
		ENDCG
	}
			FallBack "Legacy Shaders/Transparent/Cutout/Diffuse"
}