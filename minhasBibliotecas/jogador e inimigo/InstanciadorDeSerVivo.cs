using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InstanciadorDeSerVivo : MonoBehaviour
{
    [SerializeField]
    public bool isMob;
    [SerializeField]
    public List<novaHabilidade> listaDeHabilidades_ataque, listaDeHabilidades_defesa;
    [SerializeField]
    public List<habilidadePre> listaDeRessistencias;
    [SerializeField]
    public string especie;
    [SerializeField]
    public float vida, armadura;
    [SerializeField]
    public GameObject GO_ataque_, GO_defesa_,GO_modelo_acao;
    [SerializeField]
    public servivo vivo;

  

    private void instanciaefeitos(List<novaHabilidade> lista,GameObject trans)
    {
        foreach(novaHabilidade aux in lista)
        {
           
            GameObject aux_ = Instantiate(GO_modelo_acao, trans.transform);
            aux_.GetComponent<acoes>().cooldown = aux.Cudau;
            foreach (habilidadePre aux_X in aux.efeitos)
            {

           
            habilidade hab_aux = new habilidade(aux_X.tipos_habilidade_, aux_X. multiplicador_habilidade, aux_X.multiplicador_lvl_jogador, aux_X.dano, aux_X.multiplicadorFraquezaResistencia, aux_X.resistencia, aux_X.fraqueza);
            aux_.GetComponent<acoes>().efeitos.Add(hab_aux);
           
            if (aux_X.fraqueza || aux_X.resistencia)
            {
                if (aux_X.fraqueza)
                {
                    vivo.ataques.Add(aux_.GetComponent<acoes>());
                }
                if (aux_X.resistencia)
                {
                    vivo.defesas.Add(aux_.GetComponent<acoes>());
                }
            }
            else
            {
                vivo.ultilitarios.Add(aux_.GetComponent<acoes>());
            }
            aux_.GetComponent<acoes>().setar(aux_X.fraqueza, aux_X.resistencia);
            }
        }
    }
    private void definirRessist(List<habilidadePre> ressist)
    {
        foreach(habilidadePre aux in ressist)
        {
            habilidade aux_ = new habilidade(aux.tipos_habilidade_,aux.multiplicador_habilidade, aux.multiplicador_lvl_jogador, aux.dano, aux.multiplicadorFraquezaResistencia, aux.resistencia, aux.fraqueza);


            vivo.fraquezasEresistencias.Add(aux_);
        }
    }
    private void Start()
    {
      
            instanciaefeitos(listaDeHabilidades_ataque, GO_ataque_);
            instanciaefeitos(listaDeHabilidades_defesa, GO_defesa_);
            definirRessist(listaDeRessistencias);

            vivo.acessarVida().configurar(vida, armadura);
            vivo.especie = especie;

            vivo.setconfigurar();
     
    }
}
[System.Serializable]
public class novaHabilidade
{
    public List<habilidadePre> efeitos;
    public float Cudau;
}