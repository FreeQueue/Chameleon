Shader "Chameleon/Reflection" {
	Properties
    {
    //摄像机渲染到的渲染纹理
    _MainTex("Main Tex",2D)="white"{}
    }
    SubShader
    {
    Tags { "RenderType"="Opaque" "Queue"="Geometry"}
    Pass{

    CGPROGRAM

    
    #pragma vertex vert
    #pragma fragment frag

    sampler2D _MainTex;
    struct a2v{
    float4 vertex:POSITION;
    float3 texcoord:TEXCOORD0;
    };

    struct v2f{
    float2 uv:TEXCOORD0;
    float4 pos:SV_POSITION;
    };

    v2f vert(a2v i){
    v2f o;
    o.uv=i.texcoord;
    o.pos=UnityObjectToClipPos(i.vertex);
    o.uv.y=1-o.uv.y;
    return o;
    }
    fixed4 frag(v2f i):SV_Target{
    return tex2D(_MainTex,i.uv);
    }
    ENDCG
    }
    }
    FallBack Off
}
