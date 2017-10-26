//Shader based off of https://www.youtube.com/watch?v=74b0why26ec
// Alpha shader using vertex fragments (as opposed to surface shader)

Shader "Custom/WaterShader" {
    Properties {
    	// Texture of the water
        _MainTex ("Water (RGB)", 2D) = "white" {}

        // Texture used to distortion the water - we have used a "cloud" image with
        // colours ranging along a grayscale
        _DistortionTexture ("Distortion Texture (RGB)", 2D) = "white" {}

        // Intensity of the distortioning of the water
        _DistortionIntensity ("Distortion Intensity", Range(0,10)) = 5

        // Speed at which to distortion the U and V components
        _DistortionUSpeed ("Distortion U Speed", Range(-0.05,0.05)) = 0.001
        _DistortionVSpeed ("Distortion V Speed", Range(-0.05,0.05)) = 0.001

    }
    SubShader
    {
    	// As this is an alpha shader we must indicate that it is Transparent (drawn later) in the render queue
        Tags { "LightMode" = "ForwardBase" "Queue" = "Transparent" }
        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            // Vertex data from game
            struct appdata
            {
                float4 vertex	: POSITION;
                float2 uv		: TEXCOORD0;	// Note that this is float2 (as opposed to float4)
                float4 color	: COLOR;		// Only really need alpha from this
            };

            // Vertex shader to fragment shader structure
            struct v2f
            {
                float4 pos		: SV_POSITION;
                float4 uv		: TEXCOORD0;
                float4 color	: TEXCOORD1;
            };

            // Shader variables
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _DistortionTexture;
            float4 _DistortionTexture_ST;
            fixed _DistortionIntensity;
            fixed _DistortionUSpeed;
            fixed _DistortionVSpeed;


            float _LightMapStrength;
            
            v2f vert (appdata v)
            {
            	// Declare the vertex to fragment structure as o
            	// This is the data that will come out of the function
                v2f o;

                // Transform the vertex point in object space to the camera's "clip space"
                o.pos = UnityObjectToClipPos(v.vertex);

                // X and Y parameters hold the UVs from the Main Texture
                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
                // Z and W parameters hold the UVs from the Distorition Texture (cloudy texture)
                o.uv.zw = TRANSFORM_TEX(v.uv, _DistortionTexture);

                // Each iteration, a different value is added to the distorition texture UVs. This causes the distortion.
                // The % 1 is required to ensure that the changing value based on time does not get too large
                o.uv.zw += float2((_Time.y * _DistortionUSpeed) % 1, (_Time.y * _DistortionVSpeed) % 1); 

                // Set the o fragment colour of o to the passed colour data from the vertex v
                o.color = v.color;

                // Convert vertext to fragment
                TRANSFER_VERTEX_TO_FRAGMENT(o);

                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {

                float2 mainUV = i.uv.xy;
                float2 distortionUV = i.uv.zw;

                // Sets distortion to the chosen distortion texture (clouds) and the distortion UV
                fixed2 distortion = tex2D( _DistortionTexture, distortionUV );

                // Ensures that uvs can be shifted in both positive and negative directions
                distortion -= .5f;

                // Sets distortion to a value interpolated between 0 and distortion with _DistortionIntensity as the scale
                // This clams the distortion to be within a minimum of 0 and maximum of distortion, the _DistortionIntensity
                // sets the scale of the distortion
                distortion = lerp( distortion, 0, _DistortionIntensity );

                // Adds distortion to the mainUV (UVs in the main/water texture).
                // This causes the actual distortioning of the water texture.
                mainUV += distortion;

                // Multiplying the mainUVs that have just been distorted with the colour of the vertecies, and the attenuation (shadow)
                fixed4 col = tex2D(_MainTex, mainUV) * i.color;

                return col;
            }
            ENDCG
        }
    }
    Fallback "VertexLit"
}