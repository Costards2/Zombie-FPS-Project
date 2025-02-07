using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InimigoControlador : MonoBehaviour
{
    private NavMeshAgent agent;
    public float velocidade;

    public Slider sldVida;
    public float distanciaDoPlayer;
    public float distanciaPerseguicao;
    public bool estaPerseguindo = false;
    public LayerMask layerMask;
    public int danoAoPlayer; 
    private CapsuleCollider capsuleCollider;
    private bool estaMorto = false;
    private bool estaVendoPlayer = false;
    private SuporteAnimacaoInimigo suporteAnimacaoInimigo;
    public GameObject barraDeVida;
    public float vida;    
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocidade;

        sldVida.maxValue = vida;
        sldVida.value = vida;

        estaVendoPlayer = false;
        estaMorto = false;

        capsuleCollider = GetComponent<CapsuleCollider>();

        suporteAnimacaoInimigo = GetComponentInChildren<SuporteAnimacaoInimigo>();

        ExibirOuOcultarBarraDeVida(false);

        audioSource.volume = AudioMng.Instance.volumeVFX;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanvasGameMng.Instance.fimDeJogo == true || estaMorto == true) return;

        VisaoInimigo();
        ControlarBarraDeVida();
        PerseguirJogador();
    }

    /// <summary>
    /// Exibe ou oculta a barra de vida do inimigo
    /// </summary>
    /// <param name="value">True Exibe e False Oculta</param>
    public void ExibirOuOcultarBarraDeVida(bool value){
        barraDeVida.SetActive(value);
    }

    /// <summary>
    /// Inimigo persegue o jogador
    /// </summary>
    private void PerseguirJogador(){
        //Calcular a distancia entre o jogador e o inimigo
        float distancia = Vector3.Distance(
            transform.position,
            PlayerMng.Instance.gameObject.transform.position
        );

        //Verificar se a distancia é a permitida para perseguir o jogador e se já pode perseguir
        if(distancia < distanciaPerseguicao || estaPerseguindo == true){
            estaPerseguindo = true;

            //Verificar se a distancia entre o inimigo e o jogador é maior que a distancia minima
            if(distancia > distanciaDoPlayer){
                //Fazer o inimigo ir até o jogador
                agent.destination = PlayerMng.Instance.transform.position;
                suporteAnimacaoInimigo.PlayRun();
            }
            else{
                //Ao chegar perto do player, o inimigo o ataca.
                agent.destination = transform.position;
                Vector3 olharParaJogador = new Vector3(
                    PlayerMng.Instance.transform.position.x,
                    transform.position.y,
                    PlayerMng.Instance.transform.position.z
                );
                transform.LookAt(olharParaJogador);
                suporteAnimacaoInimigo.PlayAttack();
            }
        }
        else{
            agent.destination = transform.position;
            suporteAnimacaoInimigo.PlayIdle();
        }
    }

    /// <summary>
    /// Faz a barra de vida do inimigo "Olhar" para o jogador
    /// </summary>
    private void ControlarBarraDeVida(){
        sldVida.transform.LookAt(
            new Vector3(
                PlayerMng.Instance.transform.position.x,
                sldVida.transform.position.y,
                PlayerMng.Instance.transform.position.z
            )
        );
    }

    /// <summary>
    /// Decrementa a vida do inimigo
    /// </summary>
    /// <param name="dano">Valor a ser tirado da vida</param>
    public void DecrementarVida(float dano){
        if(estaMorto == true) return;

        vida -= dano;
        estaPerseguindo = true;
        if(vida<=0){
            estaMorto = true;
            
            CanvasGameMng.Instance.IncrementarMortesZumbi();//Contabilizar a morte zumbi

            var instanciarInimigos = FindObjectOfType<InstanciarInimigos>();//Decrementar a quantidade de zumbis no jogo
            instanciarInimigos.DecrementarQtdInimigosNaFase();

            agent.destination = transform.position;
            suporteAnimacaoInimigo.PlayDeath();
            Destroy(capsuleCollider);
            Destroy(sldVida.gameObject);
            Destroy(gameObject,5f);
            return;
        }
        else{
            sldVida.value = vida;
        }
    }

    public void VisaoInimigo(){
        RaycastHit hit;
        Vector3 posicaoVisibilidade = new Vector3(
            transform.position.x,
            1,
            transform.position.z
        );

        //Emitir o Raio e obter as informações do objeto colidido com ele
        if(Physics.Raycast(
            posicaoVisibilidade,
            transform.TransformDirection(Vector3.forward),
            out hit,
            10,
            layerMask)
        ){
            Debug.DrawRay(
                posicaoVisibilidade,
                transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.yellow
            );
            estaVendoPlayer = true;
        }
        else{
            estaVendoPlayer = false;
        }
    }

    public void DanoAoPlayer(){
        if(estaVendoPlayer == true){
            CanvasGameMng.Instance.DecrementarVidaJogador(danoAoPlayer);
        }
    }

    public void ConfigurarAudio(AudioClip audioClip){
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
