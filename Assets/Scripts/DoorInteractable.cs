using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour
{
    [SerializeField] GameObject doorPivot;
    Transform closedPos;
    Transform openPos;

    [SerializeField] bool needsKey;
    [SerializeField] bool westDoor;
    [SerializeField] bool eastDoor;

    [SerializeField] InventoryObject inventory;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        closedPos = doorPivot.transform;
        openPos = closedPos;
        openPos.rotation = Quaternion.Euler(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCloseDoor()
    {
        if(needsKey)
        {
            foreach (var item in inventory.Container)
            {
                if(item.item.itemName == "CursedKey" && westDoor)
                {
                    animator.SetTrigger("OpenCloseDoor");

                }
                
                if(item.item.name == "Strange Key" && eastDoor)
                {
                    animator.SetTrigger("OpenCloseDoor");
                }
            }
        }
        else
        {
            animator.SetTrigger("OpenCloseDoor");
        }
    }
}
