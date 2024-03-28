using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
   public static Inventario main;
    public bool jogador;
    [SerializeField]
    public InventarioScriptavel _inventario;
    private void Awake()
    {
        if(jogador)
        {
            Inventario.main = this;
        }
      
    }

    public void adicionarItem(ScriptavelItem a, int b)
    {
        if (verificarSeTem(a))
        {
            acheEsteItem(a).quantidade += b;
        }
        else
        {

            slotInventario temp = new slotInventario();
            temp.data = a;
            temp.quantidade = b;
            _inventario.ItensInventario.Add(temp);

        }
    }
    public bool verificarSeTem(ScriptavelItem a)    {        return _inventario.ItensInventario.Exists(x=>x.data == a);        }
    public bool verificarSeTem(ScriptavelItem a , int b)
    {
        slotInventario temp = _inventario.acheEsteIten(a);
        if (temp != null)
        {
            return temp.quantidade >= b;

        }
        else
        {
            return false;
        }
    }
    public slotInventario acheEsteItem(ScriptavelItem a)
    {
        return _inventario.ItensInventario.Find(x => x.data == a);
    }
}
[System.Serializable]
public class slotInventario {
    public ScriptavelItem data;
    public int quantidade =0; 
}
