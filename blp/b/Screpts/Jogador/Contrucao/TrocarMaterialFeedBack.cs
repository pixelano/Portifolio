using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contrucao
{
    public class TrocarMaterialFeedBack : MonoBehaviour
    {
        public Material verde, vermelho;
        public MeshRenderer mesh;
        public bool disponivel;
        private void Start()
        {
            mesh = GetComponent<MeshRenderer>();
        }
     


        private void OnTriggerStay(Collider other)
        {
            if (!other.isTrigger)
            {
                disponivel = false;
                mesh.material = vermelho;
            }
        }
        void OnTriggerExit(Collider other)
        {
            disponivel = true;
            mesh.material = verde; 
        }

    }
}
