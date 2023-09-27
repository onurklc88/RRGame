// Toony Colors Pro+Mobile 2
// (c) 2014-2023 Jean Moreno

Shader "Toony Colors Pro 2/User/Shader_MobWPulseNDissolve"
{
	Properties
	{
		[TCP2HeaderHelp(Base)]
		_BaseColor ("Color", Color) = (1,1,1,1)
		[TCP2ColorNoAlpha] _HColor ("Highlight Color", Color) = (0.75,0.75,0.75,1)
		[TCP2ColorNoAlpha] _SColor ("Shadow Color", Color) = (0.2,0.2,0.2,1)
		[MainTexture] _BaseMap ("Albedo", 2D) = "white" {}
		_Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
		[TCP2Separator]

		[TCP2Header(Ramp Shading)]
		
		_RampThreshold ("Threshold", Range(0.01,1)) = 0.5
		_RampSmoothing ("Smoothing", Range(0.001,1)) = 0.5
		[TCP2Separator]

		[TCP2HeaderHelp(Emission)]
		[Toggle(DAMAGE_PULSE)] _DamagePulse("Enable Damage Pulse", Float) = 0
		[TCP2ColorNoAlpha] [HDR] _Emission ("Emission Color", Color) = (0,0,0,1)
		_DamageTexture("Damage Texture", 2D) = "white" {}
		_PulseSpeed("Pulse Speed", Range(0,10)) = 0
		_MinPulseEmission("Min Emission", Range(0, 1)) = 0
		_MaxPulseEmission("Max Emission", Range(0, 1)) = 0
		[TCP2Separator]
		
		[TCP2HeaderHelp(Vertex Displacement)]
		[Toggle(TCP2_VERTEX_DISPLACEMENT)] _UseVertexDisplacement ("Enable Vertex Displacement", Float) = 0
		_DisplacementTex ("Displacement Texture", 2D) = "black" {}
		 _DisplacementStrength ("Displacement Strength", Range(-1,1)) = 0.01
		[TCP2Separator]
		
		[TCP2HeaderHelp(Normal Mapping)]
		[Toggle(_NORMALMAP)] _UseNormalMap ("Enable Normal Mapping", Float) = 0
		[NoScaleOffset] _BumpMap ("Normal Map", 2D) = "bump" {}
		_BumpScale ("Scale", Float) = 1
		[TCP2Separator]
		
		[TCP2HeaderHelp(Dissolve)]
		[Toggle(TCP2_DISSOLVE)] _UseDissolve ("Enable Dissolve", Float) = 0
		[NoScaleOffset] _DissolveMap ("Map", 2D) = "gray" {}
		_DissolveAmount ("Amount", Range(0,1)) = 0.5
		_DissolveWidth ("Width", Range(0, 0.2)) = 0.05
		[TCP2Separator]
		
		[ToggleOff(_RECEIVE_SHADOWS_OFF)] _ReceiveShadowsOff("Receive Shadows", Float) = 1

			// Avoid compile error if the properties are ending with a drawer
			[HideInInspector] __dummy__("unused", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"RenderPipeline" = "UniversalPipeline"
				"RenderType" = "TransparentCutout"
				"Queue" = "AlphaTest"
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
			#include "SimpleNoise.hlsl"
			// Uniforms

			// Shader Properties
		TCP2_TEX2D_WITH_SAMPLER(_DamageTexture);
		TCP2_TEX2D_WITH_SAMPLER(_DisplacementTex);
		TCP2_TEX2D_WITH_SAMPLER(_BumpMap);
		TCP2_TEX2D_WITH_SAMPLER(_BaseMap);
		TCP2_TEX2D_WITH_SAMPLER(_DissolveMap);

		CBUFFER_START(UnityPerMaterial)
			
			// Shader Properties
			float4 _DisplacementTex_ST;
			float _DisplacementStrength;
			float _BumpScale;
			float4 _BaseMap_ST;
			float _DissolveAmount;
			float _DissolveWidth;
			float _Cutoff;
			fixed4 _BaseColor;
			half4 _Emission;
			float4 _DamageTexture_ST;
			float _PulseSpeed;
			float _MinPulseEmission;
			float _MaxPulseEmission;
			float _RampThreshold;
			float _RampSmoothing;
			fixed4 _SColor;
			fixed4 _HColor;
		CBUFFER_END

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
			#pragma multi_compile _ TCP2_DISSOLVE DAMAGE_PULSE

			// -------------------------------------

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			#pragma vertex Vertex
			#pragma fragment Fragment

			//--------------------------------------
			// Toony Colors Pro 2 keywords
			#pragma shader_feature_local_vertex TCP2_VERTEX_DISPLACEMENT
			#pragma shader_feature_local _NORMALMAP
			#pragma shader_feature_local_fragment TCP2_DISSOLVE
			#pragma shader_feature_local_fragment DAMAGE_PULSE

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
				float3 pack0 : TEXCOORD3; /* pack0.xyz = tangent */
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
				output.pack2.xy.xy = input.texcoord0.xy * _BaseMap_ST.xy + _BaseMap_ST.zw;
				
				// Shader Properties Sampling
				float3 __vertexDisplacement = ( input.normal.xyz * TCP2_TEX2D_SAMPLE_LOD(_DisplacementTex, _DisplacementTex, output.pack2.xy * _DisplacementTex_ST.xy + _DisplacementTex_ST.zw, 0).rgb * _DisplacementStrength );

				#if defined(TCP2_VERTEX_DISPLACEMENT)
				input.vertex.xyz += __vertexDisplacement;
				#endif
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

				return output;
			}

			half4 Fragment(Varyings input
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

				float3 positionWS = input.worldPosAndFog.xyz;
				float3 normalWS = normalize(input.normal);
				half3 tangentWS = input.pack0.xyz;
				half3 bitangentWS = input.pack1.xyz;
				#if defined(_NORMALMAP)
				half3x3 tangentToWorldMatrix = half3x3(tangentWS.xyz, bitangentWS.xyz, normalWS.xyz);
				#endif

				// Shader Properties Sampling
				float4 __normalMap = ( TCP2_TEX2D_SAMPLE(_BumpMap, _BumpMap, input.pack2.xy).rgba );
				float __bumpScale = ( _BumpScale );
				float4 __albedo = ( TCP2_TEX2D_SAMPLE(_BaseMap, _BaseMap, input.pack2.xy).rgba );
				float4 __mainColor = ( _BaseColor.rgba );
				float __alpha = ( __albedo.a * __mainColor.a );
				float __dissolveValue = ( _DissolveAmount );
				float __cutoff = ( _Cutoff );
				float __ambientIntensity = ( 1.0 );
				float3 __emission = ( _Emission.rgb );
				float4 __damageSample = ( TCP2_TEX2D_SAMPLE(_DamageTexture, _DamageTexture, input.pack2.xy * _DamageTexture_ST.xy + _DamageTexture_ST.zw) );
				float __dissolveMap = ( TCP2_TEX2D_SAMPLE(_DamageTexture, _DamageTexture, input.pack2.xy * _DamageTexture_ST.xy + _DamageTexture_ST.zw).a );
				float __rampThreshold = ( _RampThreshold );
				float __rampSmoothing = ( _RampSmoothing );
				float3 __shadowColor = ( _SColor.rgb );
				float3 __highlightColor = ( _HColor.rgb );

				#if defined(_NORMALMAP)
				half4 normalMap = __normalMap;
				half3 normalTS = UnpackNormalScale(normalMap, __bumpScale);
					#if defined(_NORMALMAP)
				normalWS = normalize( mul(normalTS, tangentToWorldMatrix) );
					#endif
				#endif

				// main texture
				half3 albedo = __albedo.rgb;
				half alpha = __alpha;

				half3 emission = half3(0, 0, 0);

				//Dissolve
#if defined(TCP2_DISSOLVE)
					float noiseVal = Unity_SimpleNoise_float(input.pack2.xy, 35.0f);
					half stepIn = _DissolveAmount + _DissolveWidth;
					half stepOut = step(noiseVal, stepIn);
					emission = stepOut * __emission;
					alpha = noiseVal;
					clip(alpha - _DissolveAmount * 1.001);
#elif defined(DAMAGE_PULSE)
#pragma region Damage_Pulse
					float pulseValue = sin(_Time.y * _PulseSpeed);
					float remappedPulseValue = Unity_Remap_float(pulseValue, float2(-1, 1), float2(_MinPulseEmission, _MaxPulseEmission));
					emission = (__emission * remappedPulseValue * __damageSample.a);
#pragma endregion Damage_Pulse
#endif
				//// Alpha Testing
				//half cutoffValue = __cutoff;
				//clip(alpha - cutoffValue);
				
				albedo *= __mainColor.rgb;

				////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				
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
				//emission += __emission;

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
				float4 texcoord0 : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct Varyings
			{
				float4 positionCS     : SV_POSITION;
				float2 pack0 : TEXCOORD1; /* pack0.xy = texcoord0 */
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

				// Texture Coordinates
				output.pack0.xy.xy = input.texcoord0.xy * _BaseMap_ST.xy + _BaseMap_ST.zw;
				// Shader Properties Sampling
				float3 __vertexDisplacement = ( input.normal.xyz * TCP2_TEX2D_SAMPLE_LOD(_DisplacementTex, _DisplacementTex, output.pack0.xy * _DisplacementTex_ST.xy + _DisplacementTex_ST.zw, 0).rgb * _DisplacementStrength );

				#if defined(TCP2_VERTEX_DISPLACEMENT)
				input.vertex.xyz += __vertexDisplacement;
				#endif

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

				// Shader Properties Sampling
				float4 __albedo = ( TCP2_TEX2D_SAMPLE(_BaseMap, _BaseMap, input.pack0.xy).rgba );
				float4 __mainColor = ( _BaseColor.rgba );
				float __alpha = ( __albedo.a * __mainColor.a );
				float __dissolveMap = ( TCP2_TEX2D_SAMPLE(_DissolveMap, _DissolveMap, input.pack0.xy).r );
				float __dissolveValue = ( _DissolveAmount );
				float __cutoff = ( _Cutoff );

				half3 albedo = half3(1,1,1);
				half alpha = __alpha;
				half3 emission = half3(0,0,0);
				
				//Dissolve
				#if defined(TCP2_DISSOLVE)
				half dissolveMap = __dissolveMap;
				half dissolveValue = __dissolveValue;
				float dissValue = dissolveValue;
				clip(dissolveMap - dissValue * 1.001);
				#endif
				// Alpha Testing
				half cutoffValue = __cutoff;
				clip(alpha - cutoffValue);

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

			//--------------------------------------
			// Toony Colors Pro 2 keywords
			#pragma shader_feature_local_fragment TCP2_DISSOLVE
			#pragma shader_feature_local_fragment DAMAGE_PULSE
			#pragma shader_feature_local_vertex TCP2_VERTEX_DISPLACEMENT

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

			//--------------------------------------
			// Toony Colors Pro 2 keywords
			#pragma shader_feature_local_fragment TCP2_DISSOLVE
			#pragma shader_feature_local_fragment DAMAGE_PULSE
			#pragma shader_feature_local_vertex TCP2_VERTEX_DISPLACEMENT

			ENDHLSL
		}

	}

	FallBack "Hidden/InternalErrorShader"
	CustomEditor "ToonyColorsPro.ShaderGenerator.MaterialInspector_SG2"
}
