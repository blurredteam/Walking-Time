using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPuzzFont : MonoBehaviour
{
    public AudioSource src;
    public AudioClip waterSfx, buttonSfx, winSfx, loseSfx, soundTrack;

    private void Start()
    {
        src.clip = soundTrack;
        src.Play();
    }
    public void waterPlaySfx()
    {
        src.clip = waterSfx;
        src.Play();
    }
    
    public void buttonPlaySfx()
    {
        src.clip = buttonSfx;
        src.Play();
    }
    
    public void winPlaySfx()
    {
        src.clip = winSfx;
        src.Play();
    }
    
    public void losePlaySfx()
    {
        src.clip = loseSfx;
        src.Play();
    }
}
