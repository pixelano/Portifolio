using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Camera;
public class LockAtJogador_ : MonoBehaviour
{
    RotacionarCamera jogador;
    private void Start()
    {
        jogador = FindAnyObjectByType<RotacionarCamera>();
    }
    private void Update()
    {
        if (jogador)
        {
            Vector3 jg = transform.position - jogador.transform.position;
            jg.y = 0;
            Quaternion aux = Quaternion.LookRotation(jg);

            transform.rotation = aux;
        }
    }

}
