using UnityEngine;

public class DisparoPlayer : MonoBehaviour
{
    public ArmaControlador pistolaControlador;
    public ArmaControlador fuzilControlador;

    public GameObject impactoBalaInimigo;
    public GameObject impactoBala;

    public int idArmaAtiva = 1; // 1 Pistola - 2 Fuzil
    private ArmaControlador armaAtiva;

    public ArmaControlador ArmaAtiva
    {
        get { return armaAtiva; } 
    }

    void Start()
    {
        AtivarArma(idArmaAtiva);
    }

    void Update()
    {
        if (PlayerManager.instance.estaMorto == true) return;

        SelecionarArma();
        DispararArma();
        RecarregarArma();
    }

    private void AtivarArma(int idArma)
    {
        fuzilControlador.gameObject.SetActive(idArma == 2);
        pistolaControlador.gameObject.SetActive(idArma == 1);

        armaAtiva = idArma == 1 ? pistolaControlador : idArma == 2 ? fuzilControlador : null;
        armaAtiva.AtivarAudioSelecaoArma();
        
        idArmaAtiva = idArma;
    }

    private void SelecionarArma()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AtivarArma(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AtivarArma(2);
        }
    }

    private void DispararArma()
    {
        if (armaAtiva == null) return;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            armaAtiva.Disparar();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            armaAtiva.CancelarDisparo();
        }
    }

    private void RecarregarArma()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            armaAtiva.RecarregarArma();
        }
    }

    public void IncrementarMunicaoPistola(int municao)
    {
        pistolaControlador.IncrementarMunicao(municao);
    }

    public void IncrementarMunicaoFuzil(int municao)
    {
        fuzilControlador.IncrementarMunicao(municao);
    }

    public void DesabilitarArmas()
    {
        pistolaControlador.gameObject.SetActive(false);
        fuzilControlador.gameObject.SetActive(false);
    }

    public void DanoAoObjeto()
    {
        if (PlayerManager.visaoCamera.AlvoVisto != null)
        {
            Quaternion rotacaoDoImpacto = Quaternion.FromToRotation(Vector3.forward,
            PlayerManager.visaoCamera.hitAlvo.normal);

            if(PlayerManager.visaoCamera.tagAlvo == "Inimigo")
            {
                Instantiate(impactoBalaInimigo, PlayerManager.visaoCamera.hitAlvo.point, rotacaoDoImpacto);
            }
            else
            {
                Instantiate(impactoBala, PlayerManager.visaoCamera.hitAlvo.point, rotacaoDoImpacto);
            }
        }
    }
}
