Shader "Custom/ReflectionShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _ReflectionTex ("Reflection (2D)", 2D) = "white" {}
        _ReflectionStrength ("Reflection Strength", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        sampler2D _ReflectionTex;
        float _ReflectionStrength;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldRefl;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            float3 albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
            float3 reflection = tex2D(_ReflectionTex, IN.uv_MainTex).rgb;
            o.Albedo = lerp(albedo, reflection, _ReflectionStrength);
            o.Specular = _ReflectionStrength;
        }
        ENDCG
    }
    FallBack "Diffuse"
}