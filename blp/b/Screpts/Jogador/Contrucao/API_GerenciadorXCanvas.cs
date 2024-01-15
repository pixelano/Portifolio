using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contrucao
{
    public class API_GerenciadorXCanvas : MonoBehaviour
    {
        public GameObject prefab;
        public GameObject contrucao_;

        public List<ItensCatalogo> itens_catalogo;

        public struct ItensCatalogo {

            public ScriptableObjectContrucaoData data_;
            public GameObject obj;

            public void definir(ScriptableObjectContrucaoData aux,GameObject pre, GameObject parent_,FeedBackCLick fb)
            {
                obj = Instantiate(pre, parent_.transform) ;
                obj.GetComponent<API_ComponentXcanvas>().definir(aux.icone,aux.nome,aux,fb);
            }

        }
      
        public void iniciar( ScriptableCatalogo listaItens ,FeedBackCLick fb)
        {
            itens_catalogo = new List<ItensCatalogo>();
            foreach (ScriptableObjectContrucaoData aux in listaItens.listaDeItens)
            {
                

                 
               itens_catalogo.Add(new ItensCatalogo());
                itens_catalogo[itens_catalogo.Count-1].definir(aux, prefab, contrucao_, fb);
            }
        }
        
    }
}
