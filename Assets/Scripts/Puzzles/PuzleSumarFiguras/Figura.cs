using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figura : MonoBehaviour
{
    public Pieza[] piezas; //Piezas que componen la figura
    public int colisiones;

    private void OnCollisionEnter2D(Collision2D other)
    {
        colisiones++;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        colisiones--;
    }
}
