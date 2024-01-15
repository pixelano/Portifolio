using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ParaTodosOsItens;
namespace Contrucao
{
    public class API_ComponentXcanvas : MonoBehaviour
    {
        
        public RawImage icone;
        public TextMeshProUGUI nome,valor,Valor_tronco;
        public ScriptableObjectContrucaoData dat;
        public FeedBackCLick fb;
        public Inventario inventario;

        public void definir(Texture2D icone_, string nome_,ScriptableObjectContrucaoData dat_,FeedBackCLick feed)
        {
            fb = feed;
            icone.texture = icone_;
            nome.text = "" + nome_;
            valor.text = "" + dat_.receita[0].Quantidade;
            dat = dat_;
            Valor_tronco.text = "" + dat_.receita[1].Quantidade + " + " + (dat_.receita[1].Quantidade / 6) + " X 3"; ;



            Transform pai_ = fb.transform.parent;
            inventario = pai_.GetComponentInChildren<Inventario>();
        }
        public void escolheuEste()
        {
            bool teste = true;
            foreach (var aux in dat.receita)
            {

                if (inventario.verificarSeTem(aux.data_custo, aux.Quantidade) == false)
                {

                    teste = false;
                    break;
                }
            }
           
            
          
            if (teste)
            {
                foreach (var aux in dat.receita)
                {

                    inventario.subitrairItens(aux.data_custo, aux.Quantidade);

                }


                fb.escolheuEstaConstrucao(dat);

            }
            


        }
    }
}
