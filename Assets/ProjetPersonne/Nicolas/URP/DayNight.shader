Shader "Unlit/DayNight"
{
    Properties
    {
		_Color ("Color", Color) =  (1,1,1,1)
		_Gloss ("Gloss", Float) =  1

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
			float _Gloss;

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
				o.uv0 = v.uv0;
				o.normal = v.normal;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.clipSpacePos = UnityObjectToClipPos(v.vertex);
                return o;
            }

			//posterize function
			float Posterize(float steps, float value)
			{
				return floor(value * steps) / steps;
			}
			

            fixed4 frag (VertexOutput o) : SV_Target
            {
				float2 uv = o.uv0;
				return fixed4(_LightColor0.rgb, 0);
				float3 colorA = float3(1,0,0);
				float3 colorB = float3(0,1,0);
				float t = uv.y;
				t = Posterize(16, t);
				//return t;
				float3 blend = lerp(colorA, colorB, t);
				

				float3 normal = normalize(o.normal); // interpolated

				//lighting
				float3 lightDir = float3(1,1,1)/*_WorldSpaceLightPos0.xyz*/;
				float3 lightColor = float3(1,1,1)/*_LightColor0.rgb*/;

				//Direct diffuse light
				float lightFalloff = max(0, dot(lightDir, normal));
				float3 directDiffuseLight = lightColor * lightFalloff;

				//Ambient light

				float3 ambientLight = float3(0.2, 0.2, 0.2);
				
				//Direct specular light
				float3 camPos = _WorldSpaceCameraPos.xyz;
				float3 fragToCam = camPos - o.worldPos;
				float3 viewDir = normalize(fragToCam);
				float3 viewReflect = reflect(-viewDir, normal);
				float specularFalloff = max(0, dot(viewReflect, lightDir));
				specularFalloff = pow(specularFalloff, _Gloss);

				float3 directSpecular = specularFalloff * lightColor;

				//Composite light
				/*float3 diffuseLight = ambientLight + directDiffuseLight
				float3 finalSurfaceColor = diffuseLight * _Color.rgb + directSpecular;

                return float4(finalSurfaceColor, 1);*/
            }
            ENDCG
        }
    }
}
