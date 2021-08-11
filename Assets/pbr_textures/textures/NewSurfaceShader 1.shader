   // Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Grid"  
{  
    Properties  
    {  
          _gridColor("网格的颜色",Color) = (0.5,0.5,0.5)  
        _tickWidth("网格的间距",Range(0.01,1)) = 0.1  
        _gridWidth("网格的宽度",Range(0.0001,0.01)) = 0.008      
    }  
    SubShader  
    {  
        //选取Alpha混合方式
        //Blend  SrcAlpha SrcAlpha
        //去掉遮挡和深度缓冲  
        //  Cull Off
        // ZWrite Off  
        //开启深度测试  
        // ZTest Always  
        CGINCLUDE  

    ENDCG  

    Pass  
    { 
        CGPROGRAM  
        //敲代码的时候要注意：“CGPROGRAM”和“#pragma...”中的拼写不同,真不知道“pragma”是什么单词  
        #pragma vertex vert  
        #pragma fragment frag  

        #include "UnityCG.cginc"  

        uniform float4 _backgroundColor;  
         uniform float4 _gridColor;  
        uniform float _tickWidth;  
        uniform float _gridWidth;  


        struct appdata  
        {  
            float4 vertex:POSITION;  
            float2 uv:TEXCOORD0;  
        };  
        struct v2f  
        {  
            float2 uv:TEXCOORD0;  
            float4 vertex:SV_POSITION;  
        };  
        v2f vert(appdata v)  
        {  
            v2f o;  
            o.vertex = UnityObjectToClipPos(v.vertex);  
            o.uv = v.uv;  
            return o;  
        }  

        float4 frag(v2f i) :COLOR
        {  
            //将坐标的中心从左下角移动到网格的中心  
            float2 r = 2.0*(i.uv - 0.5);  
            float aspectRatio = _ScreenParams.x / _ScreenParams.y;  

            float4 backgroundColor = _backgroundColor;

            float4 gridColor = _gridColor;

            float4 pixel = backgroundColor;
            float a = 0;
            //定义网格的的间距  
            const float tickWidth = _tickWidth;  
            if (fmod(r.x, tickWidth) < _gridWidth)  
            {  
                pixel = gridColor;  

            }  

            if (mod(r.y, tickWidth) < _gridWidth)  
            {  
                pixel = gridColor;  

            }  

            if (abs(pixel.x) == backgroundColor.x
                && abs(pixel.y) == backgroundColor.y
                && abs(pixel.z) == backgroundColor.z
                && abs(pixel.w) == backgroundColor.w
                )
            {
                discard;
            }

            return pixel;


        }  
        ENDCG  
    }  

}  

}  
