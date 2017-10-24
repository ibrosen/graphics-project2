Shader "Custom/SandShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

//		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
//		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
//		// #pragma instancing_options assumeuniformscaling
//		UNITY_INSTANCING_CBUFFER_START(Props)
//			// put more per-instance properties here
//		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
//	FallBack "Diffuse"



//	SubShader
//	{
//		Pass
//		{
//			CGPROGRAM
//			#pragma vertex vert
//			#pragma fragment frag
//
//			#include "UnityCG.cginc"
//
//		sampler2D _MainTex;
//
//		struct Input {
//			float2 uv_MainTex;
//		};
//
//		half _Glossiness;
//		half _Metallic;
//		fixed4 _Color;
//
//		void surf (Input IN, inout SurfaceOutputStandard o) {
//			// Albedo comes from a texture tinted by color
//			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//			o.Albedo = c.rgb;
//			// Metallic and smoothness come from slider variables
//			o.Metallic = _Metallic;
//			o.Smoothness = _Glossiness;
//			o.Alpha = c.a;
//		}
//
//			uniform float3 _PointLightColor;
//			uniform float3 _PointLightPosition;
//
//			struct vertIn
//			{
//				float4 vertex : POSITION;
//				float4 normal : NORMAL;
//				float4 color : COLOR;
//			};
//
//			struct vertOut
//			{
//				float4 vertex : SV_POSITION;
//				float4 color : COLOR;
//			};
//
//			// Implementation of the vertex shader
//			vertOut vert(vertIn v)
//			{
//				vertOut o;
//
//				// Convert Vertex position and corresponding normal into world coords.
//				// Note that we have to multiply the normal by the transposed inverse of the world 
//				// transformation matrix (for cases where we have non-uniform scaling; we also don't
//				// care about the "fourth" dimension, because translations don't affect the normal) 
//				float4 worldVertex = mul(unity_ObjectToWorld, v.vertex);
//				float3 worldNormal = normalize(mul(transpose((float3x3)unity_WorldToObject), v.normal.xyz));
//
//				// Calculate ambient RGB intensities
//				float Ka = 1;
//				float3 amb = v.color.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;
//
//				// Calculate diffuse RBG reflections, we save the results of L.N because we will use it again
//				// (when calculating the reflected ray in our specular component)
//				float fAtt = 1;
//				float Kd = 1;
//				float3 L = normalize(_PointLightPosition - worldVertex.xyz);
//				float LdotN = dot(L, worldNormal.xyz);
//				float3 dif = fAtt * _PointLightColor.rgb * Kd * v.color.rgb * saturate(LdotN);
//
//				// Calculate specular reflections
//				float Ks = 1;
//				float specN = 5; // Values>>1 give tighter highlights
//				float3 V = normalize(_WorldSpaceCameraPos - worldVertex.xyz);
//				//float3 R = float3(0.0, 0.0, 0.0);
//				float3 R = 2 * dot(L, v.normal) * v.normal - L;
//				float3 spe = fAtt * _PointLightColor.rgb * Ks * pow(saturate(dot(V, R)), specN);
//
//				// Combine Phong illumination model components
//				o.color.rgb = amb.rgb + dif.rgb + spe.rgb;
//				o.color.a = v.color.a;
//
//				// Transform vertex in world coordinates to camera coordinates
//				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
//
//				return o;
//			}
//
//			// Implementation of the fragment shader
//			fixed4 frag(vertOut v) : SV_Target
//			{
//				return v.color;
//			}
//				ENDCG
//		}
//	}
}
