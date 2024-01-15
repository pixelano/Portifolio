using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visitantes
{
    [CreateAssetMenu(fileName = "FichaVisitante_Vazio", menuName = "Visitantes/NovoVisitante", order = 1)]

    public class FichaVIsitante : ScriptableObject
    {
        public string nomeVisitante;
        public float TempoParaRetorno;
        public float nivelFun;
        public GameObject modelo;
        [Space]
        public float paciencia, BarracasQueIraVisitar, TempoDeCaminhada;
    }
}
