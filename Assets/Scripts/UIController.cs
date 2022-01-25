using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        this.SetPauseMenuState(false);
        this.SetPauseInventoryMenuState(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPauseMenuState(bool status)
    {
        this.pause.SetActive(status);
    }

    public void SetPauseInventoryMenuState(bool status)
    {
        this.inventory.SetActive(status);
    }

    public bool IsPauseActive()
    {
        return this.pause.activeSelf;
    }

    public bool IsInventoryActive()
    {
        return this.inventory.activeSelf;
    }
}
