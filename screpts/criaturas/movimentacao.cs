using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentacao : MonoBehaviour
{
    //entrada
    private bool w, a, s, d,shift,space;
    //public float sda;

    //scripts
    chamador _chamar;

    public Vector3 direcao,aux_dir,velocidademodular;

    public bool _teclasTRUE()
    {
        if (w || a || s || d|| shift || space )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 direcao_() { return direcao; }
     void pegatecla()
    {
        if (_chamar.sensores().temchao() == true)
        {
         
            direcao = new Vector3(0, 0, 0);
            
            if (Input.GetKey(KeyCode.Space)) { space = true; } else { space = false; }
            if (Input.GetKeyDown(KeyCode.LeftShift)) { shift = true; }
            if (Input.GetKeyUp(KeyCode.LeftShift)) { shift = false; }

            if (Input.GetKey(KeyCode.A)) { a = true; } else { a = false; }
            if (Input.GetKey(KeyCode.S)) { s = true; } else { s = false; }
            if (Input.GetKey(KeyCode.D)) {; d = true; } else { d = false; }
            if (Input.GetKey(KeyCode.W)) { w = true; } else { w = false; }

            if (_chamar.sensores().tem_frente() == false && Input.GetKey(KeyCode.W)) {   direcao +=transform.forward; }
            if (_chamar.sensores().tem_atras() == false && Input.GetKey(KeyCode.S)) { direcao += -transform.forward; }
            if (_chamar.sensores().tem_direita() == false && Input.GetKey(KeyCode.D)) { direcao +=transform.right; }
            if (_chamar.sensores().tem_esquerda() == false && Input.GetKey(KeyCode.A)) {  direcao +=-transform.right ; }
       
        }
      


    }
    public Vector3 m,_hm_aux;
        private float _aux_pulo;
    public bool _flag_pulo;
    public void pule()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _flag_pulo == false && _aux_pulo <1)
        {
            _hm_aux = transform.position;
            
           

         
         
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _flag_pulo == true)
        {

            _chamar.gravidade().adicionarforca(transform.up, ((_chamar.estatus().forca() / _chamar.estatus().Massa()*10)*_chamar.estatus().agilidade()));
           

        }

        if (_chamar.sensores().temchao() ) { _flag_pulo = true;    } else { _flag_pulo = false; }



    }
    public void rig_mover()
    {

        if (_chamar.estatus().jogador()&& _chamar.sensores().temchao() == true) {

            // _chamar.characterC().Move(direcao* _chamar.estatus().velocidade() *  Time.deltaTime);
            direcao *=(1 * (_chamar.estatus().velocidade() / 100));

        }
        else
        {
            // fazer a mov dos npc
        }
        
        


    }
    public void mover()
    {
      
        pegatecla();
        rig_mover();
        
    }
    public void agachar() { }
    void Start()
    {
        _chamar = gameObject.GetComponent<chamador>();
        
        _flag_pulo = false;
    }
    private void Update()
    {

        pule();
    }
     
}
