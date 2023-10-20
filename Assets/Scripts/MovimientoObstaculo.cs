using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoObstaculo : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;

    void Update()
    {
        transform.Translate(Vector3.left * velocidadMovimiento * Time.deltaTime);
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}