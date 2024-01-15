using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parques;

namespace Visitantes
{

    public class SpawnerVisitantes : MonoBehaviour
    {
        public ListaVisitantes ListaFicha;
        public List<visitantes_controle> VisitantesFora, VisitantesDentro;
        public int numeroMaximoDeVisitantes = 200;
        [System.Serializable]
        public struct visitantes_controle {
            public FichaVIsitante ficha;
            public float contadorTempo;
            public void attTempo(float tempo)
            {
                contadorTempo = tempo;
            }

            public void zerarTempo()
            {
                contadorTempo = 0;
            }
            public void definir(FichaVIsitante fic)
            {
                ficha = fic;
            }

        }
        private List<int> ContadorVisitantes = new List<int>();

        public float AttFun;
        private float fun;
        public TodosOsParques TOP;
        public GerenciadorDeParque GP;


        private void Start()
        {
            TOP = FindObjectOfType<TodosOsParques>();
            VisitantesFora = new List<visitantes_controle>();
            VisitantesDentro = new List<visitantes_controle>();
            ContadorVisitantes.Add(0);
            foreach (FichaVIsitante visitante in ListaFicha.lista)
            {
                if (visitante.nivelFun <= fun)
                {

                    visitantes_controle aux = new visitantes_controle();

                    aux.definir(visitante);
                    VisitantesFora.Add(aux);
                }
            }
        }
        int loop_Visitantes;

        private void Update()
        {
            if (GP != null)
            {
                if (AttFun != fun)
                {
                    fun = AttFun;

                    foreach (FichaVIsitante visitante in ListaFicha.lista)
                    {
                        if (visitante.nivelFun <= fun)
                        {

                            visitantes_controle aux = new visitantes_controle();
                            aux.definir(visitante);
                            VisitantesFora.Add(aux);
                        }
                    }
                }
                if (VisitantesFora.Count > 0)
                {
                    List<visitantes_controle> aux = new List<visitantes_controle>();
                    for (int x = 0; x < VisitantesFora.Count; x++)
                    {

                        if (VisitantesFora[x].contadorTempo + VisitantesFora[x].ficha.TempoParaRetorno < Time.time)
                        {
                            VisitantesFora[x].zerarTempo();

                            aux_visitante = VisitantesFora[x];
                            Invoke("spawnaer", x);
                            aux.Add(VisitantesFora[x]);

                        }


                    }

                    VisitantesDentro.AddRange(aux);
                    VisitantesFora.RemoveAll(x => aux.Contains(x));


                }
            }
            else
            {
                GP = TOP.estaDentro(transform.position);
               
            }
        } 
        public GerenciadorDeParque retornarGP()
        {
            return GP;
        }
        public float DistanciaSpawn;
        visitantes_controle aux_visitante;
        public void spawnaer()
        {
           
           GameObject aux = Instantiate(aux_visitante.ficha.modelo,transform.position + new Vector3(Random.Range(-DistanciaSpawn,DistanciaSpawn),
                0, Random.Range(-DistanciaSpawn, DistanciaSpawn)),Quaternion.identity, transform);
            int cc = ContadorVisitantes[ContadorVisitantes.Count - 1] + 1;
            IaVisitante aux_ = aux.GetComponentInChildren<IaVisitante>();
            aux_.ingresso = cc;
            aux_.spv = this;
            aux_.GP = GP;
            
            ContadorVisitantes.Add(cc);
            if(ContadorVisitantes.Count > numeroMaximoDeVisitantes)
            {
                ContadorVisitantes.Clear();
                ContadorVisitantes.Add(0);
            }
        }
        public void voltar(FichaVIsitante fich,GameObject sel_f)
        {
            visitantes_controle aux = VisitantesDentro.Find(x => x.ficha == fich);

            aux.attTempo(Time.time);
            VisitantesDentro.Remove(VisitantesDentro.Find(x => x.ficha == fich));
            VisitantesFora.Add(aux);
           

            Destroy(sel_f.transform.parent.gameObject);
        }



    }
}
