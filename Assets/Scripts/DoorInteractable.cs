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

    [SerializeField] AudioSource openAudio;
    [SerializeField] AudioSource lockedAudio;

    Animator animator;

    bool playSound = false;

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
                    openAudio.Play();
                    playSound = false;
                    break;
                }
                else if(item.item.itemName == "StrangeKey" && eastDoor)
                {
                    animator.SetTrigger("OpenCloseDoor");
                    openAudio.Play();
                    playSound = false;
                    break;
                }
                else
                {
                    playSound = true;
                }
            }

            if(inventory.Container.Count == 0)
            {
                playSound = true;
            }

            if(playSound)
            {
                lockedAudio.Play();
            }
        }
        else
        {
            animator.SetTrigger("OpenCloseDoor");
            openAudio.Play();
        }
    }
}
