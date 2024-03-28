using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftItem : MonoBehaviour
{
    public List<API_Grid> listaDeItensParaOCraft;
    public itensEmEstoque receitaAlvo;

    public RawImage icone;
    public bool receitaLiberada;


    bool flag;

    private void receitaLiberada_(bool x)
    {
        receitaLiberada = x;
        icone.color = x ? Color.green : Color.grey;

    }
    private void Update()
    {
        if (!flag)
        {
            foreach (var a in listaDeItensParaOCraft)
            {
                if (a.TeveAlteracao)
                {
                    flag = true;
                    a.TeveAlteracao = false;
                    break;
                }

            }
        }
        else
        {
            bool temp = true;
            foreach(var x in receitaAlvo.necessarios)
            {
                bool temp_b = false;
             
                foreach (var a in listaDeItensParaOCraft)
                    {
                       
                       foreach(var b in a.inventario)
                            {
                                if (b.id_ == x.item.itemDrop.data.id_)
                                {
                                    temp_b = true;
                                    break;
                                }
                       }
                    if (temp_b)
                        break;

                 }
                if (!temp)
                    break;
                if (!temp_b)
                    temp = false;
            }
            receitaLiberada_(temp);
            flag = false;
        }
        
    }
}
