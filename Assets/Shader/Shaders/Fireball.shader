// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Fireball"
{
	Properties
	{
		_Flame("Flame", 2D) = "white" {}
		_Move("Move", Vector) = (1,0,0,0)
		_Flowmap("Flowmap", 2D) = "white" {}
		_Map("Map", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Flame;
		uniform float2 _Move;
		uniform sampler2D _Flowmap;
		uniform float4 _Flowmap_ST;
		uniform float _Map;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Flowmap = i.uv_texcoord * _Flowmap_ST.xy + _Flowmap_ST.zw;
			float4 lerpResult10 = lerp( float4( i.uv_texcoord, 0.0 , 0.0 ) , tex2D( _Flowmap, uv_Flowmap ) , _Map);
			float2 panner2 = ( 1.0 * _Time.y * _Move + lerpResult10.rg);
			float4 tex2DNode1 = tex2D( _Flame, panner2 );
			o.Albedo = tex2DNode1.rgb;
			o.Emission = saturate( ( tex2DNode1 + -0.777148 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
0;462;1398;508;1507.055;-115.2245;1.231666;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-1184.041,-208.4868;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-1282.84,87.91331;Float;False;Property;_Map;Map;3;0;Create;True;0;0;False;0;0;0.1078198;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;8;-1365.855,-90.37281;Float;True;Property;_Flowmap;Flowmap;2;0;Create;True;0;0;False;0;f3daec234e2bf9e47a80d42ace2a3a4e;f3daec234e2bf9e47a80d42ace2a3a4e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;6;-956.6988,131.0657;Float;False;Property;_Move;Move;1;0;Create;True;0;0;False;0;1,0;0.1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.LerpOp;10;-869.4398,-58.98668;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;2;-716.0685,-7.574371;Float;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-700.6345,325.4583;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;-0.777148;0;-1;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-478.5515,-12.54928;Float;True;Property;_Flame;Flame;0;0;Create;True;0;0;False;0;6a9fe2bd3a5e2274b802c7f0acd844e8;6a9fe2bd3a5e2274b802c7f0acd844e8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-401.626,244.7522;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;17;-192.5849,195.7998;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Fireball;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;9;0
WireConnection;10;1;8;0
WireConnection;10;2;11;0
WireConnection;2;0;10;0
WireConnection;2;2;6;0
WireConnection;1;1;2;0
WireConnection;14;0;1;0
WireConnection;14;1;15;0
WireConnection;17;0;14;0
WireConnection;0;0;1;0
WireConnection;0;2;17;0
ASEEND*/
//CHKSM=708CA9894BFCE236DB3049B6879823AFB8453E66