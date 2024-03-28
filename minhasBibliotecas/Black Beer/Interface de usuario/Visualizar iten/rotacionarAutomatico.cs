using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacionarAutomatico : MonoBehaviour
{
    public float velocidadeRotacao = 100.0f;
    public bool rotacionar = true;
    private void Update()
    {
        if (rotacionar)
        {
            transform.Rotate(transform.up* velocidadeRotacao * Time.deltaTime);
        }
    }
}
