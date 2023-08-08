Shader "UI/Code"
{
    Properties
    {
        _Columns ("Columns", Float) = 120
        _Speed ("Speed", Float) = -20
        _Text_Color ("Text Color", Color) = (0,1,0,1)

        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend One OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #include "../../../Resources/Shaders/TrueShadow.cginc"
            #pragma vertex vert
            #pragma fragment frag_custom

            /// Adapted from https://thebookofshaders.com/examples/?chapter=08

            half random(half x)
            {
                return frac(sin(x) * 43758.5453);
            }

            half random(half2 p)
            {
                float3 p3 = frac(float3(p.xyx) * .1031);
                p3 += dot(p3, p3.yzx + 33.33);
                return frac((p3.x + p3.y) * p3.z);
            }

            half rchar(half2 outer, half2 inner)
            {
                half grid = 5.;
                half2 margin = half2(.2, .05);
                half seed = 23.184;
                half2 borders = step(margin, inner) * step(margin, 1. - inner);
                return step(.5, random(outer * seed + floor(inner * grid))) * borders.x * borders.y;
            }

            half _Columns;
            half _Speed;

            half code(half2 st)
            {
                half2 ipos = floor(st * _Columns);

                ipos += half2(.0, floor(_Time.y * _Speed * random(ipos.x)));

                half2 fpos = frac(st * _Columns);
                half2 center = .5 - fpos;

                half pct = random(ipos);
                half glow = (1. - dot(center, center) * 3.) * 2.0;

                return rchar(ipos, fpos) * pct * glow;
            }

            half4 _Text_Color;

            fixed4 frag_custom(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

                half2 resInvariantUV = IN.vertex.xy / ((_ScreenParams.x + _ScreenParams.y) / 2);
                color.rgb += lerp(half3(0,0,0), _Text_Color.rgb * _Text_Color.a, code(resInvariantUV)) * color.a;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                return color;
            }
            ENDCG
        }
    }
}
