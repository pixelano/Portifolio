using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interacoes;
namespace paradasEpontos
{

    [System.Serializable]
    public class ColiderParadas : MonoBehaviour
    {

        // public List<teste2> destinos = new List<teste2>();
        public List<caminhos> destinos = new List<caminhos>();
        [System.Serializable]
        public struct caminhos
        {
            public ColiderParadas destino_;
            public NavMeshLink Link;

            public void definirLink(NavMeshLink a)
            {
                Link = a;
            }
            public void definirDestino(ColiderParadas aux)
            {
               
                destino_ = aux;
                Debug.Log(destino_);
            }
            public caminhos(ColiderParadas a, NavMeshLink b)
            {
                destino_ = a;
                Link = b;
            }
        }

        ColiderParadas escolherDestinho(ColiderParadas origem)
        {
            if (destinos.Count == 1)
            {
                if (destinos[0].destino_ == origem)
                {
                    return null;
                }

                return destinos[0].destino_;
            }
            else
            {
                List<caminhos> auxiliar = destinos.FindAll(x => x.destino_ != origem);
                caminhos aux = auxiliar[Random.Range(0, auxiliar.Count)];

                return aux.destino_;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Visitantes")
            {
                visitanteParadas visitante = other.GetComponent<visitanteParadas>();

                visitante.estaEmTransicao = true;
                ColiderParadas aux = escolherDestinho(visitante.origem);
                if (aux != null)
                {
                    visitante.irpara(aux.transform.position);
                    visitante.origem = this;
                }
                else
                {
                    visitante.origem = null;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Visitantes")
            {
                visitanteParadas aux = other.GetComponent<visitanteParadas>();
                if (!aux.origem == this)
                {

                    other.GetComponent<visitanteParadas>().estaEmTransicao = false;
                }


            }
        }

        public void Start()
        {
            foreach (caminhos aux in destinos)
            {
                novoLink(aux);
            }
        }
        public void attParadas(List<DefinirEstradas> paradas)
        {
            List<GameObject> aux_ = new List<GameObject>();

            foreach(DefinirEstradas a in paradas)
            {
                aux_.Add(a.gameObject);
            }
        
            foreach(caminhos aux in destinos)
            {
                if(!aux_.Exists(x=>x == aux.destino_.gameObject)) {
                    aux_.Remove(aux_.Find(x=>x == aux.destino_.gameObject));
                }
            }

            foreach (GameObject a in aux_) {
                destinos.Add(new caminhos(a.GetComponent<ColiderParadas>(),null));
                       novoLink(destinos[destinos.Count - 1]);
            }

        }
        void novoLink(caminhos alvo)
        {
            //  if (!alvo.Link)
            NavMeshLink aux = gameObject.AddComponent<NavMeshLink>();
            alvo.definirLink(aux);

            alvo.Link.endPoint = alvo.destino_.transform.position - this.transform.position;
            alvo.Link.startPoint = Vector3.zero;


        }

    }
}