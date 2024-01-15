using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParaTodosOsItens
{

    [CreateAssetMenu(fileName = "InvetarioVazio", menuName = "Itens/Inventario_", order = 1)]
    public class SalvarInventario : ScriptableObject
    {
        [SerializeField]
        public List<ObjInventario> _inventario ;
    }
}
