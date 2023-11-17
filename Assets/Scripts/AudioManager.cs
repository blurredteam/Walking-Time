using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
  [Header("--------------Audio Source--------------")]
  [SerializeField] private AudioSource musicSrc;
  [SerializeField] private AudioSource sfxSrc;
    [SerializeField] private AudioSource levelSrc;

    [Header("--------------Audio Clip--------------")]
  public AudioClip backgroundMenu;
  public AudioClip backgroundLevel1;
  public AudioClip buttonMenu;
  public AudioClip buttonLevel;
  public AudioClip buttonBtwPuzzles;
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
}


