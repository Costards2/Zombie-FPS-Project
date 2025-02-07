using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGameMng : MonoBehaviour
{
    public static CanvasGameMng Instance;

    void Awake(){
        if(Instance == null){
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    [Header("Conf Status Player")]
    public TextMeshProUGUI txtVida;
    public TextMeshProUGUI txtMunicao;
    public int vidaJogador;
    private int vidaMaximaJogador = 100;
    public GameObject pnlStatusPlayer;

    [Header("Conf Topo")]
    public GameObject pnlTopo;
    public GameObject [] iconesChaves;
    public TextMeshProUGUI txtTempo;
    public TextMeshProUGUI txtObjetivo;
    public GameObject miniMapa;
    private float totalTempo;
    private int totalChavesColetadas; //mudar para private

    [Header("Conf Geral")]
    public bool fimDeJogo;

    [Header("Conf Game Over")]
    public GameObject pnlGameOver;

    [Header("Conf Fim de Jogo")]
    public GameObject pnlFimDeJogo;
    public TextMeshProUGUI txtTempoFinal;
    public TextMeshProUGUI txtTotalZumbisMortos;
    private int totalZumbisMortos;
    private int tempoFinal;
    public GameObject cameraPlayer;
    public GameObject cameraFimDeJogo;
    public HelicopterControlador helicopterControlador;

    // Start is called before the first frame update
    void Start()
    {
        vidaJogador = vidaMaximaJogador;
        txtVida.text = $"+{vidaJogador}";

        totalChavesColetadas = 0;
        totalTempo = 0;
        txtTempo.text = $"{totalTempo}";
        txtObjetivo.text = $"Colete as 7 chaves!";

        totalZumbisMortos = 0;

        fimDeJogo = false;

        AudioMng.Instance.PlayAudioGame();

        CanvasLoadingMng.Instance.OcultarTelaDeCarregamento();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            VoltarParaMenu();
        }

        if(Input.GetKeyDown(KeyCode.M)){
            miniMapa.SetActive(!miniMapa.activeSelf);
        }
        
        if(fimDeJogo == true) return;
        ContarTempo();
        AtualizarMunicaoUI();
    }

    private void AtualizarMunicaoUI(){
        int pente = PlayerMng.disparoPlayer.ArmaAtiva.Pente;
        int municao = PlayerMng.disparoPlayer.ArmaAtiva.MunicaoAtual;
        
        //Se o pente ou a munição for inferior a 10, eu vou colocar o 0 na frente do numero
        string valorPente = pente < 10 ? $"0{pente}" : $"{pente}";
        string valorMunicao = municao < 10 ? $"0{municao}" : $"{municao}";
        txtMunicao.text = $"{valorPente}/{valorMunicao}";
    }

    public void DecrementarVidaJogador(int dano){
        if(fimDeJogo == true) return;

        vidaJogador -= dano;
        if(vidaJogador <= 0){
            vidaJogador = 0;
            PlayerMng.Instance.MatarJogador();
            pnlStatusPlayer.SetActive(false);

            pnlGameOver.SetActive(true);//Ativar tela de GameOver

            Invoke("ReiniciarJogo",4.5f);//Reniciar Jogo depois de um tempo
        }
        txtVida.text = $"+{vidaJogador}";
    }

    public void IncrementarVidaJogador(){
        vidaJogador += 25;
        if(vidaJogador > vidaMaximaJogador){
            vidaJogador = vidaMaximaJogador;
        }
        txtVida.text = $"+{vidaJogador}";
    }

    public void ReiniciarJogo(){
        CanvasLoadingMng.Instance.ExibirTelaDeCarregamento();//Exibir tela de carregamento

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DesbloquearMouse(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void VoltarParaMenu(){
        DesbloquearMouse();
        
        CanvasLoadingMng.Instance.ExibirTelaDeCarregamento();//Exibir a tela de carregamento

        SceneManager.LoadScene(0);
    }

    private void ContarTempo(){
        totalTempo += Time.deltaTime;
        txtTempo.text = $"{(int)totalTempo}";
    }

    public void IncrementarChave(){
        totalChavesColetadas++;
        iconesChaves[totalChavesColetadas].SetActive(true);

        //verificar se coletou todas as chaves
        if(ColetouTodasAsChaves()){
            txtObjetivo.text = "Encontre o portão final!";
        }
    }

    public bool ColetouTodasAsChaves(){
        return totalChavesColetadas == iconesChaves.Length-1 ? true : false;
    }

    public void DefinirFimDeJogo(){
        fimDeJogo = true;
        PlayerMng.movimentarPlayer.MutarAudio();
        cameraPlayer.SetActive(false);
        cameraFimDeJogo.SetActive(true);
        pnlStatusPlayer.SetActive(false);
        pnlTopo.SetActive(false);
        StartCoroutine(IniciarVooHelicopter());
    }

    public void ExibirTelaFinal(){

        tempoFinal = (int) totalTempo;// Pegar o tempo final do jogo
        txtTempoFinal.text = $"{tempoFinal}";
        txtTotalZumbisMortos.text = $"{totalZumbisMortos}";
        
        DBMng.SalvarDados(totalZumbisMortos,tempoFinal);//Salvar os Dados

        pnlFimDeJogo.SetActive(true);
        

        DesbloquearMouse();

        PlayerMng.disparoPlayer.DesabilitarArmas();
    }

    public void IncrementarMortesZumbi(){
        totalZumbisMortos++;
    }

    IEnumerator IniciarVooHelicopter(){
        yield return new WaitForSeconds(3f);
        helicopterControlador.IniciarVoo();
    }
}
