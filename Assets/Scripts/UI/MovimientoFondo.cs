using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoFondo : MonoBehaviour
{
    private float intensidad = 400f; // Intensidad de la agitaci�n
    private float velocidad = 0.5f; // Velocidad de la agitaci�n

    private Vector3 posicionInicial;

    void Start()
    {
        // Guarda la posici�n inicial del objeto
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Calcula un desplazamiento aleatorio en las direcciones X, Y y Z
        float offsetX = Mathf.Sin(Time.time * velocidad) * intensidad;
        float offsetY = Mathf.Cos(Time.time * velocidad) * intensidad;
        float offsetZ = Mathf.Sin(Time.time * velocidad) * intensidad;

        // Aplica el desplazamiento a la posici�n del objeto
        transform.position = posicionInicial + new Vector3(offsetX, offsetY, offsetZ);
    }
}


