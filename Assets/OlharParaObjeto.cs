using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlharParaObjeto : MonoBehaviour
{
    public GameObject alvo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Fazer o texto "Olhar" para o jogador
        gameObject.transform.LookAt(alvo.transform);
    }
}
