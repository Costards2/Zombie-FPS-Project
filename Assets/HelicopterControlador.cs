using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControlador : MonoBehaviour
{
    public Animator animator;
    public GameObject cameraHelicoptero;
    public Transform[] posicoesCamera;
    private int posicaoCamera = 0;
    public AudioSource audioSource;

    void Start(){
        audioSource.volume = AudioMng.Instance.volumeVFX;
    }
    public void IniciarVoo(){
        animator.SetBool("isFly", true);
    }

    public void ExibirTelaFimDeJogo(){
        CanvasGameMng.Instance.ExibirTelaFinal();
    }

    public void MoverCameraProximaPosicao(){
        posicaoCamera++;
        cameraHelicoptero.transform.position = posicoesCamera[posicaoCamera].position;
        cameraHelicoptero.transform.rotation = posicoesCamera[posicaoCamera].rotation;
    }
}
