using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Interacoes
{
    public class TocarSom : MonoBehaviour,IAtivarComClick
    {
        public AudioClip audio_;

        private  List<AudioSource > audioSource;

        public void executar(GameObject aux) { }
        public void executar()
        {
            if(audioSource == null)
            {
                audioSource = new List<AudioSource>();
            }
            AudioSource audioSource_ = gameObject.AddComponent<AudioSource>();


            // Atribui o áudio ao AudioSource
            audioSource_.clip = audio_;

            // Toca o áudio
            audioSource_.Play();
            audioSource.Add(audioSource_);

            // Programa a remoção do AudioSource após a duração do áudio
            Invoke("RemoverAudioSource", audio_.length);
        }

        private void RemoverAudioSource()
        {
           
            Destroy(audioSource[0]);
            audioSource.RemoveAll(x => x == null);
        }

    }
}
