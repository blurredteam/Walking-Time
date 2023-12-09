using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GeneradorPuzleSumar : MonoBehaviour
{
    public List<Figura> figurasTotales;
    public List<Figura> figuras;
    public Pieza[] piezasTotales;
    int numTotalPiezasAPoner = 0;
    
    private Figura figuraElegida;
    
    [SerializeField] private Camera _puzzleCamera;
    private GameObject selectedObject;
    public Button volverBtn;

    Vector3 offset;

    public GameObject panFinal;
    
    public Transitioner transition;
    public float transitionTime = 1f;

    [SerializeField] private AudioClip fondo;

    private void Awake()
    {
        transition = ScenesManager.instance.transitioner;
    }

    void Start()
    {
        AudioManager.instance.PlayBackMusic(fondo);
        int numfigurasTotales = Random.Range(1, 3);
        
        List<Figura> figurasTotalesDisponibles = new List<Figura>(figurasTotales);

        for (int i = 0; i < Mathf.Min(numfigurasTotales, figurasTotales.Count); i++)
        {
            
            int index = Random.Range(0, figurasTotalesDisponibles.Count);
            Figura figura = figurasTotalesDisponibles[index];
            numTotalPiezasAPoner += figura.piezas.Length;
            figura.gameObject.SetActive(true);
            figuras.Add(figura);
            figurasTotalesDisponibles.RemoveAt(index);
        }
        switch (numfigurasTotales)
        {
            case 2:
                figuras[1].transform.position = new Vector3(-6, -2.5f, 1);
                break;
            case 3:
                figuras[1].transform.position = new Vector3(-6, -2.5f, 1);
                figuras[2].transform.position = new Vector3(6, -2.5f, 1);
                break;
        }

        volverBtn.onClick.AddListener(delegate
        {
            AudioManager.instance.ButtonSound();
            StartCoroutine(EsperarYSalir());
        });
    }
    
    IEnumerator EsperarYSalir()
    {
        AudioManager.instance.ButtonSound();
        AudioManager.instance.LoseMusic();
        volverBtn.gameObject.SetActive(false);
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        
        volverBtn.gameObject.SetActive(true);
        transition.DoTransitionOnce();
        LevelManager.instance.teamEnergy -= 10*LevelManager.instance.expEnergy;
        LevelManager.instance.expEnergy+=1;
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzleSumarFiguras);
        LevelManager.instance.ActivateScene();
        AudioManager.instance.PlayAmbient();
    }

    private void Update()
    {
        Vector3 mousePosition = _puzzleCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.ButtonSound3();
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject && targetObject.gameObject.layer==7)
            {
                selectedObject = targetObject.transform.gameObject;
                var position = selectedObject.transform.position;
                offset = position - mousePosition;
            }
        }

        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            int piezasSobrantes = 0;
            int estanColocadas = 0;
            foreach (var figura in figuras)
            {
                if (selectedObject.GetComponent<Collider2D>().OverlapPoint(figura.transform.position))
                {
                    selectedObject.transform.position = figura.transform.position - new Vector3(0.1f, 0.07f);

                    foreach (var pieza in figura.piezas)
                    {
                        if (pieza.name.Equals(selectedObject.name))
                        {
                            pieza.colocada = true;
                            pieza.GetComponent<Collider2D>().enabled = false;
                        }


                    }
                    foreach (var pieza in piezasTotales)
                    {
                        if (!pieza.colocada)
                        {
                            if (Vector3.Distance(pieza.gameObject.transform.position,
                                    figura.gameObject.transform.position) < 1f)
                            {
                                piezasSobrantes++;
                            }
                        }
                        else
                        {
                            estanColocadas++;
                        }
                    }


                }

                
            }

            if (estanColocadas==numTotalPiezasAPoner && piezasSobrantes == 0)
            {
                StartCoroutine(EsperarYRecompensa(true));
            }

            selectedObject = null;
        }
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
        panFinal.SetActive(true);
        volverBtn.gameObject.SetActive(false);
        transition.DoTransitionOnce();
        
        
        yield return new WaitForSeconds(1.5f);
        
        if (ganado)
        {
            AudioManager.instance.WinMusic();
            Recompensas(10);
            
        }
        else
        {
            AudioManager.instance.LoseMusic();
            Recompensas(-10);
            
        }
        

        yield return new WaitForSeconds(transitionTime);
        
        volverBtn.gameObject.SetActive(true);
        transition.DoTransitionOnce();
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzleSumarFiguras);
        LevelManager.instance.ActivateScene();
        AudioManager.instance.PlayAmbient();
    }
}