using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GeneradorPuzleSumar : MonoBehaviour
{
    public Figura[] figuras;
    public Pieza[] piezasTotales;
    private Figura figuraElegida;
    [SerializeField] private Camera _puzzleCamera;
    private GameObject selectedObject;
    public Button volverBtn;

    Vector3 offset;

    public GameObject panFinal;

    void Start()
    {
        int index = Random.Range(0, figuras.Length - 1);
        figuras[index].gameObject.SetActive(true);

        figuraElegida = figuras[index];

        volverBtn.onClick.AddListener(delegate
        {
            AudioManager.instance.ButtonSound();
            AudioManager.instance.LoseMusic();

            ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzleSumarFiguras);
            LevelManager.instance.ActivateScene();
        });
    }

    private void Update()
    {
        Vector3 mousePosition = _puzzleCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
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
            if (selectedObject.GetComponent<Collider2D>().OverlapPoint(figuraElegida.transform.position))
            {
                selectedObject.transform.position = figuraElegida.transform.position;
                
                foreach (var pieza in figuraElegida.piezas)
                {
                    if (pieza.name.Equals(selectedObject.name))
                    {
                        pieza.colocada = true;
                        pieza.GetComponent<Collider2D>().enabled = false;
                    }

                    
                }

                
            }
            int piezasSobrantes = 0;
            int estanColocadas = 0;

            foreach (var pieza in piezasTotales)
            {
                if (!pieza.colocada)
                {
                    if (Vector3.Distance(pieza.gameObject.transform.position,
                            figuraElegida.gameObject.transform.position) < 1f)
                    {
                        piezasSobrantes++;
                    }
                }
                else
                {
                    estanColocadas++;
                }
            }

            if (estanColocadas==figuraElegida.piezas.Length && piezasSobrantes == 0)
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
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzleSumarFiguras);
        LevelManager.instance.ActivateScene();
    }
}