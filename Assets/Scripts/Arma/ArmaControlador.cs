using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaControlador : MonoBehaviour
{
    private Animator animator;
    private int pente; //Armazena a quantidade de balas no pente da arma
    public int municaoPorPente; //Quantidade de bala máxima que o pente suporta
    public int municaoMaxima; //Quantidade de munição máxima que a arma pode ter
    private int municaoAtual; //Quantidade de munição atual da arma
    public float danoInimigo; //Valor do dano a vida do inimigo
    public GameObject capsula; //A capsula ejetada após atirar
    public Transform posicaoCapsula; //Posição de onde a capsula será ejetada
    public GameObject muzzleFlash; //Fogo que sai após atirar
    public AudioClip[] audios; //0 - Tiro, 1 - Recarregamento, 2 - Arma Sem Bala, 3 - Usar Arma
    public AudioSource audioSource;

    public int Pente {
        get{return pente;}
    }
    public int MunicaoAtual{
        get{return municaoAtual;}
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pente = municaoPorPente;
        municaoAtual = municaoMaxima;

        //Configurar o volume do audio da arma
    }

    private void PlayDisparo(){
        animator.SetBool("Fire", true);
        animator.SetBool("Idle", true);
        animator.SetBool("Reload", false);
    }
    private void PlayCancelarDisparo(){
        animator.SetBool("Fire", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Reload", false);
    }
    private void PlaySemMunicao(){
        animator.SetBool("Fire", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Empty", true);
    }
    private void PlayRecarregar(){
        animator.SetBool("Reload", true);
        animator.SetBool("Idle", true);
        animator.SetBool("Empty", false);
    }

    //Permitir ativar a animação de disparo
    public void Disparar(){
        //Verificar se tem bala no pente
        if(pente > 0){
            PlayDisparo();
        }
    }
    
    //Cancelar o disparo
    public void CancelarDisparo(){
        PlayCancelarDisparo();
    }

    //Recarregar Arma
    public void RecarregarArma(){
        //Se arma tem munição e verificar a quantidade de municao no pente
        if(municaoAtual > 0 && pente < municaoPorPente){
            //Acionar o audio de recarregamento

            //Calcular uma diferença entre a munição que eu posso ter no pente com o que eu ja tenho no pente
            int diferenca = municaoPorPente - pente;

            //Verificar se a diferença é menor que a quantidade munição atual
            if(diferenca < municaoAtual){
                pente += diferenca;
                municaoAtual -= diferenca;
            }
            else{
                pente += MunicaoAtual;
                municaoAtual = 0;
            }
            PlayRecarregar();
        }
    }

    private void InstanciarBala(){
        //Dar dano ao objeto
        PlayerManager.disparoPlayer.DanoAoObjeto();

        pente--;

        //Verificar se acabou o pente
        if(pente <= 0){
            //Ativar o audio de sem munição

            PlaySemMunicao();
            pente = 0;
        }

        //Ativar o audio do disparo

        //Ativar o muzzleFlash
        muzzleFlash.SetActive(true);

        //Instanciar a capsula
        
        GameObject cp = Instantiate(capsula);
        cp.transform.position = posicaoCapsula.position;
        cp.transform.rotation = posicaoCapsula.rotation;
        cp.GetComponent<EjetarCapsula>().Ejetar();
    }

    //Incrementar mais munições a arma
    public void IncrementarMunicao(int municao){
        municaoAtual += municao;
        if(municaoAtual > municaoMaxima){
            municaoAtual = municaoMaxima;
        }
    }

    //Ativar o audio de Selecao da Arma
    public void AtivarAudioSelecaoArma(){
        
    }
}