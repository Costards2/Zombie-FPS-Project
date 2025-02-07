using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuporteAnimacaoInimigo : MonoBehaviour
{
    private Animator animator;
    private InimigoControlador controlador;
    
    private InimigoDistancia inimigoDistancia;//Variavel que manipula a distancia do inimigo distante

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controlador = GetComponentInParent<InimigoControlador>();

        try{
            inimigoDistancia = GetComponentInParent<InimigoDistancia>();
        }
        catch{}            

        int idIdle = new System.Random().Next(1,6);
        animator?.SetFloat("id_idles",idIdle);

        int idDeath = new System.Random().Next(1,3);
        animator?.SetFloat("id_deaths", idDeath);
    }

    public void PlayIdle(){
        animator?.SetBool("run", false);
        animator?.SetBool("idle", true);
        animator?.SetBool("attack", false);
    }

    public void PlayRun(){
        animator?.SetBool("run", true);
        animator?.SetBool("idle", false);
        animator?.SetBool("attack", false);
    }

    public void PlayAttack(){
        animator?.SetBool("run", false);
        animator?.SetBool("idle", false);
        animator?.SetBool("attack", true);
    }

    public void PlayDeath(){
        animator?.SetTrigger("death");
    }

    public void DanoAoPlayer(){
        controlador?.DanoAoPlayer();
    }

    public void AtaqueDistancia(){
        inimigoDistancia?.AtaqueDistancia();
    }
}
