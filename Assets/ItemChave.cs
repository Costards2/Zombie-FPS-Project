using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChave : MonoBehaviour
{
    public AudioClip audioChave;
    private void OnTriggerEnter(Collider colisor){
        if(colisor.gameObject.tag.Equals("Player")){
            AudioMng.Instance.PlayAudioVFX(audioChave);
            CanvasGameMng.Instance.IncrementarChave();
            Destroy(gameObject);
        }
    }
}
