sampler sprite;
sampler sprite2;

struct VertexToPixel
{
    float4 Position     : POSITION;
    float4 Color        : COLOR0;
	float2 UV			: TEXCOORD0;
};

float2 lightPos = float2(1.0, 1.0);
float density = 1.0f;
float weight = 1.0f;
float decay = 0.5f;
float exposure = 1.0f;
float4 PixelShaderFunction(VertexToPixel input) : COLOR0
{
	//int NUM_SAMPLES = 12;

	//float4 realColor = tex2D(sprite2, input.UV);

	/*half2 deltaTexCoord = (input.UV - lightPos.xy);
	deltaTexCoord *= 1.0f / NUM_SAMPLES * density;
	half3 color = tex2D(sprite, input.UV);
	half illuminationDecay = 1.0f;

	for(int i = 0; i < NUM_SAMPLES; i++)
	{
		input.UV -= deltaTexCoord;
		half3 samp = tex2D(sprite, input.UV);
		samp *= illuminationDecay * weight;
		color += samp;
		illuminationDecay *= decay;
	}

	return realColor + float4(color * exposure, 1);*/

	float4 lightColor = tex2D(sprite, input.UV);

	/*lightColor.r = floor(lightColor.r * 32.0f) / 32.0f;
	lightColor.g = floor(lightColor.g * 32.0f) / 32.0f;
	lightColor.b = floor(lightColor.b * 32.0f) / 32.0f;*/


	return tex2D(sprite2, input.UV) + lightColor;
}

technique BasicTexture
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
