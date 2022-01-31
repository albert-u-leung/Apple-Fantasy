using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealthController : MonoBehaviour
{

    public float playerHealth, playerHealthMax = 100f, previousHealth;
    public float bleedingSpeed = 30f, recoveringSpeed = 30f ;
    
    public bool isBleeding;

    public GameObject globalVolume;
    public Vignette vg;
    public Volume v;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerHealthMax;
        previousHealth = playerHealth;
        
        v = globalVolume.GetComponent<Volume>();
        v.profile.TryGet(out vg);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth < 0)
        {
            print("Player die!");
            Destroy(gameObject);
        }

        if (isBleeding)
        {
            Bleed();
        }

        if (!isBleeding)
        {
            Recover();
        }

        HurtingVFX();
    }

    private void Bleed()
    {
        playerHealth -= Time.deltaTime * bleedingSpeed;
    }

    private void Recover()
    {
        if (playerHealth < playerHealthMax)
        {
            playerHealth += Time.deltaTime * recoveringSpeed;
        }
    }

    public void EnemyHurt()
    {
        playerHealth -= Time.deltaTime * bleedingSpeed;
        print("enemy hurting");
    }

    private void HurtingVFX()
    {
        if (playerHealth < previousHealth)
        {
            vg.color.value = new Color(255f, 0f, 0f);
            print("red");
        }
        else
        {
            vg.color.value = new Color(0f, 0f, 0f);
            print("black");
        }
        previousHealth = playerHealth;
    }
    
    /*private void OnCollisionEnter(Collision other)
{
    if (other.gameObject.name.StartsWith("Enemy"))
    {
        print("collide enemy");
        EnemyHurt();
        vg.color.value = new Color(255f, 0f, 0f);
        print("color changed");
        Invoke("ResetVgColor", 2f);
        print("called inoke");
        
    }
}*/
    /*private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name.StartsWith("Enemy"))
        {
            print("leave");
            vg.color.value = new Color(0f, 0f, 0f);
        }
    }*/
}
