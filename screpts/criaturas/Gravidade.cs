using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravidade : MonoBehaviour
{

    private chamador chamar;
    public float massa,distancia;
    private Vector3 referencialGravitacional,orientacaio_gravitacional, direcao, adicionarforca_;
    private Vector3 forca_gravitacional;
    private bool flag;
    private RaycastHit hit;
   
    private void Start()
    {
        adicionarforca_ = Vector3.zero;
           orientacaio_gravitacional = -transform.up;

        flag = true;

        if (GetComponent<chamador>()) {
            chamar = GetComponent<chamador>();
        }
    }
    public void atualizarOrientacaoGravitacional(Vector3 aux)
    {
        orientacaio_gravitacional = aux;
    }

    public void pegarDirecaoGravidade()
    {
        referencialGravitacional = Vector3.zero;
        massa = chamar.estatus().Massa();


        Physics.BoxCast(transform.position -orientacaio_gravitacional, transform.localScale * 0.9f,
            orientacaio_gravitacional, out hit,new Quaternion(0,0,0,0), 1200);

        direcao = hit.collider.ClosestPoint(transform.position);
                
        distancia = hit.distance;
        if (distancia < 1)
        {
            distancia = 0;
        }

        referencialGravitacional.x = (direcao.x * orientacaio_gravitacional.x);
        referencialGravitacional.y = (direcao.y * orientacaio_gravitacional.y);
        referencialGravitacional.z = (direcao.z * orientacaio_gravitacional.z);
       

    }
    public void gravitacionar()
    {
        pegarDirecaoGravidade();
        //  forca_gravitacional = -transform.up * (chamar.estatus().forca() * chamar.estatus().Massa());

        forca_gravitacional = referencialGravitacional * ((10 * massa)/5);

        
        chamar.characterC().Move(((((forca_gravitacional* (1+((distancia+20) * 0.1f))) + adicionarforca_))/ 333)+ chamar.movimentacao().direcao);
        // forca_gravitacional  / 5000
     // Debug.Log("a soma de dividido pela constante"+forca_gravitacional + " com " + adicionarforca_ + "é de : " + (((((forca_gravitacional * (1 + ((distancia + 20) * 0.1f))) + adicionarforca_)) / 500)));
       
        
        if (adicionarforca_.x > 1 || adicionarforca_.z > 1 || adicionarforca_.y > 1)
        {
           
            adicionarforca_ -= -orientacaio_gravitacional*0.5f;

        }
        Debug.Log("gravidade "+((forca_gravitacional * (1 + ((distancia + 20) * 0.1f)))) +" forca do puçp"+adicionarforca_+((((forca_gravitacional * (1 + ((distancia + 20) * 0.1f))) + adicionarforca_)) / 333));
        if (Vector3.Distance(Vector3.zero , (forca_gravitacional * (1 + ((distancia + 20) * 0.1f)))) > Vector3.Distance(Vector3.zero, adicionarforca_))
        {

   
               adicionarforca_ += orientacaio_gravitacional *3 ;
        }
        if (adicionarforca_.y< 0)
        {
            adicionarforca_ = Vector3.zero;
        }

    }
    public void adicionarforca(Vector3 direcao,float forca)
    {


        //  float aux = (forca - Mathf.Cos(forca_gravitacional.y) * massa * 10)*1  ;
        float aux = forca;   
            
            direcao *= aux;
   
            adicionarforca_ += direcao;



    }


    
    private void Update()
    {
         gravitacionar();
        
       
    }
    public void OnDrawGizmos()
    {

        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawWireCube(direcao,new Vector3(0.5f,0.5f,0.5f));
        Gizmos.DrawWireCube(transform.position - orientacaio_gravitacional, new Vector3(0.2f,55f, 0.2f));
        
        

    }

}
