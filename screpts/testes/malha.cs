using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class malha : MonoBehaviour
{
    private Mesh mesh_;
    private MeshFilter mf;
    private MeshRenderer mr;
    public Material mat;
    private MeshCollider mc;


    private List<Vector3> verticesMesh; // pra cada lateral criada aqui vai ter o ultimo vertice gerado    
        private List<int> triangulo; //ordem dos vertices para se criar um triangulo
    private Vector3[,,] vertices;// vertices criados com seus velores de V3 na ordem para serem usados na triangulacao
    private Vector3 pivo,inicial;
    private int comprimento, // tamanho de cada bloco
               seguimentos, //quantidade de seguimentos (quantos blocos)
               cortes; // divisoes que se tem em cada bloco

    // são baseados no comprimento
    private float max_altura, max_largura;
    private float min_altura, min_largura
        ,distanciaCorte;

    private float largura, altura;

    // perling noise
    private Vector3 perlingnoise;
    void definirlimites()
    {
        
        if(comprimento <= 3)
        {
            cortes = 3;
        }
        else
        {
            cortes = comprimento - 1;
        }

        max_largura = (comprimento * 0.75f);
        min_largura = comprimento * 0.2f;

        max_altura = comprimento * 0.9f;
        min_altura = comprimento * 0.25f;

    }
    void criar_escala()
    {
        altura = Mathf.PerlinNoise(perlingnoise.x, perlingnoise.y) * max_altura;
        largura = Mathf.PerlinNoise(perlingnoise.x, perlingnoise.z) * max_largura;

        altura += min_altura;
        largura += min_largura;

        pivo = transform.position;
        inicial = pivo - new Vector3(largura/2,altura/2, comprimento/2);
        distanciaCorte = comprimento / cortes;
    }
    void definirvertices()
    {
        // frente
        vertices[0, 0, 0] = inicial;
        vertices[0, 1, 0] = inicial + new Vector3(0, altura, 0);
        vertices[1, 0, 0] = inicial + new Vector3(largura / 2, 0, 0);

        vertices[1, 1, 0] = inicial + new Vector3(largura/2, altura, 0);
   
        vertices[2, 0, 0] = inicial + new Vector3(largura , 0, 0);
        vertices[2, 1, 0] = inicial + new Vector3(largura, altura, 0);

        // atras
        vertices[0, 0, cortes] = inicial;
        vertices[0, 1, cortes] = inicial + new Vector3(0, altura, 0);
        vertices[1, 0, cortes] = inicial + new Vector3(largura / 2, 0, 0);

        vertices[1, 1, cortes] = inicial + new Vector3(largura / 2, altura, 0);

        vertices[2, 0, cortes] = inicial + new Vector3(largura, 0, 0);
        vertices[2, 1, cortes] = inicial + new Vector3(largura, altura, 0);



        for (int i = 1; i <cortes ; i++)
        {

            // bot
            vertices[0, 0, i] = inicial;
            vertices[0, 1, i] = inicial + new Vector3(0,0,distanciaCorte );
            vertices[1,0,i] = inicial + new Vector3(largura/2,0,0);

            vertices[1,1,i] = inicial + new Vector3(largura / 2,0, distanciaCorte);
            vertices[2,0, i] = inicial + new Vector3(largura, 0, 0);
            vertices[2, 1, i] = inicial + new Vector3(largura,0, distanciaCorte);

            // top
            vertices[3, 0, i] = inicial+ new Vector3(0,altura,0);
            vertices[3, 1, i] = inicial + new Vector3(0, altura,distanciaCorte);
            vertices[4, 0, i] = inicial + new Vector3(largura / 2, altura, 0);

            vertices[4, 1, i] = inicial + new Vector3(largura / 2, altura, distanciaCorte);
            vertices[5, 0, i] = inicial + new Vector3(largura, altura, 0);
            vertices[5, 1, i] = inicial + new Vector3(largura, altura, distanciaCorte);

            // left
            vertices[6, 0, i] = inicial;
            vertices[6,1,i] = inicial + new Vector3(0,0,distanciaCorte);
            vertices[7,0,i] = inicial + new Vector3(0,altura,0);

            vertices[7,1,i] = inicial + new Vector3(0,altura,distanciaCorte);

            // right
            vertices[8, 0, i] = inicial + new Vector3(largura,0,0);
            vertices[8, 1, i] = inicial + new Vector3(largura, 0, distanciaCorte);
            vertices[9, 0, i] = inicial + new Vector3(largura, altura, 0);

            vertices[9, 1, i] = inicial + new Vector3(largura, altura, distanciaCorte);



        }

    }
    private void definirDetalhes()
    {
        //vai mudar um pouco (simetricamente) os valores de vertices
    }


    private void criararestas(){
        //
        for (int i = 0; i < 2; i++)
        {
            triangulo.Add(0 + (i * 6));
            triangulo.Add(1 + (i * 6));
            triangulo.Add(2 + (i * 6));

            triangulo.Add(2 + (i * 6));
            triangulo.Add(1 + (i * 6));
            triangulo.Add(3 + (i * 6));

            triangulo.Add(2 + (i * 6));
            triangulo.Add(3 + (i * 6));
            triangulo.Add(4 + (i * 6));

            triangulo.Add(4 + (i * 6));
            triangulo.Add(3 + (i * 6));
            triangulo.Add(5 + (i * 6));
        }


        // fretnte incia
        verticesMesh.Add(vertices[0, 0, 0]);
        verticesMesh.Add(vertices[0, 1, 0]);
        verticesMesh.Add(vertices[1, 0, 0]);

        verticesMesh.Add(vertices[1, 1, 0]);
        verticesMesh.Add(vertices[2, 0, 0]);
        verticesMesh.Add(vertices[2, 1, 0]);

        // traseira inicial
        verticesMesh.Add(vertices[0, 0, cortes]);
        verticesMesh.Add(vertices[0, 1, cortes]);
        verticesMesh.Add(vertices[1, 0, cortes]);

        verticesMesh.Add(vertices[1, 1, cortes]);
        verticesMesh.Add(vertices[2, 0, cortes]);
        verticesMesh.Add(vertices[2, 1, cortes]);




        for (int z = 1; z < cortes; z++)
        {
            for (int i = 0; i < 2; i++)
            {
                triangulo.Add(verticesMesh.Count + 1 + + 0 + (i * 6));
                triangulo.Add(verticesMesh.Count + 1 + + 1 + (i * 6));
                triangulo.Add(verticesMesh.Count + 1 + + 2 + (i * 6));

                triangulo.Add(verticesMesh.Count + 1 + + 2 + (i * 6));
                triangulo.Add(verticesMesh.Count + 1 + + 1 + (i * 6));
                triangulo.Add(verticesMesh.Count + 1 + + 3 + (i * 6));

                triangulo.Add(verticesMesh.Count + 1 + + 2 + (i * 6));
                triangulo.Add(verticesMesh.Count + 1 + + 3 + (i * 6));
                triangulo.Add(verticesMesh.Count + 1 + + 4 + (i * 6));

                triangulo.Add(verticesMesh.Count + 1 + + 4 + (i * 6));
                triangulo.Add(verticesMesh.Count + 1 + + 3 + (i * 6));
                triangulo.Add(verticesMesh.Count + 1 + + 5 + (i * 6));

              
                    verticesMesh.Add(vertices[0 + (3 * i), 0, z]);
                    verticesMesh.Add(vertices[0 + (3 * i), 1, z]);
                    verticesMesh.Add(vertices[1 + (3 * i), 0, z]);

                    verticesMesh.Add(vertices[1 + (3 * i), 1, z]);
                    verticesMesh.Add(vertices[2 + (3 * i), 0, z]);
                    verticesMesh.Add(vertices[2 + (3 * i), 1, z]);

                
            }
            for(int i = 0; i < 2; i++)
            {
                triangulo.Add(verticesMesh.Count + 1   + (i * 4));
                triangulo.Add(verticesMesh.Count + 1+1 + (i * 4));
                triangulo.Add(verticesMesh.Count + 1+2 + (i * 4));

                triangulo.Add(verticesMesh.Count + 1+2 + (i*4));
                triangulo.Add(verticesMesh.Count + 1+1 + (i * 4));
                triangulo.Add(verticesMesh.Count + 1+3 + (i * 4));

                verticesMesh.Add(vertices[6 + (i * 2), 0, z]);
                verticesMesh.Add(vertices[6 + (i * 2), 1, z]);
                verticesMesh.Add(vertices[7 + (i * 2), 0, z]);
                verticesMesh.Add(vertices[7 + (i * 2), 1, z]);

            }

        }

    }
    
    private void Start()
    {
     
        definirlimites();
        vertices = new Vector3[10, 2, cortes];
        criar_escala();
        definirvertices();
        definirDetalhes();
        criararestas();

        mesh_.vertices= verticesMesh.ToArray();
        mesh_.triangles= triangulo.ToArray ();

        mesh_.RecalculateNormals();
        mesh_.RecalculateBounds();

        if(!mf)
            mf = gameObject.AddComponent<MeshFilter>();
        mf.mesh = mesh_;

        if(!mr)
            mr = gameObject.AddComponent<MeshRenderer>();
        mr.material = mat;
        if (!mc)
            mc = gameObject.AddComponent <MeshCollider>();
        mc.sharedMesh = mesh_;
        
    }

    /* criar o primeiro e o ultimo triangulo(na distancia de primeiro+comprimento) e guardar a posicao do seus vertices
    * o eixo X é fixo em inteiro e obedece o valor maximo de comprimento-2
    * 
    * Y/Z vao seguir alguma formula senoide
    * 
    * v3 primeiro é a posicao do primeiro vertice do primeiro triangulo ele vai ciar um v3 
    *
    *--> ter a posição em v3 dos vertices a serem cirados(x,y,z)
    *
    *--> ter a ordem em int de vertices para se formar um triangulo (0,2,1,2,3,1,...)
    *--> criar a lista dos vertices na ordem a serem cirados os triangulos com o v3
    *
    *atribuir os vertices e os triangulos na malha, recalcular o normal e o  bound
    *
    *
    */





}
