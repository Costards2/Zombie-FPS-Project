using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InstanciarInimigos : MonoBehaviour
{
    public GameObject[] inimigos;
    public float tempoDeEspera;
    public int maximoDeInimigosNaFase;
    private float tempoProximoInimigo;
    private int totalInimigosNaFase;

    public AudioClip[] audiosZumbi;
    // Start is called before the first frame update
    void Start()
    {
        totalInimigosNaFase = 0;
        tempoProximoInimigo = tempoDeEspera + Time.time;

        //Instanciar os primeiros inimigos
        for(int i = 0; i < maximoDeInimigosNaFase; i++){
            InstanciarInimigo(50,50);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Verificar se é possivel instanciar novos inimigos
        if((totalInimigosNaFase < maximoDeInimigosNaFase) && Time.time > tempoProximoInimigo){
            InstanciarInimigo(20,20);
            tempoProximoInimigo = Time.time + tempoDeEspera;
        }
    }

    private void InstanciarInimigo(float distanciaMaxX, float distanciaMaxZ){
        float posicaoZ = Random.Range(
            PlayerMng.Instance.transform.position.z - distanciaMaxZ,
            PlayerMng.Instance.transform.position.z + distanciaMaxZ
        );
        
        float posicaoX = Random.Range(
            PlayerMng.Instance.transform.position.x - distanciaMaxX,
            PlayerMng.Instance.transform.position.x + distanciaMaxX
        );

        //Localizar o inimigo na area azul do NavMesh
        NavMeshHit hit;
        NavMesh.SamplePosition(
            new Vector3(posicaoX, 0, posicaoZ),
            out hit,
            Mathf.Infinity,
            1
        );

        //Instanciar o inimigo na posição
        int inimigoSorteado = new System.Random().Next(0,inimigos.Length);
        var novoInimigo = Instantiate(inimigos[inimigoSorteado]);

        //Configurar audio do inimigo sorteado
        var audioSorteado = new System.Random().Next(0, audiosZumbi.Length);
        novoInimigo.GetComponent<InimigoControlador>().ConfigurarAudio(audiosZumbi[audioSorteado]);

        //Posicionar o inimigo na posição sorteada
        var agent = novoInimigo.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        novoInimigo.transform.position = hit.position;
        agent.enabled = true;
        var rotacaoSorteada = Quaternion.Euler(0, new System.Random().Next(0,361),0);
        novoInimigo.transform.rotation = rotacaoSorteada;

        totalInimigosNaFase++;
    }

    public void DecrementarQtdInimigosNaFase(){
        totalInimigosNaFase--;
    }
}
