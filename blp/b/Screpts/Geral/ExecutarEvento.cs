using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ParaTodos
{
    public class ExecutarEvento : MonoBehaviour
    {
        public UnityEvent ev;
        public GameObject obj_callback;

        public void executar(GameObject aux)
        {
            obj_callback = aux;
       
        }
      
    }
}
