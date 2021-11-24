using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private UIController UI;
    [SerializeField]
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            this.OpenPauseMenu();
        }

        if (Input.GetKey(KeyCode.I))
        {
            this.OpenInventory();
        }
    }

    private void OpenPauseMenu()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            this.UI.SetPauseMenuState(true);
        }
        else
        {
            Time.timeScale = 1;
            this.UI.SetPauseMenuState(false);
        }
        
    }

    private void OpenInventory()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            this.UI.SetPauseInventoryMenuState(true);
        }
        else
        {
            Time.timeScale = 1;
            this.UI.SetPauseInventoryMenuState(false);
        }
        
    }
}
