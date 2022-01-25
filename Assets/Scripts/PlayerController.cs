using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InventoryObject inventory;

    private PlayerCharacter player;
    private float health;

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
        health = player.GetHealth();
    }

    private void Start()
    {
        Debug.Log(this.player.GetItems().Count);
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

    public void ChangeHealth(float health, bool add)
    {
        if(add)
        {
            this.health = this.health + health < maxHealth ? this.health + health : maxHealth;
        }
        else
        {
            this.health = this.health - health > minHealth ? this.health - health : minHealth;
        }

        this.player.SetHealth(this.health);

        Debug.Log("Health: " + this.player.GetHealth());
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
                    item.PickItemUp();
                    inventory.AddItem(item.itemObject, 1);
                    Destroy(item.gameObject);
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
                impact.transform.GetComponentInParent<MirrorPortal>().ChangeLevel();
            }
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
