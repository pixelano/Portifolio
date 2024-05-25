using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MenuShowItens : MonoBehaviour
{
    public InventoryManager managerMaster;
    int iShowIten;
    public List<category> showitens_ = new List<category>();
    
    public class category
    {
        public GameObject pivo_;
        public List<SlotIten3D> obj = new List<SlotIten3D>();
    }
 
    public int lengVert, lengHor = 5;
    public float spacin_slot, spacin_;
    public Transform pivo;
    public GameObject pref;
    float spacing;
    int Ymin, Ymax;
    int _y, _x;

    SlotIten3D select;
    private void OnEnable()
    {
       List<Inventory> _inventory = managerMaster._PlayerInventory();
        ShowListItens(_inventory);
        Ymax = lengHor;
         spacing = pref.transform.lossyScale.x + spacin_slot;
    }
    public void ShowListItens(List<Inventory> a)
    {
        float x_ = showitens_.Count ==0 ? pivo.transform.position.x : showitens_[showitens_.Count -1].pivo_.transform.position.x;
        float y_ = pivo.transform.position.y;

        category c = new category();
        c.pivo_ = new GameObject();
        c.pivo_.transform.parent = pivo.transform;
        c.pivo_.transform.position = new Vector3(x_, y_, 0);

        float x__;
            x__ = x_;

        for(int d = 0; d < a.Count; d++)
        {
           
           
            if ( d!= 0 && d % lengHor == 0)
            {
                x__ = x_;
                y_ -= spacin_slot*3;
            }

            SlotIten3D sl = Instantiate(pref, c.pivo_.transform).AddComponent< SlotIten3D>();  
            sl.transform.position = new Vector3(x__, y_, 0);
            c.obj.Add(sl);
            sl.def(a[d]);
            x__ += spacing;
        }

    }
    public void refresh()
    {
     // fazer depoisç
    }

    public void prabaixo()
    {

    }
    public void praCima()
    {
        if (_y == 0)
            return;
        _y -= lengHor;
        _y = Mathf.Abs(_y);

        if(_y < Ymin)
        {
            Ymin--;
            Ymax--;
            MoverTelaPara(Vector3.down * spacing);
        }

        if(select != null)
        {
            select.OnPointerExit();
        }
       // select = showitens_[iShowIten].

    }
    public void praEsquerda()
    {

    }
   
    public void praDireita()
    {

    }
    public void Confirmar()
    {

    }
   void MoverTelaPara(Vector3 a)
    {
        pivo.transform.position += a;
            
    }
}
public class SlotIten3D :MonoBehaviour
{
    Inventory data;
    public bool aaa;
    Renderer _icon;
    Renderer icon {
        get
        {
            if(_icon == null)
            {
                _icon = transform.GetChild(0).GetComponent<Renderer>();

            }
            return _icon;
        }
        set {

            _icon = value;
           

        }
    }
    Renderer _isSelect;
    Renderer isSelect
    {
        get
        {
            if (_isSelect == null)
            {
                _isSelect = transform.GetChild(1).GetComponent<Renderer>();

            }
            return _isSelect;
        }
        set
        {

            _isSelect = value;


        }
    }

    Renderer _isEquip;
    Renderer isEquip
    {
        get
        {
            if (_isEquip == null)
            {
                _isEquip = transform.GetChild(0).GetComponent<Renderer>();

            }
            return _isEquip;
        }
        set
        {

            _isEquip = value;


        }
    }
    Coroutine equip,select;

    private void Update()
    {
        if(aaa == true)
        {
            aaa = false;
            OnPointerEnter();
        }
    }
    public void def(Inventory d)
    {
        data = d;

        icon.material.SetTexture("_MainTex", data.data.icon);
        icon.material.SetColor("_Color", Color.white);

       
    }

    IEnumerator lerpR(Renderer a ,float b)
    {

        while(Mathf.Abs( a.material.GetColor("_Color").a - b) > 0.01f)
        {
            Color c = a.material.GetColor("_Color");
            c.a = Mathf.Lerp(c.a, b, 0.2f);
            a.material.SetColor("_Color", c);
            yield return null;
        }
        Color c_ = a.material.GetColor("_Color");
        c_.a = b;
        a.material.SetColor("_Color", c_);
    }
    public void IsEquip(bool a)
    {
        if (select != null)
        {
            StopCoroutine(select);
        }
        select = StartCoroutine(lerpR(isEquip, a ? 1 : 0));
    }
    public void OnPointerClick()
    {
        if (equip != null)
        {
            StopCoroutine(equip);
        }
        equip = StartCoroutine(lerpR(isSelect, 0.5f));
    }

    public void OnPointerEnter()
    {
        if (equip != null)
        {
            StopCoroutine(equip);
        }
        equip = StartCoroutine(lerpR(isSelect, 0.2f));
    }

    public void OnPointerExit()
    {
        if (equip != null)
        {
            StopCoroutine(equip);
        }
        equip = StartCoroutine(lerpR(isSelect,0));
    }
}