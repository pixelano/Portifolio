using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interacoes;

namespace paradasEpontos
{
    [System.Serializable]
    public class DefinirEstradas : MonoBehaviour,IAtivarComClick
    {
        public string nome;

        public List<DefinirEstradas> paradas = new List<DefinirEstradas>();
        public ColiderParadas clp;

     public void executar()
        {
            // abrir o canvas
            //mandar quem esta usando
            // receber o feedback de quem foi escolido

            global.ativarDesativar();
            global.oEscolido(this);
        

        }
       

        public void ParadaEscolida(DefinirEstradas aux)
        {
            if (paradas.Contains(aux))
            {
                paradas.Remove(aux);
            }
            else
            {
                paradas.Add(aux);
            }
            attPulos();
        }
      
        GerenciadorDeTodasAsEstradas global;
        void attPulos()
        {
            clp.attParadas(paradas);
        }

        private void Awake()
        {
            global = FindAnyObjectByType<GerenciadorDeTodasAsEstradas>();
            if(global == null)
            {
                Debug.LogErrorFormat("faltou adicionar o canvas global de estradas");
            }
            global.registrar(this);
            clp = GetComponent<ColiderParadas>();
        }
     

        public void executar(GameObject aux)
        {

        }
    }
}
