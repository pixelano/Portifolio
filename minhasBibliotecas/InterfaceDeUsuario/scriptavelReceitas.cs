using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NovaReceita", menuName = "Itens/NovaReceita", order = 1)]
public class scriptavelReceitas : ScriptableObject
{
    public List<itemQuantidade> receita;
    [System.Serializable]
        public class itemQuantidade { public Scriptavel_ObjInventario item; public int quantidade; }

}
