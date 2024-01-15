using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Camera
{
    public class SegueObjeto : MonoBehaviour
    {
        public Transform objetoCentral; // O objeto central que você deseja seguir
        public float distanciaDesejada,alturaObjeto ; // A distância desejada entre este objeto e o objeto central

        public Vector3 offset; // O deslocamento inicial entre os objetos

        private void Start()
        {
            if (objetoCentral == null)
            {
                Debug.LogError("Objeto central não atribuído. Por favor, atribua um objeto central.");
                enabled = false; // Desativa o script se o objeto central não estiver atribuído
                return;
            }

            // Calcula o deslocamento inicial entre os objetos
            offset = transform.position - objetoCentral.position;
        }

        private void Update()
        {
            // Calcula a nova posição baseada na distância desejada
            Vector3 novaPosicao = objetoCentral.position + offset.normalized * distanciaDesejada;

            // Mantém a altura do objeto atual
            novaPosicao.y = objetoCentral.position.y + alturaObjeto;

            // Define a nova posição do objeto atual
            transform.position = novaPosicao;
        }
    }
}
