using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AUXSeparadorPorCategoria : MonoBehaviour
{
    public categoria _categoria;

    public SeparadorPorCategoria origem;
    public TextMeshProUGUI _tmpro;
    public RawImage icone;

    public GerenciamentoDeInventario _gerenciadordeInventario;
    public botaum _botao;
    public void executar()
    {
        origem.executarFiltro(_categoria);
    }
    public void botarIcone(Texture a)
    {
        icone.texture = a;
        try {
            Destroy(_tmpro.gameObject);
        }
        catch
        {

        }
        }
    public void botarLetra(string a)
    {
        _tmpro.text = a ;
        try
        {


            Destroy(icone.gameObject);
        }
        catch { } 
        }
    public void executaracao()
    {
        _gerenciadordeInventario.acaobotao(_botao.acao);
    }
    
}
