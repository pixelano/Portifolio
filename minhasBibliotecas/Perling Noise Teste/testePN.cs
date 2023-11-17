using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testePN : MonoBehaviour
{
    public int tamanhoAmostragem;
    public List<List<GameObject>> listaGO = new List<List<GameObject>>();
    public GameObject modelo;
    public float escalaModelo;
    public List<Material> mts = new List<Material>();
    private void arrumarAmostraGem()
    {
       for(int x = 0; x < listaGO.Count; x++)
        {

            for (int z = 0; z < listaGO.Count; z++)
            {
                Destroy(listaGO[x][z]);
            }
        }

        listaGO.Clear();

        iniciar();
    }
    private void iniciar()
    {
        for(int y= 0; y <tamanhoAmostragem; y++)
        {

            listaGO.Add(new List<GameObject>());
            for (int x = 0; x < tamanhoAmostragem; x++)
            {
                listaGO[y].Add(Instantiate(modelo, new Vector3(x * escalaModelo, 0, y * escalaModelo), Quaternion.identity, transform));
            }
        }
        removendo = false;
    }
    private void Start()
    {
        //listaGO.Add(new List<GameObject>());
        iniciar();
        //arrumarAmostraGem();
    }

    private void mudarCor(GameObject go,int x)
    {
        go.GetComponent<Renderer>().material = mts[x];
    }

    public float valorDeCorte,frequencia,amplitude,resultadoPERLING;
    private bool removendo;
    private void Update()
    {
        if (listaGO.Count != tamanhoAmostragem && removendo == false)
        {
            removendo = true;
            arrumarAmostraGem();
        }
        if (removendo == false)
        {
            for (int x = 0; x < tamanhoAmostragem; x++)
            {
                for (int y = 0; y < tamanhoAmostragem; y++)
                {
                    resultadoPERLING = Mathf.PerlinNoise((x * escalaModelo) / frequencia, (y * escalaModelo) / frequencia) * amplitude;

                    if (resultadoPERLING > valorDeCorte)
                    {
                        mudarCor(listaGO[y][x], 0);
                    }
                    else
                    {
                        mudarCor(listaGO[y][x], 1);
                    }
                }
            }
        }
    }
}
