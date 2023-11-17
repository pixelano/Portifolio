using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class acoes : MonoBehaviour
{
    public Collider colid;

    private bool causarDano, causarDefesa;
    [SerializeField]
    public List<habilidade> efeitos;
    public float cooldown,ultimaVezUsado;
    private servivo eumesmo;

    public float level=1,experiencia,maxExperiencia=100;
    //
    public void setar(bool dano,bool defesa)
    {
        causarDano= dano;
        causarDefesa = defesa;
    }
    public servivo mesmo() { return eumesmo; }
    //
    public void att(float lvl_j)
    {
        foreach(habilidade aux in efeitos)
        {
            aux.attLvlJogador(lvl_j);
        }
    }
    public void configurar(servivo aux)
    {
        eumesmo = aux;
    }
    //
    public void executar()
    {
        colid.enabled = true;
        eumesmo.flags.atacando = true;
      
      
    }
    public void fechar()
    {
        colid.enabled = false;
        eumesmo.flags.atacando = false;
        if (causarDefesa)
        {

            eumesmo.acessarVida().SeDefender(false,0);
        }
    }
    //
    public void usarHabilidade()
    {
        if(Mathf.Abs(Time.time - ultimaVezUsado ) > cooldown)
        {
            ultimaVezUsado = Time.time;

            executar();
        }
    }
    public void ganharexperiencia(servivo alvo)
    {
        float exp = level * 1.33f * ((alvo.nivel - level) * (alvo.nivel * 0.33f)) + 1;
        experiencia += exp;
      
        mesmo().experiencia += exp * 0.033f;

        if (experiencia > maxExperiencia)
        {
            level++;
            experiencia -= maxExperiencia;
            maxExperiencia = level * 1.33f * 100;
            foreach(habilidade aux in efeitos)
            {
                aux.attLvlHabilidade(level);
            }

        }
    }
    public float calcularDano(servivo inimigo)
    {
        float danofinal = 0;
       
      
       
        foreach (habilidade minhahabilidade in efeitos)
        {
            danofinal += minhahabilidade.usarHabilidade();
            if (inimigo.fraquezasEresistencias.Exists(x=>x.tipos_habilidade_ == minhahabilidade.tipos_habilidade_))
            {
              
                habilidade habilidadeAlvo = inimigo.fraquezasEresistencias.Find(x => x.tipos_habilidade_ == minhahabilidade.tipos_habilidade_) ;
                if (habilidadeAlvo.resistencia || habilidadeAlvo.fraqueza)
                {
                  
                    if (habilidadeAlvo.resistencia)
                    {
                        danofinal -= habilidadeAlvo.danoHabilidade() * habilidadeAlvo.multiplicadorFraquezaResistencia;
                          }
                    if (habilidadeAlvo.fraqueza)
                    {
                      
                        danofinal += habilidadeAlvo.danoHabilidade() * habilidadeAlvo.multiplicadorFraquezaResistencia;
                    }
                }


            }
        }

        return danofinal;
    }
    //
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<servivo>() )
        {
            if (other.GetComponent<servivo>().minhaVida.estavivo() && other.GetComponent<servivo>().especie != eumesmo.especie)
            {
                if (causarDano)
                {

                    ganharexperiencia(other.GetComponent<servivo>());
                    other.GetComponent<servivo>().sendoAtacado( eumesmo.gameObject);
                    if (other.GetComponent<servivo>().acessarVida().tomarDano(calcularDano(other.GetComponent<servivo>())))
                        mesmo().ganharexp(other.GetComponent<servivo>().nivel);
                }
            }            
        }
        else
        {
            if (other.GetComponent<acoes>())
            {
                if (other.GetComponent<acoes>().mesmo().minhaVida.estavivo() && other.GetComponent<acoes>().mesmo().especie != eumesmo.especie)
                {
                    if (causarDefesa && other.GetComponent<acoes>().causarDano)
                    {

                        ganharexperiencia(other.GetComponent<servivo>());

                    }

                    if (causarDano)
                    {
                        if (other.GetComponent<acoes>().causarDefesa)
                        {

                            other.GetComponent<acoes>().mesmo().acessarVida().SeDefender(true, other.GetComponent<acoes>().calcularDano(mesmo()));

                            ganharexperiencia(other.GetComponent<acoes>().mesmo());
                            other.GetComponent<acoes>().mesmo().acessarVida().tomarDano(calcularDano(mesmo()));
                            mesmo().ganharexp(other.GetComponent<acoes>().mesmo().nivel);

                        }


                    }
                }
            }
        }
    }
    


}

[System.Serializable]
public class habilidade {

    [SerializeField]
    public List<tiposDeHabilidade >tipos_habilidade_;
    [SerializeField]
    public float nivel =1, multiplicador_habilidade,
        multiplicador_lvl_jogador, dano, lvl_jogador =1 ,multiplicadorFraquezaResistencia;
    [SerializeField]
    public bool fraqueza, resistencia;

     public habilidade(List<tiposDeHabilidade>tipo , float multiplicador_habilidade_, float multiplicador_lvl_jogador_, float dano_, float multiplicadorFraquezaResistencia_,bool resistencia_, bool fraqueza_)
    {
        tipos_habilidade_ = tipo;
        multiplicador_lvl_jogador = multiplicador_lvl_jogador_;
        multiplicador_habilidade = multiplicador_habilidade_;
        dano = dano_;
        multiplicadorFraquezaResistencia = multiplicadorFraquezaResistencia_;
        resistencia = resistencia_;
        fraqueza = fraqueza_;
        
    }
   public habilidade() { }
    public void attLvlJogador(float lvlJ)
    {
        lvl_jogador = lvlJ;
    }
    public void attLvlHabilidade(float lvl)
    {
        nivel = lvl;
    }
    public float usarHabilidade()
    {
        return danoHabilidade();
    }
    public float danoHabilidade()
    {
        return dano * (((Mathf.Pow(nivel,2) * (multiplicador_habilidade /100 )+ Mathf.Pow(lvl_jogador,2)* (multiplicador_lvl_jogador/100))/100)+1);
    }
        

}
