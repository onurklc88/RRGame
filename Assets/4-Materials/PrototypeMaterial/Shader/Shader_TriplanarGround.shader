// Toony Colors Pro+Mobile 2
// (c) 2014-2023 Jean Moreno

Shader "Toony Colors Pro 2/User/Shader_TriplanarGround"
{
	Properties
	{
		[TCP2HeaderHelp(Base)]
		_BaseColor ("Color", Color) = (1,1,1,1)
		[TCP2ColorNoAlpha] _HColor ("Highlight Color", Color) = (0.75,0.75,0.75,1)
		[TCP2ColorNoAlpha] _SColor ("Shadow Color", Color) = (0.2,0.2,0.2,1)
		[TCP2Separator]

		[TCP2Header(Ramp Shading)]
		
		_RampThreshold ("Threshold", Range(0.01,1)) = 0.5
		_RampSmoothing ("Smoothing", Range(0.001,1)) = 0.5
		[TCP2Separator]

		[TCP2HeaderHelp(Emission)]
		[TCP2ColorNoAlpha] [HDR] _Emission ("Emission Color", Color) = (0,0,0,1)
		[TCP2Separator]
		
		[TCP2HeaderHelp(Rim Lighting)]
		[Toggle(TCP2_RIM_LIGHTING)] _UseRim ("Enable Rim Lighting", Float) = 0
		[TCP2ColorNoAlpha] _RimColor ("Rim Color", Color) = (0.8,0.8,0.8,0.5)
		_RimMinVert ("Rim Min", Range(0,2)) = 0.5
		_RimMaxVert ("Rim Max", Range(0,2)) = 1
		[TCP2Separator]
		
		[TCP2HeaderHelp(Triplanar Mapping)]
		_TriGround ("Ground", 2D) = "white" {}
		[NoScaleOffset] _TriSide ("Walls", 2D) = "white" {}
		[TCP2Vector4Floats(Contrast X,Contrast Y,Contrast Z,Smoothing,1,16,1,16,1,16,0.01,1)] _TriplanarBlendStrength ("Triplanar Parameters", Vector) = (2,8,2,0.5)
		[TCP2Separator]
		
		[TCP2HeaderHelp(Normal Mapping)]
		[Toggle(_NORMALMAP)] _UseNormalMap ("Enable Normal Mapping", Float) = 0
		[NoScaleOffset] _BumpMap ("Normal Map", 2D) = "bump" {}
		_BumpScale ("Scale", Float) = 1
		[NoScaleOffset] _ParallaxMap ("Height Map", 2D) = "black" {}
		_Parallax ("Height", Range(0.005,0.08)) = 0.02
		[TCP2Separator]
		
		[TCP2HeaderHelp(Texture Blending)]
		[NoScaleOffset] _BlendingSource ("Blending Source", 2D) = "black" {}
		_BlendTex1 ("Texture 1", 2D) = "white" {}
		_BlendTex2 ("Texture 2", 2D) = "white" {}
		_BlendTex3 ("Texture 3", 2D) = "white" {}
		_BlendTex4 ("Texture 4", 2D) = "white" {}
		[TCP2Separator]
		
		[ToggleOff(_RECEIVE_SHADOWS_OFF)] _ReceiveShadowsOff ("Receive Shadows", Float) = 1

		// Avoid compile error if the properties are ending with a drawer
		[HideInInspector] __dummy__ ("unused", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"RenderPipeline" = "UniversalPipeline"
			"RenderType"="Opaque"
		}

		HLSLINCLUDE
		#define fixed half
		#define fixed2 half2
		#define fixed3 half3
		#define fixed4 half4

		#if UNITY_VERSION >= 202020
			#define URP_10_OR_NEWER
		#endif
		#if UNITY_VERSION >= 202120
			#define URP_12_OR_NEWER
		#endif
		#if UNITY_VERSION >= 202220
			#define URP_14_OR_NEWER
		#endif

		// Texture/Sampler abstraction
		#define TCP2_TEX2D_WITH_SAMPLER(tex)						TEXTURE2D(tex); SAMPLER(sampler##tex)
		#define TCP2_TEX2D_NO_SAMPLER(tex)							TEXTURE2D(tex)
		#define TCP2_TEX2D_SAMPLE(tex, samplertex, coord)			SAMPLE_TEXTURE2D(tex, sampler##samplertex, coord)
		#define TCP2_TEX2D_SAMPLE_LOD(tex, samplertex, coord, lod)	SAMPLE_TEXTURE2D_LOD(tex, sampler##samplertex, coord, lod)

		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

		// Uniforms

		// Shader Properties
		TCP2_TEX2D_WITH_SAMPLER(_ParallaxMap);
		TCP2_TEX2D_WITH_SAMPLER(_BlendingSource);
		TCP2_TEX2D_WITH_SAMPLER(_BlendTex1);
		TCP2_TEX2D_WITH_SAMPLER(_BlendTex2);
		TCP2_TEX2D_WITH_SAMPLER(_BlendTex3);
		TCP2_TEX2D_WITH_SAMPLER(_BlendTex4);
		TCP2_TEX2D_WITH_SAMPLER(_BumpMap);
		TCP2_TEX2D_WITH_SAMPLER(_TriGround);
		TCP2_TEX2D_WITH_SAMPLER(_TriSide);

		CBUFFER_START(UnityPerMaterial)
			
			// Shader Properties
			float _RimMinVert;
			float _RimMaxVert;
			float _Parallax;
			float4 _BlendTex1_ST;
			float4 _BlendTex2_ST;
			float4 _BlendTex3_ST;
			float4 _BlendTex4_ST;
			float _BumpScale;
			float4 _TriGround_ST;
			float4 _TriplanarBlendStrength;
			fixed4 _BaseColor;
			half4 _Emission;
			float _RampThreshold;
			float _RampSmoothing;
			fixed4 _RimColor;
			fixed4 _SColor;
			fixed4 _HColor;
		CBUFFER_END

		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/ParallaxMapping.hlsl"
		
		// Calculates UV offset for parallax bump mapping
		inline float2 TCP2_ParallaxOffset( half h, half height, half3 viewDir )
		{
			h = h * height - height/2.0;
			float3 v = normalize(viewDir);
			v.z += 0.42;
			return h * (v.xy / v.z);
		}
		
		// Built-in renderer (CG) to SRP (HLSL) bindings
		#define UnityObjectToClipPos TransformObjectToHClip
		#define _WorldSpaceLightPos0 _MainLightPosition
		
		ENDHLSL

		Pass
		{
			Name "Main"
			Tags
			{
				"LightMode"="UniversalForward"
			}

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard SRP library
			// All shaders must be compiled with HLSLcc and currently only gles is not using HLSLcc by default
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 3.0

			// -------------------------------------
			// Material keywords
			#pragma shader_feature_local _ _RECEIVE_SHADOWS_OFF

			// -------------------------------------
			// Universal Render Pipeline keywords
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
			#pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
			#pragma multi_compile_fragment _ _SHADOWS_SOFT
			#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
			#pragma multi_compile _ SHADOWS_SHADOWMASK

			// -------------------------------------

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			#pragma vertex Vertex
			#pragma fragment Fragment

			//--------------------------------------
			// Toony Colors Pro 2 keywords
			#pragma shader_feature_local TCP2_RIM_LIGHTING
			#pragma shader_feature_local _NORMALMAP

			// vertex input
			struct Attributes
			{
				float4 vertex       : POSITION;
				float3 normal       : NORMAL;
				float4 tangent      : TANGENT;
				float4 texcoord0 : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			// vertex output / fragment input
			struct Varyings
			{
				float4 positionCS     : SV_POSITION;
				float3 normal         : NORMAL;
				float4 worldPosAndFog : TEXCOORD0;
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord    : TEXCOORD1; // compute shadow coord per-vertex for the main light
			#endif
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				half3 vertexLights : TEXCOORD2;
			#endif
				float4 pack0 : TEXCOORD3; /* pack0.xyz = tangent  pack0.w = rim */
				float3 pack1 : TEXCOORD4; /* pack1.xyz = bitangent */
				float2 pack2 : TEXCOORD5; /* pack2.xy = texcoord0 */
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			Varyings Vertex(Attributes input)
			{
				Varyings output = (Varyings)0;

				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				// Texture Coordinates
				output.pack2.xy = input.texcoord0.xy;
				// Shader Properties Sampling
				float __rimMinVert = ( _RimMinVert );
				float __rimMaxVert = ( _RimMaxVert );

				float3 worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
				VertexPositionInputs vertexInput = GetVertexPositionInputs(input.vertex.xyz);
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				output.shadowCoord = GetShadowCoord(vertexInput);
			#endif

				VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(input.normal, input.tangent);
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				// Vertex lighting
				output.vertexLights = VertexLighting(vertexInput.positionWS, vertexNormalInput.normalWS);
			#endif

				// world position
				output.worldPosAndFog = float4(vertexInput.positionWS.xyz, 0);

				// normal
				output.normal = normalize(vertexNormalInput.normalWS);

				// tangent
				output.pack0.xyz = vertexNormalInput.tangentWS;
				output.pack1.xyz = vertexNormalInput.bitangentWS;

				// clip position
				output.positionCS = vertexInput.positionCS;

				half3 viewDirWS = SafeNormalize(GetCameraPositionWS() - vertexInput.positionWS);
				half ndv = abs(dot(viewDirWS, vertexNormalInput.normalWS));
				half ndvRaw = ndv;

				#if defined(TCP2_RIM_LIGHTING)
				half rim = 1 - ndvRaw;
				rim = smoothstep(__rimMinVert, __rimMaxVert, rim);
				output.pack0.w = rim;
				#endif

				return output;
			}

			half4 Fragment(Varyings input
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

				float3 positionWS = input.worldPosAndFog.xyz;
				float3 normalWS = normalize(input.normal);
				float3 normalWS_Vertex = normalWS;
				half3 viewDirWS = SafeNormalize(GetCameraPositionWS() - positionWS);
				half3 tangentWS = input.pack0.xyz;
				half3 bitangentWS = input.pack1.xyz;
				#if defined(_NORMALMAP)
				half3x3 tangentToWorldMatrix = half3x3(tangentWS.xyz, bitangentWS.xyz, normalWS.xyz);
				//Parallax Offset
				float __parallaxHeightMap = ( TCP2_TEX2D_SAMPLE(_ParallaxMap, _ParallaxMap, input.pack2.xy).a );
				float __parallaxHeight = ( _Parallax );
				half height = __parallaxHeightMap;
				half3 viewDirTS = GetViewDirectionTangentSpace(half4(tangentWS, 0), normalWS, viewDirWS);
				float2 offset = TCP2_ParallaxOffset(height, __parallaxHeight, viewDirTS);
				input.pack2.xy += offset;
				#endif

				// Shader Properties Sampling
				float4 __blendingSource = ( TCP2_TEX2D_SAMPLE(_BlendingSource, _BlendingSource, input.pack2.xy).rgba );
				float4 __blendTexture1 = ( TCP2_TEX2D_SAMPLE(_BlendTex1, _BlendTex1, input.pack2.xy * _BlendTex1_ST.xy + _BlendTex1_ST.zw).rgba );
				float4 __blendTexture2 = ( TCP2_TEX2D_SAMPLE(_BlendTex2, _BlendTex2, input.pack2.xy * _BlendTex2_ST.xy + _BlendTex2_ST.zw).rgba );
				float4 __blendTexture3 = ( TCP2_TEX2D_SAMPLE(_BlendTex3, _BlendTex3, input.pack2.xy * _BlendTex3_ST.xy + _BlendTex3_ST.zw).rgba );
				float4 __blendTexture4 = ( TCP2_TEX2D_SAMPLE(_BlendTex4, _BlendTex4, input.pack2.xy * _BlendTex4_ST.xy + _BlendTex4_ST.zw).rgba );
				float4 __normalMap = ( TCP2_TEX2D_SAMPLE(_BumpMap, _BumpMap, input.pack2.xy).rgba );
				float __bumpScale = ( _BumpScale );
				float4 __triplanarParameters = ( _TriplanarBlendStrength.xyzw );
				float4 __mainColor = ( _BaseColor.rgba );
				float __ambientIntensity = ( 1.0 );
				float3 __emission = ( _Emission.rgb );
				float __rampThreshold = ( _RampThreshold );
				float __rampSmoothing = ( _RampSmoothing );
				float3 __rimColor = ( _RimColor.rgb );
				float __rimStrength = ( 1.0 );
				float3 __shadowColor = ( _SColor.rgb );
				float3 __highlightColor = ( _HColor.rgb );

				// Texture Blending: initialize
				fixed4 blendingSource = __blendingSource;
				fixed4 tex1 = __blendTexture1;
				fixed4 tex2 = __blendTexture2;
				fixed4 tex3 = __blendTexture3;
				fixed4 tex4 = __blendTexture4;
				#if defined(_NORMALMAP)
				half4 normalMap = __normalMap;
				half3 normalTS = UnpackNormalScale(normalMap, __bumpScale);
					#if defined(_NORMALMAP)
				normalWS = normalize( mul(normalTS, tangentToWorldMatrix) );
					#endif
				#endif

				// main texture
				half3 albedo = half3(1,1,1);
				half alpha = 1;

				half3 emission = half3(0,0,0);
				half4 albedoAlpha = half4(albedo, alpha);
				
				// Triplanar Texture Blending
				half2 uv_ground = positionWS.xz;
				half2 uv_sideX = positionWS.zy;
				half2 uv_sideZ = positionWS.xy;
				float3 triplanarNormal = normalWS_Vertex;
				
				//ground
				half4 triplanar = ( TCP2_TEX2D_SAMPLE(_TriGround, _TriGround, uv_ground * _TriGround_ST.xy + _TriGround_ST.zw).rgba );
				albedoAlpha.rgb *= triplanar.rgb;
				
				// Texture Blending: sample
				albedoAlpha = lerp(albedoAlpha, tex1, blendingSource.r);
				albedoAlpha = lerp(albedoAlpha, tex2, blendingSource.g);
				albedoAlpha = lerp(albedoAlpha, tex3, blendingSource.b);
				albedoAlpha = lerp(albedoAlpha, tex4, blendingSource.a);
				triplanar = albedoAlpha;
				albedoAlpha.rgb = half3(1, 1, 1);
				
				//walls
				fixed4 tex_sideX = ( TCP2_TEX2D_SAMPLE(_TriSide, _TriSide, uv_sideX).rgba );
				fixed4 tex_sideZ = ( TCP2_TEX2D_SAMPLE(_TriSide, _TriSide, uv_sideZ).rgba );
				
				//blending
				half3 blendWeights = pow(abs(triplanarNormal), __triplanarParameters.xyz / __triplanarParameters.w);
				blendWeights = blendWeights / (blendWeights.x + abs(blendWeights.y) + blendWeights.z);
				
				triplanar = tex_sideX * blendWeights.x + triplanar * blendWeights.y + tex_sideZ * blendWeights.z;
				albedoAlpha *= triplanar;
				albedo = albedoAlpha.rgb;
				alpha = albedoAlpha.a;
				
				albedo *= __mainColor.rgb;

				// main light: direction, color, distanceAttenuation, shadowAttenuation
			#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord = input.shadowCoord;
			#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
				float4 shadowCoord = TransformWorldToShadowCoord(positionWS);
			#else
				float4 shadowCoord = float4(0, 0, 0, 0);
			#endif

			#if defined(URP_10_OR_NEWER)
				#if defined(SHADOWS_SHADOWMASK) && defined(LIGHTMAP_ON)
					half4 shadowMask = SAMPLE_SHADOWMASK(input.uvLM);
				#elif !defined (LIGHTMAP_ON)
					half4 shadowMask = unity_ProbesOcclusion;
				#else
					half4 shadowMask = half4(1, 1, 1, 1);
				#endif

				Light mainLight = GetMainLight(shadowCoord, positionWS, shadowMask);
			#else
				Light mainLight = GetMainLight(shadowCoord);
			#endif

				// ambient or lightmap
				// Samples SH fully per-pixel. SampleSHVertex and SampleSHPixel functions
				// are also defined in case you want to sample some terms per-vertex.
				half3 bakedGI = SampleSH(normalWS);
				half occlusion = 1;

				half3 indirectDiffuse = bakedGI;
				indirectDiffuse *= occlusion * albedo * __ambientIntensity;
				emission += __emission;

				half3 lightDir = mainLight.direction;
				half3 lightColor = mainLight.color.rgb;

				half atten = mainLight.shadowAttenuation * mainLight.distanceAttenuation;

				half ndl = dot(normalWS, lightDir);
				half3 ramp;
				
				half rampThreshold = __rampThreshold;
				half rampSmooth = __rampSmoothing * 0.5;
				ndl = saturate(ndl);
				ramp = smoothstep(rampThreshold - rampSmooth, rampThreshold + rampSmooth, ndl);

				// apply attenuation
				ramp *= atten;

				half3 color = half3(0,0,0);
				// Rim Lighting
				#if defined(TCP2_RIM_LIGHTING)
				half rim = input.pack0.w;
				rim = ( rim );
				half3 rimColor = __rimColor;
				half rimStrength = __rimStrength;
				//Rim light mask
				emission.rgb += ndl * atten * rim * rimColor * rimStrength;
				#endif
				half3 accumulatedRamp = ramp * max(lightColor.r, max(lightColor.g, lightColor.b));
				half3 accumulatedColors = ramp * lightColor.rgb;

				// Additional lights loop
			#ifdef _ADDITIONAL_LIGHTS
				uint pixelLightCount = GetAdditionalLightsCount();

				LIGHT_LOOP_BEGIN(pixelLightCount)
				{
					#if defined(URP_10_OR_NEWER)
						Light light = GetAdditionalLight(lightIndex, positionWS, shadowMask);
					#else
						Light light = GetAdditionalLight(lightIndex, positionWS);
					#endif
					half atten = light.shadowAttenuation * light.distanceAttenuation;

					#if defined(_LIGHT_LAYERS)
						half3 lightDir = half3(0, 1, 0);
						half3 lightColor = half3(0, 0, 0);
						if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
						{
							lightColor = light.color.rgb;
							lightDir = light.direction;
						}
					#else
						half3 lightColor = light.color.rgb;
						half3 lightDir = light.direction;
					#endif

					half ndl = dot(normalWS, lightDir);
					half3 ramp;
					
					ndl = saturate(ndl);
					ramp = smoothstep(rampThreshold - rampSmooth, rampThreshold + rampSmooth, ndl);

					// apply attenuation (shadowmaps & point/spot lights attenuation)
					ramp *= atten;

					accumulatedRamp += ramp * max(lightColor.r, max(lightColor.g, lightColor.b));
					accumulatedColors += ramp * lightColor.rgb;

					#if defined(TCP2_RIM_LIGHTING)
					// Rim light mask
					half3 rimColor = __rimColor;
					half rimStrength = __rimStrength;
					emission.rgb += ndl * atten * rim * rimColor * rimStrength;
					#endif
				}
				LIGHT_LOOP_END
			#endif
			#ifdef _ADDITIONAL_LIGHTS_VERTEX
				color += input.vertexLights * albedo;
			#endif

				accumulatedRamp = saturate(accumulatedRamp);
				half3 shadowColor = (1 - accumulatedRamp.rgb) * __shadowColor;
				accumulatedRamp = accumulatedColors.rgb * __highlightColor + shadowColor;
				color += albedo * accumulatedRamp;

				// apply ambient
				color += indirectDiffuse;

				color += emission;
				return half4(color, alpha);
			}
			ENDHLSL
		}

		// Depth & Shadow Caster Passes
		HLSLINCLUDE

		#if defined(SHADOW_CASTER_PASS) || defined(DEPTH_ONLY_PASS)

			#define fixed half
			#define fixed2 half2
			#define fixed3 half3
			#define fixed4 half4

			float3 _LightDirection;
			float3 _LightPosition;

			struct Attributes
			{
				float4 vertex   : POSITION;
				float3 normal   : NORMAL;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct Varyings
			{
				float4 positionCS     : SV_POSITION;
				float4 pack0 : TEXCOORD1; /* pack0.xyz = positionWS  pack0.w = rim */
			#if defined(DEPTH_ONLY_PASS)
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			#endif
			};

			float4 GetShadowPositionHClip(Attributes input)
			{
				float3 positionWS = TransformObjectToWorld(input.vertex.xyz);
				float3 normalWS = TransformObjectToWorldNormal(input.normal);

				#if _CASTING_PUNCTUAL_LIGHT_SHADOW
					float3 lightDirectionWS = normalize(_LightPosition - positionWS);
				#else
					float3 lightDirectionWS = _LightDirection;
				#endif
				float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, lightDirectionWS));

				#if UNITY_REVERSED_Z
					positionCS.z = min(positionCS.z, UNITY_NEAR_CLIP_VALUE);
				#else
					positionCS.z = max(positionCS.z, UNITY_NEAR_CLIP_VALUE);
				#endif

				return positionCS;
			}

			Varyings ShadowDepthPassVertex(Attributes input)
			{
				Varyings output = (Varyings)0;
				UNITY_SETUP_INSTANCE_ID(input);
				#if defined(DEPTH_ONLY_PASS)
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
				#endif

				float3 worldNormalUv = mul(unity_ObjectToWorld, float4(input.normal, 1.0)).xyz;

				float3 worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
				VertexPositionInputs vertexInput = GetVertexPositionInputs(input.vertex.xyz);
				half3 viewDirWS = SafeNormalize(GetCameraPositionWS() - vertexInput.positionWS);
				half ndv = abs(dot(viewDirWS, worldNormalUv));
				half ndvRaw = ndv;
				output.pack0.xyz = vertexInput.positionWS;

				#if defined(DEPTH_ONLY_PASS)
					output.positionCS = TransformObjectToHClip(input.vertex.xyz);
				#elif defined(SHADOW_CASTER_PASS)
					output.positionCS = GetShadowPositionHClip(input);
				#else
					output.positionCS = float4(0,0,0,0);
				#endif

				return output;
			}

			half4 ShadowDepthPassFragment(
				Varyings input
			) : SV_TARGET
			{
				#if defined(DEPTH_ONLY_PASS)
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
				#endif

				float3 positionWS = input.pack0.xyz;

				half3 viewDirWS = SafeNormalize(GetCameraPositionWS() - positionWS);
				half3 albedo = half3(1,1,1);
				half alpha = 1;
				half3 emission = half3(0,0,0);

				return 0;
			}

		#endif
		ENDHLSL

		Pass
		{
			Name "ShadowCaster"
			Tags
			{
				"LightMode" = "ShadowCaster"
			}

			ZWrite On
			ZTest LEqual

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.0

			// using simple #define doesn't work, we have to use this instead
			#pragma multi_compile SHADOW_CASTER_PASS

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing
			#pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

			#pragma vertex ShadowDepthPassVertex
			#pragma fragment ShadowDepthPassFragment

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

			ENDHLSL
		}

		Pass
		{
			Name "DepthOnly"
			Tags
			{
				"LightMode" = "DepthOnly"
			}

			ZWrite On
			ColorMask 0

			HLSLPROGRAM

			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.0

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			// using simple #define doesn't work, we have to use this instead
			#pragma multi_compile DEPTH_ONLY_PASS

			#pragma vertex ShadowDepthPassVertex
			#pragma fragment ShadowDepthPassFragment

			ENDHLSL
		}

	}

	FallBack "Hidden/InternalErrorShader"
	CustomEditor "ToonyColorsPro.ShaderGenerator.MaterialInspector_SG2"
}

/* TCP_DATA u config(ver:"2.9.6";unity:"2021.3.26f1";tmplt:"SG2_Template_URP";features:list["UNITY_5_4","UNITY_5_5","UNITY_5_6","UNITY_2017_1","UNITY_2018_1","UNITY_2018_2","UNITY_2018_3","UNITY_2019_1","UNITY_2019_2","UNITY_2019_3","UNITY_2019_4","UNITY_2020_1","UNITY_2021_1","UNITY_2021_2","TEMPLATE_LWRP","BUMP","BUMP_SCALE","PARALLAX","BUMP_SHADER_FEATURE","TRIPLANAR","TEXTURE_BLENDING","TEXBLEND_LINEAR","BLEND_TEX1","BLEND_TEX2","BLEND_TEX3","BLEND_TEX4","SPECULAR_SHADER_FEATURE","EMISSION","RIM","RIM_VERTEX","RIM_SHADER_FEATURE","RIM_LIGHTMASK"];flags:list[];flags_extra:dict[];keywords:dict[RENDER_TYPE="Opaque",RampTextureDrawer="[TCP2Gradient]",RampTextureLabel="Ramp Texture",SHADER_TARGET="3.0",BLEND_TEX1_CHNL="r",BLEND_TEX2_CHNL="g",BLEND_TEX3_CHNL="b",BLEND_TEX4_CHNL="a",RIM_LABEL="Rim Lighting"];shaderProperties:list[,,,,,,,,,,,,,,,,,,,,,,,,,,,sp(name:"Ground Texture";imps:list[imp_mp_texture(uto:True;tov:"";tov_lbl:"";gto:False;sbt:False;scr:False;scv:"";scv_lbl:"";gsc:False;roff:False;goff:False;sin_anm:False;sin_anmv:"";sin_anmv_lbl:"";gsin:False;notile:False;triplanar_local:False;def:"white";locked_uv:True;uv:0;cc:4;chan:"RGBA";mip:-1;mipprop:False;ssuv_vert:False;ssuv_obj:False;uv_type:Texcoord;uv_chan:"XZ";tpln_scale:1;uv_shaderproperty:__NULL__;uv_cmp:__NULL__;sep_sampler:__NULL__;prop:"_TriGround";md:"";gbv:False;custom:False;refs:"";pnlock:False;guid:"dbbaf340-9bc4-4b7f-855f-edb29655faba";op:Multiply;lbl:"Ground";gpu_inst:False;locked:True;impl_index:0)];layers:list[];unlocked:list[];clones:dict[];isClone:False)];customTextures:list[];codeInjection:codeInjection(injectedFiles:list[];mark:False);matLayers:list[]) */
/* TCP_HASH c7d0f65f8c4a368451b669576b81ee48 */
