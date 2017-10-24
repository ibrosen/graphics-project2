// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/WaterShader" {
    Properties {
        _MainTex ("Water (RGB)", 2D) = "white" {}
        _WarpTex ("Warp Texture (RGB)", 2D) = "white" {}
        _WarpIntensity ("Warp Intensity", Range(0,10)) = 1
        _WarpUSpeed ("Warp U Speed", Range(-10,10)) = 1
        _WarpVSpeed ("Warp V Speed", Range(-10,10)) = 1
        _Brightness ("Brightness", Range(0,2)) = 1
    }
    SubShader
    {
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

            struct appdata
            {
                float4 vertex         : POSITION;
                float2 uv            : TEXCOORD0;
                float4 color        : COLOR;
            };

            struct v2f
            {
                float4 pos             : SV_POSITION;
                float4 uv             : TEXCOORD0;
                float4 color         : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _WarpTex;
            float4 _WarpTex_ST;
            fixed _WarpIntensity;
            fixed _WarpUSpeed;
            fixed _WarpVSpeed;
            fixed _Brightness;

            float _LightMapStrength;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.zw = TRANSFORM_TEX(v.uv, _WarpTex);
                o.uv.zw += float2((_Time.y * _WarpUSpeed) % 1, (_Time.y * _WarpVSpeed) % 1); 
                o.color = v.color;
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float2 mainUV = i.uv.xy;
                float2 warpUV = i.uv.zw;

                fixed2 warp = tex2D( _WarpTex, warpUV );
                warp -= .5f;
                warp = lerp( warp, 0, _WarpIntensity );
                mainUV += warp;

                float attenuation = LIGHT_ATTENUATION(i);
                fixed4 col = tex2D(_MainTex, mainUV) * i.color * _Brightness * attenuation;

                return col;
            }
            ENDCG
        }
    }
    Fallback "VertexLit"
}