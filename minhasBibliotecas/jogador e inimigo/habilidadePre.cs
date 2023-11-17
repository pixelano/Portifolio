using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "prefabHabilidade", menuName = "Habilidades/habilidade_pref", order = 1)]
public class habilidadePre : ScriptableObject {

    [SerializeField]
    public List<tiposDeHabilidade> tipos_habilidade_;
    [SerializeField]
    public float multiplicador_habilidade, multiplicador_lvl_jogador, dano, multiplicadorFraquezaResistencia;
    [SerializeField]
    public bool fraqueza, resistencia;
   
    

}
