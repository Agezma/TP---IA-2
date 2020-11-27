// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Path"
{
	Properties
	{
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 2
		_Map("Map", 2D) = "white" {}
		_TexturesCom_FloorStreets0083_1_seamless_S("TexturesCom_FloorStreets0083_1_seamless_S", 2D) = "white" {}
		_Grass("Grass", 2D) = "white" {}
		_Water("Water", 2D) = "white" {}
		_Vector0("Vector 0", Vector) = (0,-4,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Map;
		uniform float4 _Map_ST;
		uniform float3 _Vector0;
		uniform sampler2D _TexturesCom_FloorStreets0083_1_seamless_S;
		uniform float4 _TexturesCom_FloorStreets0083_1_seamless_S_ST;
		uniform sampler2D _Grass;
		uniform float4 _Grass_ST;
		uniform sampler2D _Water;
		uniform float4 _Water_ST;
		uniform float _EdgeLength;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float2 uv_Map = v.texcoord * _Map_ST.xy + _Map_ST.zw;
			float4 tex2DNode9 = tex2Dlod( _Map, float4( uv_Map, 0, 0.0) );
			v.vertex.xyz += ( tex2DNode9.b * _Vector0 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexturesCom_FloorStreets0083_1_seamless_S = i.uv_texcoord * _TexturesCom_FloorStreets0083_1_seamless_S_ST.xy + _TexturesCom_FloorStreets0083_1_seamless_S_ST.zw;
			float2 uv_Map = i.uv_texcoord * _Map_ST.xy + _Map_ST.zw;
			float4 tex2DNode9 = tex2D( _Map, uv_Map );
			float2 uv_Grass = i.uv_texcoord * _Grass_ST.xy + _Grass_ST.zw;
			float2 uv_Water = i.uv_texcoord * _Water_ST.xy + _Water_ST.zw;
			o.Albedo = ( ( tex2D( _TexturesCom_FloorStreets0083_1_seamless_S, uv_TexturesCom_FloorStreets0083_1_seamless_S ) * tex2DNode9.g ) + ( tex2DNode9.r * tex2D( _Grass, uv_Grass ) ) + ( tex2DNode9.b * tex2D( _Water, uv_Water ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
0;506;1383;464;686.4216;-212.5343;1.129385;True;False
Node;AmplifyShaderEditor.SamplerNode;9;-1183.42,93.25341;Float;True;Property;_Map;Map;5;0;Create;True;0;0;False;0;4bc64fb6c1a7f4540bd1127d044385ee;4bc64fb6c1a7f4540bd1127d044385ee;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;13;-1217.983,-146.9548;Float;True;Property;_TexturesCom_FloorStreets0083_1_seamless_S;TexturesCom_FloorStreets0083_1_seamless_S;6;0;Create;True;0;0;False;0;334749de5047ad341bc1db943be3a98c;334749de5047ad341bc1db943be3a98c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;14;-1204.157,331.7333;Float;True;Property;_Grass;Grass;7;0;Create;True;0;0;False;0;09fccae9f7ea18545a6fa0061cb4814b;09fccae9f7ea18545a6fa0061cb4814b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;15;-1026.162,590.9507;Float;True;Property;_Water;Water;8;0;Create;True;0;0;False;0;f30f74eefca2ed34a9678ed4af15d301;f30f74eefca2ed34a9678ed4af15d301;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-678.8105,247.0557;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-639.0638,423.3234;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-599.3173,-79.55811;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;18;-371.3835,564.9641;Float;False;Property;_Vector0;Vector 0;9;0;Create;True;0;0;False;0;0,-4,0;0,-4,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-357.3812,169.2905;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-28.76686,475.6909;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;373.2729,176.2678;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;Path;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;2;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;0;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;11;0;9;1
WireConnection;11;1;14;0
WireConnection;12;0;9;3
WireConnection;12;1;15;0
WireConnection;10;0;13;0
WireConnection;10;1;9;2
WireConnection;16;0;10;0
WireConnection;16;1;11;0
WireConnection;16;2;12;0
WireConnection;17;0;9;3
WireConnection;17;1;18;0
WireConnection;0;0;16;0
WireConnection;0;11;17;0
ASEEND*/
//CHKSM=2565E2AC888B202BE4F666D171710C359BC43CBC