using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
public class rotaciorItemDisplayMouse : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject target;
    public rotacionarAutomatico _rotacionarAutomatico;
    public void OnBeginDrag(PointerEventData eventData)
    {
        _rotacionarAutomatico.rotacionar = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 temp = Input.mousePosition;
      //  temp.Normalize();
        Vector3 tempV = new Vector3(-temp.y, -temp.x, 0);
       
        direcao = tempV;
    }
    private void Update()
    {
        if (_rotacionarAutomatico.rotacionar == false && target != null)
        {
           
                target.transform.rotation = Quaternion.Euler(direcao);
          }
    }
    Vector3 direcao;
    public void OnEndDrag(PointerEventData eventData)
    {

        _rotacionarAutomatico.rotacionar = true ;
    }
}
