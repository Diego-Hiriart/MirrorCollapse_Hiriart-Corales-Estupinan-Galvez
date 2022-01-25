using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Enemy enemy;
    GameObject player;
    protected NavMeshAgent enemyAgent;
    private LevelController level;
    [SerializeField] private string enemyID;

    bool isChasing;

    // Start is called before the first frame update
    void Start()
    {      
        enemyAgent = this.GetComponent<NavMeshAgent>();
        this.level = GetComponentInParent<LevelController>();
        this.AddThisToLevel();
    }

    private void AddThisToLevel()
    {
        this.level.AddEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyAgent != null && player != null && isChasing )
        {
            enemyAgent.SetDestination(player.transform.position);         
        }

        if (this.enemy.GetHealth()<=0)
        {
            this.level.AddDefeatedEnemy(this.enemy);
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

    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            isChasing = false;
            player = null;
        }
    }

    public string GetEnemyID()
    {
        return this.enemyID;
    }
}
