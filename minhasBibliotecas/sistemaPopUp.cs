using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class sistemaPopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

  

    public void abrir()
    {

        Debug.Log("a");
    }

    public void fechar()
    {


        Debug.Log("b");
    }
    public void att()
    {
      
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        abrir();


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fechar();
    }
}
