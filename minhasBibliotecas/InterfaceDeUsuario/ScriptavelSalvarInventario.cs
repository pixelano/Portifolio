using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SaveDeItens", menuName = "Itens/SaveItens", order = 1)]
public class ScriptavelSalvarInventario : ScriptableObject
{
    public List<API_Grid.inventarioSlot> inventario;
}
