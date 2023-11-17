using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "prefab_grupo", menuName = "Mobs/Grupo", order = 1)]

public class prefabDegrupos : ScriptableObject
{
    public int quantidade;
    public float distancia;
    [SerializeField]
    public List<mobsDeGrupo> mobsDeGrupo_;
   


  
}
