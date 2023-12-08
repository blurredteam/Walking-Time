using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettingsManager : MonoBehaviour
{
    [SerializeField] public GameObject SoundPanelSettings;
    [SerializeField] public GameObject PanelSettings;
    [SerializeField] private Button BotonPanelSettings;


    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider ButtonsSlider;
    [SerializeField] private Slider SoundtrackSlider;
    [SerializeField] private Slider BackgroundSlider;



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
        if (PlayerPrefs.HasKey("backgroundVol"))
        {
            LoadLevel1();
        }
        else { SetBackgroundVolume(); }

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
        BackgroundSlider.value = PlayerPrefs.GetFloat("backgroundVol");
        SetBackgroundVolume();
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
    public void SetBackgroundVolume()
    {
        float volume = BackgroundSlider.value;
        Mixer.SetFloat("BackgroundVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("backgroundVol", volume);
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
    public void ActivateSoundPanel()
    {
        if (SoundPanelSettings.activeSelf) //objeto activo
        {
            SoundPanelSettings.SetActive(false);
        }
        else
        {
            SoundPanelSettings.SetActive(true);
        }

    }

}
