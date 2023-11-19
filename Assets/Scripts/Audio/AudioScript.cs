using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{
    [SerializeField] private AudioSource src;
    [SerializeField] private AudioClip sfx_button;
    [SerializeField] private List<Button> _buttons;

    private void Start()
    {
        for(int i=0; i< _buttons.Count; i++) 
        {
            _buttons[i].onClick.AddListener(delegate { SoundButton(); });
        }
    }
    public void SoundButton()
    {
        src.clip = sfx_button;
        src.Play();
    }

   
}
