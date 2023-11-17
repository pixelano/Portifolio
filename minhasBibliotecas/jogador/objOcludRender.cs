using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objOcludRender : MonoBehaviour
{
    public List<int> efeito;
    public List<GameObject> malhas;
    private void Start()
    {
        FindObjectOfType<gerenciadorRender>().novainstancia(this);
    }

    public void ativarDesativarMalha(bool aux,int indiceMesh)
    {
        if (aux)
        {
            desativarmesh();
            ativarmesh(indiceMesh);
        }
        else
        {
            desativarmesh();
        }
    }
    private void desativarmesh()
    {
        foreach(GameObject aux in malhas)
        {
            aux.SetActive(false);
        }
    }
    private void ativarmesh(int mesh)
    {
        malhas[mesh].SetActive(true);
    }
}
