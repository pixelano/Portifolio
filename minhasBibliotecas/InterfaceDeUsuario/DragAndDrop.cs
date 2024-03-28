using System.Collections;
using System.Collections.Generic;
using TMPro;

//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IItemDeInventario
{
    public Image image;
    public API_Grid origem;
    public TextMeshProUGUI text;
    public bool draged;
    private void Start()
    {
        attQ(origem.acheEsteItem(ID_pack).quantidade);
    }

    public void attQ(int x)
    {
        text.text = "" + x;
    }
    [HideInInspector] public Transform parentAfterDrag;
    public RectTransform pref;

   public void OnBeginDrag(PointerEventData eventData){
        if (Input.GetMouseButton(0))
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.parent.parent.parent.parent);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
            draged = true;
        }
        else
        {
            if (origem.acheEsteItem(ID_pack).quantidade <= 1)
                return;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.parent.parent.parent.parent);
            transform.SetAsLastSibling();
         
            draged = true;

            GameObject a = Instantiate(this.gameObject, parentAfterDrag);
            image.raycastTarget = false;    
            API_Grid.inventarioSlot x = origem.acheEsteItem(ID_pack);
            int QM = (int)(x.quantidade / 2);
            x.quantidade -= QM;
            API_Grid.inventarioSlot novo = origem.clonar(x);
            novo.id_stack = -1;
            ID_pack = -1;
            origem.inventario.Add(novo);
            novo.quantidade = QM;
            attQ(QM);

            //

        }
     
    }
   public void OnDrag(PointerEventData eventData){
        if (draged)
        {
            transform.position = Input.mousePosition;
        }
      
    }
   public void OnEndDrag(PointerEventData eventData){

        if (draged) 
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
            draged = false;
        }
    }

       public void definirIcone(Image a){
        image = a;
       }
       public int ID_item;
       public int ID_pack;
    public int nomeDoItem(){return ID_item;}
}
