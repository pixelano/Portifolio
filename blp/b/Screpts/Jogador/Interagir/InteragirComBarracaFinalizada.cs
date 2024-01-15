using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParaTodosOsItens;
using Contrucao;
using barracas;
namespace Interacoes
{
    public class InteragirComBarracaFinalizada : MonoBehaviour, IAtivarComClick
    {
        
        public ScriptableObjectContrucaoData data;
        public UsarBarraca bar;
        public GameObject notificacao;
        public int multiplicador;
        public void executar() {  }
        public void executar(GameObject aux)
        {
            if (multiplicador > data.MaximoInteracoesParaColetar *0.1f)
            {
                Debug.Log("foi");

                Inventario invent_ = aux.transform.parent.GetComponentInChildren<Inventario>();
                foreach (var _aux in data.Gera)
                {
                    invent_.adicionarItem(_aux.data_custo, _aux.Quantidade * multiplicador);
                }
                multiplicador = 0;
               
            }
            att_notificacao();
        }
        public void att_notificacao()
        {
            if(multiplicador > data.MaximoInteracoesParaColetar * 0.1f)
            {
                
                    notificacao.SetActive(true);
             
            }
            else
            {
               
                    notificacao.SetActive(false);
                
            }
        }
    }
}
