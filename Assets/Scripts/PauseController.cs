using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private Button toMainMenu;
    [SerializeField]
    private LevelController currentLevel;


    private void Awake()
    {
        this.SetSettingsMenuState(false);
        this.settingsButton.onClick.AddListener(OpenSettings);
        this.toMainMenu.onClick.AddListener(QuitToMainMenu);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetSettingsMenuState(bool status)
    {
        this.settingsMenu.SetActive(status);
    }

    private void OpenSettings()
    {
        this.settingsMenu.GetComponent<SettingsController>().LoadApplySettings();
        this.settingsMenu.SetActive(true);
    }

    private void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");        
    }
}
