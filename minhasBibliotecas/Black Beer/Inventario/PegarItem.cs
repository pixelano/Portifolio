using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarItem : MonoBehaviour
{

    // os objetos devem ter rb
    public bool ObjetosSaoAtraidosParaOJogador;
    BoxCollider collider;
    public float distanciaParaColetar;
     Inventario _inventario;
    private void Awake()
    {
        if(ObjetosSaoAtraidosParaOJogador){
            collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.size = new Vector3(distanciaParaColetar, 1, distanciaParaColetar);

        }
       
      
     }
   
    private void Start()
    {
        _inventario = GetComponent<Inventario>();
        if(_inventario == null)
        {
            gameObject.AddComponent<Inventario>();
        }
    }
    private void Update()
    {
       
        if (!ObjetosSaoAtraidosParaOJogador)
        {
            RaycastHit ch = new RaycastHit();
            Ray raio = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Physics.Raycast(raio, out ch, distanciaParaColetar);
            if (!ch.collider)
                return;
            Debug.Log(ch.collider.name);
            if (entrada.instact.pegarItem())
            {
                pegarItem(ch.collider.gameObject);
            }
        }
        if(atrais.Count > 0)
        {
            foreach(var a in atrais)
            {
                a.AddForce((gameObject.transform.position - a.transform.position ) * velocidadeAtracao,ForceMode.Force);
            
            }
            int tempI = atrais.Count;
            for(int x = 0;x < tempI; x++)
            {
                if( Vector3.Distance( atrais[x].transform.position,transform.position) < 1)
                {
                    GameObject tempG = atrais[x].gameObject;
                   
                    pegarItem(tempG);
                    atrais[x] = null;
                
                    
                }
            }
            atrais.RemoveAll(x => x==null) ;
        }
    }
    public void pegarItem(GameObject a)
    {

        _inventario.adicionarItem(a.GetComponent<itemInstancia>().dataItem , 1);
        Destroy(a.gameObject);

    }
    public float velocidadeAtracao;
    public List<Rigidbody> atrais = new List<Rigidbody>();
    private void OnTriggerEnter(Collider other)
    {
        if (ObjetosSaoAtraidosParaOJogador)
        {
            if (other.GetComponent<itemInstancia>())
            {
                Rigidbody temp = other.GetComponent<Rigidbody>();
                temp.useGravity = false;
                atrais.Add(temp);

            }


        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (ObjetosSaoAtraidosParaOJogador)
        {
            if (collider.GetComponent<itemInstancia>())
            {
                Rigidbody temp = collision.gameObject.GetComponent<Rigidbody>();
                temp.useGravity = true ;
                try
                {
                    atrais.Remove(temp);
                }
                catch 
                {

                }
            }


        }
    }
}
