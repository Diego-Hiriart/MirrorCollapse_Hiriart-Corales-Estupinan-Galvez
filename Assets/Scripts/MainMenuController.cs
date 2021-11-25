using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private Button newGame;
    [SerializeField]
    private Button loadGame;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button quitGame;
    [SerializeField]
    private AudioMixer effectsMixer;
    [SerializeField]
    private AudioMixer musicMixer;


    private void Awake()
    {
        LoadApplySettings();
        this.SetSettingsMenuState(false);
        this.newGame.onClick.AddListener(delegate { CreateNewGame(); } );
        this.loadGame.onClick.AddListener(delegate { LoadLastGame(); });
        this.settingsButton.onClick.AddListener(delegate { OpenSettings(); });
        this.quitGame.onClick.AddListener(delegate { Quit(); });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void SetSettingsMenuState(bool status)
    {
        this.settingsMenu.SetActive(status);
    }

    private void CreateNewGame()
    {
        SceneManager.LoadScene("PartOne");
    }

    private void LoadLastGame()
    {

    }

    private void OpenSettings()
    {
        this.settingsMenu.GetComponent<SettingsController>().LoadApplySettings();
        this.settingsMenu.SetActive(true);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
