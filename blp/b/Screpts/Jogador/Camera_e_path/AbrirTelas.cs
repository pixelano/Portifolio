using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace _Camera
{
    public class AbrirTelas : MonoBehaviour
    {
        [SerializeField]
        public novaTela contrucoes;
        [System.Serializable]
        public struct novaTela
        {
            public bool flipflop;
       
            public RawImage contrucoes;

            public void abrir() {
                flipflop = !flipflop;
                contrucoes.gameObject.SetActive(flipflop);

                
            }
        }
        public void abrirFechar()
        {
            contrucoes.abrir();
        }
        private void Update()
        {
         
            if (Input.GetKeyDown(KeyCode.I))
            {
               
                contrucoes.abrir();
            }
        }
    }
}
