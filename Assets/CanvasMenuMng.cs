using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenuMng : MonoBehaviour
{
    public GameObject canvasMundo;
    public GameObject pnlConfiguracoes;
    public GameObject pnlCreditos;
    public Slider sldVFX;
    public Slider sldMusica;
    private Volume volumes;
    // Start is called before the first frame update
    void Start()
    {
        ExibirMenu();
        ConfigurarPainelConfiguracoes();
        AudioMng.Instance.PlayAudioMenu();
        CanvasLoadingMng.Instance.OcultarTelaDeCarregamento();
    }

    public void Jogar(){
        CanvasLoadingMng.Instance.ExibirTelaDeCarregamento();
        SceneManager.LoadScene(1);
    }

    public void Fechar(){
        Application.Quit();
    }

    private void ConfigurarPainelConfiguracoes(){
        volumes = DBMng.ObterVolumes();
        sldVFX.value = volumes.vfx;
        sldMusica.value = volumes.musica;
        AudioMng.Instance.MudarVolume(volumes);
    }

    private void AtualizarVolumes(){
        volumes = DBMng.ObterVolumes();
        AudioMng.Instance.MudarVolume(volumes);
    }

    public void MudarVolumeVFX(){
        DBMng.SalvarVolume(sldVFX.value, volumes.musica);
        AtualizarVolumes();
    }
    public void MudarVolumeMusica(){
        DBMng.SalvarVolume(volumes.vfx, sldMusica.value);
        AtualizarVolumes();
    }

    public void ExibirMenu(){
        canvasMundo.SetActive(true);
        pnlConfiguracoes.SetActive(false);
        pnlCreditos.SetActive(false);
    }
    public void ExibirConfiguracoes(){
        canvasMundo.SetActive(false);
        pnlConfiguracoes.SetActive(true);
        pnlCreditos.SetActive(false);
    }
    public void ExibirCreditos(){
        canvasMundo.SetActive(false);
        pnlConfiguracoes.SetActive(false);
        pnlCreditos.SetActive(true);
    }
}
