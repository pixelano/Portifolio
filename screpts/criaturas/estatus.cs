using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// classe somente para leitura

public class estatus : MonoBehaviour
{
    /* 
     * Estatus foi feito para centralizar todos os dados do jogador
     * como os Efeitos só afetam os Estatus achei pratico deichar no mesmo script 
     *
     * --velocidade --
     * velocidadefinal = é a velocidade que se esta
     * _base_velocidade = para quando não se estiver correndo
     * _base_velocidade_maxima = é o maximo de velocidade que eu POSSO chegar
     * _base_velocidade = a menor velocidade que eu POSSO andar
     * _base_velocidade_bonus = é o valor que se é somado na velocidade final para se chegar na velocidade maxima
     * 
     * 
     */

    private float forca_,_agilidade;

    public float x, y, z,_tempo;
    //velocidade
    private float _base_velocidade,_base_velocidade_bonus,_base_velocidade_maxima, velocidadefinal, _degres_velocidade, _add_velocidade;
    private bool _jogador, _item;
    private float _estamina;
    private float _agrs_estamina,_degres_estamina,_max_estamina, _furtividade;
    private float _alturaDePulo;
    private bool _pulando;

    private float _vida,_vida_maxima,_defesa,_;

    Lista_efeitos _lista_efeitos;
    float[] _efeitos_base;

    //          propriedades fisicas ? :v
    private float massa;
    public float Massa() { return massa; }
    public float forca() { return forca_; }
    public float agilidade() { return _agilidade; }
    public void estamina() { 
        if (_estamina < 0) _estamina = 0;

        if(_estamina<_max_estamina && _base_velocidade_bonus == _degres_velocidade) {
            _estamina += _agrs_estamina; }
    
        if (_base_velocidade_bonus == _add_velocidade && _estamina> 0) {
        
            _estamina -= _degres_estamina; }

   
    }

    public bool jogador() { return _jogador; }
    public bool item() { return _item; }
    public float velocidade()
    {
      
        if (_estamina > 0)
        {

            if (Mathf.Round(velocidadefinal) <= _base_velocidade_maxima && Mathf.Round(velocidadefinal) >= _base_velocidade || _base_velocidade_bonus == _add_velocidade && Mathf.Round(velocidadefinal) < _base_velocidade_maxima)
            {


                if (Mathf.Round(velocidadefinal) != _base_velocidade_maxima || _base_velocidade_bonus == _degres_velocidade)
                {
                    velocidadefinal += _base_velocidade_bonus * forca() / Massa();
                }

            }
            if (Mathf.Round(velocidadefinal) < _base_velocidade) { velocidadefinal = _base_velocidade; }
            if (Mathf.Round(velocidadefinal) > _base_velocidade_maxima) { velocidadefinal = _base_velocidade_maxima--; }

        }
        else { velocidadefinal = _base_velocidade; }
        Mathf.Round(velocidadefinal);
       
        
        return  velocidadefinal;

    }
    
    public void correr(bool a) { if (a == true) { _base_velocidade_bonus = _add_velocidade; } else { _base_velocidade_bonus = _degres_velocidade; } }
    public bool pulando() { return _pulando; }
    public float alturaPulo()
    {
        return _alturaDePulo;
    }
    public void att_vida(float aux,float aux2)
    {
        _vida = aux;
        _vida_maxima = aux2;
    }
    // efeitos
    public void adicionar_efeito(efeito _efeito)
    {            _lista_efeitos.adicionar_primeiro(_efeito);  }   
    private void att_estatus_base()
    {
        _max_estamina = _efeitos_base[1] + _lista_efeitos.somadeAtributos[1, 1];
        _agrs_estamina = _efeitos_base[2] + _lista_efeitos.somadeAtributos[2, 1];

        _base_velocidade = _efeitos_base[3] + _lista_efeitos.somadeAtributos[3, 1];
        _base_velocidade_maxima = _base_velocidade * 3;

        _alturaDePulo = _efeitos_base[5] + _lista_efeitos.somadeAtributos[5, 1];
       
    }
    private void pegaBaseInicial()
    {
        _efeitos_base[1] = _max_estamina ;
        _efeitos_base[2] = _agrs_estamina;
        _efeitos_base[3] = _base_velocidade;
      //  _efeitos_base[4] = _base_velocidade_maxima ;
        _efeitos_base[5] = _alturaDePulo;
    }
    void Start()
    {
        _jogador = true;
        _lista_efeitos = gameObject.AddComponent<Lista_efeitos>();
        _efeitos_base = new float[gameObject.AddComponent<efeito>().quantidade_tipos() + 1];

        // afetados por efeito
        _add_velocidade = 0.05f; // contrario de _degres_velocidade
        _degres_velocidade = -1f; // foi criado para poder controlar a desaceleração      
        _base_velocidade = 3; // andar maix rapido *afeta a velocidade maxima
        _base_velocidade_maxima = _base_velocidade * 3; // o quanto de velocidadefinal eu posso chegar
       
        _alturaDePulo = 4; // define qunatos "metros" pode-se pular
        _estamina = 99; // estamina atual

        _agrs_estamina = 0.01f;
        _degres_estamina = 0.1f;
        _max_estamina = 100;
        // sistemas ainda não implementados

        massa = 50;
        forca_ = 25;
        _agilidade = 20;
        // vida <--




        //não afetados por efeito
        _base_velocidade_bonus = 0; // correr mais rapido ate chegar no limite
        velocidadefinal = 0;
        _item = false;
        
        pegaBaseInicial();

    }
    private void Update()
    {
        // att estatus
        att_estatus_base();
        _tempo = Time.realtimeSinceStartup;
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;


       
    }
}
public class efeito : MonoBehaviour
{
    // *** EFEITOS SÃO SÓ PARA ALTERAR OS ATRIBUTOS DOS ESTATUS !!!! ***//

    /*
     * Para poder fazer varios tipos de efeitos diferentes a classe efeito foi feita
     * uma algoritimo generico para criar varios efeitos
     * 
     * ** Lembretes**
     * filtrar pelo _tipo
     * somar todos os _valorDeEfeito em uma unica variavel quando instanciado
     * e somar com o respectivo atributo no estatus
     * 
     */
    public string _nome,_tipo;
    float _duracao,_tempo_i, _valorDeEfeito;
    public efeito() { }
    public efeito(string nome,string tipo, float duracao, float valorDeEfeito)
    {
        _nome = nome;
        _tipo = tipo;
        _duracao = duracao;
        _valorDeEfeito = valorDeEfeito;
    }
    public float valorDeEfeito() { return _valorDeEfeito; }
    public int tipo() {
        /* por motivo de não conseguir criar uma array que o index é baseado em string :v
         *  aqui vai ser convertido string em int, segue a tabela de converção ^.~
         *  
         *   !! ao adicionar mais tipos de efeitos tem que atualizar o resetar estatus e o defir base em estatus
         *  
         *  3 = velocidade = altera a velocidade base e a velocidade maxima
         *  5 = pulo = aumenta o limite do pulo
         *  
         *  implementar sistema de vida e de estamina
         *  
         *  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\
         *  |0      |   Null              | \
         *  |1      |   estamina maxima   | |
         *  |2      |   estamina regem    | |
         *  |3      |  velocidade  | |
         *  |4      |    | |
         *  |5      |  altura do pulo     | |
         *  |6      |                     
         *  |7      |
         *  |8      |                
         *  |9      |                
         *   ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~| |
         *   \                             \|
         *    \_____________________________\
         */

        switch (_tipo)
        {
            case "estamina maxima":
                return 1;
                break;

            case "estamina regem":
                return 2;
                break;

            case "velocidade":
                return 3;
                break;

            case "---------":
                return 4;
                break;

            case "altura do pulo":
                return 5;
                break;

            default:
                return 0;
                break;
        }
        

        return 0;
        
        
    }
    public int quantidade_tipos() { return 5 +1; } // ele diminui 1 pro zero O.O


    public bool acabou()
    {

     
        
        if((Time.realtimeSinceStartup - _tempo_i)   > _duracao)
        {
            return true;
        }
        else { return false; }

        
    }
    public void tempoinicial() { _tempo_i = Time.realtimeSinceStartup; }
    
   public string teste()
    {
        return _nome;
    }

    public float teste2()
    {
        return (_tempo_i);
    }
    public float teste3()
    {
        return _duracao;
    }
    private void Update()
    {
        
       
    }


}
/* Gomu e Lista_efeitos <- Lista encadeada para armazenar os efeitos ativos no personagem 
 *
 * **Lembrete**
 * quando me lembrar como que se faz aquela coisa de herdar classe e alterar uma função implementar aqui para não precisar fazer varias
 * listas encadeadas e sim uma central com varias ramificações
 * 
 * Fazer função que filtra os efeitos por tipo PARA LAYOUT!!! <--
 */

public class Gomu : MonoBehaviour
{
   public Gomu _proximo;
    public efeito _efeito;
    
    public void definir_proximo(Gomu aux) { _proximo = aux; }
    public void definir_efeito(efeito aux) { _efeito = aux; }



}
public class Lista_efeitos : MonoBehaviour
{
   public float[,] somadeAtributos;

    public int contador;
    public Gomu primeiro, ultimo , auxiliar;
    

    public bool vazia()
    {
        if (contador == 0)
        {
            return true;
        }
        else return false;
    }
    public void adicionar_primeiro(efeito aux)

    {
        Gomu novo = gameObject.AddComponent<Gomu>();
        novo.definir_efeito(aux);

        if (vazia())
        {

            primeiro = novo;
            ultimo = novo;
            auxiliar = primeiro;
        }
        else
        {
            novo.definir_proximo(primeiro);
            primeiro = novo;          
                    }
        contador++;

    }
    private void remover()
    {
        //voce quer remover o aux
        if( contador == 1)
        {
            primeiro = transform.GetComponent<Gomu>();
            ultimo = primeiro;

            contador = 0;
            auxiliar = primeiro;
    //        resetar_valores();

        }
        else
        {
            if (auxiliar == primeiro)
            {
                primeiro = auxiliar._proximo;
                primeiro.definir_proximo(auxiliar._proximo._proximo);
                auxiliar.definir_proximo(primeiro);

            }
            else
            {


                if (auxiliar == ultimo)
                {
                    ultimo = ache_anterior(auxiliar, primeiro);
                    ultimo.definir_proximo(primeiro);
                    //   ache_anterior(auxiliar, primeiro).definir_proximo(primeiro);
                    auxiliar.definir_proximo(primeiro);
                }
                else
                {
                    ache_anterior(auxiliar, primeiro).definir_proximo(auxiliar._proximo);
                    auxiliar.definir_proximo(auxiliar._proximo);
                }
            }
            contador--;
        }

    }
    private Gomu ache(Gomu aux,Gomu aux2)
    {
        //não pode ser usado quando a lista esta vazia
        // aux2 tem que ser a primeira
        // voce quer achar o aux

        auxiliar = aux2;

        if (vazia()||ultimo != aux) {
            return null;
                }

        if(auxiliar == aux)
        {
            return auxiliar;
        }
        else
        {
            return ache(aux, auxiliar._proximo);
        }

    }
    private Gomu ache_anterior(Gomu aux, Gomu aux2)
    {
        //não pode ser usado quando a lista esta vazia
        // aux2 tem que ser a primeira
        //aux principal e tu quer achar quem vem atras dele
        //  ache aux apartir de aux2
       
        if(aux == primeiro) {
             return ultimo; }
       

        if (aux2._proximo == aux)
        {           
            return aux2;
        }
        else
        {
            return ache_anterior(aux, aux2._proximo);
        }

    }
    //atribuir a soma de todos os elementos em uma variavel para cada tipo de elemento
    private void soma_valores()
    {

        if (vazia() == false)
        {
            somadeAtributos[auxiliar._efeito.tipo(),0] += auxiliar._efeito.valorDeEfeito();
        }
    }
 
    bool auxiliar_resetar_b, aux_corre_b;
    private void resetar_valores()
    {
        if (auxiliar_resetar_b == true)
        {
           
             
                somadeAtributos[0, 1] = somadeAtributos[0, 0];
                somadeAtributos[1, 1] = somadeAtributos[1, 0];
                somadeAtributos[2, 1] = somadeAtributos[2, 0];
                somadeAtributos[3, 1] = somadeAtributos[3, 0];
                somadeAtributos[4, 1] = somadeAtributos[4, 0];
                somadeAtributos[5, 1] = somadeAtributos[5, 0];

                somadeAtributos[0, 0] = 0;
                somadeAtributos[1, 0] = 0;
                somadeAtributos[2, 0] = 0;
                somadeAtributos[3, 0] = 0;
                somadeAtributos[4, 0] = 0;
                somadeAtributos[5, 0] = 0;

                auxiliar_resetar_b = false;
                aux_corre_b = true;                     
     
        }

    }

    private void corre_lista()
    {

        if (vazia())
        {
            somadeAtributos[0, 1] = 0;
            somadeAtributos[1, 1] = 0;
            somadeAtributos[2, 1] = 0;
            somadeAtributos[3, 1] = 0;
            somadeAtributos[4, 1] = 0;
            somadeAtributos[5, 1] = 0;
        }
        else
        {        
            if (vazia() == false)
            {
                if (auxiliar._efeito.acabou())
                {
                    remover();                        
                }

                if (auxiliar == ultimo)
                {
                   
                        soma_valores();
                                            
                        auxiliar = primeiro;
                    
                        auxiliar_resetar_b = true;
                        aux_corre_b = false;
                  
                }
                else
                {          
                    auxiliar_resetar_b = false;

                    soma_valores();
                    auxiliar = auxiliar._proximo;
                }
            }         
        }
    }

    // testar se alguem pode ser removido
    private void Update()
    {        
        resetar_valores();

        if (aux_corre_b == true)
        {     corre_lista();                  
        }
    }
    private void Start()
    {
        contador = 0;
        auxiliar_resetar_b = false;
        aux_corre_b = true;

         primeiro = gameObject.AddComponent<Gomu>();
         ultimo = gameObject.AddComponent<Gomu>();
        auxiliar = primeiro;

        somadeAtributos = new float[GetComponent<efeito>().quantidade_tipos(), 2];
            }
}
