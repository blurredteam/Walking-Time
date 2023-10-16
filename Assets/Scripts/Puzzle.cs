using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Puzzle : MonoBehaviour
{
    public List<Pieza> listaPiezas;
    public List<List<Vector3>> listaCombinaciones;
    private List<Vector3> posiciones=new List<Vector3>();
    private List<Vector3> resultado;
    
    public GameObject espacio;

    public GameObject piezaObjective;

    public GameObject selectedObject;
    public Vector3 previousPosition;
    Vector3 offset;

    private void Start()
    {
        int i = listaPiezas.Capacity+2;
        
        while (i >= 0)
        {
            var position = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
            if (!posiciones.Contains(position))
            {
                posiciones.Add(position);
                listaPiezas[i].transform.position = position;
                i--;
                if (i == listaPiezas.Capacity + 1)
                {
                    posiciones.Add(position);
                    espacio.transform.position = position;
                    i--;
                }
            }
            else if (i == listaPiezas.Capacity + 1)
            {
                posiciones.Add(position);
                piezaObjective.transform.position = position;
                i--;
            }
        }

        bool elegido=false;
        while (!elegido)
        {
            var ranNum = Random.Range(0, listaCombinaciones.Count);
            if (posiciones.Equals(listaCombinaciones[ranNum]))
            {
                elegido = true;
            }

            resultado = new List<Vector3>(listaCombinaciones[ranNum]);
            
        }
        
        foreach (var pieza in listaPiezas)
        {
            if (Vector3.Distance(espacio.transform.position, pieza.transform.position) < 2.5)
            {
                pieza.GetComponent<Collider2D>().enabled = true;
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

                foreach (var pieza in listaPiezas)
                {
                    pieza.GetComponent<Collider2D>().enabled = false;
                }

                foreach (var pieza in listaPiezas)
                {
                    if (Vector3.Distance(espacio.transform.position, pieza.transform.position) < 2.5)
                    {
                        pieza.GetComponent<Collider2D>().enabled = true;
                    }
                }
            }
            else
                selectedObject.transform.position = previousPosition;

            selectedObject = null;
        }

        if (posiciones.Equals(resultado))
        {
            print("Ganaste");
        }
    }
}