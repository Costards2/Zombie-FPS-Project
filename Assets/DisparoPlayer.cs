using UnityEngine;

public class DisparoPlayer : MonoBehaviour
{
    public ArmaControlador pistolaControlador;
    public ArmaControlador fuzilControlador;
    public GameObject impactoBalaInimigo;
    public GameObject impactorBala;
    
    private int _idArmaAtiva = 1; // 1 - Pistola, 2 - Fuzil

    public int IDArmaAtiva
    {
        get { return _idArmaAtiva; }
        
        set
        {
            _idArmaAtiva = Mathf.Clamp(value, 1, 2); // Garante que fique entre 0 e 5
        }
    }

    private ArmaControlador armaAtiva;

    public ArmaControlador ArmaAtiva{
        get {return armaAtiva;}
    }

    // Start is called before the first frame update
    void Start()
    {
        AtivarArma(_idArmaAtiva);
    }

    // Update is called once per frame
    void Update()
    {
        if(CanvasGameMng.Instance.fimDeJogo == true) return;//Verificar se o jogo acabou
        if(PlayerMng.Instance.estaMorto == true) return;

        SelecionarArma();
        DispararArma();
        RecarregarArma();

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            IDArmaAtiva++;
            AtivarArma(IDArmaAtiva);
        }
        else if (scroll < 0f)
        {
            IDArmaAtiva--;
            AtivarArma(IDArmaAtiva);
        }
    }

    private void AtivarArma(int idArma){
        pistolaControlador.gameObject.SetActive(idArma == 1);
        fuzilControlador.gameObject.SetActive(idArma == 2);
        armaAtiva = idArma == 1 ? pistolaControlador : idArma == 2 ? fuzilControlador : null;
        _idArmaAtiva = idArma;

        if ((armaAtiva == pistolaControlador && idArma == 1) || (armaAtiva == fuzilControlador && idArma == 2)) return;
       
        armaAtiva.AtivarAudioSelecaoArma();
    }

    private void SelecionarArma(){
        //Selecionar qual arma usar
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            AtivarArma(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            AtivarArma(2);
        }
    }

    private void DispararArma(){
        //Verificar se a arma ativa é inválida
        if(armaAtiva == null) return;

        //Identificar se o botão de atirar foi clicado
        if(Input.GetKey(KeyCode.Mouse0)){
            armaAtiva.Disparar();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)){
            armaAtiva.CancelarDisparo();
        }
    }

    private void RecarregarArma(){
        if(Input.GetKeyDown(KeyCode.R)){
            armaAtiva.RecarregarArma();
        }
    }

    public void IncrementarMunicaoPistola(int municao){
        pistolaControlador.IncrementarMunicao(municao);
    }

    public void IncrementarMunicaoFuzil(int municao){
        fuzilControlador.IncrementarMunicao(municao);
    }

    public void DesabilitarArmas(){
        pistolaControlador.gameObject.SetActive(false);
        fuzilControlador.gameObject.SetActive(false);
    }

    public void DanoAoObjeto(){
        //Verificar se a camera está vendo algum objeto
        if(PlayerMng.visaoCamera.AlvoVisto != null){
            //Calcular a rotação inversa da colisão
            Quaternion rotacaoDoImpacto = Quaternion.FromToRotation(Vector3.forward,
            PlayerMng.visaoCamera.hitAlvo.normal);
            //Verificar se está vendo o inimigo para emitir a particula
            if(PlayerMng.visaoCamera.tagAlvo == "Inimigo"){
                Instantiate(impactoBalaInimigo, PlayerMng.visaoCamera.hitAlvo.point,rotacaoDoImpacto);

                //Pegar o código do inimigo e tirar o dano dele
                var inimigo = PlayerMng.visaoCamera.AlvoVisto.GetComponent<InimigoControlador>();
                inimigo.DecrementarVida(armaAtiva.danoInimigo);
            }
            else{
                Instantiate(impactorBala, PlayerMng.visaoCamera.hitAlvo.point,rotacaoDoImpacto);
            }
        }
    }
}
