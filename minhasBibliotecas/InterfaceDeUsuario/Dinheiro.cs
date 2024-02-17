using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinheiro : MonoBehaviour
{

    public static Dinheiro Carteira;

    public int valores;
    void Start()
    {
        if(Dinheiro.Carteira == null)
        {
            Dinheiro.Carteira = this;
        }
    }


}
