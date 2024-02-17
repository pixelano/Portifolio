using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class SlotInventario : MonoBehaviour,IDropHandler
{
    [HideInInspector]
    public API_Grid gerenciadorDaGrid;
    
    [HideInInspector]
    public bool empilhavel;
    public int id_Slot;
 
    public void OnDrop(PointerEventData eventData) {


        GameObject dropped = eventData.pointerDrag;
        DragAndDrop draggableItem = dropped.GetComponent<DragAndDrop>();

        if (empilhavel == false) {
            if (transform.childCount != 0)
                return;

            draggableItem.parentAfterDrag = transform;
            //tranferir poara outro inventario
            if (draggableItem.origem != gerenciadorDaGrid) {

                int temp = gerenciadorDaGrid.adicionarEsteItem(draggableItem, id_Slot);
                draggableItem.origem.removerEsteItem(draggableItem, false);
                try {
                    draggableItem.origem = gerenciadorDaGrid;
                    draggableItem.ID_pack = temp;
                 
                    gerenciadorDaGrid.temEsteItem(draggableItem).id_stack = id_Slot;

                } catch { }
            }
        }
        else {
            if (draggableItem.origem != gerenciadorDaGrid) {
                if (transform.childCount != 0) {
                    if (transform.GetChild(0).GetComponent<DragAndDrop>().ID_item != draggableItem.ID_item)
                        return;
                    //stakar

                    gerenciadorDaGrid.adicionarEsteItem(draggableItem, id_Slot);

                 
                    draggableItem.origem.removerEsteItem(draggableItem, true);
                  

                       
                  
                    // destruir item se ele for todo transferido
                } 
                else {
                    Debug.Log(id_Slot);
                    gerenciadorDaGrid.adicionarEsteItem(draggableItem, id_Slot);
                    draggableItem.origem.removerEsteItem(draggableItem, false);
                    draggableItem.parentAfterDrag = transform;
                    draggableItem.origem = gerenciadorDaGrid;
                    draggableItem.ID_pack = id_Slot;



                }

            }
            else {
                if (transform.childCount != 0) {
                    if (transform.GetChild(0).GetComponent<DragAndDrop>().ID_item != draggableItem.ID_item)
                        return;
                    //stakar

                    gerenciadorDaGrid.adicionarEsteItem(draggableItem, id_Slot);
                    draggableItem.origem.removerEsteItem(draggableItem, true);
                   
                    // destruir item se ele for todo transferido
                } else {
                   
                    draggableItem.parentAfterDrag = transform;
                    draggableItem.origem = gerenciadorDaGrid;

                    draggableItem.origem.temEsteItem(draggableItem).id_stack = id_Slot;
                    draggableItem.ID_pack = id_Slot;
                     }
            }
        }
     
    }

  

}


