Shader "Unlit/WaveShader" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Brightness ("Brightness", Float) = 1
		_Saturation ("Saturation", Float) = 1
		_Contrast ("Contrast", Float) = 1
		_LightColor ("LightColor", Vector) = (0,0,0,0)
		_WaveXpoint ("WaveXpoint", Float) = 10
		_Speed ("Speed", Float) = 5
		_Height ("Height", Float) = 2
		_FactorWave ("FactorWave", Float) = 2
		_MaxLegth ("MaxLegth", Float) = 180
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_MatrixMVP;

			struct Vertex_Stage_Input
			{
				float3 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixMVP, float4(input.pos, 1.0));
				return output;
			}

			Texture2D<float4> _MainTex;
			SamplerState sampler_MainTex;

			struct Fragment_Stage_Input
			{
				float2 uv : TEXCOORD0;
			};

			float4 frag(Fragment_Stage_Input input) : SV_TARGET
			{
				return _MainTex.Sample(sampler_MainTex, float2(input.uv.x, input.uv.y));
			}

			ENDHLSL
		}
	}
}