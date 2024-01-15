using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace paradasEpontos
{
    public class GerenciadorDeTodasAsEstradas : MonoBehaviour 
    {

        public List<DefinirEstradas> todasAsEstradas =new List<DefinirEstradas>();
        public List<NomeDeCadaParada> nomes = new List<NomeDeCadaParada>();
        public DefinirEstradas sendousado;
        public GameObject prefabNome, ListaDeNomes,fundo;
        public TextMeshProUGUI titulo;
        int contador=0;

       

        private void Update()
        {
            if (sendousado)
            {  
                foreach(NomeDeCadaParada aux in nomes)
                {
                    if(sendousado.paradas.Exists(x=> x.nome == aux.nome))
                    {
                        Button aux_ = aux.GetComponent<Button>();//

                        ColorBlock aux__ = aux_.colors;
                        aux__.normalColor = new Color(0.1f, 0.1f, 0.1f, 1);
                        aux_.colors = aux__;
                    }
                    else
                    {
                        Button aux_ = aux.GetComponent<Button>();//

                        ColorBlock aux__ = aux_.colors;
                        aux__.normalColor = new Color(1, 1, 1,1);
                        aux_.colors = aux__;

                    }
                }

                nomes.Find(x => x.nome == sendousado.nome).gameObject.SetActive(false);
                flag_ = true;
            }
            if(flag_ == true && sendousado == null)
            {
                foreach (NomeDeCadaParada aux in nomes)
                {
                    aux.gameObject.SetActive(true);
                }
                flag_ = false;
            }
        }
        bool flag_;
        public void ativarDesativar() {
          
            fundo.SetActive(!fundo.active);
        }
        public void Click_EscolherParada(string aux)
        {
            sendousado.ParadaEscolida(todasAsEstradas.Find(x=>x.nome == aux));
        }
        void criarNome(DefinirEstradas aux)
        {
            GameObject aa = Instantiate(prefabNome,ListaDeNomes.transform);
//            aa.transform.SetParent( ListaDeNomes.transform);

            NomeDeCadaParada bb = aa.GetComponent<NomeDeCadaParada>();
            bb.gdtae = this;
            bb.definirNome(aux.nome);
            nomes.Add(bb);

        }
        void removerNome(DefinirEstradas aux)
        {
            NomeDeCadaParada aa = nomes.Find(x => x.nome == aux.nome);
            Destroy(aa.gameObject);
            nomes.Remove(aa);
        }
        public void registrar(DefinirEstradas aux)
        {
            if (!todasAsEstradas.Contains(aux)) {
                todasAsEstradas.Add(aux);
                criarNome(aux);
            }
        }
        public void oEscolido(DefinirEstradas aux)
        {
            if (sendousado != aux)
            {
                sendousado = aux;
                titulo.text = aux.name;
            }
            else
            {
                sendousado = null;
            }
        }
        public void remover(DefinirEstradas aux)
        {
            if (todasAsEstradas.Contains(aux))
            {
                todasAsEstradas.Remove(aux);
                removerNome(aux);
            }
        }
    }

  
}
