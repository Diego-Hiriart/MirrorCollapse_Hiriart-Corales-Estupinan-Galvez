using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour
{
    [SerializeField] GameObject doorPivot;
    Transform closedPos;
    Transform openPos;

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
        animator.SetTrigger("OpenCloseDoor");
    }
}
