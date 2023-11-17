using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class minhavida
{
    public float vida_maxima, armadura_maxima;
    public float vida =1 , armadura =1, defender;
    private bool defendendo;

    public float decompor = 100;

    public bool decompondo()
    {
        decompor -= Time.deltaTime;
    if(decompor < 0)
        {
           return true;
        }
        else
        {
            return false;

        }
    }


    public float retonavida() { return vida; }
    public void SeDefender(bool aux,float valor)
    {
        defendendo = aux;
        defender = valor;
        

    }

    public void recuperarVida(float vida_)
    {
        float aux = vida_ + vida;

        vida = aux > vida_maxima ? vida_maxima: aux;
    }

    public bool tomarDano(float dano)
    {
        
        dano = defendendo ? dano - defender : dano;

        dano = dano < 1 ? 1 : dano;

       float danoPosArmadura = (100 / (100 + armadura)) * dano;
       float danoVida = dano / (dano - danoPosArmadura + 1);

        armadura = armadura - danoPosArmadura < 0 ? 0 : armadura - danoPosArmadura;
        vida -= danoVida < 0 ? 0 : danoVida;

        return vida < 0;

    }
    public void configurar(float voda,float arma)
    {
        vida = voda;
        armadura = arma;
        vida_maxima = voda;
        armadura_maxima = arma;
    }
    public bool estavivo()
    {
        return vida >= 0;
    }
}
