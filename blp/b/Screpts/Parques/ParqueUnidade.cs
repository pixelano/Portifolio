using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parques
{
    public class ParqueUnidade : MonoBehaviour
    {

        public TodosOsParques top;
        private void Start()
        {
            top = FindAnyObjectByType<TodosOsParques>();    
            gameObject.name = "" + Time.deltaTime;
            top.NovaBarraca(transform.position);
            this.enabled = false;
        }
    
    }
}
