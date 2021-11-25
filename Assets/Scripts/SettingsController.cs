using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private Slider masterVolume;
    [SerializeField]
    private Slider effectsVolume;
    [SerializeField]
    private Slider musicVolume;
    [SerializeField]
    private Button saveSettings;
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private AudioMixer effectsMixer;
    [SerializeField]
    private AudioMixer musicMixer;

    private void Awake()
    {
        LoadApplySettings();
        this.masterVolume.onValueChanged.AddListener(delegate { MasterVolume(); });
        this.effectsVolume.onValueChanged.AddListener(delegate { FXVolume(); });
        this.musicVolume.onValueChanged.AddListener(delegate { MusicVolume(); });
        this.saveSettings.onClick.AddListener(delegate { SafePreferences(); });
        this.closeButton.onClick.AddListener(delegate { CloseSettings(); });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadApplySettings()
    {
        if (PlayerPrefs.HasKey(PrefsKeys.masterVolKey))
        {
            this.masterVolume.value = PlayerPrefs.GetFloat(PrefsKeys.masterVolKey);         
        }
        if (PlayerPrefs.HasKey(PrefsKeys.effectsVolKey))
        {
            this.effectsVolume.value = PlayerPrefs.GetFloat(PrefsKeys.effectsVolKey);          
        }
        if (PlayerPrefs.HasKey(PrefsKeys.musicVolKey))
        {
            this.musicVolume.value = PlayerPrefs.GetFloat(PrefsKeys.musicVolKey);          
        }
    }

    private void MasterVolume()
    {
        AudioListener.volume = this.masterVolume.value;
    }

    private void FXVolume()
    {
        effectsMixer.SetFloat("Volume", this.effectsVolume.value);
    }

    private void MusicVolume()
    {
        musicMixer.SetFloat("Volume", this.musicVolume.value);
    }

    private void SafePreferences()
    {
        PlayerPrefs.SetFloat(PrefsKeys.masterVolKey, masterVolume.value);
        PlayerPrefs.SetFloat(PrefsKeys.effectsVolKey, effectsVolume.value);
        PlayerPrefs.SetFloat(PrefsKeys.musicVolKey, musicVolume.value);
        PlayerPrefs.Save();
        this.gameObject.SetActive(false);
    }

    private void CloseSettings()
    {
        this.gameObject.SetActive(false);
    }
}
