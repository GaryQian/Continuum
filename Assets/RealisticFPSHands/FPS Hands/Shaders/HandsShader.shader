// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Knife/HandsShader"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Albedo("Albedo", 2D) = "white" {}
		_Tint("Tint", Color) = (1,1,1,1)
		_Normals("Normals", 2D) = "bump" {}
		_NormalScale("NormalScale", Range( 0 , 2)) = 1
		_Specular("Specular", 2D) = "gray" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_AO("AO", 2D) = "white" {}
		_BloodMaskNoise("BloodMaskNoise", 2D) = "white" {}
		_MaskPower("MaskPower", Range( 0 , 15)) = 1
		_Bloody("Bloody", Range( 0 , 1)) = 0
		_BloodAlbedo("BloodAlbedo", 2D) = "white" {}
		_BloodColor("BloodColor", Color) = (0,0,0,0)
		_BloodNormals("BloodNormals", 2D) = "bump" {}
		_BloodNormalScale("BloodNormalScale", Range( 0 , 2)) = 0
		_BloodSpecular("BloodSpecular", 2D) = "white" {}
		_BloodySmoothness("BloodySmoothness", Range( 0 , 1)) = 0
		_BloodAO("BloodAO", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.5
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _NormalScale;
		uniform sampler2D _Normals;
		uniform float4 _Normals_ST;
		uniform float _BloodNormalScale;
		uniform sampler2D _BloodNormals;
		uniform float4 _BloodNormals_ST;
		uniform sampler2D _BloodMaskNoise;
		uniform float4 _BloodMaskNoise_ST;
		uniform float _MaskPower;
		uniform float _Bloody;
		uniform float4 _Tint;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _BloodColor;
		uniform sampler2D _BloodAlbedo;
		uniform float4 _BloodAlbedo_ST;
		uniform sampler2D _Specular;
		uniform float4 _Specular_ST;
		uniform sampler2D _BloodSpecular;
		uniform float4 _BloodSpecular_ST;
		uniform float _Smoothness;
		uniform float _BloodySmoothness;
		uniform sampler2D _AO;
		uniform float4 _AO_ST;
		uniform sampler2D _BloodAO;
		uniform float4 _BloodAO_ST;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 uv_Normals = i.uv_texcoord * _Normals_ST.xy + _Normals_ST.zw;
			float2 uv_BloodNormals = i.uv_texcoord * _BloodNormals_ST.xy + _BloodNormals_ST.zw;
			float2 uv_BloodMaskNoise = i.uv_texcoord * _BloodMaskNoise_ST.xy + _BloodMaskNoise_ST.zw;
			float lerpResult26 = lerp( 0.0 , pow( tex2D( _BloodMaskNoise, uv_BloodMaskNoise ).r , _MaskPower ) , _Bloody);
			float BloodyFraction25 = lerpResult26;
			float3 lerpResult30 = lerp( UnpackScaleNormal( tex2D( _Normals, uv_Normals ) ,_NormalScale ) , UnpackScaleNormal( tex2D( _BloodNormals, uv_BloodNormals ) ,_BloodNormalScale ) , BloodyFraction25);
			o.Normal = lerpResult30;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float2 uv_BloodAlbedo = i.uv_texcoord * _BloodAlbedo_ST.xy + _BloodAlbedo_ST.zw;
			float4 lerpResult15 = lerp( ( _Tint * tex2D( _Albedo, uv_Albedo ) ) , ( _BloodColor * tex2D( _BloodAlbedo, uv_BloodAlbedo ) ) , BloodyFraction25);
			o.Albedo = lerpResult15.rgb;
			float2 uv_Specular = i.uv_texcoord * _Specular_ST.xy + _Specular_ST.zw;
			float4 tex2DNode5 = tex2D( _Specular, uv_Specular );
			float2 uv_BloodSpecular = i.uv_texcoord * _BloodSpecular_ST.xy + _BloodSpecular_ST.zw;
			float4 tex2DNode36 = tex2D( _BloodSpecular, uv_BloodSpecular );
			float4 lerpResult32 = lerp( tex2DNode5 , tex2DNode36 , BloodyFraction25);
			o.Specular = lerpResult32.rgb;
			float lerpResult37 = lerp( tex2DNode5.a , tex2DNode36.a , BloodyFraction25);
			float lerpResult24 = lerp( _Smoothness , _BloodySmoothness , BloodyFraction25);
			o.Smoothness = ( lerpResult37 * lerpResult24 );
			float2 uv_AO = i.uv_texcoord * _AO_ST.xy + _AO_ST.zw;
			float2 uv_BloodAO = i.uv_texcoord * _BloodAO_ST.xy + _BloodAO_ST.zw;
			float lerpResult34 = lerp( tex2D( _AO, uv_AO ).r , tex2D( _BloodAO, uv_BloodAO ).r , BloodyFraction25);
			o.Occlusion = lerpResult34;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13201
7;29;1844;1044;1463.191;353.369;1.3;True;True
Node;AmplifyShaderEditor.SamplerNode;16;-815.0402,-851.8306;Float;True;Property;_BloodMaskNoise;BloodMaskNoise;7;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;20;-590.04,-504.8306;Float;False;Property;_MaskPower;MaskPower;8;0;1;0;15;0;1;FLOAT
Node;AmplifyShaderEditor.PowerNode;19;-203.94,-656.8306;Float;False;2;0;FLOAT;0,0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;27;-377.0977,-603.6608;Float;False;Constant;_Float0;Float 0;12;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;12;-307.0402,-517.8306;Float;False;Property;_Bloody;Bloody;9;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;26;-6.097702,-601.1608;Float;False;3;0;FLOAT;0,0;False;1;FLOAT;0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-730,-87;Float;True;Property;_Albedo;Albedo;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;9;-822.9,-285.8;Float;False;Property;_Tint;Tint;1;0;1,1,1,1;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.GetLocalVarNode;28;-377.6977,-47.76081;Float;False;25;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;11;-935.6824,-497.8598;Float;True;Property;_BloodAlbedo;BloodAlbedo;10;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RegisterLocalVarNode;25;231.9023,-757.1608;Float;False;BloodyFraction;-1;True;1;0;FLOAT;0,0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;36;-565.5273,875.1205;Float;True;Property;_BloodSpecular;BloodSpecular;14;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;23;-907.04,595.1694;Float;False;Property;_BloodySmoothness;BloodySmoothness;15;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;31;-549.5273,183.1205;Float;False;Property;_BloodNormalScale;BloodNormalScale;13;0;0;0;2;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;14;-923.0402,-666.8306;Float;False;Property;_BloodColor;BloodColor;11;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;5;-681,297;Float;True;Property;_Specular;Specular;4;0;None;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;8;-927,515;Float;False;Property;_Smoothness;Smoothness;5;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;6;-959,232;Float;False;Property;_NormalScale;NormalScale;3;0;1;0;2;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-483,-177;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;24;-247.1977,511.3392;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;33;-700.5273,659.1205;Float;True;Property;_AO;AO;6;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;29;-280.5273,182.1205;Float;True;Property;_BloodNormals;BloodNormals;12;0;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;35;-312.5273,703.1205;Float;True;Property;_BloodAO;BloodAO;16;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;37;63.47266,352.1205;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;4;-663,104;Float;True;Property;_Normals;Normals;2;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-496.0402,-309.8306;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;234.4727,337.1205;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;34;40.47266,481.1205;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;15;-249.0402,-310.8306;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;32;68.47266,207.1205;Float;False;3;0;COLOR;0.0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;30;33.47266,-15.87955;Float;False;3;0;FLOAT3;0.0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0.0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;496,9;Float;False;True;3;Float;ASEMaterialInspector;0;0;StandardSpecular;Knife/HandsShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;0;16;1
WireConnection;19;1;20;0
WireConnection;26;0;27;0
WireConnection;26;1;19;0
WireConnection;26;2;12;0
WireConnection;25;0;26;0
WireConnection;10;0;9;0
WireConnection;10;1;1;0
WireConnection;24;0;8;0
WireConnection;24;1;23;0
WireConnection;24;2;28;0
WireConnection;29;5;31;0
WireConnection;37;0;5;4
WireConnection;37;1;36;4
WireConnection;37;2;28;0
WireConnection;4;5;6;0
WireConnection;17;0;14;0
WireConnection;17;1;11;0
WireConnection;38;0;37;0
WireConnection;38;1;24;0
WireConnection;34;0;33;1
WireConnection;34;1;35;1
WireConnection;34;2;28;0
WireConnection;15;0;10;0
WireConnection;15;1;17;0
WireConnection;15;2;28;0
WireConnection;32;0;5;0
WireConnection;32;1;36;0
WireConnection;32;2;28;0
WireConnection;30;0;4;0
WireConnection;30;1;29;0
WireConnection;30;2;28;0
WireConnection;0;0;15;0
WireConnection;0;1;30;0
WireConnection;0;3;32;0
WireConnection;0;4;38;0
WireConnection;0;5;34;0
ASEEND*/
//CHKSM=01E1906FEF9AA74B88638BBC20D3A5AB7F75EAB8