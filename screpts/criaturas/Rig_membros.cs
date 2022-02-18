using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rig_membros : MonoBehaviour
{
    /*vida
     * defesa
     */
    private float[,] _listavida ;
    private int _conta;
    Rig_membros[] _listamembros;
    Rig_membros pai;



    
    public float _defesa, _vida, _vida_maxima,_nerf_Dano;
    public bool membro_vitale;
    private float _aux_vida, _aux_vidaMaxima;
    private bool _pai;
    private int _id;

    public float vida() { return _vida; }
    public float vida_maxima() { return _vida_maxima; }
    public void aumentar_vida(float aux)
    {
        if (_vida < _vida_maxima)
        {
            _vida += aux;
        }
    }
    public void diminuir_vida(float dano, float defesa_) { _vida -= (dano / defesa_); }
    public void aumentar_vida_maxima(float aux) { _vida_maxima += aux; }
    public void diminuir_vida_maxima(float aux) { _vida_maxima -= aux; }

    private void att_vida()
    {
        if (_pai == false)
        {
            pai._listavida.SetValue(vida_maxima(), _id, 0);
            pai._listavida.SetValue(vida(), _id, 1);
        }

    }
    private void somador_vida()
    {

        if (_pai == true)
        {
            // impar = 1 = vida maxima
            // par = 0 = vida
            float aux2 = 0;
            _aux_vida = 0;
            _aux_vidaMaxima = 0;
            foreach (int aux in _listavida)
            {
                aux2++;
                if (aux2 % 2 == 0)
                {
                    _aux_vida += aux;
                }
                else
                {
                    _aux_vidaMaxima += aux;
                }

            }
            _vida_maxima = _aux_vidaMaxima;
            _vida = _aux_vida;

            GetComponent<estatus>().att_vida(_vida, _vida_maxima);
        }
    }

    public float defeza() { return _defesa; }    
    public void diminuir_defesa(float aux) { _defesa-= aux ; }
    public void aumentar_defesa(float aux) { _defesa += aux; }
    public void criar_membro()
    {
        if (transform.root.GetComponentInParent<Rig_membros>() != transform.GetComponent<Rig_membros>())
        {
            pai = transform.root.GetComponentInParent<Rig_membros>();
            _pai = false;

            _id = pai._conta;
            pai._conta++;
            pai._listavida = new float[pai._conta,2];

        }
    }
    
    private void Update()
    {
        if (_defesa <= 0)
            _defesa = 1;
        if (membro_vitale == true && _vida <= 0)
            // fazer o personagem morrer automaticamente
            if (_vida > _vida_maxima)
                _vida = _vida_maxima;   
        att_vida();
        somador_vida();
    }
    private void Start()
    {
        
        _vida = _vida_maxima;

        if (transform.root.GetComponentInParent<Rig_membros>() == transform.GetComponent<Rig_membros>())
        {
            _pai = true; _listavida = new float[10,2];
        }

        criar_membro();

    }



}
