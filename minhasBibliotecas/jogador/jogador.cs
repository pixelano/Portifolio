using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class jogador : MonoBehaviour
{

    public bool pulando;
    private NavMeshAgent agente;
    public movimentacao2 movJog;
    public servivo servo;
    bool atacou;
    float ultimoAtaq;
    private void Update()
    {
        if (servo.minhaVida.estavivo())
        {
            movJog.movimentacao();
            


            if (Input.GetKey(KeyCode.Alpha2))
            {
                if (atacou == false)
                {
                    ultimoAtaq = Time.time;
                    atacou = true;

                    servo.usarHabilidade(0, 0);
                }
            }

            if (atacou)
            {
                if (Time.time > ultimoAtaq + 1)
                {
                    atacou = false;
                    servo.fecharHabilidade(0);
                }
            }
        }
    }

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        servo = GetComponent<servivo>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("terreno"))
        {
            
            if (collision.collider.gameObject.layer == 6)
            {
                pulando = false;
                agente.enabled = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("terreno"))
        {
            Debug.Log("saio");
            if (collision.collider.gameObject.layer == 6)
            {
                pulando = true;
            }
        }
    }
    
}
