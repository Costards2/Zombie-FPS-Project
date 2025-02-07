using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMiniMapa : MonoBehaviour
{
    public Camera cameraMiniMapa;
    // Start is called before the first frame update
    void Start()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
    }

    void OnDestroy(){
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
    }

    void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera){
        if(camera == cameraMiniMapa){
            RenderSettings.fog = false;
        }
        else {
            RenderSettings.fog = true;
        }
    }
}
