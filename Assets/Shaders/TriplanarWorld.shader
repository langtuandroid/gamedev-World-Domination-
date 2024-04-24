Shader "Tri-Planar World" {
  Properties {
		_Side("Side", 2D) = "white" {}
		_Top("Top", 2D) = "white" {}
		_Grass("Grass", 2D) = "white" {}
		_Ash("Ash", 2D) = "white" {}
		_SideScale("Side Scale", Float) = 2
		_TopScale("Top Scale", Float) = 2
		
		_ExtraBrightness ("Brightness", Float) = .1
	}
	
	SubShader {
		Tags {
			"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"
			//"Queue"="Geometry"
			//"IgnoreProjector"="False"
			//"RenderType"="Opaque"
		}

		Cull Off
		//ZWrite On
		   Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma surface surf Lambert  noambient alpha:fade //addshadows
		#pragma exclude_renderers flash

		sampler2D _Side, _Top, _Grass, _Ash;
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
			float3 worldPos;
			float3 worldNormal;
			  float2 uv_Grass;
		};
			 fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten) {
             return fixed4(s.Albedo*saturate(atten), s.Alpha);
         }


		void surf (Input IN, inout SurfaceOutput o) {
			float3 projNormal = saturate(pow(IN.worldNormal * 1.4, 4));
			
			// SIDE X
			float3 x = tex2D(_Side, frac(IN.worldPos.zy * _SideScale)) * abs(IN.worldNormal.x);
			
			// TOP / BOTTOM
			float4 y = 0;
			//if (IN.worldNormal.y > 0) {
				y = tex2D(_Top, frac(float2(IN.worldPos.x, IN.worldPos.z) * _TopScale+float2(.5,.5))) * abs(IN.worldNormal.y);
			
				float3 topC = tex2D(_Grass, IN.uv_Grass);// lerp(tex2D(_Grass, IN.uv_Grass), 0, y.a);

			topC = lerp(topC,1,y.r*(y.a) );

			//temp
			// 
			topC = y*y.a;
			
			topC = colors[round(y.r * 10)-1] * y.a;
			
			if(y.b>.4)
				topC = float4(1, 1, 1, 1)*y.a;
			topC += float4(.6, .6, .6, 1) * (1 - y.a);
			//if (y.a < .02)
			
			// SIDE Z	
			float3 z = tex2D(_Side, frac(IN.worldPos.xy * _SideScale)) * abs(IN.worldNormal.z);
			
			o.Albedo = z;
			o.Albedo = lerp(o.Albedo, x, projNormal.x);
			o.Albedo = lerp(o.Albedo, topC, projNormal.y);
			o.Albedo = (o.Albedo)+_ExtraBrightness;

			float4 col = tex2D(_Top, frac(float2(IN.worldPos.x, IN.worldPos.z) * _TopScale + float2(.5, .5)));

			o.Albedo = col.rgb;
			o.Alpha = col.a;
		} 
		ENDCG
	}
	Fallback "Diffuse"
}