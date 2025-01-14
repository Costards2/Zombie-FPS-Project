using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public static VisaoCamera visaoCamera;
    public static MovimentarPlayer movimentarPlayer;
    public static DisparoPlayer disparoPlayer;

    public bool estaMorto;

    void Start()
    {
        if(instance == null)
        {
            visaoCamera =GetComponentInChildren<VisaoCamera>();
            movimentarPlayer = GetComponent<MovimentarPlayer>();
            disparoPlayer = GetComponent<DisparoPlayer>();

            instance = this;
            return;
        }

        Destroy(gameObject);
    }

    void Update()
    {
        //Lanterna
        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }
    }

    public void EstaMorto()
    {
        estaMorto = true;
        Destroy(GetComponent<CapsuleCollider>());
    }
}
