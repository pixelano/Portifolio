using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace ParaTodosOsItens
{
    [CreateAssetMenu(fileName = "DataItemVazio", menuName = "Itens/Item_Data", order = 1)]

    public class ScriptableItensData : ScriptableObject
    {
        public Texture2D icone_;
        public string Nome;
        public GameObject obj;

      
    }
}
