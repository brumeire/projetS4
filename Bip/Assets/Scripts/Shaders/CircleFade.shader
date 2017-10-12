Shader "Customs/CircleFade"
{ 
Properties
{
_MainTex ("Base (RGB)", 2D) = "white" {}
_Color ("_Color", Color) = (1,1,1,1)
_Offset ("Offset", Range(-1,1)) = 0.5
_CenterSize("CenterSize", Range(-1,1)) = 0.5
_Space1("_Space1", Range(0,1)) = 0
_Width1("_Width1", Range(0,1)) = 0
_Space2("_Space2", Range(0,1)) = 0
_Width2("_Width2", Range(0,1)) = 0
_InOut ("InOut", Range(0,1)) = 0.5
_Alpha ("Alpha", Range (0,1)) = 1.0

// required for UI.Mask
/*_StencilComp ("Stencil Comparison", Float) = 8
_Stencil ("Stencil ID", Float) = 0
_StencilOp ("Stencil Operation", Float) = 0
_StencilWriteMask ("Stencil Write Mask", Float) = 255
_StencilReadMask ("Stencil Read Mask", Float) = 255
_ColorMask ("Color Mask", Float) = 15
*/
}

SubShader
{
Tags {"Queue"="Transparent" "IgnoreProjector"="true" "RenderType"="Transparent"}
ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off
// required for UI.Mask
/*Stencil
{
Ref [_Stencil]
Comp [_StencilComp]
Pass [_StencilOp] 
ReadMask [_StencilReadMask]
WriteMask [_StencilWriteMask]
}*/

Pass
{

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

struct appdata_t
{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
half2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
fixed4 color    : COLOR;
};

sampler2D _MainTex;
fixed4 _Color;
float _Offset;
float _CenterSize;
float _Space1;
float _Width1;
float _Space2;
float _Width2;
float _InOut;
fixed _Alpha;


v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}

float4 frag (v2f i) : COLOR
{

float2 uv = i.texcoord.xy;
float4 tex = tex2D(_MainTex, uv)*i.color;
float alpha = tex.a;
float range = _Offset + _Space1;
float widthCircle = _Width1 + range;
float2 center = float2(0.5,0.5);
float dist = 1.0;

if (length(center - uv) < _CenterSize)
{
	dist = 0;
}
else if (length(center - uv) < widthCircle) 
{
	dist = dist - step(_Offset, length(center - uv)) + step(range, length(center - uv)) - step(widthCircle, length(center - uv));
}
else
{
	dist = dist - step(_Offset, length(center - uv)) + step(range + _Space2*2, length(center - uv)) - step(widthCircle + _Width2*2, length(center - uv));
}
float c;
if (_InOut==0) { c = dist; } else { c= 1-dist; }
tex.a = alpha*c-_Alpha;
return tex;
}
ENDCG
}
}
Fallback "Sprites/Default"

}




























/*{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Tint", Color) = (1,1,1,1)
		_Offset("Offset", Range(-1,1)) = 0.5
		_Space("_Space", Range(0,1)) = 0
		_Width("_Width", Range(0,1)) = 0
		_InOut("InOut", Range(0,1)) = 0.5
		_SrcBlend("_SrcBlend", Float) = 0
		_DstBlend("_DstBlend", Float) = 0
		_BlendOp("_BlendOp",Float) = 0
		_Z("_Z", Float) = 0
	}

		SubShader
	{
		Tags
	{
		"IgnoreProjector" = "True"
		"RenderType" = "TransparentCutout"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}
		Cull Off
		Lighting Off
		ZWrite[_Z]
		BlendOp[_BlendOp]
		Blend[_SrcBlend][_DstBlend]

		CGPROGRAM
#pragma surface surf Lambert vertex:vert nofog keepalpha addshadow fullforwardshadows


		sampler2D _MainTex;
	fixed4 _Color;
	float _Offset;
	float _Space;
	float _Width;
	float _InOut;
	float _Alpha;

	struct Input
	{
		float2 uv_MainTex;
		fixed4 color;
	};

	void vert(inout appdata_full v, out Input o)
	{
		v.vertex = UnityPixelSnap(v.vertex);
		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.color = v.color * _Color;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{

		float2 uv = IN.uv_MainTex;
		float4 tex = tex2D(_MainTex, uv)* IN.color;
		float alpha = tex.a;
		float range = _Offset + _Space;
		float widthCircle = _Width + range;
		float2 center = float2(0.5,0.5);



		//float dist = 1.0 - step(_Offset, length(center - uv)) + step(range, length(center - uv)) - step(widthCircle, length(center - uv));
		//if (dist >= 1) {dist = 1.0 - smoothstep(_Offset, _Offset + 0.15, length(center - uv));}
		float dist = 1.0 - smoothstep(_Offset, _Offset + 0.15, length(center - uv));
		//float dist = 2;


		float c = 0;
		if (_InOut == 0) { c = dist; }
		else { c = 1 - dist; }
		tex.a = alpha*c;

		o.Albedo = tex.rgb * tex.a;
		o.Alpha = tex.a;

		clip(o.Alpha - 0.05);
	}
	ENDCG
	}

		Fallback "Transparent/VertexLit"
}*/