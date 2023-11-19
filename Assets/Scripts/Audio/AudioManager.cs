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
    [SerializeField] private AudioSource levelSrc;

    [Header("--------------Audio Clip--------------")]
    public AudioClip backgroundMenu;
    public AudioClip backgroundLevel1;
    public AudioClip buttonMenu;
    public AudioClip buttonLevel;
    public AudioClip buttonBtwPuzzles;

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
        //levelSrc.clip = backgroundMenu;
        levelSrc.Play();
  }

    public void PlaySfx(AudioClip clip)
    {
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
}


