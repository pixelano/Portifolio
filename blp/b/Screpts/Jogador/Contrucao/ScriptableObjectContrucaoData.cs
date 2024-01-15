using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParaTodosOsItens;

namespace Contrucao
{
    [CreateAssetMenu(fileName = "DataContrucao", menuName = "Contrucoes/construcaoData", order = 1)]

    public class ScriptableObjectContrucaoData : ScriptableObject
    {
        public Texture2D icone;
        public string nome;
        public List<custo> receita;
        public int MaximoInteracoesParaColetar;
        public List<custo> Gera;
        [Space]
        public GameObject Modelo_Finalizado, Modelo_Estagio_1, Modelo_Estagio_2, Modelo_Estagio_3,Modelo_Fantasma;
        [Space]
        public int tamanhoDaFila;
        public float TempoPorOperacao;
        [System.Serializable]
       public struct custo
        {
            public ScriptableItensData data_custo;
            public int Quantidade;
        }
    }
}
