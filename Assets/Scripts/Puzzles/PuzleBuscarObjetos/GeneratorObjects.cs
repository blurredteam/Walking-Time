using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GeneratorObjects : MonoBehaviour
{
    // Start is called before the first frame update

    // public GameObject objetoAEncontrar;
    // public List<Vector3> arrayPos;
    private bool empezar = false;
    public GameObject[] figuras;
    private GameObject figuraElegida;

    // [SerializeField] private Camera _puzzleCamera;
    [SerializeField] private Button exitBtn;

    [SerializeField] private AudioClip fondo;

    public TextMeshProUGUI textoVictoria;
    public TextMeshProUGUI textoIntentos;
    public GameObject textoExplicativo;

    public GameObject panelFinal;

    public Image imagenFiguraUI;
    public Image imagenobjetoAEncontrar;

    public int intentos = 3;

    public Transitioner transition;
    public float transitionTime = 1f;

    private bool botonPulsado;

    private void Awake()
    {

        transition = ScenesManager.instance.transitioner;
        
    }

    void Start()
    {
        // TODOS LAS CASILLAS TENDRAN QUE TENER ALGO ASI
        exitBtn.onClick.AddListener(delegate
        {
            StartCoroutine(EsperarYSalir());

        });
        AudioManager.instance.PlayBackMusic(fondo);

        // ---------------------------------------------
        textoIntentos.text = "Intentos restantes: " + intentos;
        textoVictoria.text = "";
        int index = Random.Range(0, figuras.Length - 1);
        figuras[index].gameObject.SetActive(true);

        figuraElegida = figuras[index];
        botonPulsado = false;

        imagenFiguraUI.sprite = figuraElegida.GetComponent<Image>().sprite;
        imagenobjetoAEncontrar.sprite = imagenFiguraUI.sprite;
        figuraElegida.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        StartCoroutine(GetButton());
    }

    IEnumerator GetButton()
    {
        if (Input.GetMouseButtonDown(0) && intentos > 0 && empezar)
        {
            if (!botonPulsado)
            {
                intentos--;
                textoIntentos.text = "Intentos restantes: " + intentos;
                if (intentos == 0)
                {
                    AudioManager.instance.LoseMusic();
                    textoVictoria.text = "Has Perdido";
                    StartCoroutine(EsperarYRecompensa(false));
                }
            }
            botonPulsado = false;
        }

        yield return new WaitForSeconds(0);
    }
    IEnumerator EsperarYSalir()
    {
        AudioManager.instance.ButtonSound();
        AudioManager.instance.LoseMusic();
        exitBtn.gameObject.SetActive(false);
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        exitBtn.gameObject.SetActive(true);

        LevelManager.instance.teamEnergy -= 10 * LevelManager.instance.expEnergy;
        LevelManager.instance.expEnergy += 1;
        transition.DoTransitionOnce();

        UserPerformance.instance.updatePuzzlesPlayed(0); //contamos el puzle como fallado

        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleFinder);
        LevelManager.instance.ActivateScene();
        AudioManager.instance.PlayAmbient();
    }

    public void SetEmpezar()
    {
        empezar = true;
        textoExplicativo.SetActive(true);
        imagenFiguraUI.gameObject.SetActive(true);
    }

    private void Recompensas(int recompensa)
    {
        if (recompensa > 0)
            LevelManager.instance.gold += recompensa;
        else
        {
            LevelManager.instance.teamEnergy -= recompensa;
        }
    }

    IEnumerator EsperarYRecompensa(bool ganado)
    {
        panelFinal.SetActive(true);
        exitBtn.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        if (ganado)
        {
            AudioManager.instance.WinMusic();
            Recompensas(10);

            UserPerformance.instance.updatePuzzlesPlayed(1);    //contamos el puzle como ganado
        }
        else
        {
            AudioManager.instance.LoseMusic();
            Recompensas(-10);

            UserPerformance.instance.updatePuzzlesPlayed(0);    //contamos el puzle como fallado
        }

        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        exitBtn.gameObject.SetActive(true);
        transition.DoTransitionOnce();

        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleFinder);
        LevelManager.instance.ActivateScene();
        AudioManager.instance.PlayAmbient();
    }

    public void ClickarImagen()
    {
        botonPulsado = true;
        if (intentos > 0 && empezar)
        {
            textoVictoria.text = "Has ganado";
            empezar = false;
            StartCoroutine(EsperarYRecompensa(true));

        }
    }
}