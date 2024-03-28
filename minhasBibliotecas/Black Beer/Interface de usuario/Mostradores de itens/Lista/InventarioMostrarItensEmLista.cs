using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioMostrarItensEmLista : MonoBehaviour , IMostradorDeItens
{
    public GameObject modeloDaLista;
    public InventarioScriptavel inventario_;
    public List<GameObject> ListaDeItensInventario = new List<GameObject>();
    public GerenciamentoDeInventario _gerenciadordeinventario;
    private void OnEnable()
    {
      
        botarTodos();
    }
    private void OnDisable()
    {

        removertodos();
    }
    public void atualizar()
    {
        removertodos();
        botarTodos();
    }
    public void botarTodos()
    {
        foreach (var a in inventario_.ItensInventario)
        {
            GameObject temp = Instantiate(modeloDaLista, transform);
            CelulaItemInvetarioLista _temp = temp.GetComponent<CelulaItemInvetarioLista>();
            _temp.data = a;

            _temp.gerenciadordeinventario = _gerenciadordeinventario;
            ListaDeItensInventario.Add(temp);
        }
    }
    public void removertodos()
    {
        while (ListaDeItensInventario.Count > 0)
        {
            Destroy(ListaDeItensInventario[0]);
            ListaDeItensInventario.RemoveAt(0);
        }
    }
    public void attvalores()
    {
        ListaDeItensInventario.RemoveAll(x => x == null);
        foreach (var a in ListaDeItensInventario)
        {
            a.GetComponent<CelulaItemInvetarioLista>().attdados();
        }
        ListaDeItensInventario.RemoveAll(x => x == null);
    }
    public InventarioScriptavel _inventario()
    {
        return inventario_;
    }


}
