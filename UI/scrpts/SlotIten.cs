using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotIten : MonoBehaviour , IPointerClickHandler,IPointerEnterHandler, IPointerExitHandler
{
    public RawImage icon;
    public ScriptableIten data;
    public EquippedItemMaster master_equip;
    public menuInventoryMaster master_menuInventory;
    public bool isDisplay = false;
    public void inic(RawImage _icon,EquippedItemMaster _master)
    {
        icon = _icon;
        isDisplay = true;
        master_equip = _master;
        r = transform.parent.GetChild(1).GetComponent<RawImage>();

    }
    public void inic(RawImage _icon, menuInventoryMaster _master)
    {
        icon = _icon;
        isDisplay = true;
        master_menuInventory = _master;
        r = transform.parent.GetChild(1).GetComponent<RawImage>();
        
       
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (master_equip != null)
        {
            master_equip.SelectIten(data, isDisplay);
        }
        if(master_menuInventory != null)
        {
            master_menuInventory.selectIten(data);
        }
        isSelect = true;
    }
    public void removeIcon()
    {
        data = null;
        isSelect = false;
        icon.color = Color.clear;
        
    }
    public void addIcon(Inventory a)
    {
        data = a.data;
        icon.texture = a.data.icon;
        icon.color = Color.white;
        // adicionar a quantidade
    }

    RawImage _r;
    private RawImage r
    {
        get
        {
            if(_r == null)
            {
                _r = transform.parent.GetChild(1).GetComponent<RawImage>();
            }
            return _r;
        }
        set
        {
            _r = value;
        }
    }
    private float f;
    private bool _isSelect;
    public bool isSelect
    {
        get => _isSelect;
        set
        {

            _isSelect = value;
            if (cor != null) StopCoroutine(cor);
            if (value == false)
            {
            
                f = 0;
                
            }
            else
            {
               
                f = 0.5f;
            }
            cor = StartCoroutine(lerp());
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelect)
            return;

        if(cor != null)StopCoroutine(cor);
        f = 0.2f;
        cor = StartCoroutine(lerp());
        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelect)
            return;

        if (cor != null) StopCoroutine(cor);
        f = 0;
        cor = StartCoroutine(lerp());
     
    }
    Coroutine cor;
    IEnumerator lerp()
    {
        Color c = r.color;
       
        while (Mathf.Abs( r.color.a - f )> 0.01f)
        {
           c = r.color;
            c.a += f > c.a ? 0.03f : -0.02f;
          
            r.color = c;

            yield return null;
        }
      
        c.a = f ;

        r.color = c;
    }

   
}
