using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Camera
{
    public class RotacionarCamera : MonoBehaviour
    {

        public Transform pontoCentral; // O objeto central em torno do qual você deseja rotacionar
        public float velocidadeRotacao = 1.0f;

        private void Update()
        {
            // Verifique se o botão direito do mouse está pressionado
            if (Input.GetMouseButton(2))
            {
                // Capture o movimento do mouse na horizontal
                float movimentoMouseX = Input.GetAxis("Mouse X");

                // Aplique a rotação em relação ao ponto central
                // Neste caso, estamos apenas rodando em torno do eixo vertical (Y)
                transform.RotateAround(pontoCentral.position, Vector3.up, movimentoMouseX * velocidadeRotacao);
            }
        }
    }
}
