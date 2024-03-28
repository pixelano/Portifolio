using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SeparadorPorCategoria : MonoBehaviour
{
    InventarioMostrarItensEmLista _InventarioMostrarItensEmLista;
    private void Awake()
    {
         if (_InventarioMostrarItensEmLista == null)
        {
            _InventarioMostrarItensEmLista = FindAnyObjectByType<InventarioMostrarItensEmLista>();
        }
        foreach (var a in categorias)
        {
            if (a.icone_Botao)
            {
                a.icone_Botao.GetComponent<AUXSeparadorPorCategoria>().origem = this;
            }
            else
            {
                a.icone_Botao = Instantiate(modeloPrefab, transform);
                AUXSeparadorPorCategoria temp = a.icone_Botao.GetComponent<AUXSeparadorPorCategoria>();
                temp.origem = this;
                temp._categoria = a;
                if (a.icone)
                {
                   
                    temp.botarIcone(a.icone);
                }
                else
                {
                    temp.botarLetra(a.nomeCategoria);
                }
            }
        }
    }
    public GameObject modeloPrefab;

    [SerializeField]
    public List<categoria> categorias;
    
    public void executarFiltro(categoria a)
    {
        _InventarioMostrarItensEmLista.removertodos();
        _InventarioMostrarItensEmLista.botarTodos();
    

        foreach(var c in _InventarioMostrarItensEmLista.ListaDeItensInventario)
        {
            if(
                a.TuposDeitensNaCategoria.Contains(c.GetComponent<CelulaItemInvetarioLista>().data.data.tipoDoItem))
            {
                c.SetActive(true);
            }
            else
            {
                c.SetActive(false);
            }
        }
      _InventarioMostrarItensEmLista.ListaDeItensInventario.RemoveAll(x => x.active == false);
    }


}
[System.Serializable]
public class categoria
{

    public Texture icone;
    public string nomeCategoria;
    public List<tipoDeItem> TuposDeitensNaCategoria;
    public GameObject icone_Botao;

}
