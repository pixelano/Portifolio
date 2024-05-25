using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    public ScriptableIten data;
    public int amount;

    public bool equipped;
    public bool eqipped { get => equipped;}

    private bool ShowQui;
    public bool _showQui {
        get => ShowQui;
    }

    
    public bool favorie;

    public Inventory(ScriptableIten a)
    {
        data = a;
        equipped = false;
        amount = 1;
        ShowQui = a.Equipavel != Equipable.Nenhum ? true : false;

    }
    public void equiped(bool a)
    {
        equipped = a;
        ShowQui = !a;

    }
    public void hide()
    {
        ShowQui = false;
    }
}
 