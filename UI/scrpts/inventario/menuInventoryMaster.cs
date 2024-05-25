using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menuInventoryMaster : MonoBehaviour
{
    
    
    public InventoryManager managerMaster;

    public RectTransform OnlyWeapons, OnlyItens;
    public RectTransform SlotIten, IconIten;

    List<SlotIten> itensInDisplay = new List<SlotIten>();
    private void OnEnable()
    {
        updateDisplay();
    }

    Inventory selected;
    #region events
    public void selectIten(ScriptableIten a)
    {
        selected = managerMaster.find(a);
        Debug.Log( "o selecionado é nulo ?" + selected == null);
    }
    public void favoriteIten()
    {
        if (selected.data == null)
            return;
        selected.favorie = true;

    }
    public void equipIten()
    {
        if ( selected.data == null)
            return;
        managerMaster.equipIten(selected);
        updateDisplay();
    }
    #endregion
    #region crud
    void removeAll()
    {
        itensInDisplay.ForEach(x => Destroy(x.transform.parent.gameObject));
        itensInDisplay.Clear();
    }
    void updateDisplay()
    {
        removeAll();
        List<Inventory> _inventory = managerMaster._PlayerInventory();

        foreach(var x in _inventory)
        {
            if (x.eqipped)
                continue;

            GameObject a = Instantiate(SlotIten.gameObject, x.data.Equipavel == Equipable.Nenhum ? OnlyItens : OnlyWeapons);
            GameObject b = Instantiate(IconIten.gameObject, a.transform);

            SlotIten c = b.AddComponent<SlotIten>();
            c.inic(b.GetComponent<RawImage>(), this);
            c.addIcon(x);
            itensInDisplay.Add(c);
        }
    }
    #endregion
    
}