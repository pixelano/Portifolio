using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
namespace paradasEpontos
{
    public class NomeDeCadaParada : MonoBehaviour
    {
        public TextMeshProUGUI texto;
        public string nome;
        public GerenciadorDeTodasAsEstradas gdtae;
        public void definirNome(string n)
        {
            nome = n;
            texto.text = n;
        }
        public void cliclk()
        {
            gdtae.Click_EscolherParada(nome);
        }

    }
}