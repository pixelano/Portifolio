using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class botaoXCompra : MonoBehaviour
{
    public GerenciadorDeComercio origem;
    public itensEmEstoque data_;
    public RawImage botaoComprar;

    bool comp;
    public void attBotaoComprar(bool x)
    {
        botaoComprar.color = x ? Color.white : Color.grey;
        comp = x;
    }
    public void clickItem()
    {
        origem.itemEscolido(data_);
    }

    public void comprar()
    {
        if(comp)
        origem.comprar();
    }
}
