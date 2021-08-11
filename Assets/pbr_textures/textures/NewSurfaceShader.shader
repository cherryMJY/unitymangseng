Shader "QQ/FloorGrid"
{
	Properties
	{
		_Color("Color",color) = (0.0,0.0,0.0,0.0)
		_XSpacing("x spacing",float)= 0.1
		_YSpacing("y spacing",float) = 0.1
		_Width("width",Range(0.0,0.6)) = 0.1
		_ViewSpacing("ViewSpacing",float) = 1000
	}
		SubShader
	{
		Tags { "RenderType" = "Transparent"
				"Queue" = "Transparent"}

		Pass
		{
		Zwrite Off
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

		struct appdata
		{
			float4 vertex : POSITION;
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
			float3 wPos:TEXCOORD0;
		};

		fixed4 _Color;
		float _XSpacing;
		float _YSpacing;
		float _Width;
		float _ViewSpacing;
		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.wPos = mul(unity_ObjectToWorld,v.vertex);
			return o;
		}
		float calculate(float pos,float mod,float height,float spacing,float width)
		{
			int degree = 0;
			float scale = 0;
			spacing = spacing;
			scale = height / spacing;
			while (scale > 1)
			{
				spacing = spacing * 10;
				scale = height / spacing;
				degree++;
			}
			float res = 0;
			width = width * height;
			float delta = 0.01 * height;
			for (int i = 0; i < 3; i++)
			{
				float _mod = mod * pow(10, degree + i);
				//float _res = abs(pos) % _mod;
				//if (_res > _mod * 0.5)
				//	_res = abs(_mod - _res);
				float _res = abs(pos + _mod * 0.5) % _mod;
				_res = abs(_res + _mod * 0.5 - _mod);
				_res = 1 - smoothstep(width - delta, width, _res);
				_res *= 1 - scale + i;
				res += _res;
			}
			return saturate(res);
		}
		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 col = _Color;
			float view = smoothstep(0.1,1.0, abs(dot(float3(0,1,0), normalize(_WorldSpaceCameraPos.xyz - i.wPos))));
			float height = abs(_WorldSpaceCameraPos.y);
			float x = calculate(i.wPos.x, _XSpacing, height, _ViewSpacing, _Width);
			float y = calculate(i.wPos.z, _YSpacing, height, _ViewSpacing, _Width);
			float a = max(x, y);
			col.a = a * _Color.a * view;
			return col;
		}
		ENDCG
	}
	}
}