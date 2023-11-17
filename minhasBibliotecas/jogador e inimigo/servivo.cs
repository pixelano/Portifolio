using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class servivo : MonoBehaviour
{
    public string especie;
    public int nivel =1;
    public float experiencia;
    private float minimoExperiencia = 100;

    [SerializeField]
    public minhavida  minhaVida = new minhavida();
   
    public List<acoes> ataques, defesas,ultilitarios;
    public List<habilidade> fraquezasEresistencias;

    public int indice_ataque, indice_defesa;
  
    public servivo eumesmo;

    [SerializeField]
    public minhasFlags flags;

    [System.Serializable]
    public struct minhasFlags {
        public bool atacando;
        public bool movimentando;
        public GameObject atacado;
       
    }

    public void sendoAtacado(GameObject aux)
    {
        if(flags.atacado == null)
        {
            flags.atacado = aux;
        }
    }
    public void usarHabilidade(int indice,int ata)
    {
        if (flags.atacando == false)
        {

           
         

            if (ata == 0)
            {
              
                indice_ataque = indice;
                ataques[indice].usarHabilidade();
                
            }
            else
            {
                indice_defesa = indice;
                defesas[indice].usarHabilidade();
            }
        }


    }
    public void fecharHabilidade(int ata)
    {
        if (flags.atacando)
        {
            if (ata == 0)
            {
                ataques[indice_ataque].fechar();
            }
            else
            {
                defesas[indice_defesa].fechar();
            }
        }
    }
    public void DesativarTudo()
    {
        foreach(acoes aux in ataques)
        {
            aux.fechar();
        }
        foreach(acoes aux in defesas)
        {
            aux.fechar();
        }

    }
    public void destruirS()
    {
        Destroy(GetComponentInParent<InstanciadorDeSerVivo>().gameObject);
    }

    public void comerAlvo(servivo alvo)
    {
        float multimplicador = nivel / alvo.nivel;

        AumentarVidaAtual(acessarVida().vida_maxima * multimplicador);
        AumentarArmaduraAtual(acessarVida().armadura_maxima * multimplicador);
        ganharexp(alvo.nivel/2);
    }

    public void AumentarVidaAtual(float quantidade)
    {
        acessarVida().recuperarVida(quantidade);
    }
    public void AumentarArmaduraAtual(float quantidade)
    {
        acessarVida().armadura += quantidade;
    }

    public void setconfigurar()
    {
        configacoes(ataques);
        configacoes(defesas);
    }
   public void configacoes(List<acoes> aux)
    {
        foreach(acoes aux_ in aux)
        {
            aux_.configurar(eumesmo);
        }
    }
    private void subioDeLVl()
    {
        nivel++;
        experiencia -= minimoExperiencia;
        minimoExperiencia = nivel * 1.33f * 100;

        att_habilidades(ataques);
        att_habilidades(defesas);
        att_rs(fraquezasEresistencias);

    }
    public void ganharexp(float aux)
    {

        float exp = nivel * 1.33f *( ((aux- nivel) * (aux* 0.33f)) + 1);
        experiencia  = exp;
      
        if (experiencia > minimoExperiencia)
        {
            subioDeLVl();
        }
    }

    public void att_habilidades(List<acoes> aux)
    {
        foreach (acoes aux_ in aux)
        {
            aux_.att(nivel);
        }
    }
    public void att_rs(List<habilidade> rs)
    {
        foreach(habilidade aux in rs)
        {
            aux.attLvlJogador(nivel);
        }
    }

    public minhavida acessarVida()
    {
        return minhaVida;
    }

 
    
    
}
/*
 * fazer quando se ganha experiencia 
 * 
 */