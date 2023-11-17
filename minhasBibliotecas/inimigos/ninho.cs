using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninho : MonoBehaviour
{

    public int quantidadeGrupos, tamanhoDistancia;
    public grupo meuGrupo;
    public List<GameObject> grupos;

    public prefabDegrupos prefabGrupo_;

    private void Start()
    {
            for (int x = 0; x < quantidadeGrupos; x++)
            {
                GameObject aux = Instantiate(meuGrupo.gameObject, novoLocal(), Quaternion.identity, transform);
                aux.name = "grupo_" + x;

                aux.GetComponent<grupo>().definirGrupo(prefabGrupo_);

                grupos.Add(aux);
            }
        

    }
    public Vector3 novoLocal()
    {

        float x = Random.Range(-tamanhoDistancia, tamanhoDistancia);
        float z = Random.Range(-tamanhoDistancia, tamanhoDistancia);
        return new Vector3(x, 0, z) + transform.position;

    }
}
