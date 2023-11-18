using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    //public Vector3 posicionBase = new Vector3(-892, 0, -9719); //centro pegado al borde izq de la imagen de fondo
    public Vector3 posicionBase;
    public Vector3 posicionInicial;
    public Vector3 posicionFinal;
    public float velocidad = 10f;
    public Tile casillaObjetivo;
    public bool moviendose = false;
    public bool previoPrimerMov = true;

    private void Start()
    {
        posicionBase = transform.position;
    }

    private void Update()
    {
        if (previoPrimerMov)
        {
            transform.position = posicionBase;
        }

        else if (!moviendose && !previoPrimerMov)
        {
            transform.position = posicionFinal;     //si no se esta moviendo, lo fijamos a la casilla
        }
    }
    public void MoverJugador(Vector3 posicionCasilla, Tile casilla)
    {

        posicionInicial = transform.position;
        posicionFinal = posicionCasilla;
        casillaObjetivo = casilla;
        StartCoroutine(MoverCoroutine());
    }

    IEnumerator MoverCoroutine()
    {
        moviendose = true;
        previoPrimerMov = false;
        float tiempoInicio = Time.time;
        float distancia = Vector3.Distance(posicionInicial, posicionFinal);

        while (transform.position != posicionFinal)
        {
            transform.position = Vector3.Lerp(posicionInicial, posicionFinal, (Time.time - tiempoInicio));
            yield return null;
        }

        moviendose = false;
        casillaObjetivo.LoadTile();
    }
}
