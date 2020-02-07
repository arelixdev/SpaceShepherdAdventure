Shader "Unlit/DayNight"
{
    Properties
    {
		_Color ("Color", Color) =  (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			#include "UnityLightingCommon.cginc"

			//mesh data, vertex coords, vertex normals, UVs, tangents, vertex colors
            struct VertexInput
            {
                float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv0 : TEXCOORD0;
            };

            struct VertexOutput
            {
                float4 clipSpacePos : SV_POSITION;
				float2 uv0 : TEXTCOORD0;
				float3 normal : NORMAL;
				float3 worldPos : TEXCOORD2;
            };

			float4 _Color;

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
				o.uv0 = v.uv0;
				o.normal = v.normal;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.clipSpacePos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (VertexOutput o) : SV_Target
            {

				return float4 (o.worldPos, 1);
				float2 uv = o.uv0;


				//lighting

				//Direct diffuse light
				float3 lightDir = float3(1,1,1)/*_WorldSpaceLightPos0.xyz*/;
				float3 lightColor = float3(1,1,1)/*_LightColor0.rgb*/;
				float lightFalloff = max(0, dot(lightDir, o.normal));
				float3 directDiffuseLight = lightColor * lightFalloff;

				//Ambient light
				float3 ambientLight = float3(0.2, 0.2, 0.2);
				
				//Direct specular light
				float3 camPos = _WorldSpaceCameraPos.xyz;



				//Composite light
				//float3 diffuseLight = ambientLight + directDiffuseLight
				//float3 final = diffuseLight * _Color.rgb;

                //return float4(diffuseLight, 1);
            }
            ENDCG
        }
    }
}
