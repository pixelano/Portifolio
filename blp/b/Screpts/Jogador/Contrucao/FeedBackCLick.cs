using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ParaTodosOsItens;
using Parques;

namespace Contrucao
{
    public class FeedBackCLick : MonoBehaviour
    {
        public Transform jogador; // O Transform do jogador
        public float distanciaDoJogador = 10f;
        public UnityEvent abrirFechar,cancelarDestruirArvore;   

        float eixoX, eixoY,altura;
        public Terrain ter;
        public GameObject contrucoes;
        Inventario invent;
        TodosOsParques top;
        bool onOf;
        private void Start()
        {
            invent = transform.parent.GetComponentInChildren<Inventario>();
            if(ter == null)
            {
                Debug.LogErrorFormat(" tem que setar o terrano na variavel Ter em FEedBackClick, que fica no jogador>logico>camera");
            }
           if(contrucoes == null)
            {
                contrucoes = new GameObject("Contrucoes");
            }
            top = FindAnyObjectByType<TodosOsParques>();
            if (top == null)
            {
                Debug.LogErrorFormat(" faltou criar o gerenciador de gerenciador de parques ");   }
        }

        Vector3 salvarUltimaPosicao;
        public void flipAreaParque( )
        {
            top.flipFlop_renderParque(salvarUltimaPosicao);
        }
        void Update()
        {
            if (onOf) {

                 if (ter != null)
                {
                    Vector3 final = (((jogador.forward * eixoY) + (jogador.right * eixoX)) / distanciaDoJogador);

                    if (fantasma)
                    {

                        eixoX += Input.GetAxis("Mouse X");
                        eixoY += Input.GetAxis("Mouse Y");
                        Vector3 jog = jogador.position;
                        jog.y = 0;


                          final.y = 0;
                        altura = ter.SampleHeight(final + jog);
                        final += jog;
                        final.y = altura;
                        fantasma.transform.position = final;

                        if (Input.GetMouseButtonDown(0) && disponivel())
                        {
                            cancelarDestruirArvore.Invoke();
                            instanciarFundacaoContrucao();
                            onOf = !onOf;
                        }
                        if (Input.GetMouseButtonDown(1))
                        {
                            cancelarDestruirArvore.Invoke();
                            onOf = !onOf;
                            foreach (var aux in data_contrucao.receita)
                            {

                                invent.adicionarItem(aux.data_custo, aux.Quantidade);


                            }
                        }
                    }
                    if (!fantasma)
                    {
                        fantasma = Instantiate(prefabFantasma);
                        tcm = fantasma.GetComponentInChildren<TrocarMaterialFeedBack>();
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        Debug.Log("b");
                        salvarUltimaPosicao = transform.position;
                        flipAreaParque();

                    }

                }
            }
            else
            {
                if (fantasma)
                {
                    Debug.Log("a");
                    
                    flipAreaParque();
                    Destroy(fantasma);
                    fantasma = null;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                }
            }

        }
        public TrocarMaterialFeedBack tcm;
        private bool disponivel()
        {
          
            return tcm.disponivel;
        }
        public void instanciarFundacaoContrucao()
        {
           
           GameObject aux = Instantiate(data_contrucao.Modelo_Estagio_1, fantasma.transform.position, Quaternion.identity, contrucoes.transform);
         //   aux.GetComponent<ContruirComClick>.contrucoes_ = contrucoes;
        }
        private ScriptableObjectContrucaoData data_contrucao;
        public void escolheuEstaConstrucao(ScriptableObjectContrucaoData contrucao)
        {
            data_contrucao = contrucao;
            abrirFechar.Invoke();
            cancelarDestruirArvore.Invoke();
            prefabFantasma = contrucao.Modelo_Fantasma;
            onOf = true;

        }
        public GameObject fantasma,prefabFantasma;
    }
}
