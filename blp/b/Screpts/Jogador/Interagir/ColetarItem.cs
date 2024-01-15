using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParaTodosOsItens;
namespace Interacoes
{
    public class ColetarItem : MonoBehaviour
    {
     public Inventario inventario;
        public float velocidade;


        private void OnTriggerStay(Collider other)
        {
            if(other.GetComponent<IColetavel>() != null)
            {
                if (Vector3.Distance(other.transform.position, transform.position) < 2)
                {
                    inventario.adicionarItem(other.GetComponent<IColetavel>().Coletar(), 1);
                    Destroy(other.gameObject);
                }
                else
                {
                   
                    other.GetComponent<Rigidbody>().AddForce((transform.position - other.transform.position)
                        * velocidade, ForceMode.Force);
                }
            }
        }
    }
}
