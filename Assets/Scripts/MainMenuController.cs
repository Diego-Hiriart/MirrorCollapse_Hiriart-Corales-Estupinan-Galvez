using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        this.newGame.onClick.AddListener(CreateNewGame);
        this.loadGame.onClick.AddListener(LoadLastGame);
        this.settingsButton.onClick.AddListener(OpenSettings);
        this.quitGame.onClick.AddListener(Quit);
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
        PrefsKeys.newGame = true;
        SceneManager.LoadScene("PartOne");      
    }

    private void LoadLastGame()
    {
        string filePath = Application.persistentDataPath + PrefsKeys.saveFileFormat + ".data";
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(filePath, FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(fs);
            SceneManager.LoadScene(save.GetLevel());          
        }
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
