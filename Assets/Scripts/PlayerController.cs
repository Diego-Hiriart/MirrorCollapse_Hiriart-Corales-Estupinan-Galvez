using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerCharacter player;
    
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 playerPos = this.transform.position;
        Quaternion playerRot = this.transform.rotation;
        SaveTransform playerTransform = new SaveTransform(playerPos.x, playerPos.y, playerPos.z, 
            playerRot.x, playerRot.y, playerRot.z, playerRot.w);
        this.player = new PlayerCharacter(playerTransform, new PlayerCharacter().GetMaxHealth(), new ItemList());
    }

    private void Start()
    {
        Debug.Log(this.player.GetItems().Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))//If a E is pressed
        {
            if (Time.timeScale == 1) {
                ObjectInteract();
            }
        }
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
        }
    }
}
