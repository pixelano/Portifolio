using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParaTodosOsItens
{
    public class Inventario : MonoBehaviour
    {

        public List<ObjInventario> _inventario;
        public Api_inventario_X_UI api_;
        public SalvarInventario bk_inventario; 
       

        private void Start()
        {
            if (bk_inventario == null)
            {
                _inventario = new List<ObjInventario>();
            }
            else
            {
                try
                {
                    if (bk_inventario._inventario.Count <= 0)
                    {
                        _inventario = new List<ObjInventario>();
                    }
                    else
                    {
                        _inventario = bk_inventario._inventario;
                    }
                }
                catch
                {
                    _inventario = new List<ObjInventario>();
                }
            }
            Invoke("atualizarInventario", 2);
            
        }
        private void OnDestroy()
        {
            if (bk_inventario != null)
            {
                bk_inventario._inventario = _inventario;
            }
        }
        public void adicionarItem(ScriptableItensData aux, int quantidade)
        {
            
            if(_inventario.Exists(x=>x.data == aux)){
                _inventario.Find(x => x.data == aux).adicionar(quantidade);
               
            }
            else
            {
                _inventario.Add(new ObjInventario());
                _inventario[_inventario.Count - 1].DefinirData(aux);
                _inventario[_inventario.Count - 1].adicionar(quantidade);
            }
            atualizarInventario();
        }
        public bool removerItem(ScriptableItensData aux ,int quantidade)
        {
            if (_inventario.Exists(x => x.data == aux) )
            {if (_inventario.Find(x => x.data == aux).quantidade >= quantidade)
                {
                    _inventario.Find(x => x.data == aux).adicionar(-quantidade);
                    atualizarInventario();
                    return true;
                }
               
                    return false;
                
            }
            else
            {
                atualizarInventario();
                return false;
            }

        }
        private void atualizarInventario()
        {
            api_.aux_inventario = _inventario;
            api_.att();
        }


        public bool subitrairItens(ScriptableItensData data ,int quantidade)
        {
          return removerItem(data, quantidade);
        }
        public bool verificarSeTem(ScriptableItensData data, int quantidade)
        {
            ObjInventario obj = _inventario.Find(x=> x.data == data);

            return obj != null ? obj.quantidade >= quantidade ? true : false : false;
        }
    }
    [System.Serializable]
    public class    ObjInventario
    {
        public ScriptableItensData data;
        public int quantidade;

        public void adicionar(int x)
        {
            quantidade += x;
        }
        public void DefinirData(ScriptableItensData aux)
        {
            data = aux;
        }
    }
}
