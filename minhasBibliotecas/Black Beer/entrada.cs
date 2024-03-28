using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entrada : MonoBehaviour
{
    public static entrada instact;
    private void Awake()
    {
        if (!entrada.instact)
        {
            entrada.instact = this;
        }
        else
        {
            Destroy(this);
        }
            
    }

    public bool pegarItem()
    {
        return Input.GetKey(KeyCode.F);
    }
    public bool alterarJanelas()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }

}
