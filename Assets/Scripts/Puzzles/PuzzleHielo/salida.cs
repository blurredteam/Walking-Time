using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salida : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entramos en el trigger");
        if (other.gameObject.name == "Player")
        {
            AudioManager.instance.WinMusic();
            panel.SetActive(true);
            
        }
        //cambio de escenas
    }

    public void SalirDelJuego()
    {
        AudioManager.instance.ButtonSound();
        
        //LevelManager.instance.teamEnergy -= 10;
        Recompensas(10);
        
    }
    
    private void Recompensas(int recompensa)
    {
        if (recompensa > 0)
        {
            AudioManager.instance.WinMusic();
            LevelManager.instance.gold += recompensa;
        }
        else
        {
            AudioManager.instance.LoseMusic();
            LevelManager.instance.teamEnergy -= recompensa;
        }
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleHielo);
        LevelManager.instance.ActivateScene();
        AudioManager.instance.PlayAmbient();
    }
}
