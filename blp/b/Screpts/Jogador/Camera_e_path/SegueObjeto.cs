using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Camera
{
    public class SegueObjeto : MonoBehaviour
    {
        public Transform objetoCentral; // O objeto central que voc� deseja seguir
        public float distanciaDesejada,alturaObjeto ; // A dist�ncia desejada entre este objeto e o objeto central

        public Vector3 offset; // O deslocamento inicial entre os objetos

        private void Start()
        {
            if (objetoCentral == null)
            {
                Debug.LogError("Objeto central n�o atribu�do. Por favor, atribua um objeto central.");
                enabled = false; // Desativa o script se o objeto central n�o estiver atribu�do
                return;
            }

            // Calcula o deslocamento inicial entre os objetos
            offset = transform.position - objetoCentral.position;
        }

        private void Update()
        {
            // Calcula a nova posi��o baseada na dist�ncia desejada
            Vector3 novaPosicao = objetoCentral.position + offset.normalized * distanciaDesejada;

            // Mant�m a altura do objeto atual
            novaPosicao.y = objetoCentral.position.y + alturaObjeto;

            // Define a nova posi��o do objeto atual
            transform.position = novaPosicao;
        }
    }
}
