 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gerenciadorRender : MonoBehaviour
{
    public List<objOcludRender> instancias;
    public float distanciaMaxima;
    public void novainstancia(objOcludRender aux)
    {
        instancias.Add(aux);
    }
   
    private void Update()
    {
        
            foreach (objOcludRender alvo in instancias)
            {
                float distancia = Vector3.Distance(alvo.transform.position, transform.position);

                int posicao =3- Mathf.Clamp((int)(distancia / distanciaMaxima), 0, 3);

            alterar(alvo, posicao);
            }
       
    }
    private void alterar(objOcludRender alvo , int naposicao)
    {
        switch (naposicao) {

            case 0:
                ativarDesativarObj(alvo, false);
                ativarDesativarMesh(alvo, false,0);
                break;
            case 1:
                ativarDesativarObj(alvo, true);
                ativarDesativarMesh(alvo, false,0);
                break;
            case 2:
                ativarDesativarObj(alvo, true);
                ativarDesativarMesh(alvo, true,0);
                break;
            case 3:
                ativarDesativarObj(alvo, true);
                ativarDesativarMesh(alvo, true, 1);
                break;


        }

    }
    private void ativarDesativarObj(objOcludRender alvo , bool aux)
    {
        alvo.gameObject.SetActive(aux);
    }
    private void ativarDesativarMesh(objOcludRender alvo, bool aux,int valorMesh)
    {
        alvo.ativarDesativarMalha(aux, valorMesh) ;
    }

}
/*
 * - desativado
 * - ativado sem renderizacao
 * - ativado com renderizacao simples
 * - ativado com renderizacao normal
 */