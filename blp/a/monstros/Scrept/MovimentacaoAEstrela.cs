using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace montros
{
    public class MovimentacaoAEstrela : MonoBehaviour
    {
        public Terrain terreno;
        public GameObject terreno_;
        public Vector3 target;
   
         List<Vector3> caminho = new List<Vector3>();
        [HideInInspector]
        public List<Vector3> aaa = new List<Vector3>();
        [HideInInspector]
        public List<Vector3> abbb = new List<Vector3>();
        Vector3 rota;


        public float distanciaMaximaASePercorrer; 
   

        public bool calcularRota_b; // remover isso

        public float alturaDoRaycast, distanciaVisao,escala;

        public LayerMask layersColisores;
        public int MaximoDeNodes;
        public float velocidade;

        void Start()
        {
            RaycastHit hit;
            Physics.Raycast(transform.position,-transform.up,out hit);
            if(hit.collider.GetComponent<Terrain>()){
            terreno = terreno == null ? FindAnyObjectByType<Terrain>() : terreno;
            }else if(hit.collider){
                terreno_ = hit.collider.gameObject;
            }else{
                Debug.LogError(transform.name+ "    "+ transform.position + "   não encontrou um chão");
            }
           
            distanciaMaximaASePercorrer = distanciaMaximaASePercorrer <=0 ? 100 : distanciaMaximaASePercorrer;
            alturaDoRaycast = alturaDoRaycast <=0 ? 1 : alturaDoRaycast;
            distanciaVisao = distanciaVisao <= 1 ? 3 : distanciaVisao;
            escala = escala < 0.5f ? 2 : escala;
            if(layersColisores == 0){
                Debug.Log(" não foi atribuido o layer dos colisores");
            }
            MaximoDeNodes = MaximoDeNodes < 10 ?90 :MaximoDeNodes;
            velocidade = velocidade <= 0 ? 4:velocidade;


        }
      List<Vector3 >converterParaVetor(List<node> a)
        {
            List<Vector3> bb = new List<Vector3>(); 
            if (a != null)
            {
               
                foreach (node c in a)
                {
                    bb.Add(c.local);
                }
                return bb;
            }
            else
            {
                Debug.Log("deu nulo essa merda");
                bb.Add(transform.position);
                return bb;
            }
        }
        public void movimentarPara(Vector3 a){
            
            target = a;
            calcularRota_b = true;
        }
        public void pare(){
         caminho.Clear();   
        }
        private void Update()
        {
            

           if(calcularRota_b)
            {

                try
                {
                    caminho = OptimizePath(converterParaVetor(calcularRota(target,layersColisores)));

                    rota = caminho[0];
                    calcularRota_b = false;
                }
                catch {
                   
                        caminho = (converterParaVetor(calcularRota(target, layersColisores)));

                        rota = caminho[0];
                        calcularRota_b = false;
                 
                }
                aaa.Clear();
                abbb.Clear();
            }
            if (caminho.Count >0)
            {
                rota = attNode();
                if (rota != null)
                {
                        transform.position = AjustarAlturaChaoS(Vector3.Lerp(transform.position, (Vector3.Normalize(rota - transform.position) * velocidade) + transform.position, Time.deltaTime));
               
                }
            }
            else
            {
                transform.position = AjustarAlturaChaoS(transform.position);
            }
        }
        public Vector3 AjustarAlturaChao(Vector3 a)
        {
            Vector3 local = a;
          local.x = (int)(((int)(local.x / escala)) * escala);
           local.z = (int)(((int)(local.z / escala)) * escala);
           if(terreno){
            local.y = (int)(((int)(terreno.SampleHeight(a) / escala)) * escala);
            }else{
                   RaycastHit hit;
            Physics.Raycast(transform.position,-transform.up,out hit);

  local.y = (int)(((int)(hit.point.y / escala)) * escala);
            }
            return local;
        }
        public Vector3 AjustarAlturaChaoS(Vector3 a)
        {
            Vector3 local = a;

            if(terreno){
            local.y = terreno.SampleHeight(a);
            }else{
                   RaycastHit hit;
            Physics.Raycast(transform.position,-transform.up,out hit);

                local.y = (hit.point.y / escala);
            }

          
            return local;
        }
    
       
        public Vector3 attNode()
        {
            Vector3 aux = AjustarAlturaChaoS(rota);

             if (Vector3.Distance(aux,   (transform.position)) < 1 )
            {
                caminho.Remove(rota);
                if (caminho.Count == 0)
                    return transform.position;
           
                return caminho[0];
            }
            return rota;
        }
   
        public List<node > calcularRota(Vector3 destino,LayerMask lms)
        {
            List<node> caminhosPossiveis = new List<node>();
            caminhosPossiveis.Add(new node(AjustarAlturaChao(transform.position),null));
            int contNode = 0;
          while(caminhosPossiveis.Count > 0)
            {

             
              
               
                node inicial = caminhosPossiveis[0];

                for(int x= 0; x < caminhosPossiveis.Count; x++)
                {
                    if(caminhosPossiveis[x].pontuacao < inicial.pontuacao)
                    {
                        inicial = caminhosPossiveis[x];
                    }
                }
                caminhosPossiveis.Remove(inicial);
               
                    if (Vector3.Distance(AjustarAlturaChao( inicial.local),AjustarAlturaChao(destino)) < 1 * escala)
                {
                    List<node> reverso = new List<node>();
                    while(inicial != null)
                    {
                        reverso.Add(inicial);
                        inicial = inicial.origem;
                    }
                    reverso.Reverse();
                    Debug.Log("CCCCCC");
                    return reverso;
                }
                if (MaximoDeNodes != 0)
                {
                    if (contNode >= MaximoDeNodes)
                    {
                        List<node> reverso = new List<node>();
                        while (inicial != null)
                        {
                            reverso.Add(inicial);
                            inicial = inicial.origem;
                        }
                        reverso.Reverse();
                        Debug.Log("bbbbb");
                        return reverso;
                    }

                    contNode++;
                }
                List<node> aoredor = testarLocais(inicial,lms);
                List<node> novos = new List<node>();
                List<node> ajustar = new List<node>();

                foreach (node aa in aoredor)
                {
                    int pontosQ = (int)(inicial.Qpontos +1);
                  

                    if (!caminhosPossiveis.Exists(x=> x.local == aa.local))
                    {
                        aa.calcular(pontosQ,AjustarAlturaChao(destino));
                        novos.Add(aa);
                    }
                   
                 
                }
               caminhosPossiveis.AddRange(novos);
                foreach(node aa in ajustar)
                {
                    node bb = caminhosPossiveis.Find(x=>x.local == aa.local);
                    bb.atualizar(aa);
                }



            }
            Debug.Log("aaa");
            return null;
        }
  


        public List<node> testarLocais(node origem,LayerMask mlks)
        {
            List<node> testados = new List<node>();
            Vector3 localOrigem =  origem.local;
            localOrigem.y += alturaDoRaycast;

            for(int x= -1; x <2; x++)
            {
                for(int z = -1; z < 2; z++)
                {
                    if (x == 0 && z == 0)
                        continue;
                   
                    Vector3 olharpara = (new Vector3(x, 0, z ));
                    olharpara.y = localOrigem.y;
                    float aux_ = Vector3.Distance(olharpara + transform.position, transform.position);
                    if (aux_ >= distanciaMaximaASePercorrer)
                    {
                      
                        continue;
                    }
                    Ray raio = new Ray(localOrigem, olharpara);
                    RaycastHit hit;
                    Physics.Raycast(raio, out hit, distanciaVisao * 1.5f, mlks);
                    if(hit.collider == null)
                    {
                        aaa.Add(AjustarAlturaChao((new Vector3(x, 0, z) * escala) + localOrigem));
                        testados.Add(new node(AjustarAlturaChao((new Vector3(x, 0, z) * escala) + localOrigem), origem));
                    }
                    else
                    {
                        abbb.Add(AjustarAlturaChao((new Vector3(x, 0, z) * escala) + localOrigem));
                    }
                }
            }

            return testados;
        }

    [System.Serializable]
        public class node
        {

            public Vector3 local;
            public node origem;

            public int Qpontos;
            public float DistanciaParaChegada;
            public float  pontuacao;
            public node(Vector3 a,node orig)
            {
                local = a;
                origem = orig;
            }
            public void calcular(int Qpontos_, Vector3 destino)
            {
                Qpontos = Qpontos_;
                DistanciaParaChegada = Vector3.Distance(local, destino);
                pontuacao = Qpontos + DistanciaParaChegada;

            }
            public void atualizar(node aa)
            {
                   Qpontos = aa.Qpontos;
           DistanciaParaChegada = aa.DistanciaParaChegada;
            pontuacao = aa.pontuacao;
        }
        }
        public static List<Vector3> OptimizePath(List<Vector3> originalPath)
        {
            if (originalPath.Count < 3)
            {
                return originalPath;
            }

            List<Vector3> optimizedPath = new List<Vector3>();
            optimizedPath.Add(originalPath[0]);

            for (int i = 1; i < originalPath.Count - 1; i++)
            {
                Vector3 previousPoint = optimizedPath[optimizedPath.Count - 1];
                Vector3 currentPoint = originalPath[i];
                Vector3 nextPoint = originalPath[i + 1];

                if (!ArePointsCollinear(previousPoint, currentPoint, nextPoint))
                {
                    optimizedPath.Add(currentPoint);
                }
            }

            optimizedPath.Add(originalPath[originalPath.Count - 1]);

            return optimizedPath;
        }

        public static bool ArePointsCollinear(Vector3 a, Vector3 b, Vector3 c)
        {
            Vector2 ab = new Vector2(b.x - a.x, b.z - a.z);
            Vector2 ac = new Vector2(c.x - a.x, c.z - a.z);

            float crossProduct = ab.x * ac.y - ab.y * ac.x;

            // Se o produto cruzado for quase zero, os pontos est�o em linha reta.
            return Mathf.Approximately(crossProduct, 0);
        }
    

}
}
