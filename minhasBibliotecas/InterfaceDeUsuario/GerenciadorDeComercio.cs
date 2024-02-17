using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorDeComercio : MonoBehaviour
{
    public List<itensEmEstoque> ItensAVenda;
    public GameObject prefab;
  
    [System.Serializable]
    public class itensEmEstoque {
        public ScriptavelItemData itemData;
        public int valor;
        public GameObject obj;
        public bool disponivel; // implementar

    }
    private itensEmEstoque escolido = null;

    public botaoXCompra botao;
    public void itemEscolido(itensEmEstoque dataItemEscolido)
    {
        escolido = dataItemEscolido;
        attBotaoComprar();
    }
    
    public void attBotaoComprar()
    {
        botao.attBotaoComprar(escolido.valor < Dinheiro.Carteira.valores);
    }
    public void comprar()
    {
        Dinheiro.Carteira.valores -= escolido.valor;
        Debug.Log("comprou algo");

        // programar o que faz asim que compra
            attBotaoComprar();
    }

    private void OnEnable()
    {
        foreach(var a in ItensAVenda)
        {
            a.obj = Instantiate(prefab, transform);
            a.obj.GetComponent<RawImage>().texture = a.itemData.texture_;
            botaoXCompra x = a.obj.GetComponent<botaoXCompra>();
            x.data_ = a;
            x.origem = this;
        } 
    }

    private void OnDisable()
    {
        foreach (var a in ItensAVenda)
        {
            Destroy(a.obj);
         
        }
    }
}
