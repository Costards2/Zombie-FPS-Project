using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoDistancia : InimigoControlador
{
    public GameObject projetilZumbi;

    public void AtaqueDistancia(){
        var projetil = Instantiate(projetilZumbi);
       projetil.GetComponent<ProjetilInimigo>().AtualizaDanoJogador(danoAoPlayer);
       projetil.transform.position = transform.position + Vector3.up;
       projetil.transform.rotation = transform.rotation;

    }
}
