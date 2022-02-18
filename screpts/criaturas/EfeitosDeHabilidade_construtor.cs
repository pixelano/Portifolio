using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade_construtor : MonoBehaviour  {
    /*Implementar
     * tempo de conjuração
     * 
     * implentar cdr
     * 
     * gambiarra para poder usar new e tirar o monobehavior 
     * to usando o CalculaArea para usar o update
     *
     */
    private string _nomeHabilidade, _nomeClasse, _elemento;
    private float _cooldown, _level_minimo, _level_habilidade, _level_xp,_tempo_duracao, _tempo_inicial;

    private int _reUso;
    private Custos  _custosDehabilidade;
    private Danos _danosDahabilidade;
    private Multiplicadores _multiplicadorDeHabilidade;
    private CalculaArea _areadeefeito;
    private bool _efeito_b, _cooldown_trava, _trava;
    private efeito _efeito;
    // pegar obj
    private GameObject _objeto;

    public Habilidade_construtor(string nomeHabilidade ,float tempodeRecarga ,chamador _chamadorGO) {
        _reUso = 1;
        _nomeHabilidade = nomeHabilidade; // nome principal da classe

       // _t_efeito = TipoDeHabilidae; // ataque, defesa , cura ...
       //  _elemento = elemento; // Fisico ou magia
        _cooldown = tempodeRecarga; // tempode de cd
        _level_minimo = 0; // level minimo do personagem para se ter a habilidade
      //  _custosDehabilidade = null; // custo dos tipos de habilidade estamina e/ou magia
      //  _danosDahabilidade = danoDahabilidade; // danos dos tipos de habilidade
      //  _multiplicadorDeHabilidade = multiplicadorDeHabilidade ; // multiplicadores dos tipos de habilidades
        _areadeefeito = new CalculaArea(_chamadorGO); // a area em que a habilidade vai pegar
        _trava = false;
        _cooldown_trava = false;
        _tempo_duracao = 0;

    }
    //atributos de habilidade
    public void reuso(int aux) { _reUso = aux; }  // quantas vezes a habilidade pode ser usada antes de entrar em cd
    public void area_dano(int x, int z, int y) { _areadeefeito = new CalculaArea(x, z, y); } // nativo ser por contato
    public void dano_contato(bool aux) { _areadeefeito = new CalculaArea(aux); }
    public void CriarEfeito(string nomeDoefeito, float tempoDoefeito, float valorDoEfeito,string tipo)
    { //implementar poder criar varios efeitos por habilidade
        _efeito_b = true;
        _efeito = new efeito(nomeDoefeito, tipo, tempoDoefeito, valorDoEfeito);
    }
    public void CriarEfeito(efeito aux)
    { //implementar poder criar varios efeitos por habilidade
        _efeito_b = true;
        _efeito = aux;
    }

    public void aplicar_efeito()
    {

        /* ultima parte para o basico funcionar
         *
         *botar uma flag de unico ou continuo
         */
        if (_trava==false) {
            _efeito.tempoinicial();
            _tempo_inicial = Time.realtimeSinceStartup;
            _trava = true;
        }


        if (trava() == false)
        {

            _areadeefeito.objeto_alvo().estatus().adicionar_efeito(_efeito);



            if (Time.realtimeSinceStartup - _tempo_inicial >= _tempo_duracao)
            {
                trava(true);
            }

        }
        else
        {
            if (Time.realtimeSinceStartup - _tempo_inicial > _cooldown)
            {
                trava(false);

            }
        }
       
        

            
        
    }

    public void trava(bool aux)
    {
        _trava = aux;
        _cooldown_trava = aux;

    }
    public bool trava()
    {
        return _cooldown_trava;
    }
    

    public void continuo(float tempoDuracao) {      _tempo_duracao = tempoDuracao;    
    }
   
    public float tempoinicial() { return _tempo_inicial; }
    public float tempoduracao() { return _tempo_duracao; }
    public float cooldown() { return _cooldown; }





    public string teste() { return _nomeHabilidade; }
    public bool trava_()
    {
        return _trava;
    }
    public CalculaArea area()
    {
        return _areadeefeito;
    }


    //  _nomeClasse  implementar um gerador de nome de classe



    // _level_habilidade     _level_xp    _t_efeito    _elemento   <- coisa basica de se implementar
    /* criar funções que alteram tanto a classe estatus quanto oo tranform do game object
     * 
     *    #fisico#
     * velocidade
     * estamina maxima
     * regeneração de estamina
     * altura do pulo
     * furtividade
     * vida maxima
     * regenerar vida
     * aumentar defesa
     * 
     * 
     *  #### esses fazer depois que tudo tiver pronto
     *  
     *  
     * #espiritual
     * 
     * 
     * #magico#
     * 
     * fazer sistema de encantamento de itens
    */

    private void Update()
    {
        if (trava() == true)
        {
            if (Time.realtimeSinceStartup - (tempoinicial() + tempoduracao()) >= cooldown())
            {
                trava(false);
            }
        }
    }
}
public class Danos {
    float _dano_magico, _dano_fisico;
    string _modo_dano,_tipo_dano;
    public Danos(float[] aux2)
    {
        aux2 = new float[3];
        if (aux2 == null)
        {
            _dano_magico = 0;
            _dano_fisico = 0;
            _modo_dano = "Sem efeito";

        }
        else
        {
            if (aux2[0] != 0 && aux2[1] == 0)
            {
                _dano_magico = 0;
                _dano_fisico = aux2[0];
                _modo_dano = "efeito fisico";
            }
            if (aux2[1] != 0 && aux2[0] == 0)
            {
                _dano_magico = aux2[1];
                _dano_fisico = 0;
                _modo_dano = "efeito magico";
            }
            if (aux2[0] != 0 && aux2[1] != 0)
            {
                _dano_magico = aux2[1];
                _dano_fisico = aux2[0];
                _modo_dano = "efeito fisico e magico";
            }
            if (aux2[2] == 1)
            {
                _modo_dano += " de cura";
                _dano_magico = -_dano_magico;
                _dano_fisico = -_dano_fisico;
            }
            if (aux2[2] == 0)
            {
                _modo_dano += " de dano";
            }
        }
    }
    public Danos()
    {
        _dano_magico = 0;
        _dano_fisico = 0;
        _modo_dano = "Sem dano";
    }
    public string modo_dano()
    {
        return _modo_dano;
    }
}
public class Custos
{
    float _custo_magico, _custo_fisico;
    string _tipo_custo;
    public Custos(float[] aux2)
    {
        aux2 = new float[2];
        if (aux2 == null)
        {
            _custo_magico = 0;
            _custo_fisico = 0;
            _tipo_custo = "Sem custo";
        }
        if (aux2[0] != 0 && aux2[1] == 0)
        {
            _custo_magico = 0;
            _custo_fisico = aux2[0];
            _tipo_custo = "Custo fisico";
        }
        if (aux2[1] != 0 && aux2[0] == 0)
        {
            _custo_magico = aux2[1];
            _custo_fisico = 0;
            _tipo_custo = "Custo magico";
        }
        if (aux2[0] != 0 && aux2[1] != 0)
        {
            _custo_magico = aux2[1];
            _custo_fisico = aux2[0];
            _tipo_custo = "Custo fisico e magico";
        }

    }

    public Custos()
    {
        _custo_magico = 0;
        _custo_fisico = 0;
        _tipo_custo = "Sem custo";
    }
}
public class Multiplicadores
{
    float _multiplicador_magico, _multiplicador_fisico;
    public Multiplicadores(float[] aux2)
    {
        aux2 = new float[2];
        if (aux2 == null)
        {
            _multiplicador_magico = 0;
            _multiplicador_fisico = 0;
           
        }
        if (aux2[0] != 0 && aux2[1] == 0)
        {
            _multiplicador_magico = 0;
            _multiplicador_fisico = aux2[0];
         
        }
        if (aux2[1] != 0 && aux2[0] == 0)
        {
            _multiplicador_magico = aux2[1];
            _multiplicador_fisico = 0;
           
        }
        if (aux2[0] != 0 && aux2[1] != 0)
        {
            _multiplicador_magico = aux2[1];
            _multiplicador_fisico = aux2[0];
           
        }

    }
    public Multiplicadores()
    {
        _multiplicador_magico = 0;
        _multiplicador_fisico = 0;
        
    }

}
public class CalculaArea : MonoBehaviour
{

    chamador objetoalvo;
    bool _porcontato;
    int _scale_x, _scale_z,_scale_y;
    Habilidade_construtor gambiarra;

    public CalculaArea(chamador aux) {
        //auto efeito
        objetoalvo = aux;
    }
    public CalculaArea( bool aux) // true = por contato   /   false = em si mesmo
    {
       // implementar por contato

    
    }

    public chamador objeto_alvo()
    {
        return objetoalvo;
    }

    public CalculaArea(int x,int z,int y)
    {
        //calcular o cubo da area
        // e pegar o game object alvo
    }

    public bool porcontato() { return _porcontato; }
    private void Start()
    {
        objetoalvo = gameObject.GetComponent<chamador>();
    }



}
