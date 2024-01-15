Shader "BarracaShaders/notificacaoViraCAmera"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
 
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        LOD 100
 
        Pass
        {
            Cull Back // Ativa o culling para as faces normais
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata_t
            {
                float4 vertex : POSITION;
            };
 
            struct v2f
            {
                float4 pos : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
 
            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.vertex = o.pos;
                return o;
            }
 
            half4 frag (v2f i) : SV_Target
            {
                half2 uv = i.pos.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                half4 col = tex2D(_MainTex, uv);
 
                // Verifique se a direção da câmera é voltada para dentro do objeto
                if (dot(normalize(i.vertex.xyz), normalize(_WorldSpaceCameraPos - i.vertex.xyz)) > 0)
                {
                    return col; // A textura é visível se a câmera estiver olhando para dentro
                }
                else
                {
                    return half4(0, 0, 0, 0); // Torna a textura transparente se a câmera estiver olhando para fora
                }
            }
            ENDCG
        }
    }
}