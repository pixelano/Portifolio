using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace _Camera
{
    public class MovimentarJogador : MonoBehaviour
    {
        NavMeshAgent agente;
        public float bias;
  

        private void Awake()
        {
            agente = GetComponent<NavMeshAgent>();
        }
        private Vector3 direcao;

        private void Update()
        {
            direcao = Vector3.zero;

            direcao += Input.GetKey(KeyCode.W) ? transform.forward : Vector3.zero;
            direcao += Input.GetKey(KeyCode.A) ? -transform.right : Vector3.zero;
            direcao += Input.GetKey(KeyCode.S) ? -transform.forward : Vector3.zero;
            direcao += Input.GetKey(KeyCode.D) ? transform.right : Vector3.zero;
            if (agente)
            {
                agente.destination = (direcao + transform.position);
            }
            else
            {
                transform.Translate(direcao * Time.deltaTime * bias);
            }
        }
    }
}
