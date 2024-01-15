using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contrucao;
using ParaTodosOsItens;

namespace Interacoes
{
    public class ContruirComClick : MonoBehaviour, IAtivarComClick
    {

        public ScriptableObjectContrucaoData data;
        public Transform contrucoes_;
        private GameObject modeloContrucao;
        private int nivel =0;
        private void Start()
        {
            Transform childTransform = transform; // ou qualquer outra maneira de obter o Transform do objeto filho.

            // Acesse o Transform do objeto pai usando a propriedade .parent.
            Transform parentTransform = childTransform.parent;
            contrucoes_ = parentTransform;
        }
        
        public void executar(GameObject call)
        {
            if (subitrair(call))
            {

                GameObject obj;
                switch (nivel)
                {
                    case 0:
                        obj = data.Modelo_Estagio_2;
                        break;
                    case 1:
                        obj = data.Modelo_Estagio_3;
                        break;
                    case 2:
                        obj = data.Modelo_Finalizado;
                        break;
                    default:
                        obj = data.Modelo_Finalizado;
                        break;


                }

                if (modeloContrucao)
                {
                    Destroy(modeloContrucao);
                    if (nivel == 2)
                    {
                        modeloContrucao = Instantiate(obj, transform.position, Quaternion.identity, contrucoes_);
                        Destroy(gameObject);
                    }
                    else
                    {

                        modeloContrucao = Instantiate(obj, transform);
                    }
                }
                else
                {
                    modeloContrucao = Instantiate(obj, transform);

                }
                nivel++;
            }
        }
        public void executar() { }
        GameObject callBack;
        Inventario invent;
        private bool subitrair(GameObject cb)
        {
            if(invent == null)
            {
                callBack = cb;
                invent = callBack.transform.parent.GetComponentInChildren<Inventario>();
            }

            
               return invent.subitrairItens(data.receita[1].data_custo, data.receita[1].Quantidade /6);
           


        }
    }
}
