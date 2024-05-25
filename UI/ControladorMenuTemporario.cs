using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ControladorMenuTemporario : MonoBehaviour
{
    public UnityEvent ParaCima,ParaBaixo;
    public UnityEvent EscolherDireita, EscolherEsquerda;
    public UnityEvent Pressconfirmar, Upconfirmar,DowConfirmar;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ParaCima.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ParaBaixo.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EscolherEsquerda.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            EscolherDireita.Invoke();}
        if (Input.GetKey(KeyCode.F))
        {

            Pressconfirmar.Invoke();
           
        }
        if (Input.GetKeyUp(KeyCode.F))
        {

            Upconfirmar.Invoke();

        }
        if (Input.GetKeyUp(KeyCode.F))
        {

            DowConfirmar.Invoke();

        }
    }
 

}
