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
            Debug.Log($"Alvo: {hit.transform.gameObject.name}");
        }
    }
}
