using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSystem : MonoBehaviour
{
    [SerializeField] WeaponObject weaponObject;
    [SerializeField] AudioSource swingAudio;
    [SerializeField] Collider ownCollider;

    //Gun stats
    private float damage;
    public float timeBetweenHits, timeBetweenShots;
    public bool allowButtonHold;


    //bools 
    bool hitting, readyToHit;

    private void Awake()
    {
        readyToHit = true;

        damage = weaponObject.atkDamage;
    }
    private void Update()
    {
        if(Time.timeScale == 1)
        {
            MyInput();
        }
    }

    private void MyInput()
    {
        if (allowButtonHold) 
            hitting = Input.GetKey(KeyCode.Mouse0);
        else 
            hitting = Input.GetKeyDown(KeyCode.Mouse0);

        //Shoot
        if (readyToHit && hitting)
        {
            Hit();
        }
    }
    private void Hit()
    {
        readyToHit = false;

        gameObject.GetComponent<Animator>().SetTrigger("DoSwing");

        swingAudio.Play();

        ownCollider.enabled = true;

        StartCoroutine(Cooldown());
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(timeBetweenHits);

        readyToHit = true;
        ownCollider.enabled = false;
        gameObject.GetComponent<Animator>().SetTrigger("Idle");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT1");
        if(other.tag == "Hitbox")
        {
            Debug.Log("hit");
            other.gameObject.GetComponentInParent<EnemyController>().ChangeHealth(damage, false);
        }
    }

    private void ResetHit()
    {
        readyToHit = true;
    }
}
