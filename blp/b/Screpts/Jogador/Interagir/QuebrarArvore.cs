using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Interacoes {
    public class QuebrarArvore : MonoBehaviour, IAtivarComClick
    {
        public UnityEvent quebrarArvore,droparItem;
        public int vida_,multiplicadorAoQuebrar;
        private float ti;
        private int _vida;
        public void executar(GameObject aux) { }
        public void executar()
        {
            if (Time.time - ti > 1) {
                ti = Time.time;
                if (vida_ <= 1)
                {

                    for (int x = 0; x < multiplicadorAoQuebrar * _vida; x++)
                    {
                        droparItem.Invoke();
                    }
                    quebrarArvore.Invoke();

                }
                if (vida_ > 1  )
                {
                    droparItem.Invoke();
                    vida_ -= 1;
                    _vida += 1;

                }
              
            }

        }
    }
}
