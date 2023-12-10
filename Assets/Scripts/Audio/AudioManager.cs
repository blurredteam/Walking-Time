using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header("--------------Audio Source--------------")]
    [SerializeField] private AudioSource musicSrc;
    [SerializeField] private AudioSource sfxSrc;
    [SerializeField] private AudioSource buttonSfxSrc;
    [SerializeField] private AudioSource menuSrc;
    [SerializeField] private AudioSource levelSrc;
    

    [Header("--------------Audio Clip--------------")]
    [SerializeField] private AudioClip backgroundMenu;
    [SerializeField] private AudioClip backgroundSelection;
    [SerializeField] private AudioClip backgroundLevel1;
    [SerializeField] private AudioClip backgroundLevel2;

    [SerializeField] private AudioClip finitoSound;


    [Header("--------------Win/Lose Sounds--------------")]
    [SerializeField] private List<AudioClip> _winSound;
    [SerializeField] private List<AudioClip> _loseSound;
    [SerializeField] private AudioClip kindaLoseSound;
    [Header("--------------Button Sounds--------------")]
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private AudioClip _buttonSound2;
    [SerializeField] private AudioClip _buttonSound3;
    [SerializeField] private AudioClip _buttonSound4;
    [SerializeField] private AudioClip _buttonSoundBlock;
    /*public AudioClip sfxIce;
    public AudioClip sfxCrash;
    public AudioClip sfxFire;
    public AudioClip sfxChoseCharacter;
    */
    public static AudioManager instance;
    private void Start()
    {
        instance = this; 
        DontDestroyOnLoad(gameObject);
        //menuSrc.Play();
        //levelSrc.Play();
    }
    public void StopMenuMusic()
    {
        menuSrc.Stop();
    }
    public void PlaySfx(AudioClip clip)
    {
        sfxSrc.volume = 0.4f;
        sfxSrc.PlayOneShot(clip);
    }
    public void PlayButtonSfx(AudioClip clip)
    {
        buttonSfxSrc.PlayOneShot(clip);
    }

    public void StopSfx()
    {
        sfxSrc.Stop();
    }
    public void PlayBackMusic(AudioClip clip)
    {
        levelSrc.Pause();
        musicSrc.clip = clip;
        musicSrc.Play();
    }
    public void PlayAmbient()
    {
       musicSrc.Stop();
        musicSrc.volume = 0.3f;
       levelSrc.Play();
    }

    public void WinMusic()
    {
        int idx = UnityEngine.Random.Range(0, _winSound.Count);
        PlaySfx(_winSound[idx]);

    }

    public void LoseMusic()
    {
        int idx = UnityEngine.Random.Range(0, _loseSound.Count);
        PlaySfx(_loseSound[idx]);
    }

    public void KindaLoseMusic()
    {
        PlaySfx(kindaLoseSound);
    }

    public void ButtonSound()
    {
        PlayButtonSfx(_buttonSound);
    }
    
    public void ButtonSound2()
    {
        PlayButtonSfx(_buttonSound2);
    }

    public void ButtonSound3()
    {
        PlayButtonSfx(_buttonSound3);
    }

    public void ButtonSound4()
    {
        PlayButtonSfx(_buttonSound4);
    }

    public void ButtonSoundBlock()
    {
        PlaySfx(_buttonSoundBlock);
    }
    public void OnLevel1()
    {
        levelSrc.clip = backgroundLevel1;
        //levelSrc.Play();
    }
    public void OnLevel2()
    {
        levelSrc.clip = backgroundLevel2;
        levelSrc.Play();
    }
    public void OnSelection()
    {
        musicSrc.volume = 0.2f;//Para q no se escuche tanto la seleccion
        PlayBackMusic(backgroundSelection);
    }

    public void RisaFinito()
    {
        PlaySfx(finitoSound);
    }

    public void LeaveGame()
    {
        musicSrc.Stop();
        sfxSrc.Stop();
        buttonSfxSrc.Stop();
        menuSrc.Stop();
        levelSrc.Stop();
    }
}


