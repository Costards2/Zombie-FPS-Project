using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisaoCamera : MonoBehaviour
{
    private GameObject alvo;
    public string tagAlvo; 
    public RaycastHit hitAlvo;

    public GameObject AlvoVisto{
        get {return alvo;}
        private set {
            alvo = value;
            tagAlvo = alvo.tag;
        }
    }
    private GameObject ultimoInimigoVisto;
    // Start is called before the first frame update
    void Start()
    {
        alvo = null;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastCamera();
    }

    private void RaycastCamera(){
        //Criar o raio apartir da camera do jogador
        Ray raio = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        //Criar a variavel que armazenar a informação do objeto visto
        RaycastHit hit;
        //Verificar se o raio encontrou algo
        if(Physics.Raycast(raio,out hit, Mathf.Infinity)){
            //Debug.Log($"Alvo: {hit.transform.gameObject.name}");

            Debug.DrawRay(transform.position, 
            transform.TransformDirection(Vector3.forward) * hit.distance, 
            Color.red
            );
            AlvoVisto = hit.transform.gameObject;
            hitAlvo = hit;

            //Lógica para exibir a vidad do inimigo que está sendo visto
            if(tagAlvo == "Inimigo"){
                //verificar se o ultimo inimigo visto é o atual inimigo
                if(ultimoInimigoVisto != hit.transform.gameObject && ultimoInimigoVisto != null){
                    ultimoInimigoVisto.GetComponent<InimigoControlador>().ExibirOuOcultarBarraDeVida(false);
                }
                AlvoVisto.GetComponent<InimigoControlador>().ExibirOuOcultarBarraDeVida(true);
                ultimoInimigoVisto = hit.transform.gameObject;
            }
            else{
                //Verificar se existe um inimigo visto na variavel
                if(ultimoInimigoVisto != null){
                    ultimoInimigoVisto.GetComponent<InimigoControlador>().ExibirOuOcultarBarraDeVida(false);
                    ultimoInimigoVisto = null;
                }
            }
        }
        else{
            tagAlvo = "";
            alvo = null;
        }
    }
}
