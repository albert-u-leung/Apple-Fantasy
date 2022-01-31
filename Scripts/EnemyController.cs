using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player, tree;
    public GameObject chasingTarget;
    public float enemyPushBackForce = 10f;

    public float playerDistance, treeDistance, locatePlayerDistance = 10f, attackRange = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        tree = GameObject.Find("TreeTargetPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            playerDistance = Vector3.Distance(transform.position, player.transform.position);
        }

        if (tree != null)
        {
            treeDistance = Vector3.Distance(transform.position, tree.transform.position);
        }

        //find target
        if (playerDistance < locatePlayerDistance)
        {
            chasingTarget = player;
        }
        else
        {
            chasingTarget = tree;
        }
        agent.SetDestination(chasingTarget.transform.position);
        
        //attack player in range
        if (chasingTarget == player)
        {
            if (playerDistance < attackRange) 
            {
                chasingTarget.GetComponent<PlayerHealthController>().EnemyHurt(); 
                print("player hurt");
            }
        }
        
        //attack tree in range
        if (chasingTarget == tree)
        {
            if (treeDistance < attackRange) 
            {
                chasingTarget.GetComponent<TreeHealthController>().EnemyHurt(); 
                print("tree hurt");
            }
        }
    }
    
    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.StartsWith("Player"))
        {
            print("collide enemy");
            other.gameObject.GetComponent<PlayerHealthController>().playerHealth -= 30f;
            other.gameObject.GetComponent<PlayerHealthController>().vg.color.value = new Color(255f, 0f, 0f);
            print("color changed");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name.StartsWith("Player"))
        {print("leave enemy");
            other.gameObject.GetComponent<PlayerHealthController>().vg.color.value = new Color(0f, 0f, 0f);
        }
    }*/
}
