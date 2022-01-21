using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    protected NavMeshAgent enemyAgent;

    bool isChasing;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyAgent != null && player != null && isChasing )
        {
            enemyAgent.SetDestination(player.transform.position);         
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            isChasing = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            isChasing = false;
            player = null;
        }
    }
}
