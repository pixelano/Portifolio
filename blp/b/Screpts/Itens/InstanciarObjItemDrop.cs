using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParaTodosOsItens
{
    public class InstanciarObjItemDrop :MonoBehaviour, IColetavel
    {
        public ScriptableItensData data;
      

     public ScriptableItensData Coletar()
        {
            return data;
        }

        public void Iniciar()
        {
           Instantiate(data.obj, transform);
            transform.name = data.Nome;

          
        }
    }
}
