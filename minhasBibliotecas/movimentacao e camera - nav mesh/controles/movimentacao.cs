using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class movimentacao : MonoBehaviour
{
    public NavMeshAgent agente;
    private void Start()
    {
       //agente = GetComponent<NavMeshAgent>();
        
    }
    public void andeAte(Vector3 local)
    {

        //agente.SetDestination(local);
        if (agente.enabled == true)
        {
            agente.destination = local;
        }
    }
    public void desativar()
    {
        agente.enabled = agente.enabled == false ? true : false;
    }
    public void alterarVelocidade(float aux )
    {

        agente.speed = aux;
    }
}
