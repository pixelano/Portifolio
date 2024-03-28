using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Reorganizarporfiltro : MonoBehaviour
{
    bool nome, peso, valor, quantidade;
    InventarioMostrarItensEmLista _inventarioMostrarItensEmLista;
    int i_TipoDeItens;
    List<tipoDeItem> _tipoDeItens;
    private void Awake()
    {
      
        if(_inventarioMostrarItensEmLista == null)
        {
            _inventarioMostrarItensEmLista = FindAnyObjectByType<InventarioMostrarItensEmLista>();
        }
        _tipoDeItens = ((tipoDeItem[])Enum.GetValues(typeof(tipoDeItem))).ToList();

    }

    List<CelulaItemInvetarioLista> filhos;
    public void pegarfilhos()
    {
        filhos = _inventarioMostrarItensEmLista.transform.GetComponentsInChildren<CelulaItemInvetarioLista>().ToList();
       
    }
    public void mostrarTodos()
    {
        foreach(var a in _inventarioMostrarItensEmLista.ListaDeItensInventario)
        {
            a.SetActive(true);
        }
    }
    public void OrganizarPorNome()
    {
        pegarfilhos();
        mostrarTodos();
        if (nome)
        {
            filhos.Sort((x, y) => x.data.data.NomeDoItem.CompareTo(y.data.data.NomeDoItem));
        }
        else
        {
            filhos.Sort((x, y) => y.data.data.NomeDoItem.CompareTo(x.data.data.NomeDoItem));
        }
        for (int x = 0; x < filhos.Count; x++)
        {
            filhos[x].transform.SetSiblingIndex(x);
        }
        nome = !nome;
    }
    public void OrganizarPorTipo()
    {
        mostrarTodos();
        foreach(var a in _inventarioMostrarItensEmLista.ListaDeItensInventario)
        {
            if(a.GetComponent<CelulaItemInvetarioLista>().data.data.tipoDoItem != _tipoDeItens[i_TipoDeItens])
            {
                a.SetActive(false);
            }
        }
        i_TipoDeItens++;
        if (i_TipoDeItens > _tipoDeItens.Count - 1)
        {
            i_TipoDeItens = 0;
        }
    }
    public void OrganizarPorPeso()
    {
        pegarfilhos();
        mostrarTodos();
        if (peso)
        {
            filhos.Sort((x, y) => x.data.data.PesoDoItem.CompareTo(y.data.data.PesoDoItem));
        }
        else
        {
            filhos.Sort((x, y) => y.data.data.PesoDoItem.CompareTo(x.data.data.PesoDoItem));
        }
        for (int x = 0; x < filhos.Count; x++)
        {
            filhos[x].transform.SetSiblingIndex(x);
        }
        peso = !peso;

    }
    public void OrganizarPorValor()
    {
        pegarfilhos();
        mostrarTodos();
        if (valor)
        {
            filhos.Sort((x, y) => x.data.data.ValorDoItem.CompareTo(y.data.data.ValorDoItem));
        }
        else
        {
            filhos.Sort((x, y) => y.data.data.ValorDoItem.CompareTo(x.data.data.ValorDoItem));
        }
        for (int x = 0; x < filhos.Count; x++)
        {
            filhos[x].transform.SetSiblingIndex(x);
        }
        valor = !valor;
    }

    public void OrganizarPorQuantidade()
    {


        pegarfilhos();
        mostrarTodos();
        if (quantidade)
        {
            filhos.Sort((x, y) => x.data.quantidade.CompareTo(y.data.quantidade));
        }
        else
        {
            filhos.Sort((x, y) => y.data.quantidade.CompareTo(x.data.quantidade));
        }
        for (int x = 0; x < filhos.Count; x++)
        {
            filhos[x].transform.SetSiblingIndex(x);
        }
        quantidade = !quantidade;
    }
    
}
