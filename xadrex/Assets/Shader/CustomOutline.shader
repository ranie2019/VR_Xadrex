Shader "Custom/Outline"
{
    Properties
    {
        _OutlineColor ("Cor do Contorno", Color) = (1,1,0,1)
        _Outline ("Largura do Contorno", Range (0.002, 0.03)) = 0.005
    }

    SubShader
    {
        Tags {"Queue"="Overlay" }  // Define a ordem de renderização
        Blend SrcAlpha OneMinusSrcAlpha  // Configuração de blend para transparência

        Cull Front  // Cull Front para evitar renderização do interior do objeto

        ZWrite On  // Ativa a escrita no buffer de profundidade
        ZTest LEqual  // Configuração de teste de profundidade

        ColorMask RGB  // Máscara de cor para RGB

        BlendOp Add  // Operação de blend para adição
        Blend SrcAlpha OneMinusSrcAlpha  // Configuração de blend para transparência

        Pass
        {
            Name "OUTLINE"

            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG
        }
    }

    SubShader
    {
        Tags {"Queue"="Overlay" }  // Define a ordem de renderização
        Blend SrcAlpha OneMinusSrcAlpha  // Configuração de blend para transparência

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            fixed4 _OutlineColor;
            fixed _Outline;

            v2f vert(appdata v)
            {
                // Faz uma cópia dos dados do vértice de entrada, mas escalada de acordo com a direção normal
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                float3 norm = mul((float3x3)unity_ObjectToWorld, normalize(v.vertex.xyz));
                float2 outline = TransformViewToProjection(norm.xy);
                o.pos.xy += outline * _Outline;

                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
}
