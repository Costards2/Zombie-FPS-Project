using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoConstante : MonoBehaviour
{
    public float velocidade;
    // Update is called once per frame
    void Update()
    {
        //Rotacionar o objeto constantemente
        transform.Rotate(Vector3.up * velocidade * Time.deltaTime);
    }
}
