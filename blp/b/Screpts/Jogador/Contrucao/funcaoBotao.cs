using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace Contrucao
{
    public class funcaoBotao : MonoBehaviour
    {
        GameObject catalogo;
        public TextMeshProUGUI texto;

        public void definirCatalogo(GameObject aux)
        {
            catalogo = aux;
           texto.text =gameObject.name;
            gameObject.name = "botao_" + gameObject.name;
        }

        public void MudarOpacidade()
        {
            catalogo.SetActive(!catalogo.active);
        }
    }
}
