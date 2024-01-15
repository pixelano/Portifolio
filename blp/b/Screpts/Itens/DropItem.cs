using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParaTodosOsItens
{
    public class DropItem : MonoBehaviour
    {
        public GameObject Prefab_ItemDrop;
        public float forca;
      public void Dropar(ScriptableItensData scd)
        {
           GameObject aux = Instantiate(Prefab_ItemDrop,transform.position +new Vector3(Random.Range(-1,1),0, Random.Range(-1, 1)) + Vector3.up * forca,Quaternion.identity);
            aux.GetComponent<InstanciarObjItemDrop>().data = scd;
            aux.GetComponent<InstanciarObjItemDrop>().Iniciar();
          aux.GetComponent<Rigidbody>().AddForce(Vector3.up * forca ,ForceMode.Impulse);
        }
    }
}
