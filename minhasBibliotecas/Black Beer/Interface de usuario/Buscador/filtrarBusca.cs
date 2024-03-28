using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class filtrarBusca : MonoBehaviour
{
    InventarioMostrarItensEmLista _inventarioMostrarItensEmLista;
    private void Awake()
    {
       
        if (_inventarioMostrarItensEmLista == null)
        {
            _inventarioMostrarItensEmLista = FindAnyObjectByType<InventarioMostrarItensEmLista>();
        }
    }
    public static string[] TokenizarPorEspacos(string texto)
    {
        string[] tokens = texto.Split(' ');

        return tokens;
    }

    public void OnValueChanged(string text)
    {
        if (string.IsNullOrEmpty(text) ){

            foreach (var a in _inventarioMostrarItensEmLista.ListaDeItensInventario)
            {
                a.SetActive(true);
            
            }
            }
        else
        {

            foreach(var a in _inventarioMostrarItensEmLista.ListaDeItensInventario)
            {
                if (!compararPalavras(a.GetComponent<CelulaItemInvetarioLista>().data.data.NomeDoItem.ToLower(), text.ToLower()))
                {
                    a.SetActive(false);
                }
                else
                {
                    a.SetActive(true);
                }
            }
           
        }
    }
    bool compararPalavras(string palavraCompleta , string ParteDePalavra)
    {
       
        return palavraCompleta.StartsWith(ParteDePalavra, StringComparison.OrdinalIgnoreCase);
    }
}
