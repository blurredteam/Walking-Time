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

    [SerializeField] private Camera _puzzleCamera;
    [SerializeField] private Button exitBtn;

    public TextMeshProUGUI textoVictoria;
    public TextMeshProUGUI textoIntentos;
    public GameObject textoExplicativo;
    public GameObject imagenobjetoAEncontrar;
    public GameObject panelFinal;

    public int intentos = 3;

    void Start()
    {
        // TODOS LAS CASILLAS TENDRAN QUE TENER ALGO ASI
        exitBtn.onClick.AddListener(delegate
        {
            ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleFinder);
            LevelManager.instance.ActivateScene();
        });
        // ---------------------------------------------
        textoIntentos.text = "Intentos restantes: " + intentos;
        textoVictoria.text = "";
        objetoAEncontrar.transform.position = arrayPos[Random.Range(0, arrayPos.Count)];
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
        
        
        yield return new WaitForSeconds(1.5f);
        
        if (ganado)
        {
            Recompensas(10);
            
        }
        else
        {
            Recompensas(-10);
            
        }
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleFinder);
        LevelManager.instance.ActivateScene();
    }
}