using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movimentacao2 : MonoBehaviour
{
    public meuConfigPulo configpulo;


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
    private Rigidbody rb;
    private Vector3 original;
    


    void Start()
    {
        direcao = Vector3.zero;
        rb =jogado.GetComponent<Rigidbody>();
        original = transform.position;
        agente = jogado.GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
    }
   
       
    public void movimentacao()
    {
            attJogador();
            attCamera();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                pulo();
            }
        
    }

    private void pulo()
    {
        if (regrasPulo())
        {
            agente.enabled = false;
            rb.AddForce(( direcao+ Vector3.up )* configpulo.focaPulo , ForceMode.Impulse);
        }
    }
    private bool regrasPulo()
    {
        bool aux = jogado.GetComponent<jogador>().pulando;
      
        
        return aux == false;
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
        if (agente.isActiveAndEnabled)
        {
            agente.SetDestination(direcao + jogado.transform.position);
        }
        rotacionaCorno();
    }

 
    public float sensibilidade,Sensibilidadevertical, minYRotation, maxYRotation;
    float saveY;
    private void attCamera()
    {
        Vector3 auxaltura = transform.localPosition;

        float mouseX = Input.GetAxis("Mouse X") * sensibilidade;

        float mouseY = -Input.GetAxis("Mouse Y")  / Sensibilidadevertical;

        float altura = auxaltura.y;
        //Debug.Log(altura);
        if(altura < maxYRotation && altura > minYRotation || altura > maxYRotation && mouseY < 0
            || altura < minYRotation && mouseY > 0)
        {
            altura += mouseY;
        }
        
        
        auxaltura.y = altura;







        transform.localPosition = auxaltura; 

         transform.RotateAround(jogado.transform.position, Vector3.up, mouseX);



    }
    public GameObject modeloCorpo;
    public float velocidadeRotacaoCorpo=1;
    private void rotacionaCorno()
    {
        if (direcao != Vector3.zero)
        {
            Vector3 direction = (direcao  * 10);

            direction.y =0;

          
            modeloCorpo.transform.rotation =Quaternion.Lerp(modeloCorpo.transform.rotation, Quaternion.LookRotation(direction),Time.deltaTime *  velocidadeRotacaoCorpo);

            // Aplicar a rotação ao objeto
          //  modeloCorpo.transform.rotation = targetRotation;// Quaternion.LerpUnclamped(modeloCorpo.transform.rotation, targetRotation, Time.deltaTime * velocidadeRotacaoCorpo);
        }
    }
  
    [System.Serializable]
    public struct meuConfigPulo
    {
        public float focaPulo;
    }

}
