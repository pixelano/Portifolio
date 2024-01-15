using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParaTodosOsItens
{
    public class Api_inventario_X_UI : MonoBehaviour
    {
        public List<ScriptableItensData> itensNaUi;
       private List<obj_UI> objEmUi = new List<obj_UI>();
        [Space]
        public GameObject prefabItemUI;
        [Space]
        public GameObject CanvasItem;
        public List<ObjInventario> aux_inventario;

        public struct obj_UI
        {
            public obj_UI(ScriptableItensData a , Api_Componemtes_UI b)
            {
                script = a;
                apiUI = b;
            }

            public ScriptableItensData script;
            public Api_Componemtes_UI apiUI;

          
        }
        public void att()
        {
            foreach(ObjInventario aux in aux_inventario)
            {
                if(objEmUi.Exists(x=>x.script == aux.data)){
                    objEmUi.Find(x => x.script == aux.data).apiUI.attQuantidade(aux.quantidade);
                }
            }
        }
        private void Start()
        {
            

            foreach (ScriptableItensData aux in itensNaUi)
            {
              

                GameObject novoIcone = Instantiate(prefabItemUI,CanvasItem.transform);

                novoIcone.name = aux.Nome;
                Api_Componemtes_UI ap = novoIcone.GetComponent<Api_Componemtes_UI>();
                ap.iniciar(aux.icone_);
                obj_UI aa = new obj_UI(aux, ap);
                objEmUi.Add(aa);
            }
        }
    }

}
