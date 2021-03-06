using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    [SerializeField] WeaponObject weaponObject;
    [SerializeField] InventoryObject inventory;
    [SerializeField] AudioSource shotAudio;

    Animator animator;

    //Gun stats
    private float damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    private float bulletsPerTap;
    public bool allowButtonHold;
    float bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    private void Awake()
    {
        bulletsLeft = 0;
        readyToShoot = true;

        damage = weaponObject.atkDamage;
        CheckBullets();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Time.timeScale == 1)
        {
            MyInput();
            CheckBullets();
        }
    }
    

    private void CheckBullets()
    {
        foreach(var item in inventory.Container)
        {
            if(item.item as AmmoObject)
            {
                var ammoObject = item.item as AmmoObject;
                bulletsLeft = ammoObject.quantity;
            }
        }
    }

    private void MyInput()
    {
        if (allowButtonHold) 
            shooting = Input.GetKey(KeyCode.Mouse0);
        else 
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Shoot
        if (readyToShoot && shooting && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        shotAudio.Play();
        animator.SetTrigger("Shoot");

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            if (rayHit.collider.CompareTag("Hitbox"))
                rayHit.collider.GetComponentInParent<EnemyController>().ChangeHealth(damage, false);
        }

        foreach (var item in inventory.Container)
        {
            if(item.item is AmmoObject)
            {
                var ammo = item.item as AmmoObject;
                ammo.quantity--;
                this.GetComponentInParent<PlayerController>().GetPlayerInfo().GetAmmoItem().SetAmmoAmount((int)ammo.quantity);
                break;
            }
        }

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShots);

        CheckBullets();
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
}