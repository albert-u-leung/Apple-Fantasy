using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickWall : MonoBehaviour
{
    public float kickForce = 500f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {

        if (other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * kickForce);
            print("added force");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * kickForce);
            print("added force");
        }
    }
}
