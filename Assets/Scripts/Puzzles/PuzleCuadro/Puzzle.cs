using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private Button continueBtn;
    [SerializeField] private Camera _puzzleCamera;

    [SerializeField] private AudioClip fondo;

    public List<GameObject> listaPiezas;
    public List<GameObject> listaAux;
    public List<Vector3> posiciones;

    public GameObject espacio;
    public GameObject ultimaPieza;

    private GameObject selectedObject;
    private Vector3 previousPosition;
    Vector3 offset;

    public GameObject panFinal;
    
    public Transitioner transition;
    public float transitionTime = 1f;

    private bool ganaste = true;

    private void Awake()
    {
        transition = ScenesManager.instance.transitioner;
    }

    private void Start()
    {
        AudioManager.instance.ButtonSound();
        AudioManager.instance.PlayBackMusic(fondo);
        continueBtn.onClick.AddListener(delegate
        {
            StartCoroutine(EsperarYSalir());
        });
        // ---------------------------------------------

        var numbers = new List<int>(9);

        for (int i = 0; i < listaPiezas.Count; i++)
        {
            numbers.Add(i);
        }

        posiciones = new List<Vector3>(9);

        for (int i = 0; i < Mathf.Sqrt(listaPiezas.Count); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(listaPiezas.Count); j++)
            {
                posiciones.Add(new Vector3(-2+2 * j, -2+2 * i, 0));
            }
        }

        do{
            listaAux = new List<GameObject>(listaPiezas);

            for (int i = 0; i < listaPiezas.Count; i++)
            {
                var thisNumber = Random.Range(0, numbers.Count);
                listaPiezas[i] = listaAux[numbers[thisNumber]];
                numbers.RemoveAt(thisNumber);
            }
        }while (ListasOrdenadasIgualmente(listaPiezas, listaAux));
        

        for (int i = 0; i < listaPiezas.Count; i++)
        {
            listaPiezas[i].transform.position = posiciones[i];
        }

        foreach (var pieza in listaPiezas)
        {
            if (Vector3.Distance(espacio.transform.position, pieza.transform.position) < 3 &&
                pieza.TryGetComponent<Collider2D>(out Collider2D col))
            {
                col.enabled = true;
            }
        }
    }
    
    IEnumerator EsperarYSalir()
    {
        continueBtn.gameObject.SetActive(false);
        AudioManager.instance.ButtonSound();
        AudioManager.instance.LoseMusic();
        AudioManager.instance.PlayAmbient();
        
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        
        continueBtn.gameObject.SetActive(true);
        transition.DoTransitionOnce();
        LevelManager.instance.teamEnergy -= 10*LevelManager.instance.expEnergy;
        LevelManager.instance.expEnergy+=1;
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzleCuadro);
        LevelManager.instance.ActivateScene();
    }
    
    private bool ListasOrdenadasIgualmente<T>(List<T> lista1, List<T> lista2)
    {
        // Comprueba si dos listas están ordenadas igualmente.
        for (int i = 0; i < lista1.Count; i++)
        {
            if (!lista1[i].Equals(lista2[i]))
            {
                return false;
            }
        }
        return true;
    }

    void Update()
    {
        Vector3 mousePosition = _puzzleCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                var position = selectedObject.transform.position;
                previousPosition = new Vector3(position.x, position.y, position.z);
                offset = position - mousePosition;
            }
        }

        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            if (selectedObject.GetComponent<Collider2D>().OverlapPoint(espacio.transform.position))
            {
                selectedObject.transform.position = espacio.transform.position;
                espacio.transform.position = previousPosition;
                (listaPiezas[listaPiezas.IndexOf(selectedObject)], listaPiezas[listaPiezas.IndexOf(espacio)]) = (
                    listaPiezas[listaPiezas.IndexOf(espacio)], listaPiezas[listaPiezas.IndexOf(selectedObject)]);

                foreach (var pieza in listaPiezas)
                {
                    if (pieza.TryGetComponent<Collider2D>(out Collider2D col))
                        col.enabled = false;
                }

                foreach (var pieza in listaPiezas)
                {
                    if (Vector3.Distance(espacio.transform.position, pieza.transform.position) < 3 &&
                        pieza.TryGetComponent<Collider2D>(out Collider2D col))
                    {
                        col.enabled = true;
                    }
                }
            }
            else
                selectedObject.transform.position = previousPosition;

            selectedObject = null;
        }

        for (int i = 0; i < listaPiezas.Count; i++)
        {
            if (listaPiezas[i] != listaAux[i])
            {
                ganaste = false;
            }
        }

        if (ganaste)
        {
            AudioManager.instance.WinMusic();
            print("Has ganado");
            ganaste = false;
            ultimaPieza.SetActive(true);

            StartCoroutine(EsperarYRecompensa(true));
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
        continueBtn.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1.5f);
        if (ganado)
        {
            
            Recompensas(10);
        }
        else
        {
            AudioManager.instance.LoseMusic();

            Recompensas(-10);
        }
        
        
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        
        continueBtn.gameObject.SetActive(true);
        transition.DoTransitionOnce();
        AudioManager.instance.PlayAmbient();
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzleCuadro);
        LevelManager.instance.ActivateScene();
    }
}
