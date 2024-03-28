using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NovoInventario", menuName = "BlackBeer/Inventario/NovoInventario", order = 2)]
public class InventarioScriptavel : ScriptableObject
{
    public List<slotInventario> ItensInventario;
    public float dinheiro;

    public slotInventario acheEsteIten(ScriptavelItem a)
    {
        return ItensInventario.Find(x => x.data == a);
    }
    public bool adicionarIten(slotInventario a, int q)
    {
        slotInventario temp = acheEsteIten(a.data);
        if(temp == null)
        {
            temp = new slotInventario();
            temp.data = a.data;
            temp.quantidade = q == 0 ? a.quantidade : q ;
            ItensInventario.Add(temp);
        }
        else
        {
            temp.quantidade += q == 0 ? a.quantidade : q;
        }
        return true;
    }
    public bool removerIten(slotInventario a , int q)
    {
        slotInventario temp = acheEsteIten(a.data);
        if (temp == null)
        {
            return false;
        }
        else
        {
            temp.quantidade -= q == 0?  a.quantidade : q;
        }
        if (temp.quantidade <= 0)
            ItensInventario.Remove(temp);
        return true;
    }
}
