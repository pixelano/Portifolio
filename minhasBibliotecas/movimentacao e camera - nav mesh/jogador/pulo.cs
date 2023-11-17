using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class pulo : MonoBehaviour
{
    public bool noAr;
    public GameObject agente;
    private void OnTriggerEnter(Collider other)
    {if (GetComponent<ParentConstraint>().constraintActive == false)
        {
            noAr = false;
            agente.transform.position = transform.position;
            agente.GetComponent<movimentacao>().andeAte(transform.position);
            agente.GetComponent<movimentacao>().desativar();

//            agente.transform.position = transform.position;

            GetComponent<ParentConstraint>().constraintActive = true;
        }
    }

}
