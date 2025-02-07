using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjetarCapsula : MonoBehaviour
{
    public Rigidbody rigidBody;
    
    public void Ejetar(){
        //Fazer a capsula ser filha da cena raiz 
        gameObject.transform.SetParent(null);
        //Aplicar a força para ejetar
        rigidBody.AddForce(transform.TransformDirection(new Vector3(100,0,0)));
        //Aplicar uma força de rotação
        rigidBody.AddTorque(transform.right * 1.5f);

        Destroy(gameObject, 3f);
    }
}
