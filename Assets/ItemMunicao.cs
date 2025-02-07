using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemMunicao : MonoBehaviour
{
    public GameObject pentePistola;
    public GameObject penteFuzil;
    public TextMeshProUGUI txtQtdMunicao;
    public GameObject iconeFuzil;
    public GameObject iconePistola;
    private int municaoParaPistola;
    private int municaoParaFuzil;
    private int idArma;

    public AudioClip audioMunicao;
    // Start is called before the first frame update
    void Start()
    {
        iconeFuzil.SetActive(false);
        iconePistola.SetActive(false);
        idArma = new System.Random().Next(1,3);
        switch(idArma){
            case 1:
                municaoParaPistola = new System.Random().Next(5,21);
                txtQtdMunicao.text = $"x{municaoParaPistola}";
                penteFuzil.SetActive(false);
                iconePistola.SetActive(true);
            break;
            case 2:
                municaoParaFuzil = new System.Random().Next(15,51);
                txtQtdMunicao.text = $"x{municaoParaFuzil}";
                pentePistola.SetActive(false);
                iconeFuzil.SetActive(true);
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Fazer o texto "Olhar" para o jogador
        txtQtdMunicao.transform.LookAt(
            new Vector3(
                PlayerMng.Instance.transform.position.x,
                txtQtdMunicao.transform.position.y,
                PlayerMng.Instance.transform.position.z
            )
        );
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag.Equals("Player")){
            switch(idArma){
                case 1:
                PlayerMng.disparoPlayer.IncrementarMunicaoPistola(municaoParaPistola);
                break;
                case 2:
                PlayerMng.disparoPlayer.IncrementarMunicaoFuzil(municaoParaFuzil);
                break;
            }
            AudioMng.Instance.PlayAudioVFX(audioMunicao);
            Destroy(gameObject);
        }        
    }
}
