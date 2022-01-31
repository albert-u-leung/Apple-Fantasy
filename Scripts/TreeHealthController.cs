using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealthController : MonoBehaviour
{
    
    public float treeHealth, treeHealthMax = 100f, previousHealth;
    public float bleedingSpeed = 10f;

    public GameObject treeObj;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (treeHealth < 0)
        {
            print("Tree die!");
            Destroy(treeObj);
        }
        HurtingVFX();


    }

    public void EnemyHurt()
    {
        treeHealth -= Time.deltaTime * bleedingSpeed;
        print("tree is being hurting");
    }

    private void HurtingVFX()
    {
        if (treeHealth < previousHealth)
        {
            //print("red");
        }
        else
        {
            //print("black");
        }
        previousHealth = treeHealth;
    }
}
