using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<ScriptableIten> LoadItems;
    [SerializeField]
    List<Inventory> PlayerInventory = new List<Inventory>();

    private void Awake()
    {
        loadInventory();
    }

    // 
    public void AddItem(ScriptableIten a)
    {
        var b = PlayerInventory.Find(x => x.data == a);
        if(b != null)
        {
            b.amount++;
            PlayerInventory.Remove(b);
            PlayerInventory.Insert(PlayerInventory.Count - 1, b);
        }
        else
        {
            Inventory temp = new Inventory(a);
            PlayerInventory.Add(temp);

        }

    }
    public void RemoveItem(ScriptableIten a)
    {
        var b = PlayerInventory.Find(x => x.data == a);
        if (b != null)
        {
            b.amount--;
        }
        else
        {
            Debug.LogError("tentou remover um item que não tinha");

        }
    }
     public Inventory find(ScriptableIten a)
    {
        return PlayerInventory.Find(x => x.data == a);
    }
    //
    public List<Inventory> _PlayerInventory()
    {
        return PlayerInventory;
    }

    public void equipIten(Inventory a)
    {
        Inventory temp = PlayerInventory.Find(x => x.eqipped && x.data.Equipavel == a.data.Equipavel &&
        x.data.TipoDeItem == a.data.TipoDeItem);

        if(temp != null)
        {
            Debug.Log("achou o item anterior e o desequipou");
            temp.equiped(false);
        }
        a.equiped(true);
    }
    public void UneQuipIten(Inventory a)
    {
        Debug.Log("desequipou");
        a.equiped(false);
    }

    #region save/load
    private void loadInventory()
    {foreach(var a in LoadItems)
        {
            var b = PlayerInventory.Find(x => x.data == a);
            if (b != null)
            {
                b.amount++;
             
            }
            else
            {
                Inventory temp = new Inventory(a);
                PlayerInventory.Add(temp);

            }
        }
        
    }
    private void SaveInventory()
    {

    }
    #endregion
}

// ¯\_(ツ)_/¯