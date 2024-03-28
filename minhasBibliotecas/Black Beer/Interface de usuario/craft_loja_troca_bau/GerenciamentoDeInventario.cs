using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GerenciamentoDeInventario : MonoBehaviour
{
    public InventarioMostrarItensEmLista mostradorDeInventario;
    public InventarioScriptavel inventarioPrincipal, inventarioSecundari;



    public slotInventario selecionado;
           slotInventario _selecionado;
    // aqui voce define o que sera feito
    public acaoDeInventario operacao;
    private acaoDeInventario _operacao;
    public RectTransform localDosBotoes;
    public GameObject prefabbotao;
    public TextMeshProUGUI titulo_;

    public rotaciorItemDisplayMouse displayDeIten;
    GameObject itemDisplay;
    private void Awake()
    {
        if(mostradorDeInventario == null)
        {
            mostradorDeInventario = FindObjectOfType<InventarioMostrarItensEmLista>();
        }
    }

    private void Update()
    {
        if (!ladoatual().ItensInventario.Contains(_selecionado))
        {
            selecionado = ladoatual().ItensInventario[0];
        }
        if (_operacao != operacao)
        {
            _operacao = operacao;
            attbotoes();
            mostrarEEsconderBotoes();
            attTitulo();
        }
        if(_selecionado != selecionado)
        {
            _selecionado = selecionado;
            botaoDeSelecao();
            displayDeItem();

        }

        if (_operacao != acaoDeInventario.inventario)
            trocarEntreInventarios();
        return;

        switch (_operacao)
        {
            case acaoDeInventario.craftar:
                trocarEntreInventarios();
                break;
            case acaoDeInventario.inventario:
                break;
            case acaoDeInventario.trocar:
                trocarEntreInventarios();
                break;
            case acaoDeInventario.vender:
                trocarEntreInventarios();
                break;
        }
    }
    void botaoDeSelecao()
    {
        if (selecionado.data == null)
            return;
        foreach (var a in botoes)
        {
            switch (a.acao)
            {

                case botaoacao.comprar:
                    
                        a.obk.SetActive(selecionado.data.ValorDoItem >0);
                   
                    break;

                case botaoacao.vender:
                    a.obk.SetActive(selecionado.data.ValorDoItem > 0);
                    break;

                case botaoacao.Construir:
                    a.obk.SetActive(selecionado.data.receita.Count > 0);
                    break;
                case botaoacao.desconstruir:
                    a.obk.SetActive(selecionado.data.receita.Count > 0);
                    break;
                case botaoacao.trocar:
                    break;

                case botaoacao.Dropar:
                    a.obk.SetActive(true);
                    break;

                    // costumize da forma que precisar...
                case botaoacao.equipar:
                    a.obk.SetActive(selecionado.data.tipoDoItem == tipoDeItem.Equipamentos || selecionado.data.tipoDoItem == tipoDeItem.Armas );
                    break;

                case botaoacao.usar:

                    a.obk.SetActive(selecionado.data.tipoDoItem == tipoDeItem.Pocoes || selecionado.data.tipoDoItem == tipoDeItem.Alimentos);
                    break;

                    // etc ....
                default:
                    break;
            }
        }
    }
    public List<botaum> botoes = new List<botaum>();
    public void criarbotao(botaoacao a , bool b)
    {
        botaum temp = new botaum();
        temp.obk = Instantiate(prefabbotao, localDosBotoes.transform);
        AUXSeparadorPorCategoria temp_ = temp.obk.GetComponent<AUXSeparadorPorCategoria>();
        temp_.botarLetra(a.ToString().ToUpper());
        temp_._gerenciadordeInventario = this;
        temp_._botao = temp;
        temp.acao = a;
        temp.lado = b;
        botoes.Add(temp);

    }
    void removerbotoes()
    {
        while(botoes.Count > 0)
        {
            Destroy(botoes[0].obk.gameObject);
            botoes.RemoveAt(0);
        }
    }
    void attbotoes()
    {
        removerbotoes();
      
        // botoes de interacao
        
            switch (_operacao)
            {

                case acaoDeInventario.inventario:
                    criarbotao(botaoacao.equipar , true);
                    criarbotao(botaoacao.usar, true);
                    criarbotao(botaoacao.Dropar, true);

                    break;
                case acaoDeInventario.craftar:
                    criarbotao(botaoacao.Construir, false);
                    criarbotao(botaoacao.desconstruir, true);
                    break;
                case acaoDeInventario.trocar:
                    criarbotao(botaoacao.trocar, true);
                    criarbotao(botaoacao.trocar, false);
                    break;
                case acaoDeInventario.vender:
                    criarbotao(botaoacao.comprar, false);
                    criarbotao(botaoacao.vender, true);
                    break;
                default:
                    break;
            }
            mostrarEEsconderBotoes();
        
    }
    public void mostrarEEsconderBotoes()
    {
       
         if(selecionado.data == null)
        {
            botoes.ForEach(x => x.obk.SetActive(false));
            return;
        }
      
            foreach (var a in botoes)
            {
                if (a.lado == estaEmQualInventario())
                {
                    a.obk.SetActive(true);
                }
                else
                {
                    a.obk.SetActive(false);
                }
            
        }
    }
    public void trocarEntreInventarios()
    {
        if (entrada.instact.alterarJanelas())
        {
            mostradorDeInventario.inventario_ = mostradorDeInventario.inventario_ == inventarioPrincipal ? inventarioSecundari : inventarioPrincipal;
            mostradorDeInventario.atualizar();
            mostrarEEsconderBotoes();
        
        }
    }
    private void attTitulo()
    {
        if (titulo_ == null)
            return;
        switch (_operacao)
        {
            case acaoDeInventario.craftar:

                titulo_.text = "Inventario / " + (estaEmQualInventario() ? " Desconstruir" : "Construir");

                break;
            case acaoDeInventario.inventario:
                titulo_.text = "Inventario ";
                break;
            case acaoDeInventario.trocar:
                titulo_.text = "Trocando itens    de  " + (estaEmQualInventario() ? "Voce para ele " : "dele para voce");
                break;
            case acaoDeInventario.vender:
                titulo_.text = (estaEmQualInventario() ? " Vendendo" : "comprando");
                break;
        }
    }
    public bool  estaEmQualInventario()
    {
        return mostradorDeInventario.inventario_ == inventarioPrincipal;
    }
    public InventarioScriptavel ladoatual()
    {
        return mostradorDeInventario.inventario_ == inventarioPrincipal ? inventarioPrincipal : inventarioSecundari ;
    }
    public InventarioScriptavel ladoaOposto()
    {
        return mostradorDeInventario.inventario_ != inventarioPrincipal ? inventarioPrincipal : inventarioSecundari;
    }

 
    public void transferirDeUmLadoParaOutro(bool removerDOOutroLado)
    {
        ladoaOposto().adicionarIten(selecionado, 1);

        if(removerDOOutroLado)
        ladoatual().removerIten(selecionado, 1);

    }
    public bool transferirDinheiro()
    {
        if (selecionado.quantidade < 1 || ladoaOposto().dinheiro < selecionado.data.ValorDoItem )
            return false;
        // logica aplicada em cima da transferencia do dinheiro, por exemplo um multiplicador porlvl, descontos, comprar é 100% masvender é 75% ...
        ladoatual().dinheiro += selecionado.data.ValorDoItem;
        ladoaOposto().dinheiro -= selecionado.data.ValorDoItem;

        return true;

    }
    public void transferirReceita()
    {
      
        // construir
        // implementar logica de desconto na quantidade de materiais, chance aleatoria de receber um item da receita de volta...
        if(ladoatual() != inventarioPrincipal)
        {
            bool temp = true;
            foreach(var a in selecionado.data.receita)
            {
                if(!inventarioPrincipal.ItensInventario.Exists(x=> x.data == a.data && x.quantidade >= a.quantidade))
                {
                    temp = false;
                    break;
                }
            }
            if (temp)
            {
              
                foreach (var a in selecionado.data.receita)
                {
                    inventarioPrincipal.removerIten(a,0);
                }
               
                Debug.Log("conseguio");
                inventarioPrincipal.adicionarIten(selecionado,1);
            }
            else
            {
                Debug.Log("falhou");
            }

        }
        else
        {
            foreach(var a in selecionado.data.receita)
            {
                inventarioPrincipal.adicionarIten(a , 0);
            }
            inventarioPrincipal.removerIten(selecionado , 1);
        }
        // adicionar logica de receber 75% dos itens de volta... etc

      
    }
    public void removerDoInventario()
    {
        ladoatual().removerIten(selecionado, 1);
    }
    public void acaobotao(botaoacao a )
    {
        if (selecionado.data == null)
            return;

        switch (a) {

            case botaoacao.comprar :
                if (transferirDinheiro())
                    transferirDeUmLadoParaOutro(true);
                break;

            case botaoacao.vender:
                if (transferirDinheiro())
                    transferirDeUmLadoParaOutro(true);
                break;

            case botaoacao.Construir:
                transferirReceita();
                break;
            case botaoacao.desconstruir:
                transferirReceita();
               
                break;
            case botaoacao.trocar:
                transferirDeUmLadoParaOutro(true);
                break;

            case botaoacao.Dropar:
                // remover do inventario
                removerDoInventario();
                break;
            case botaoacao.equipar:
                // equipar implica em não alterar no inventario
                break;
          
            case botaoacao.usar:
                // remover do inventario
                removerDoInventario();
                break;
           

            default:
                break;
        }
        mostradorDeInventario.attvalores();


    }
    public void displayDeItem()
    {
        if (displayDeIten == null || selecionado.data == null)
            return;
        if (itemDisplay != null || selecionado.quantidade <= 0)
            Destroy(itemDisplay);
        itemDisplay = Instantiate(selecionado.data.modeloDoObj, displayDeIten._rotacionarAutomatico.transform);
        displayDeIten.target = itemDisplay;
        itemDisplay.layer = 6;
        Transform[] temp = itemDisplay.GetComponentsInChildren<Transform>();
        foreach (var a in temp)
        {
            a.gameObject.layer = 6;
        }
    }

 
}
public class botaum { 
    
    public GameObject obk;
    public botaoacao acao;
    public bool lado;
}

public enum acaoDeInventario
{
    trocar,vender,craftar,inventario,
}
public enum botaoacao {
    equipar, usar, Dropar, Construir,desconstruir,trocar,comprar,vender
}

