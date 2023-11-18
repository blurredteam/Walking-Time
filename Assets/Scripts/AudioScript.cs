using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField]public AudioSource src;
    [SerializeField]public AudioClip sfx_button,sfxContinue;

    public void soundButton()
    {
        src.clip = sfx_button;
        src.Play();
    }

    public void continueButton()
    {
        src.clip = sfxContinue;
        src.Play();   
        
        
    }
}
