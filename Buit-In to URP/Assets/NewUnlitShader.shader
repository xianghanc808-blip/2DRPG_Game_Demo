Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        [Header(Texture)]
        _BaseMap ("Base Map", 2D) = "white"{}
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalRenderPipeline"
            "RenderType" = "Opaque"
        }

        HLSLINCLUDE
            //预处理指令
            #pragma multi_compile _MAIN_LIGHT_SHADOWS // 主光源阴影
            #pragma multi_compile _MAIN_LIGHT_SHADOWS_CASCADE // 主光源阴影级联
            #pragma multi_compile _MAIN_LIGHT_SHADOWS_SCREEN // 主光源阴影屏幕空间

            #pragma multi_compile_fragment _LIGHT_LAYERS // 光照层
            #pragma multi_compile_fragment _LIGHT_COOKIES // 光照饼干
            #pragma multi_compile_fragment _SCREEN_SPACE_OCCLUSION // 屏幕空间遮挡
            #pragma multi_compile_fragment _ADDITIONAL_LIGHT_SHADOWS // 额外光源阴影
            #pragma multi_compile_fragment _SHADOWS_SOFT // 阴影软化
            #pragma multi_compile_fragment _REFLECTION_PROBE_BLENDING // 反射探针混合
            #pragma multi_compile_fragment _REFLECTION_PROBE_BOX_PROJECTION // 反射探针盒投影

            //头文件
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl" // 核心库
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl" // 光照库

            //常量定义
            CBUFFER_START(UnityPerMaterial)
                sampler2D _BaseMap;
            CBUFFER_END

        ENDHLSL

        pass
        {
            Name "UniversalForward"
            Tags
            {
                "LightMode" = "UniversalForward"
            }

            HLSLPROGRAM
                
                #pragma vertex MainVertexShader
                #pragma fragment MainFragmentShader

                void MainVertexShader(float4 positionObjectSpace : POSITION)//返回裁剪空间坐标
                {
                    GetVertexPositionInputs(positionObjectSpace.xyz);
                }

                void MainFragmentShader()//返回颜色
                {
                    //return float4 (1,1,1,1);
                }

            ENDHLSL
        }
    }
}
