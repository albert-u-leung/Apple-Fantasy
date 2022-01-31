using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;
    
    private float counter;
    public float counterMax = 30f;

    public Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void ResetIt()
    {
        counter = counterMax;
    }
    
    void Update()
    {
        counter -= Time.deltaTime; // always count down
        if(counter< 0) // counter expired
        {
            Instantiate(enemy,respawnPoint.position,Quaternion.identity);
            ResetIt();
        }
    }
}
