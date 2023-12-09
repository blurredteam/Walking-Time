using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class salida : MonoBehaviour
{
    public GameObject panel;
    public GameObject botonSalir1;

    private Transitioner transition;
    public float transitionTime = 1f;
    [SerializeField] private Button continueBtn;

    void Start()
    {
        transition = ScenesManager.instance.transitioner;
        panel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            AudioManager.instance.WinMusic();
            panel.SetActive(true);
            botonSalir1.SetActive(false);
        }
    }

    public void SalirDelJuegoVictoria()
    {
        AudioManager.instance.ButtonSound();

        LevelManager.instance.gold += 10;

        UserPerformance.instance.updatePuzzlesPlayed(1); //contamos el puzle como ganado

        StartCoroutine(FadeHielo());
    }
    public void SalirDelJuegoDerrota()
    {
        AudioManager.instance.ButtonSound();

        AudioManager.instance.LoseMusic();
        LevelManager.instance.teamEnergy -= 10 * LevelManager.instance.expEnergy;
        LevelManager.instance.expEnergy += 1;

        UserPerformance.instance.updatePuzzlesPlayed(0); //contamos el puzle como fallado

        StartCoroutine(FadeHielo());
    }

    IEnumerator FadeHielo()
    {
        continueBtn.interactable = false;
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(1f);

        continueBtn.interactable = true;
        transition.DoTransitionOnce();

        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleHielo);
        LevelManager.instance.ActivateScene();
        AudioManager.instance.PlayAmbient();
    }
}
