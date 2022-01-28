using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public InventoryObject inventory;
    [SerializeField] public GameObject bat;
    [SerializeField] public GameObject pistol;

    public GameObject ammo;

    private PlayerCharacter player;

    Image redScreenImage;

    float maxHealth;
    float minHealth;
    
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 playerPos = this.transform.position;
        Quaternion playerRot = this.transform.rotation;
        SaveTransform playerTransform = new SaveTransform(playerPos.x, playerPos.y, playerPos.z, 
            playerRot.x, playerRot.y, playerRot.z, playerRot.w);
        this.player = new PlayerCharacter(playerTransform, new PlayerCharacter().GetMaxHealth(), new ItemList());

        maxHealth = player.GetMaxHealth();
        minHealth = player.GetMinHealth();
    }

    private void Start()
    {
        redScreenImage = GameObject.FindWithTag("RedScreen").GetComponent<Image>();
        ChangeRedScreenAlpha(player.GetHealth());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))//If E is pressed
        {
            if (Time.timeScale == 1) 
            {
                ObjectInteract();
            }
        }
    }

    public void ChangeRedScreenAlpha(float health)
    {
        Debug.Log(health);

        float value = 100 - health;
        value /= 100;
        value /= 4;
        Debug.Log(value);
        redScreenImage.color = new Color(255, 0, 0, value);
    }
    
    public void EquipWeapon(bool isGun)
    {
        if(isGun)
        {
            bat.SetActive(false);
            pistol.SetActive(true);
        }
        else
        {
            bat.SetActive(true);
            pistol.SetActive(false);
        }
    }

    public void ChangeHealth(float health, bool add)
    {
        if(add)
        {
            this.player.SetHealth(this.player.GetHealth() + health < maxHealth ? this.player.GetHealth() + health : maxHealth);
        }
        else
        {
            this.player.SetHealth(this.player.GetHealth() - health > minHealth ? this.player.GetHealth() - health : minHealth);
        }

        ChangeRedScreenAlpha(player.GetHealth());
    }

    public PlayerCharacter GetPlayerInfo()
    {
        Vector3 pos = this.transform.position;
        Quaternion rot = this.transform.rotation;
        this.player.SetTransform(new SaveTransform(pos.x, pos.y, pos.z, rot.x, rot.y, rot.z, rot.w));
        return this.player;
    }

    private void ObjectInteract()
    {
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impact;//To store what the ray hit       
        if (Physics.Raycast(raycast, out impact, 3f))
        {          
            ItemController item;
            SavingController saveMirror;
            if (impact.collider.TryGetComponent<ItemController>(out item))
            {
                if (item.GetItem().IsPickable())
                {
                    if (!item.GetItem().IsWeaponAmmo())
                    {
                        item.PickItemUp();
                        inventory.AddItem(item.itemObject, 1);
                        Destroy(item.gameObject);
                    }
                    else
                    {
                        bool hasAmmo = false;
                        foreach (var itemContainer in inventory.Container)
                        {
                            if (itemContainer.item is AmmoObject)//If theres already ammo in the inventory, just add to it
                            {
                                item.PickItemUp();
                                this.player.GetAmmoItem().SetAmmoAmount(item.GetItem().GetAmmoAmount()+ (int)item.ammoQuantity);
                                (itemContainer.item as AmmoObject).quantity += item.ammoQuantity;
                                Destroy(item.gameObject);
                                hasAmmo = true;
                                break;
                            }
                        }
                        if (!hasAmmo)//If theres no ammo already, add new AmmoObject
                        {
                            item.PickItemUp();
                            this.player.GetAmmoItem().SetAmmoAmount(item.GetItem().GetAmmoAmount() + (int)item.ammoQuantity);
                            inventory.AddItem(item.itemObject, 1);
                            Destroy(item.gameObject);
                        }
                    }                 
                }
            }
            else if (impact.collider.TryGetComponent<SavingController>(out saveMirror))
            {
                saveMirror.SaveGame();
            }
            else if (impact.collider.tag == "Door")
            {
                impact.transform.gameObject.GetComponentInParent<DoorInteractable>().OpenCloseDoor();
            }
            else if (impact.collider.tag == "Switch")
            {
                impact.transform.GetComponent<LightSwitch>().TurnOnOffLights();
            }
            else if(impact.collider.tag == "Portal")
            {
                PrefsKeys.inventory = this.player.GetInventory();
                impact.transform.GetComponentInParent<MirrorPortal>().ChangeLevel();
            }
        }
    }
}
