using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contrucao
{

    [CreateAssetMenu(fileName = "CatalogoVazio", menuName = "Contrucoes/AbaCatalogo", order = 1)]

    public class ScriptableCatalogo : ScriptableObject
    {
        public List<ScriptableObjectContrucaoData> listaDeItens;

    }
}
