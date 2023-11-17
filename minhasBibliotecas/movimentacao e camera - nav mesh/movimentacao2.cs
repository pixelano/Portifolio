using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movimentacao2 : MonoBehaviour
{

    private Dictionary<KeyCode, Vector3> direcoes = new Dictionary<KeyCode, Vector3>()
    {
        { KeyCode.W, Vector3.forward },
        { KeyCode.A, Vector3.left },
        { KeyCode.S, Vector3.back },
        { KeyCode.D, Vector3.right }
    };
    private Vector3 direcao;
    private NavMeshAgent agente;

    public GameObject jogado;
    private Vector3 original;


    void Start()
    {
        direcao = Vector3.zero;

        original = transform.position;
        agente = jogado.GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
    }
    void Update()
    {
        attJogador();
        attCamera();
        
    }

    private void attJogador()
    {

        direcao = Vector3.zero;
        
        foreach (var kvp in direcoes)
        {
            if (Input.GetKey(kvp.Key))
            {
                //direcao += kvp.Value;


                if (kvp.Value == Vector3.forward)
                {
                    direcao += transform.forward;
                }
                if (kvp.Value == Vector3.left)
                {
                    direcao += -transform.right;
                }
                if (kvp.Value == Vector3.back)
                {
                    direcao += -transform.forward;
                }
                if (kvp.Value == Vector3.right)
                {
                    direcao += transform.right;
                }


            }
        }
        agente.SetDestination(direcao + jogado.transform.position);
        rotacionaCorno();
    }

 
    public float sensibilidade;
    private void attCamera()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensibilidade;

        transform.RotateAround(jogado.transform.position, Vector3.up, mouseX);
      
    }
    public GameObject modeloCorpo;
    public float velocidadeRotacaoCorpo=1;
    private void rotacionaCorno()
    {
        if (direcao != Vector3.zero)
        {
            Vector3 direction = (direcao  * 10)+ jogado.transform.position;

            direction.y = 0;
            // Calcular a rotação para a direção desejada
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Aplicar a rotação ao objeto
            modeloCorpo.transform.rotation = Quaternion.Slerp(modeloCorpo.transform.rotation, targetRotation, Time.deltaTime * velocidadeRotacaoCorpo);
        }
    }
 
}
