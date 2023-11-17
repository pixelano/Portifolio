using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movimentacao : MonoBehaviour
{

    private Dictionary<KeyCode, Vector3> direcoes = new Dictionary<KeyCode, Vector3>()
    {
        { KeyCode.W, Vector3.forward },
        { KeyCode.A, Vector3.left },
        { KeyCode.S, Vector3.back },
        { KeyCode.D, Vector3.right }
    };
    public GameObject jogado,focoCamera;
    private Vector3 direcao;
    private NavMeshAgent agente;
    private Vector3 pontoFOco;


    public float sensitivity = 1f;
    public float ajusteXcamera , ajusteYcamera =1f, distanciaCamera;

    private float distancia_X,distancia_Z;






    private void Start()
    {
        pontoFOco =focoCamera.transform.position;
        agente = jogado.GetComponent<NavMeshAgent>();



    }
   public float ultimomouse;
    private void pegaMouse()
    {
        ultimomouse += -Input.GetAxis("Mouse X") * sensitivity;
        ultimomouse = Mathf.Abs(ultimomouse) > 1 ?  ultimomouse > 0 ? 1 : -1    : ultimomouse;
        distancia_X =  Mathf.Clamp(ultimomouse, -1, 1);

      

    }
    Quaternion rot;
    public float tempoCamera;
    private void Update()
    {
        direcao = Vector3.zero;

        foreach (var kvp in direcoes)
        {
            if (Input.GetKey(kvp.Key))
            {
                //direcao += kvp.Value;


                if (kvp.Value == Vector3.forward) {
                    direcao += transform.forward;
                }
                if (kvp.Value == Vector3.left) {
                    direcao += -transform.right;
                }
                if (kvp.Value == Vector3.back) {
                    direcao += -transform.forward;
                }
                if (kvp.Value == Vector3.right) {
                    direcao += transform.right;
                }
        

            }
        }
      

        agente.SetDestination(direcao + jogado.transform.position);
         pegaMouse();

        focoCamera.transform.position = new Vector3(jogado.transform.position.x, focoCamera.transform.position.y,
            jogado.transform.position.z) + jogado.transform.right * ajusteXcamera;
        transform.position = jogado.transform.position  +
                distanciaCamera* distancia_X * jogado.transform.right + distanciaCamera * distancia_Z * jogado.transform.forward + new Vector3(ajusteXcamera, ajusteYcamera, 0);
        if (direcao == -transform.forward)
        {
            if (distancia_X != 0)
            {
                distancia_X = Mathf.Lerp(0, distancia_X, tempoCamera);
                ultimomouse = Mathf.Lerp(0, ultimomouse, tempoCamera);

            }
            distancia_Z = Mathf.Lerp(Mathf.Pow(Mathf.Abs(distancia_X), 2) + 1 , distancia_Z,tempoCamera);
        }
        else
        {
            if (direcao == transform.forward)
            {
                if (distancia_X != 0)
                {
                    distancia_X = Mathf.Lerp(0, distancia_X, tempoCamera);
                    ultimomouse = Mathf.Lerp(0, ultimomouse, tempoCamera);

                }
            }

            if (direcao == transform.right)
            {



                distancia_X = Mathf.Lerp(1, distancia_X, tempoCamera);
                ultimomouse = Mathf.Lerp(1, ultimomouse, tempoCamera);



            }
            if (direcao == -transform.right)
            {

                distancia_X = Mathf.Lerp(-1, distancia_X, tempoCamera);
                ultimomouse = Mathf.Lerp(-1, ultimomouse, tempoCamera);


            }

            distancia_Z = Mathf.Pow(Mathf.Abs(distancia_X), 2) - 1;

        }

    }
  
}
/*
 * deve ser colocado em uma camera e referenciar o jogador
 * 
 */