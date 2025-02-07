using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMng : MonoBehaviour
{
    
    public static AudioMng Instance;

    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public AudioSource audioMusica;
    public AudioSource audioVFX;
    public AudioClip[] audiosCena; // 0 - Menu, 1 - Game

    public float volumeVFX;

    public void MudarVolume(Volume volume){
        volumeVFX = volume.vfx;
        audioMusica.volume = volume.musica;
        audioVFX.volume = volumeVFX;
    }

    public void PlayAudioMenu(){
        if(audioMusica.clip != audiosCena[0]){
            audioMusica.Stop();
            audioMusica.clip = audiosCena[0];
            audioMusica.Play();
        }
    }

    public void PlayAudioGame(){
        if(audioMusica.clip != audiosCena[1]){
            audioMusica.Stop();
            audioMusica.clip = audiosCena[1];
            audioMusica.Play();
        }
    }

    public void PlayAudioVFX(AudioClip audioClip){
        audioVFX.PlayOneShot(audioClip);
    }
}
