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
  private void Start()
  {
    musicSrc.clip = backgroundMenu;
    musicSrc.Play();
  }

  public void PlaySfx(AudioClip clip)
  {
    sfxSrc.PlayOneShot(clip);
  }
  
  public void PlayBackMusic(AudioClip clip)
  {
    musicSrc.PlayOneShot(clip);
  }
}


