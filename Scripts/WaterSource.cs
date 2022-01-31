using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.StartsWith("Player"))
        {
            other.gameObject.GetComponent<AttackController>().isPickUpBullets = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.StartsWith("Player"))
        {
            other.gameObject.GetComponent<AttackController>().isPickUpBullets = false;
        }
       
    }
}
