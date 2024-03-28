using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class CelulaItemInvetarioLista : MonoBehaviour
{
    public slotInventario data;
    public TextMeshProUGUI NomeDoItem, tipoDoItem, ValorDoItem, PesoDoItem,QuantidadeDeItens;
    public GerenciamentoDeInventario gerenciadordeinventario;
    private void Start()
    {

        attdados();
    }
    public void attdados()
    {
        if (data.quantidade <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
        if (NomeDoItem)
        {
            NomeDoItem.text = data.data.NomeDoItem;
        }
        if (tipoDoItem)
        {
            tipoDoItem.text = "" + data.data.tipoDoItem.ToString();
        }
        if (ValorDoItem)
        {
            ValorDoItem.text = "" + data.data.ValorDoItem;
        }
        if (PesoDoItem)
        {
            PesoDoItem.text = "" + data.data.PesoDoItem;
        }
        if (QuantidadeDeItens)
        {
            QuantidadeDeItens.text = "" + data.quantidade;
        }
   }
    
    public void selecionar_()
    {
        gerenciadordeinventario.selecionado = data;
    }
}
