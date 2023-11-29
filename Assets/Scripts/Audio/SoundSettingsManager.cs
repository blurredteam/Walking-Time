using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettingsManager : MonoBehaviour
{
    [SerializeField] public GameObject PanelSettings;
    [SerializeField] private Button BotonPanelSettings;


    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider ButtonsSlider;
    [SerializeField] private Slider SoundtrackSlider;
    [SerializeField] private Slider Level1Slider;
    [SerializeField] private Slider MainMenuSlider;


    void Start()
    {
        if (PlayerPrefs.HasKey("masterVol"))
        {
            LoadMaster();
        }
        else { SetMasterVolume(); }
        if (PlayerPrefs.HasKey("sfxVol"))
        {
            LoadSFX();
        }
        else { SetSFXVolume(); }
        if (PlayerPrefs.HasKey("buttonsVol"))
        {
            LoadButtons();
        }
        else { SetButtonsVolume(); }
        if (PlayerPrefs.HasKey("soundtrackVol"))
        {
            LoadSoundtrack();
        }
        else { SetSoundtrackVolume(); }
        if (PlayerPrefs.HasKey("level1Vol"))
        {
            LoadLevel1();
        }
        else { SetLevel1Volume(); }
        if (PlayerPrefs.HasKey("mainmenuVol"))
        {
            LoadMainMenu();
        }
        else { SetMainMenuVolume(); }
    }

    public void LoadMaster()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("masterVol");
        SetMasterVolume();
    }
    public void LoadSFX()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVol");
        SetSFXVolume();
    }
    public void LoadButtons()
    {
        ButtonsSlider.value = PlayerPrefs.GetFloat("buttonsVol");
        SetButtonsVolume();
    }
    public void LoadSoundtrack()
    {
        SoundtrackSlider.value = PlayerPrefs.GetFloat("soundtrackVol");
        SetSoundtrackVolume();
    }
    public void LoadLevel1()
    {
        Level1Slider.value = PlayerPrefs.GetFloat("level1Vol");
        SetLevel1Volume();
    }
    public void LoadMainMenu()
    {
        MainMenuSlider.value = PlayerPrefs.GetFloat("mainmenuVol");
        SetMainMenuVolume();
    }

    public void SetMasterVolume()
    {
        float volume = MasterSlider.value;
        Mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVol", volume);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        Mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVol", volume);
    }
    public void SetButtonsVolume()
    {
        float volume = ButtonsSlider.value;
        Mixer.SetFloat("ButtonsVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("buttonVol", volume);
    }
    public void SetSoundtrackVolume()
    {
        float volume = SoundtrackSlider.value;
        Mixer.SetFloat("SoundtrackVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("soundtrackVol", volume);
    }
    public void SetLevel1Volume()
    {
        float volume = Level1Slider.value;
        Mixer.SetFloat("Level1Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("level1Vol", volume);
    }
    public void SetMainMenuVolume()
    {
        float volume = MainMenuSlider.value;
        Mixer.SetFloat("MainMenuVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("mainmenuVol", volume);
    }



    public void ActivateSettingsPanel()
    {
        if (PanelSettings.activeSelf) //objeto activo
        {
            PanelSettings.SetActive(false);
        }
        else
        {
            PanelSettings.SetActive(true);
        }

    }

}
