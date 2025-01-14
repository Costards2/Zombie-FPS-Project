using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjetarCapsula : MonoBehaviour
{
    public Rigidbody rb;

    public void Ejetar()
    {
        gameObject.transform.SetParent(null);

        rb.AddForce(transform.TransformDirection(new Vector3(100, 0, 0)));

        rb.AddTorque(transform.right * 1.5f);

        Destroy(gameObject, 3f);
    }
}
