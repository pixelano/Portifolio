using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NovoItem", menuName = "BlackBeer/Itens/NovoItem", order = 2)]
public class ScriptavelItem : ScriptableObject

{
    public string NomeDoItem;
    public tipoDeItem tipoDoItem;
    public float ValorDoItem, PesoDoItem;
    public List<slotInventario> receita;
    public GameObject modeloDoObj;
    public GameObject prefabDoObj;
    
}

public enum tipoDeItem {
Armas,Equipamentos,Pocoes,Pergaminhos,Alimentos,Ingredientes,Livros,Chaves,Micelanea
}

