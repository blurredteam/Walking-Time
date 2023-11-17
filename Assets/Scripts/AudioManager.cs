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

    [SerializeField] private List<AudioClip> _winSound;
    [SerializeField] private List<AudioClip> _loseSound;
    [SerializeField] private AudioClip kindaLoseSound;
    [SerializeField] private AudioClip _buttonSound;
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
        Debug.Log(idx);
        PlaySfx(_winSound[idx]);

    }

    public void LoseMusic()
    {
        int idx = UnityEngine.Random.Range(0, _loseSound.Count);
        Debug.Log(idx);
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
}


