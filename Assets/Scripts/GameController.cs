using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController UI;
    [SerializeField] private Canvas hud;
    [SerializeField] private PlayerController player;
    [SerializeField] private AudioMixer effectsMixer;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] InventoryController inventoryController;

    private void Awake()
    {
        LoadApplySettings();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.OpenClosePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            this.OpenCloseInventory();
        }
    }

    private void LoadApplySettings()
    {
        if (PlayerPrefs.HasKey(PrefsKeys.masterVolKey))
        {
            AudioListener.volume = PlayerPrefs.GetFloat(PrefsKeys.masterVolKey);
        }
        if (PlayerPrefs.HasKey(PrefsKeys.effectsVolKey))
        {
            effectsMixer.SetFloat("Volume", PlayerPrefs.GetFloat(PrefsKeys.effectsVolKey));
        }
        if (PlayerPrefs.HasKey(PrefsKeys.musicVolKey))
        {
            musicMixer.SetFloat("Volume", PlayerPrefs.GetFloat(PrefsKeys.musicVolKey));
        }
    }

    private void OpenClosePauseMenu()
    {
        if (!this.UI.IsPauseActive())
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            this.UI.SetPauseMenuState(true);
            this.hud.gameObject.SetActive(false);
        }
        else
        {
            if (!this.UI.IsInventoryActive())
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                this.UI.SetPauseMenuState(false);
                this.hud.gameObject.SetActive(true);
            }
            else
            {
                this.UI.SetPauseMenuState(false);
            }
        }
    }

    public void OpenCloseInventory()
    {
        if (!this.UI.IsPauseActive() && !this.UI.IsInventoryActive())
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            this.UI.SetPauseInventoryMenuState(true);
            this.hud.gameObject.SetActive(false);
        }
        else if(!this.UI.IsPauseActive())
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            this.UI.SetPauseInventoryMenuState(false);
            this.hud.gameObject.SetActive(true);
        }        
    }
}
