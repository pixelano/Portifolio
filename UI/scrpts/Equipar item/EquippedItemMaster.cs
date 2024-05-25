using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EquippedItemMaster : MonoBehaviour
{
    public InventoryManager managerMaster;

    public int ItensDisplayCount;
    public RectTransform SlotIten, IconIten, Rect_ListaItensInventario;

    #region controlar tela

     int RInd_x, RInd_y;
     int LInd_y;
    int FInd_x;
    private SlotIten f__current;
    public SlotIten f__Confirmar, f__Cancelar;
     stat RL_;

    enum stat
    {
        equipados,inventario,funcao
    }
    public void praCima()
    {
        switch (RL_)
        {
            case stat.inventario:
                if (RInd_y == 0)
                    return;

                itensInDisplay[(RInd_y * 3) + RInd_x].OnPointerExit(null);
                RInd_y--;// a lista da tela é de cima pra baixo então a array deve se comportar inversamente do que se fala no eixo y
                itensInDisplay[(RInd_y * 3) + RInd_x].OnPointerEnter(null);
                break;
            case stat.equipados:
                if (LInd_y == 0)
                    return;

                acheOSlotDeEquipado(LInd_y).slot.OnPointerExit(null);
                LInd_y--;
                acheOSlotDeEquipado(LInd_y).slot.OnPointerEnter(null);
                break;
            case stat.funcao:
                RL_ = stat.inventario;
                f__current.OnPointerExit(null);
                break;
        }
       
    }
    public void praBaixo()
    {
        switch (RL_)
        {
            case stat.inventario:
                if (RInd_y == 5)
                {
                    RL_ = stat.funcao;
                    return;
                }

                itensInDisplay[(RInd_y * 3) + RInd_x].OnPointerExit(null);
                RInd_y++;// a lista da tela é de cima pra baixo então a array deve se comportar inversamente do que se fala no eixo y
                itensInDisplay[(RInd_y * 3) + RInd_x].OnPointerEnter(null);
                break;
            case stat.equipados:
                if (LInd_y == 5)
                    return;

                acheOSlotDeEquipado(LInd_y).slot.OnPointerExit(null);
                LInd_y++;
                acheOSlotDeEquipado(LInd_y).slot.OnPointerEnter(null);
                break;
            case stat.funcao:

                f__current = FInd_x == 0 ? f__Cancelar : f__Confirmar;
                f__current.OnPointerEnter(null);
                break;
        }

      
    }
    public void praEsquerda()
    {
        switch (RL_)
        {
            case stat.inventario:
                if (RInd_x == 0)
                {
                    acheOSlotDeEquipado(LInd_y).slot.OnPointerEnter(null);
                    RL_ = stat.equipados;
                    return;
                }

                itensInDisplay[(RInd_y * 3) + RInd_x].OnPointerExit(null);
                RInd_x--;
                itensInDisplay[(RInd_y * 3) + RInd_x].OnPointerEnter(null);
                break;
            case stat.equipados:
               
                break;
            case stat.funcao:
                if (FInd_x == 0)
                    return;
                f__current.OnPointerExit(null);
                FInd_x = 0;
                f__current = FInd_x == 0 ? f__Cancelar : f__Confirmar;
                f__current.OnPointerEnter(null);

                break;
        }
       
    
    }
    public void praDireita()
    {
        switch (RL_)
        {
            case stat.inventario:
                if (RInd_x == 2)
                {
                    return;
                }

                itensInDisplay[(RInd_y * 3) + RInd_x].OnPointerExit(null);
                RInd_x++;
                itensInDisplay[(RInd_y * 3) + RInd_x].OnPointerEnter(null);
                break;
            case stat.equipados:
                acheOSlotDeEquipado(LInd_y).slot.OnPointerExit(null);
                RL_ = stat.inventario;
                break;
            case stat.funcao:
                if (FInd_x == 1)
                    return;
                f__current.OnPointerExit(null);
                FInd_x = 1;
                f__current = FInd_x == 0 ? f__Cancelar : f__Confirmar;
                f__current.OnPointerEnter(null);
                break;
        }
      
    }
    public void Confirmar()
    {
        switch (RL_)
        {
            case stat.inventario:
                if (itensInDisplay[(RInd_y * 3) + RInd_x].data == null)
                    return;

                itensInDisplay[(RInd_y * 3) + RInd_x].OnPointerClick(null);
                break;
            case stat.equipados:
                if (acheOSlotDeEquipado(LInd_y).slot.data == null)
                    return;

                acheOSlotDeEquipado(LInd_y).slot.OnPointerClick(null);
                break;
            case stat.funcao:
                ButtonConfirm();
                break;
        }

       
    }

  

    private EquippedSlot acheOSlotDeEquipado(int a)
    {
     

        if( a < 3)
        {
            return ItensPlayerShowEquip[a];
        }
        else
        {
            return ItensPetShowEquip[a - 3];
        }

    }
    #endregion

    #region inciadores de script
    private void Awake()
    {
        inicialize();
       
    }
   
    private void OnEnable()
    {
        AttDisplay();
    }
    #endregion

    #region   events
    bool selecDisplay;
    private Inventory selected;
    // clikou em um item
    public void SelectIten(ScriptableIten a , bool b)
    {

        if (selected != null) {
           if( selected.data == a)
            return;

            if (b)
            {
                SlotIten temp_ = itensInDisplay.Find(x => x.data == selected.data);
                if(temp_ == null)
                {
                    temp_ = ItensPlayerShowEquip.Find(x => x.slot.data == selected.data).slot;
                    if( temp_ == null)
                    {
                        
                              temp_ = ItensPetShowEquip.Find(x => x.slot.data == selected.data).slot;
                    }
                }
                temp_.isSelect = false;


            }
            else
            {

                EquippedSlot c = ItensPlayerShowEquip.Find(x => x.slot.data == selected.data);
                if (c.slot == null)
                {
                    ItensPetShowEquip.Find(x => x.slot.data == selected.data);
                }

                c.slot.isSelect = false;

            }
        }
        selected = managerMaster.find(a);
        selecDisplay = b;
        


    }
        //Button confir
    public void ButtonConfirm()
    {
      
        if (selected.data == null)
            return;
        itensInDisplay.Find(x => x.data == selected.data).isSelect = false;
        if (selecDisplay)
        {
         
            EquipItenEvent();
           
        }
      
      //  selected = null;
        AttDisplay();
    }
    //button cancel
    public void ButtonCancel()
    {
       
        if (selected.data == null)
            return;
        itensInDisplay.Find(x => x.data == selected.data).isSelect = false;
        if (selecDisplay)
        {
          
            HideEquip();
        }
        else
        {
      
            UnequipItenEvent();
        }
       // selected = null;
        AttDisplay();
    }
    
    [SerializeField]
    public List<EquippedSlot> ItensPlayerShowEquip, ItensPetShowEquip;
    public void EquipItenEvent() {

        if (selected.data.Equipavel == Equipable.Nenhum)
            return;

        EquippedSlot temp;

        temp = FindSlotIten(selected);
        if (temp.slot == null)
            return;

        managerMaster.equipIten(selected);
        equipIten(selected, temp);
    }
    private EquippedSlot FindSlotIten(Inventory a)
    {
        return a.data.Equipavel == Equipable.Jogador ? ItensPlayerShowEquip.Find(x => x.typeItem == a.data.TipoDeItem)
                        : ItensPetShowEquip.Find(x => x.typeItem == a.data.TipoDeItem);
    }
    private void equipIten(Inventory a , EquippedSlot b) {
        b.slot.addIcon(a);
    }
    public void HideEquip()
    {
        selected.hide();
      

    }
    public void UnequipItenEvent()
    {
        managerMaster.UneQuipIten(selected);
      

        EquippedSlot temp;

        temp = selected.data.Equipavel == Equipable.Jogador ? ItensPlayerShowEquip.Find(x => x.slot.data == selected.data)
            : ItensPetShowEquip.Find(x => x.slot.data == selected.data);

        temp.slot.removeIcon();
    }
    #endregion

    #region crud
    List<SlotIten> itensInDisplay = new List<SlotIten>();
    private void inicialize()
    {
        
        for(int x=0; x < ItensDisplayCount; x++)
        {
            GameObject a = Instantiate(SlotIten.gameObject, Rect_ListaItensInventario);
            GameObject b = a.transform.GetChild(0).gameObject;

            SlotIten c = b.AddComponent<SlotIten>();
          
            c.inic(b.GetComponent<RawImage>(), this);

            itensInDisplay.Add(c);

        }
    }
    private void AttDisplay()
    {

        //remover todos os icones atuais
        removeAllIcons();
        //botar os novos icones
        List<Inventory> _inventory =  managerMaster._PlayerInventory().FindAll(x=>x.favorie == false);
        List<Inventory> fav_inventory = managerMaster._PlayerInventory().FindAll(x => x.favorie);

        int c = 0;

        // mostrar as armas favoritas
        for(int x = 0;x < fav_inventory.Count && c < ItensDisplayCount; x++)
        {
            if (fav_inventory[x].eqipped || !fav_inventory[x]._showQui)
                continue;
            itensInDisplay[c].addIcon(fav_inventory[x]);

            c++;
        }
      
        
        // mostrar as ultimas armas pegas
        for (int x = _inventory.Count-1;x > 0
            && c <ItensDisplayCount; x--)
        {
          
            if (_inventory[x].eqipped || !_inventory[x]._showQui)
                continue;
            itensInDisplay[c].addIcon(_inventory[x]);

            c++;
            if (c > ItensDisplayCount)
                break;

        }

        List<Inventory> equipL = managerMaster._PlayerInventory().FindAll(x => x.eqipped == true);

        foreach(var a in equipL)
        {
            equipIten(a, FindSlotIten(a));
        }

    }
    private void removeAllIcons()
    {
        foreach(var a in itensInDisplay)
        {
            a.removeIcon();
        }
    }
    #endregion
}
[System.Serializable]
public struct EquippedSlot
{
    public SlotIten slot;
    public TypeItem typeItem;
}

// ¯\_(ツ)_/¯
