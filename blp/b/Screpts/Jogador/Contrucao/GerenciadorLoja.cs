using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Contrucao
{
    public class GerenciadorLoja : MonoBehaviour
    {
        public GameObject modelo_catalogo,modelo_botão;
        [Space]
        public GameObject fundo_, botoesFiltro_;
       
        public List<novoCatalogo> catalogos;
        public FeedBackCLick fb;

        [System.Serializable]
        public struct novoCatalogo
        {
            public string nomeCatalogo;
            public ScriptableCatalogo catalogo_lista_contrucao;
            public GameObject catalogo;

            public void definirGo(GameObject aux)
            {
                catalogo = aux;
            }
        }
            
        private void Start()
        {
            foreach(novoCatalogo catalogo_ in catalogos)
            {
                GameObject botao_ = Instantiate(modelo_botão, botoesFiltro_.transform);
                GameObject catalogo__ = Instantiate(modelo_catalogo, fundo_.transform);
                botao_.name = ""+ catalogo_.nomeCatalogo;
                botao_.GetComponent<funcaoBotao>().definirCatalogo(catalogo__);
                catalogo__.name = catalogo_.nomeCatalogo;
               

                catalogo__.GetComponent<API_GerenciadorXCanvas>().iniciar(catalogo_.catalogo_lista_contrucao,fb);
                catalogo_.definirGo(catalogo__);
            }
        }
    }
}
