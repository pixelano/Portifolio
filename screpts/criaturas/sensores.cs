using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensores : MonoBehaviour
{
    /*
     * botar os membros em uma layer diferente que os sensores estão batendo neles ou
     * fazer com que os sensores ignorem os membros <---- melhor opção de codigo
     * 
     */


   public bool _collidindo_comColider,temchaum1,temteto1;
    chamador chamar;

    bool _temchao;
    

    public bool tem_frente() { if ((Physics.Raycast(transform.position, transform.forward, (transform.localScale.x * 0.2f)))){ return true; } else { return false; } }
    public bool tem_esquerda() { if (Physics.Raycast(transform.position, -transform.right, (transform.localScale.z * 0.2f))) { Debug.DrawRay(transform.position, transform.forward,Color.blue);  return true; } else { return false; } }
    public bool tem_direita() { if (Physics.Raycast(transform.position, transform.right , (transform.localScale.z * 0.2f))) { Debug.DrawRay(transform.position, -transform.forward, Color.blue); return true; } else { return false; } }
    public bool tem_atras() { if (Physics.Raycast(transform.position, -transform.forward , (transform.localScale.x * 0.2f))) {return true; } else { return false; } }
    public bool temchao()
    {
        if(chamar.gravidade().distancia == 0)
        {
             return true;    
           //_temchao = true;
        }
        else { return false; 
            //_temchao = false;
            
           }  
    }

    public bool temteto()
    {
        if (Physics.Raycast(transform.position, transform.up, (transform.localScale.y * 0.8f)))
        {
            return true;
            //_temchao = true;
        }
        else
        {
            return false;
            //_temchao = false;

        }
    }

    void Update()
    {

        temchaum1 = temchao();
        temteto1 = temteto();

    }
    void Start()
    {
        
        chamar = GetComponent<chamador>();
    }
}
