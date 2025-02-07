using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimentarPlayer : MonoBehaviour
{
    private Camera playerCamera;
    public float velocidadeCamera;
    public float limiteCameraX;
    private float rotacaoX;

    public float velocidadeCaminhada;
    public float velocidadeCorrida;
    public float forcaPulo;
    public float forcaGravidade;
    private Vector3 direcaoMovimentacao;
    CharacterController characterController;

    public AudioSource audioSource;
    public AudioClip[] audiosMovimentacao; //0 - Andar, 1 - Correr

    // Start is called before the first frame update
    void Start()
    {
        //Obtem a referencia da camera principal da cena
        playerCamera = Camera.main;

        characterController = GetComponent<CharacterController>();
        direcaoMovimentacao = Vector3.zero;

        //Travar e ocultar o mouse no inicio do jogo
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Configura o volume de movimentação do player
        audioSource.volume = AudioMng.Instance.volumeVFX;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanvasGameMng.Instance.fimDeJogo == true) return;
        if(PlayerMng.Instance.estaMorto == true) return;
        
        //Movimentar o player

        //Obter Referencia frente e lateral do objeto
        Vector3 frente = transform.TransformDirection(Vector3.forward);
        Vector3 direita = transform.TransformDirection(Vector3.right);

        //Input que faz o jogador correr
        bool estaCorrendo = Input.GetKey(KeyCode.LeftShift);

        //Calcular a velocidade de movimento
        float velocidadeFrente = estaCorrendo == true ? velocidadeCorrida : velocidadeCaminhada;
        velocidadeFrente *= Input.GetAxis("Vertical");
        float velocidadeLateral = estaCorrendo == true ? velocidadeCorrida : velocidadeCaminhada;
        velocidadeLateral *= Input.GetAxis("Horizontal");

        //Calcular a Direção inicial do eixo Y
        float direcaoEmY = direcaoMovimentacao.y;

        //Calcular a direcao do player
        direcaoMovimentacao = (frente * velocidadeFrente) + (direita * velocidadeLateral);

        //Verificar se está se movimentando para poder tocar o audio da movimentação
        if(direcaoMovimentacao != Vector3.zero){
            if(estaCorrendo == true){
                if(audioSource.clip != audiosMovimentacao[1]){
                    audioSource.Stop();
                    audioSource.clip = audiosMovimentacao[1];
                    audioSource.Play();
                }
            }
            else{
                if(audioSource.clip != audiosMovimentacao[0]){
                    audioSource.Stop();
                    audioSource.clip = audiosMovimentacao[0];
                    audioSource.Play();
                }
            }
            if(audioSource.isPlaying == false){
                audioSource.Play();
            }
        }
        else{
            audioSource.Stop();
        }

        //Vericar se o jogador está no chão para efetuar o pulo
        if(Input.GetButton("Jump") && characterController.isGrounded == true){
            direcaoMovimentacao.y = forcaPulo;
        }
        else {
            direcaoMovimentacao.y = direcaoEmY;
        }

        //Verificar se o jogador não está no chão
        if(characterController.isGrounded == false){
            direcaoMovimentacao.y -= forcaGravidade * Time.deltaTime;
        }

        //Movimentar o personagem
        characterController.Move(direcaoMovimentacao * Time.deltaTime);

        //Movimentar a camera do player
        rotacaoX += -Input.GetAxis("Mouse Y") * velocidadeCamera;
        rotacaoX = Mathf.Clamp(rotacaoX,-limiteCameraX,limiteCameraX);
        playerCamera.transform.localRotation = Quaternion.Euler(rotacaoX,0,0);
        transform.rotation *= Quaternion.Euler(0,Input.GetAxis("Mouse X") * velocidadeCamera, 0);
    }

    public void MutarAudio(){
        audioSource.volume = 0;
    }
}
