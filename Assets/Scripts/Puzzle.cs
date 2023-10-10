using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Puzzle : MonoBehaviour
{
    public List<GameObject> listaPiezas;
    public List<GameObject> listaAux;
    public List<Vector3> posiciones;

    public GameObject espacio;
    public GameObject ultimaPieza;

    private GameObject selectedObject;
    private Vector3 previousPosition;
    Vector3 offset;

    private void Start()
    {
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
                posiciones.Add(new Vector3(1 + 2 * j, 1 + 2 * i, 0));
            }
        }

        listaAux = new List<GameObject>(listaPiezas);

        for (int i = 0; i < listaPiezas.Count; i++)
        {
            var thisNumber = Random.Range(0, numbers.Count);
            listaPiezas[i] = listaAux[numbers[thisNumber]];
            numbers.RemoveAt(thisNumber);
        }

        for (int i = 0; i < listaPiezas.Count; i++)
        {
            listaPiezas[i].transform.position = posiciones[i];
        }

        foreach (var pieza in listaPiezas)
        {
            if (Vector3.Distance(espacio.transform.position, pieza.transform.position) < 2.5 &&
                pieza.TryGetComponent<Collider2D>(out Collider2D col))
            {
                col.enabled = true;
            }
        }
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
                    if (Vector3.Distance(espacio.transform.position, pieza.transform.position) < 2.5 &&
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

        bool ganaste=true;

        for (int i = 0; i < listaPiezas.Count; i++)
        {
            if (listaPiezas[i] != listaAux[i])
            {
                ganaste = false;
            }
        }

        if (ganaste)
        {
            print("Has ganado");
            ganaste = false;
            ultimaPieza.SetActive(true);
        }
    }
}