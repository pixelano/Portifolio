using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIten", menuName = "Inventory/Iten", order = 1)]
public class ScriptableIten : ScriptableObject
{

    //quem vai equipar o item
    public Equipable Equipavel;
    //classificacao do iten
    public TypeItem TipoDeItem;
    public List<ScriptablePowers> powers;
    public Texture icon;
}
public enum Equipable
{
    Jogador,Pet,Nenhum
}
public enum TypeItem
{
   TipoA,TipoB,TpoC
}