using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    GameObject player;
    bool isAttacking;
    [SerializeField] float atkDamage;

    // Update is called once per frame
    void Update()
    {
        // if(isAttacking)
        // {
        //     StartCoroutine(DoAttack());
        // }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            isAttacking = true;
            player = other.gameObject;
            StartCoroutine(DoAttack());
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            isAttacking = false;
            player = null;
            StopAllCoroutines();
        }
    }

    IEnumerator DoAttack()
    {
        while(isAttacking)
        {
            player.GetComponent<PlayerController>().ChangeHealth(atkDamage, false);

            yield return new WaitForSeconds(2f);
        }
    }


}
