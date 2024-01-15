using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParaTodos
{
    public class ReproduzirSom : MonoBehaviour
    {
        public AudioClip audio__;
        public void executar()
        {
            AudioSource audioSource__ = GetComponent<AudioSource>();

            if (audioSource__ == null)
            {
                audioSource__ = gameObject.AddComponent<AudioSource>();
            }

            audioSource__.clip = audio__;
            audioSource__.Play();

            Destroy(gameObject, audio__.length);
        }
    }
}
