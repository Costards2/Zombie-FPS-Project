using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLoadingMng : MonoBehaviour
{
    public static CanvasLoadingMng Instance;
    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    
    public GameObject pnlLoading;

    public void ExibirTelaDeCarregamento(){
        pnlLoading.SetActive(true);
    }

    public void OcultarTelaDeCarregamento(){
        pnlLoading.SetActive(false);
    }
}
