using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Interacoes {
    public class ClickarEmEvento : MonoBehaviour
    {
        [SerializeField]
        private float DistanciaMaximaParaClikar;
        [SerializeField]
        private Transform go;
        [SerializeField]
        public bool desativar_;
        public LayerMask lm;

        public void AtivarDesativar()
        {
            desativar_ = !desativar_;
        }
        private void Update()
        {
            if (!desativar_) {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity,lm))
                    {
                        if (Vector3.Distance(go.position, hit.transform.position) < DistanciaMaximaParaClikar)
                        {
                       
                            if (hit.collider.GetComponents<IAtivarComClick>() != null)
                            {
                               
                                IAtivarComClick[] aux = hit.collider.GetComponents<IAtivarComClick>();
                                
                                foreach (IAtivarComClick aa in aux)
                                {
                                    aa.executar(gameObject);
                                    aa.executar();
                                }
                            }
                        }


                    }
                }
            }
        }
    }
}
