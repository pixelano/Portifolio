using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chamador : MonoBehaviour
{
    /* Todo os objetos devem ser instanciado neles as classes Chamador e Estatus
     * eu 1010 instanciar a Estatus no Chamador mas sempre dava erro
     * então este lembrete é para não me esquecer disto :v
     * ~~desoraka
     * 
     *-- por motivos obvios o Rig_membros tem que ser posto manualmente por não ser pocivel adicionar o scrept em n membros filhos :v
     *   mas vou deichar para o membro pai por facilidade
     *
     */

    public float x, y, z;

    movimentacao _mov;
    estatus _ests;
    cameraC_ _cam;
    CharacterController _charC;
    sensores _sens;
    EfeitosDeHabilidade _efeito;
    Habilidade_construtor _efeito_contrutor;
    Rig_membros _r_m_pai;
    Gravidade grav;
    public Gravidade gravidade() { return grav; }
    public sensores sensores() { return _sens; }
    public movimentacao movimentacao() { return _mov; }
    public CharacterController characterC() { return _charC; }
    public cameraC_ camera_() { return _cam; }
    public estatus estatus() { return _ests; }
    public Rig_membros rmembros() { return _r_m_pai; }
    
   
    //habilidades
    public EfeitosDeHabilidade efeito() { return _efeito; }
    void teclado()
    {
        if (Input.anyKey || GetComponent<movimentacao>()._teclasTRUE())
        {
            
               GetComponent<movimentacao>().mover();
            }
    }
   public void Start()
    {
      
        _ests = gameObject.GetComponent<estatus>();

        gameObject.AddComponent<movimentacao>();
        gameObject.AddComponent<sensores>();
        gameObject.AddComponent<CharacterController>();
        gameObject.AddComponent<EfeitosDeHabilidade>();
        gameObject.AddComponent<Habilidade_construtor>();
        gameObject.GetComponent<CalculaArea>();
        gameObject.AddComponent<Gravidade>();

        _r_m_pai =  gameObject.GetComponent<Rig_membros>();
      

        if (estatus().jogador()) {
            _sens = gameObject.GetComponent<sensores>();
            _cam = gameObject.GetComponentInChildren<cameraC_>();
            _charC = gameObject.GetComponent<CharacterController>();
            _mov = gameObject.GetComponent<movimentacao>();
            _efeito = gameObject.GetComponent<EfeitosDeHabilidade>();
            _efeito_contrutor = gameObject.GetComponent<Habilidade_construtor>();
            grav = gameObject.GetComponent<Gravidade>();
        }

        else { if (estatus().item())
            {
                Debug.Log("erro jogador falso");
                //classes de item ??
            }
            else
            {
                Debug.Log("erro jogador falso");

                //classes de NPC

            }
        }
      
    }

    void Update()
    {
        estatus().estamina();
        teclado();

        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;



    }
}

    /*              start
     * definir um tamanho generico de lista
     * ao adicionar na lista aumentar o contador da lista
     * e o tamanho da lista deve ser baseado no contador
     * 
     *              loop
     * os objetos da lista devem atualizar os seus valores contantimente
     * 
     * 
     * float aux2 = 0;
            auxA = 0;
            auxB = 0;
            foreach (int aux in _listavida)
            {
                aux2++;
                if (aux2 % 2 == 0)
                {
                    auxA += aux;
                }
                else
                {
                    auxB += aux;
                }

            }

            variavelfinalB = auxB;
            variavelfinalA= auxA;
     * 
     */



