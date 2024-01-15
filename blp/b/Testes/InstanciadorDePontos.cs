using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using barracas;

public class InstanciadorDePontos : MonoBehaviour
{
    public GameObject modelo;
    [Range(1,1000)]
    public int quantidade;
    [Range(1, 800)]
    public float distancia;
    private List<GameObject> instancias = new List<GameObject>();

    public bool racalcular = true;
    private void Update()
    {

        if (racalcular)
        {
            racalcular = false;
            if (instancias.Count > 0)
            {
                foreach(GameObject aux in instancias)
                {
                    Destroy(aux);
                }
                instancias.Clear();
            } 
          
        }

        if(instancias.Count < quantidade)
        {
            tempo_ += Time.deltaTime;
            if (tempo_> tempoEntreSpawn)
            {
                tempo_ = 0;
                Vector3 posicaoAleatoria = new Vector3(Random.Range(-distancia, distancia), 0, Random.Range(-distancia, distancia));
                posicaoAleatoria += transform.position;
                instancias.Add(Instantiate(modelo, posicaoAleatoria, Quaternion.identity, transform));
                contagem++;
            }
        }
    }
    public int contagem;
    public float tempoEntreSpawn;
    private float tempo_;
   
}
