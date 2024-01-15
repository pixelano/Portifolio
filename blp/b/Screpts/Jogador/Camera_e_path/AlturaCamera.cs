using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Camera
{
    public class AlturaCamera : MonoBehaviour
    {
        public float _altura_adicional, _altura_minima, _altura_maxima;
        [Range(10,50)]
        public float distancia;
        [Space]
        [Range(0,6)]
        public float _ponto_focal_altura;
        public Transform jogador;
        float distanciaInicial;
        void Start()
        {
            // Calcule a distância inicial entre objetoA e objetoB no início
            distanciaInicial = Vector3.Distance(transform.position, jogador.position);
        }
        void Update()
        {
            float distanciaAtual = Vector3.Distance(transform.position, jogador.position);

            float distanciaRelativa = distanciaAtual - distancia;

            transform.position = Vector3.Lerp(transform.position, transform.position + (transform.forward * distanciaRelativa),Time.deltaTime);

            Vector3 localJ = jogador.transform.localPosition;
            localJ.y = _ponto_focal_altura;
            jogador.transform.localPosition = localJ;
            RaycastHit rh;
            Physics.Raycast(transform.position, -transform.up, out rh);
          

            if(rh.distance > transform.position.y -jogador.transform.position.y)
            {
                _altura_adicional = rh.distance - _altura_minima;
                Vector3 novo = transform.position;
                novo.y -= _altura_adicional *Time.deltaTime;
                transform.position = novo;
                _altura_adicional = 0;
            }

            if (rh.distance < transform.position.y - jogador.transform.position.y)
            {
                _altura_adicional = rh.distance - _altura_maxima;
                Vector3 novo = transform.position;
                novo.y -= _altura_adicional *Time.deltaTime;
                transform.position = novo;
                _altura_adicional = 0;
            }
        }
    }
}
