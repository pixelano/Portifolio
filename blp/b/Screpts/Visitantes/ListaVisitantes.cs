using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visitantes
{
    [CreateAssetMenu(fileName = "ListDeVisitantes_", menuName = "Visitantes/ListaDeVisitantes", order = 1)]

    public class ListaVisitantes : ScriptableObject
    {
public        List<FichaVIsitante> lista;
    }
}
