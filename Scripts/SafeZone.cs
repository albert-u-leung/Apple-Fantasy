using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{

    public float dangerZoneDamageSpeed = 10f, safeZoneRecoverSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.StartsWith("Player"))
        {
            print("player exit sate zone");
            //player bleeding
            other.gameObject.GetComponent<PlayerHealthController>().isBleeding = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("Player"))
        {
            print("player enter and stay");
            //player recovering
            other.gameObject.GetComponent<PlayerHealthController>().isBleeding = false;
        }
    }
       
    
}
