using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilInimigo : MonoBehaviour
{
    public float velocidade;
    private int danoAoJogador;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
    }

    public void AtualizaDanoJogador(int dano){
        danoAoJogador = dano;
    }

    private void OnTriggerEnter(Collider colisor){
        if(colisor.gameObject.tag == "Player"){
            CanvasGameMng.Instance.DecrementarVidaJogador(danoAoJogador);
        }
        Destroy(gameObject);
    }
}
