using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosDeHabilidade : MonoBehaviour
{
    public efeito coelho1,pressa1,baiano1;
    public Habilidade_construtor aumentarPulo,aumentarVelocidade,aumentarEstaminaRegem;



    private Habilidade_construtor criarHabilidade(string nome, float contador, chamador go)
    {
        return new Habilidade_construtor(nome,contador,go) ;

   

    }
    private efeito criarEfeito(string nome, string tipo, float duracao, float valorDeEfeito)
    {
        return new efeito( nome,  tipo,  duracao,  valorDeEfeito);  
    }



    private void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            aumentarPulo.aplicar_efeito();
        }
        if (Input.GetKey(KeyCode.K))
        {
            aumentarVelocidade.aplicar_efeito();
        }
        if (Input.GetKey(KeyCode.H))
        {
            aumentarEstaminaRegem.aplicar_efeito();
        }


    }
    private void Start() {
               // ## AREA DESTINADA PARA CRIACAO DE EFEITOS !!!##
        coelho1 = criarEfeito("coelho I", "altura do pulo", 10, 2);
        pressa1 = criarEfeito("pressa I", "velocidade", 10, 2);
        baiano1 = criarEfeito("Regem Estamina", "estamina regem", 10, 0.03f);

               // ## AREA DESTINADA PARA CRIACAO DE HABILIDADE!!!##

        // aumenta o tamanho do pulo maximo
        aumentarPulo = criarHabilidade("sautitante", 15, GetComponent<chamador>());
        aumentarPulo.CriarEfeito(coelho1);
        //aumenta a velocidade base
        aumentarVelocidade = criarHabilidade("corredor", 15, GetComponent<chamador>());
        aumentarVelocidade.CriarEfeito(pressa1);
        //aumenta a regeneração de estamina
        aumentarEstaminaRegem = criarHabilidade("regenerar estamina", 30, GetComponent<chamador>());
        aumentarEstaminaRegem.CriarEfeito(baiano1);



    }

}
