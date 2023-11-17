using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grupo : MonoBehaviour
{

    public List<mobsDeGrupo> prefab;
    public int quantidade;
    public float distancia;
   
    public List<GameObject> filhos, curiosos, Ncuriosos,mortos;

    

    public Vector3 novoLocal()
    {

        float x = Random.Range(-distancia, distancia);
        float z = Random.Range(-distancia, distancia);
        return new Vector3(x, 0, z) + transform.position;
    
    }
    private void iniciarGrupo()
    {


        //
        List<int> quantidadeTipoMob = new List<int>();
        for (int y = 0; y < prefab.Count; y++)
        {
            quantidadeTipoMob.Add((int)(quantidade * (prefab[y].porcentagem / 100)));
        }

        for (int y = 0; y < prefab.Count; y++)
        {
            for (int x = 0; x < quantidadeTipoMob[y]; x++)
            {
                GameObject aux = Instantiate(prefab[y].prefab, novoLocal(), Quaternion.identity, transform);
                aux.name = "mob_" + x;
                filhos.Add(aux);

            }
        }
    }
    public void definirGrupo(prefabDegrupos aux)
    {
        List<mobsDeGrupo> prefab = aux.mobsDeGrupo_; int quantidade = aux.quantidade; float distancia = aux.distancia;
        this.prefab = prefab;
        this.quantidade = quantidade;
        this.distancia = distancia;
        iniciarGrupo();
    }
   
}
[System.Serializable]
public class mobsDeGrupo
{

    public GameObject prefab;
    public float porcentagem;
}
