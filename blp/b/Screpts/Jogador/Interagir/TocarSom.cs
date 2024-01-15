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


            // Atribui o �udio ao AudioSource
            audioSource_.clip = audio_;

            // Toca o �udio
            audioSource_.Play();
            audioSource.Add(audioSource_);

            // Programa a remo��o do AudioSource ap�s a dura��o do �udio
            Invoke("RemoverAudioSource", audio_.length);
        }

        private void RemoverAudioSource()
        {
           
            Destroy(audioSource[0]);
            audioSource.RemoveAll(x => x == null);
        }

    }
}
