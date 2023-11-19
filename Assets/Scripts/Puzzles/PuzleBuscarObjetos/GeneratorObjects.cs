using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorObjects : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject objetoAEncontrar;
    public List<Vector3> arrayPos;
    private bool empezar = false;
    public GameObject[] figuras;
    private GameObject figuraElegida;

    [SerializeField] private Camera _puzzleCamera;
    [SerializeField] private Button exitBtn;

    public TextMeshProUGUI textoVictoria;
    public TextMeshProUGUI textoIntentos;
    public GameObject textoExplicativo;
    public GameObject imagenobjetoAEncontrar;
    public GameObject panelFinal;

    public Image imagenFiguraUI;

    public int intentos = 3;
    
    public Transitioner transition;
    public float transitionTime = 1f;

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
        // ---------------------------------------------
        textoIntentos.text = "Intentos restantes: " + intentos;
        textoVictoria.text = "";
        int index = Random.Range(0, figuras.Length - 1);
        figuras[index].gameObject.SetActive(true);

        figuraElegida = figuras[index];
        imagenFiguraUI.gameObject.SetActive(true);
        imagenFiguraUI.sprite = figuraElegida.GetComponent<SpriteRenderer>().sprite;
    }
    IEnumerator EsperarYSalir()
    {
        AudioManager.instance.ButtonSound();
        AudioManager.instance.LoseMusic();
        exitBtn.gameObject.SetActive(false);
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        exitBtn.gameObject.SetActive(true);
        
        LevelManager.instance.teamEnergy -= 10*LevelManager.instance.expEnergy;
        LevelManager.instance.expEnergy+=1;
        transition.DoTransitionOnce();
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleFinder);
        LevelManager.instance.ActivateScene();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = _puzzleCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && intentos > 0 && empezar)
        {
            //Esto es para que si pasas el raton por ecnima del objecto sepa que es ese objeto
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                textoVictoria.text = "Has ganado";
                StartCoroutine(EsperarYRecompensa(true));
            }
            else
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
        }
    }

    public void SetEmpezar()
    {
        empezar = true;
        textoExplicativo.GetComponent<RectTransform>().transform.position = new Vector3(120f, 70f, 0f);
        imagenobjetoAEncontrar.GetComponent<RectTransform>().transform.position = new Vector3(270f, 70f, 0);
        textoExplicativo.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0f);
        imagenobjetoAEncontrar.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 0);
    }
    
    private void Recompensas(int recompensa)
    {
        if(recompensa>0)
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
            
        }
        else
        {
            //AudioManager.instance.LoseMusic();
            Recompensas(-10);
            
        }
        
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        exitBtn.gameObject.SetActive(true);
        transition.DoTransitionOnce();
        
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleFinder);
        LevelManager.instance.ActivateScene();
    }
}