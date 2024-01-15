using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParaTodos;


namespace Parques
{
    public class GerenciadorDeParque : MonoBehaviour
    {
        float DistanciaMaxima = 30;
        
        int contadorLista;
       public List<GameObject> AsBarracas =new List<GameObject>();
        public List<GameObject> OsVisitantes = new List<GameObject>();
        public GameObject render;

        public Rigidbody rb;
        public TodosOsParques TOP;
        public LocalNavMeshBuilder builder;
        
        public Mesh malha;

      
        public GameObject BarracaPerto(Vector3 aux)
        {
            GameObject a = AsBarracas[0];
            float x = Mathf.Infinity;
            foreach(GameObject aa in AsBarracas)
            {
                float bb = Vector3.Distance(aa.transform.position, aux);
                if(x > bb)
                {
                    x = bb;
                    a = aa;
                }
            }
            return a;
        }
        private void Start()
        {
            //criat box collider e ativar o trigger, rigibody e desativar a fisica,

            fun += 1;
            builder.m_Size = transform.localScale + new Vector3(0,50,0);
        }
      
        bool flag_cm = false;
        
        void calcularTamanho(Vector3 escala,Vector3 pos)
        {

            

            float sx = transform.localScale.x/2, sz = transform.localScale.z/2;
        

           

            
           
                float x = Mathf.Abs(transform.position.x - pos.x) + (escala.z / 2);
                if (sx < x )
                {
                    sx = x;
                }
                float z = Mathf.Abs(transform.position.z - pos.z) + (escala.x/2);
                if (sz < z)
                {
                    sz = z;
                }



            sx *= 2;
            sz *= 2;

            //transform.localScale += new Vector3(escala.x, 0, escala.z)/2;
            //builder.m_Size = transform.localScale + new Vector3(0, 50, 0);


            transform.localScale = new Vector3(sx, transform.localScale.y, sz);
            builder.m_Size = transform.localScale + new Vector3(0, 50, 0);

        }
        void calcularTamanho_()
        {
            if (AsBarracas.Count > 1)
            {
                float posX = 0;
                float posZ = 0;
                for (int x = 0; x < AsBarracas.Count; x++)
                {
                    float auxX = Mathf.Abs(AsBarracas[x].transform.position.x - transform.position.x);
                    if (auxX > posX)
                        posX = auxX;

                    float auxZ = Mathf.Abs(AsBarracas[x].transform.position.z - transform.position.z);
                    if (auxZ > posZ)
                        posZ = auxZ;

                }
                posX += 10;
                posZ += 10;

                transform.localScale = new Vector3(posX * 2, transform.localScale.y, posZ * 2);
                builder.m_Size = transform.localScale + new Vector3(0, 50, 0);
            }
        }
        void calcularMeio()
        {
           
            if (AsBarracas.Count > 2)
            {

                vertices.Clear();

                foreach (GameObject aux in AsBarracas)
                {

                    vertices.Add(aux.transform.position);
                }

 
            }

            
        }
        public List<Vector3> vertices = new List<Vector3>();
        void calcularNormal()
        {


            // Crie uma nova malha
            malha= new Mesh();





            // Converta os vértices em um array
            malha.vertices = vertices.ToArray();

            // Crie uma lista de triângulos para formar um polígono convexo
            List<int> triangles = new List<int>();
            for (int i = 1; i < vertices.Count - 1; i++)
            {
                triangles.Add(0);
                triangles.Add(i);
                triangles.Add(i + 1);
            }

            // Converta os triângulos em um array
            malha.triangles = triangles.ToArray();

            // Recalcule as normais para a iluminação
            malha.RecalculateNormals();
           // malha.Clear();
          

        }
      
     
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ParqueUnidade>())
            {
                    AsBarracas.Add(other.gameObject);
                fun++;
                calcularMeio();
                calcularNormal();
                calcularTamanho_();
            }
            if (other.CompareTag("Visitantes"))
            {
                OsVisitantes.Add(other.gameObject);
            }

            if (other.GetComponent<GerenciadorDeParque>())
            {
                if (other.GetComponent<GerenciadorDeParque>().fun > fun)
                {
                }
                else
                {
                   
                    //  AsBarracas.AddRange(other.GetComponent<GerenciadorDeParque>().AsBarracas);
                    
                    calcularTamanho(other.transform.localScale, other.transform.position);
                    
                    fun += other.GetComponent<GerenciadorDeParque>().fun;
                  TOP.remover(other.GetComponent<GerenciadorDeParque>());

                  

                }
            }

        }
        private void OnDestroy()
        {
            malha = null;
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Visitantes"))
            {
                OsVisitantes.Remove(other.gameObject);
            }
            if (other.GetComponent<ParqueUnidade>())
            {
                AsBarracas.RemoveAll(x=>x==other.gameObject);
                fun--;
                calcularMeio();
                calcularNormal();
                calcularTamanho_();
            }
        }




        public float fun;
    
    }
}
