using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform objetoCentral; // El objeto alrededor del cual quieres rotar.
    public float velocidadRotacion = 30.0f; // Velocidad de rotación en grados por segundo.
    public float velocidadRotacionPropia = 60.0f;
    public Transform puntoObjetivo;

    private bool pulsado;

    private void Start()
    {
        pulsado = false;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject&& targetObject.transform==gameObject.transform.parent)
            {
                // Calcular la posición relativa del objeto con respecto al punto objetivo
                Vector3 posicionRelativaObjetivo = transform.position - puntoObjetivo.position;

                // Calcular la distancia del objeto al punto objetivo
                float distanciaAlObjetivo = posicionRelativaObjetivo.magnitude;

                // Asignar una puntuación en función de la distancia
                int puntuacion = CalcularPuntuacion(distanciaAlObjetivo);
                Debug.Log("Puntuación: " + puntuacion);
                pulsado = true;
            }
        }
        else if (!pulsado)
        {
            // Calcula la posición relativa del objeto al objeto central.
            Vector3 posicionRelativa = transform.position - objetoCentral.position;

            // Calcula el ángulo de rotación basado en la velocidad y el tiempo.
            float anguloRotacion = velocidadRotacion * Time.deltaTime;

            // Rota la posición relativa alrededor del objeto central.
            Vector3 nuevaPosicionRelativa = Quaternion.Euler(0, 0, anguloRotacion) * posicionRelativa;

            // Establece la nueva posición del objeto basada en la posición relativa.
            transform.position = objetoCentral.position + nuevaPosicionRelativa;

            float anguloRotacionPropia = velocidadRotacionPropia * Time.deltaTime;
            transform.Rotate(Vector3.forward * anguloRotacionPropia);
        }
    }

    int CalcularPuntuacion(float distancia)
    {
        // Define tu lógica para asignar puntuaciones en función de la distancia.
        // Puedes usar una función matemática o establecer rangos de distancia para diferentes puntuaciones.
        if (distancia < 0.5f)
        {
            return 100;
        }
        else if (distancia < 0.7f)
        {
            return 50;
        }
        else
        {
            return 10;
        }
    }
}