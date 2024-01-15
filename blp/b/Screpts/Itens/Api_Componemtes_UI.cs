using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace ParaTodosOsItens
{
    public class Api_Componemtes_UI : MonoBehaviour
    {
        public RawImage icone;
        public TextMeshProUGUI txt;
        public void iniciar(Texture2D t2)
        {
            icone.texture = t2;
        }
        public void attQuantidade(int q)
        {
            txt.text = q + "";
        }
    }
}
