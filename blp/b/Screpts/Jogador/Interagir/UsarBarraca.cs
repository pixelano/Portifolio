using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contrucao;
using Interacoes;
namespace barracas
{
    public class UsarBarraca : MonoBehaviour
    {
        public ScriptableObjectContrucaoData data;
        public InteragirComBarracaFinalizada icbf;
       
        public List<int> fila = new List<int>();
        public bool lotada;
        public float escala;
        usandoAbarraca teste;
        [System.Serializable]
        public struct usandoAbarraca
        {
            public Vector3 local;
            public bool usouBarraca;
        }
        float tempoBarraca;
        public usandoAbarraca usarBarraca_(int visitante)
        {

            icbf.att_notificacao();
            usandoAbarraca operacao = new usandoAbarraca();
            lotada = fila.Count >= data.tamanhoDaFila ? true : false;
           
            operacao.usouBarraca = false;
            if (fila.Contains(visitante))
            {

                operacao.local = transform.position + (transform.forward * (fila.IndexOf(visitante) + 1) * escala);
                if (fila.IndexOf(visitante) == 0)
                {
                    tempoBarraca += Time.deltaTime;
                    if (tempoBarraca > data.TempoPorOperacao)
                    {
                     
                        icbf.multiplicador++;
                           tempoBarraca = 0;
                        operacao.local += (-transform.forward * escala * data.tamanhoDaFila) +
                            new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                        operacao.usouBarraca = true;
                        fila.Remove(visitante);

                    }

                }

            }
            else
            {

                fila.Add(visitante);
            }
            return operacao;
        }
        public void sairBarraca(int visitante)
        {
            fila.Remove(visitante);
        }
    }
}
