using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using barracas;
using Parques;
using ParaTodos;

namespace Visitantes
{
    public class IaVisitante : MonoBehaviour
    { 

     
        public FichaVIsitante ficha;
        public NavMeshAgent agente;
        [HideInInspector]
        public SpawnerVisitantes spv;
        public int ingresso;
        public float DistanciaDoAlvoCaminhada;
        float tempoMaximoDeVisita,TempoEmParque;

        float tempoCaminhaAtual,tempoPacienciaAtual;
        
         
       public GerenciadorDeParque GP;
        // tem que melhorar isso aqui <-----------
        public Vector3 localNoParque()
        {

            if(!GP.malha)
            {
                if (GP.AsBarracas.Count == 0)
                {
                    return transform.position +
                        new Vector3(Random.Range(-DistanciaDoAlvoCaminhada, DistanciaDoAlvoCaminhada), 0
                        , Random.Range(-DistanciaDoAlvoCaminhada, DistanciaDoAlvoCaminhada));
                }
                else
                {


                    return GP.AsBarracas[GP.AsBarracas.Count - 1].transform.position +
                        new Vector3(Random.Range(-DistanciaDoAlvoCaminhada, DistanciaDoAlvoCaminhada), 0
                        , Random.Range(-DistanciaDoAlvoCaminhada, DistanciaDoAlvoCaminhada));

                        }
            }
            else
            {
               
                return GetRandomPositionInMesh(GP.malha);
            }
        }
        private void irParaCasa()
        {
            agente.SetDestination(spv.transform.position);

            if(agente.remainingDistance < 2 || agente.isStopped)
            {
                spv.voltar(ficha, transform.gameObject);
            }
        }
        private UsarBarraca escolherBarraca()
        {
            return GP.BarracaPerto(transform.position).GetComponent<UsarBarraca>();
        }
        UsarBarraca barracaEscolida;
        void sairBarraca()
        {
            if(barracaEscolida != null)
            {
                barracaEscolida.sairBarraca(ingresso);
                barracaEscolida = null;
            }
            else
            {
            }
        }
        // que porra é essa ? <--------------
       public void attParque()
        {

            GP = spv.GP;
           
        }

        private void Update()
        {
            TempoEmParque += Time.deltaTime;
            if(TempoEmParque < tempoMaximoDeVisita)
            {
                if (GP != null)
                {

                    tempoCaminhaAtual += Time.deltaTime;

                    if (tempoCaminhaAtual > ficha.TempoDeCaminhada)
                    {
                        tempoPacienciaAtual += Time.deltaTime;
                        if (tempoPacienciaAtual < ficha.paciencia)
                        {
                            if (barracaEscolida == null)
                            {
                                barracaEscolida = escolherBarraca();
                                // arrumar isso <------------------------
                                if (barracaEscolida.lotada || barracaEscolida.icbf.multiplicador >= barracaEscolida.data.MaximoInteracoesParaColetar)
                                {
                                    barracaEscolida = null;
                                }
                            }
                            else
                            {
                                var aux = barracaEscolida.usarBarraca_(ingresso);

                                agente.destination = aux.local;

                                if (aux.usouBarraca)
                                {
                                    tempoCaminhaAtual = 0;
                                    agente.SetDestination(localNoParque());
                                    sairBarraca();


                                }

                            }
                        }
                        else
                        {
                            sairBarraca();
                            tempoCaminhaAtual = 0;
                        }

                    }
                    else
                    {
                        if (agente.isStopped || agente.remainingDistance < 1)
                        {
                            agente.SetDestination(localNoParque());

                        }

                    }
                }
                else
                {
                    attParque();
                }
            }
            else
            {
                sairBarraca();
                irParaCasa();
            }
        }
        private void Start()
        {
            tempoMaximoDeVisita = (ficha.paciencia + ficha.TempoDeCaminhada) *ficha.BarracasQueIraVisitar;
        }
        /*
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 8)
            {
                barracasPerto.Add(other.GetComponent<UsarBarraca>());
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 8)
            {
                barracasPerto.Remove(other.GetComponent<UsarBarraca>());
            }
        }
        */
        //
        public List<Vector3> vertices; // Lista de Transform contendo os pontos
     
        

        private float DistanciaMaxima = 10f;  // Defina a distância máxima desejada
        private Vector3 pontoDeOrigem;  // Defina o ponto de origem desejado

        Vector3 GetRandomPositionInMesh(Mesh mesh)
        {
          
            int randomTriangleIndex = Random.Range(0, mesh.triangles.Length / 3);
            int startIndex = randomTriangleIndex * 3;
           
            Vector3 vertexA = mesh.vertices[mesh.triangles[startIndex]];
            Vector3 vertexB = mesh.vertices[mesh.triangles[startIndex + 1]];
            Vector3 vertexC = mesh.vertices[mesh.triangles[startIndex + 2]];

            float u = Random.Range(0f, 1f);
            float v = Random.Range(0f, 1f);

            if (u + v > 1f)
            {
                u = 1f - u;
                v = 1f - v;
            }

            Vector3 randomPosition = vertexA + u * (vertexB - vertexA) + v * (vertexC - vertexA);

         
                return randomPosition;
          
        }

    }
}
