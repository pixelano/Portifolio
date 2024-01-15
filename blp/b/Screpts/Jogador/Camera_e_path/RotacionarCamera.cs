using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Camera
{
    public class RotacionarCamera : MonoBehaviour
    {

        public Transform pontoCentral; // O objeto central em torno do qual voc� deseja rotacionar
        public float velocidadeRotacao = 1.0f;

        private void Update()
        {
            // Verifique se o bot�o direito do mouse est� pressionado
            if (Input.GetMouseButton(2))
            {
                // Capture o movimento do mouse na horizontal
                float movimentoMouseX = Input.GetAxis("Mouse X");

                // Aplique a rota��o em rela��o ao ponto central
                // Neste caso, estamos apenas rodando em torno do eixo vertical (Y)
                transform.RotateAround(pontoCentral.position, Vector3.up, movimentoMouseX * velocidadeRotacao);
            }
        }
    }
}
