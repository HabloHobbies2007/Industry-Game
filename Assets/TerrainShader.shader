Shader "Custom/TerrainShader"
{
	Properties{
		_TexArr("Textures", 2DArray) = ""{}

		_MainTex("Ground Texture", 2D) = "white" {}

		_TexScale("Texture Scale", Float) = 1
	}

	SubShader{
		Tags{"RenderType"="Opaque"}
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
		#pragma require 2darray

		sampler2D _MainTex;
		sampler2D _WallTex;
		UNITY_DECLARE_TEX2DARRAY(_TexArr);
		float _TexScale;

		struct Input{
			float3 worldPos;
			float3 worldNormal;
			float2 uv_TexArr;
		};

		void surf(Input IN, inout SurfaceOutputStandard o){

			float3 scaledWorldPos = IN.worldPos/_TexScale;
			float3 pWeight = abs(IN.worldNormal);
			pWeight /= pWeight.x + pWeight.y + pWeight.z;

			int texIndex = floor(IN.uv_TexArr.x + 0.1);
			float3 projected;

			projected = float3(scaledWorldPos.y, scaledWorldPos.z, texIndex);
			float3 xP = UNITY_SAMPLE_TEX2DARRAY(_TexArr, projected) * pWeight.x;

			projected = float3(scaledWorldPos.x, scaledWorldPos.z, texIndex);
			float3 yP = UNITY_SAMPLE_TEX2DARRAY(_TexArr, projected) * pWeight.y;

			projected = float3(scaledWorldPos.x, scaledWorldPos.y, texIndex);
			float3 zP = UNITY_SAMPLE_TEX2DARRAY(_TexArr, projected) * pWeight.z;

			//float3 xP = tex2D(_MainTex, scaledWorldPos.yz) * pWeight.x;
			//float3 yP = tex2D(_MainTex, scaledWorldPos.xz) * pWeight.y;
			//float3 zP = tex2D(_MainTex, scaledWorldPos.xy) * pWeight.z;


			o.Albedo = xP + yP + zP;
		}

		ENDCG
	}

	Fallback "Diffuse"
}
