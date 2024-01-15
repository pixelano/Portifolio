using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace paradasEpontos
{
    public class visitanteParadas : MonoBehaviour
    {

        public bool estaEmTransicao;
        public ColiderParadas origem;
        NavMeshAgent agente;
        void Start()
        {
            agente = GetComponent<NavMeshAgent>();
        }

        public void irpara(Vector3 aux)
        {
            agente.SetDestination(aux);
        }


    }
}