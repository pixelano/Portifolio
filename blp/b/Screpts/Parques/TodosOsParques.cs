using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parques
{
    public class TodosOsParques : MonoBehaviour
    {
        public List<GerenciadorDeParque> todosOsParques_ = new List<GerenciadorDeParque>();
        public GameObject prefabParqueMeio;

        public salvarParques sv;
        public bool salvar, carregar;
        private void OnDestroy()
        {
            if (salvar)
            {
                sv.salvar(todosOsParques_);
            }
        }
        private void Awake()
        {
            if (carregar)
            {
                foreach(var aux in sv.parques)
                {
                    NovaBarraca(aux.local,aux.fun);
                }
            }
        }
        public void NovaBarraca(Vector3 aux)
        {
            if(todosOsParques_.Count >= 1)
            {
                bool flag_ = true;
                foreach(GerenciadorDeParque cada in todosOsParques_) {
                    float distancia = Vector3.Distance(aux, cada.transform.position);
                    if (distancia < cada.transform.localScale.x / 2 || distancia < cada.transform.localScale.z / 2)
                    {
                        flag_ = false;
                        break;
                    }
                }
                if (flag_)
                {
                    todosOsParques_.Add(Instantiate(prefabParqueMeio, aux, Quaternion.identity, transform).GetComponent<GerenciadorDeParque>());
                    todosOsParques_[todosOsParques_.Count - 1].TOP = this;
                }
            }
            else
            {
                todosOsParques_.Add( Instantiate(prefabParqueMeio, aux, Quaternion.identity, transform).GetComponent< GerenciadorDeParque>());
                todosOsParques_[todosOsParques_.Count - 1].TOP = this;

            }
        }

        public void NovaBarraca(Vector3 aux,float fun)
        {
            if (todosOsParques_.Count >= 1)
            {
                bool flag_ = true;
                foreach (GerenciadorDeParque cada in todosOsParques_)
                {
                    float distancia = Vector3.Distance(aux, cada.transform.position);
                    if (distancia < cada.transform.localScale.x / 2 || distancia < cada.transform.localScale.z / 2)
                    {
                        flag_ = false;
                        break;
                    }
                }
                if (flag_)
                {
                    todosOsParques_.Add(Instantiate(prefabParqueMeio, aux, Quaternion.identity, transform).GetComponent<GerenciadorDeParque>());
                    todosOsParques_[todosOsParques_.Count - 1].TOP = this;
                    todosOsParques_[todosOsParques_.Count - 1].fun = fun;
                }
            }
            else
            {
                todosOsParques_.Add(Instantiate(prefabParqueMeio, aux, Quaternion.identity, transform).GetComponent<GerenciadorDeParque>());
                todosOsParques_[todosOsParques_.Count - 1].TOP = this;
                todosOsParques_[todosOsParques_.Count - 1].fun = fun;

            }
        }
        public void remover(GerenciadorDeParque gp)
        {
            todosOsParques_.Remove(gp);
            Destroy(gp.gameObject);
        }
    
    public GerenciadorDeParque estaDentro(Vector3 aux)
        {
            GerenciadorDeParque flag_ = null;
            foreach (GerenciadorDeParque cada in todosOsParques_)
            {
                float distancia = Vector3.Distance(aux, cada.transform.position);
                if (distancia < cada.transform.localScale.x / 2 || distancia < cada.transform.localScale.z / 2)
                {
                    flag_ = cada;
                    break;
                }
            }
            
            return flag_;
        }

        public void flipFlop_renderParque(Vector3 aux)
        {
            if (todosOsParques_.Count > 0)
            {
                float minDist = Mathf.Infinity;
                GerenciadorDeParque gp = todosOsParques_[0];
                foreach (GerenciadorDeParque cada in todosOsParques_)
                {
                    float distancia = Vector3.Distance(aux, cada.transform.position + (cada.transform.localScale / 2));
                    if (distancia < minDist)
                    {
                        minDist = distancia;
                        gp = cada;
                    
                    }
                }
              
                gp.render.GetComponent<MeshRenderer>().enabled = !gp.render.GetComponent<MeshRenderer>().enabled;            }
        }
    }
    [CreateAssetMenu(fileName = "SaveVazio", menuName = "Salvamento/SalvarParques", order = 1)]
    public class salvarParques : ScriptableObject
    {
        public List<cadaparque> parques;

        public struct cadaparque {
           public Vector3 local;
            public float fun;
       public cadaparque(Vector3 local_ , float f)
            {
                local = local_;
                fun = f;
            }
        }
       

        public void salvar(List<GerenciadorDeParque> aux)
        {
            parques = new List<cadaparque>();

            foreach(GerenciadorDeParque val in aux)
            {
                parques.Add( new cadaparque(val.transform.position,val.fun));
            }
        }
    }
}
