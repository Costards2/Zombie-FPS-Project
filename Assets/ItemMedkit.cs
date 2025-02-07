using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMedkit : MonoBehaviour
{
    public AudioClip audioMedkit;
    private void OnTriggerEnter(Collider colisor){
        if(colisor.gameObject.tag == "Player"){
            AudioMng.Instance.PlayAudioVFX(audioMedkit);
            CanvasGameMng.Instance.IncrementarVidaJogador();
            Destroy(gameObject);
        }
    }
}
