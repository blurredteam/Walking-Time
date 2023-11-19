using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class salida : MonoBehaviour
{
    public GameObject panel;
    public GameObject botonSalir1;
    
    public Transitioner transition;
    public float transitionTime = 1f;
    [SerializeField] private Button continueBtn;

    private void Awake()
    {
        transition = ScenesManager.instance.transitioner;
    }

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
            botonSalir1.SetActive(false);
            
        }
        //cambio de escenas
    }

    public void SalirDelJuegoVictoria()
    {
        AudioManager.instance.ButtonSound();
        
        //LevelManager.instance.teamEnergy -= 10;
        Recompensas(10);
        
    }
    public void SalirDelJuegoDerrota()
    {
        AudioManager.instance.ButtonSound();
        AudioManager.instance.LoseMusic();
        //LevelManager.instance.teamEnergy -= 10;
        Recompensas(-10);
        
    }
    
    private void Recompensas(int recompensa)
    {
        StartCoroutine(EsperarYRecompensa(recompensa));
    }
    
    IEnumerator EsperarYRecompensa(int recompensa)
    {
        if(recompensa>0){
            //AudioManager.instance.WinMusic();
            LevelManager.instance.gold += recompensa;
        }
        else
        {
            
            LevelManager.instance.teamEnergy -= 10*LevelManager.instance.expEnergy;
            LevelManager.instance.expEnergy+=1;
        }
        continueBtn.enabled = false;
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        continueBtn.enabled = true;
        transition.DoTransitionOnce();
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleHielo);
        LevelManager.instance.ActivateScene();
        AudioManager.instance.PlayAmbient();
    }
}
