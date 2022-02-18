using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraC_ : MonoBehaviour
{
    public bool teste;
   
    float camSpeed = -0.5f;
    public float x, y, aa;
    
    public float sensibilidade;
    Vector3 rotateValue;

    // public float pegaeixo(char a) { if (a == 'x') { return x; } else { return y; } }
                 
    void giracorpo()
    {
        x = Input.GetAxis("Mouse X")* sensibilidade;
        y = Input.GetAxis("Mouse Y")* (sensibilidade/1.5f);

        rotateValue = new Vector3(y, 0, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;
        transform.eulerAngles += rotateValue * camSpeed;

        GetComponentInParent<chamador>().transform.Rotate(new Vector3(0, x, 0));
      
    }

     void Start()
    {
        sensibilidade = 1;
    }
     void Update()
    {
       
        giracorpo();
    }
    
}
