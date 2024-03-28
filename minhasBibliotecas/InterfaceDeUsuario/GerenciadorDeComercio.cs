using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NovoItemItemCataogo", menuName = "Itens/NovoItemItemCataogo", order = 1)]
public class itensEmEstoque : ScriptableObject
{
    public ScriptavelItemData itemData;
    public int valor;
    public GameObject obj;
    public bool disponivel;

    public List<recip> necessarios;
    [System.Serializable]
    public class recip {
        public ScriptavelItemData item;
        public int quantidade;
    }


}
public class GerenciadorDeComercio : MonoBehaviour
{
    public List<itensEmEstoque> ItensAVenda;
    public GameObject prefab;

    public bool LojaCraft;

    private itensEmEstoque escolido = null;

    public botaoXCompra botao;
    public API_Grid inventario;
    public void itemEscolido(itensEmEstoque dataItemEscolido)
    {
        escolido = dataItemEscolido;
        attBotaoComprar();
    }
    
    public void attBotaoComprar()
    {
        if (LojaCraft)
        {
            botao.attBotaoComprar(escolido.valor < Dinheiro.Carteira.valores);
        }
        else
        { bool temp = true;
            foreach(var a in escolido.necessarios)
            {
                temp = inventario.temEstaQuantidade(a.item.id_, a.quantidade);
                if (!temp)
                    break;
            }
            botao.attBotaoComprar(temp);

        }
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
