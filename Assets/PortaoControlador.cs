using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaoControlador : MonoBehaviour
{
    private void OnTriggerEnter(Collider colisor){
        //Verificar se coletou todas as chaves
        if(CanvasGameMng.Instance.ColetouTodasAsChaves() == false) return;

        if(colisor.gameObject.tag.Equals("Player")){
            CanvasGameMng.Instance.DefinirFimDeJogo();
        }
    }
}
