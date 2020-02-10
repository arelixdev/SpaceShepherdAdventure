Shader "Custom/DayNightCycle"
{
    Properties
    {
		_MyColor ("Example Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
		#pragma surface surf Lambert
		//#pragma target 3.0

		struct Input 
		{
			float2 uvMainTex;
		};

		fixed4 _MyColor;

		void surf (Input IN, inout SurfaceOutput o)
		{
			o.Albedo = _MyColor.rgb;
		}
        ENDCG
    }
    FallBack "Diffuse"
}
