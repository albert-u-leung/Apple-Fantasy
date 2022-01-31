using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 moveDir;
    public GameObject player;

    //public float enemyPushBackForce = 100000f;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        moveDir = gameObject.GetComponent<Rigidbody>().velocity.normalized;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.StartsWith("Enemy"))
        {
            //knock back
            var force = other.gameObject.GetComponent<EnemyController>().enemyPushBackForce;
            var enemy = other.gameObject.GetComponent<Rigidbody>(); 
            enemy.AddForce (moveDir * force, ForceMode.Impulse);
            enemy.AddForce (other.gameObject.GetComponent<Transform>().up * force * 5f, ForceMode.Impulse);
            player.GetComponent<AttackController>().killNumber += 1;
            Destroy(other.gameObject,0.2f);
            Destroy(gameObject, 0.3f);
            
            //kill
        }
        if (other.gameObject.name.Equals("Ground"))
        {
            Destroy(gameObject,0.1f);
        }
    }
}

