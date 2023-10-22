using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salida : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entramos en el trigger");
        if (other.gameObject.name == "Player")
        {
            panel.SetActive(true);
        }
        //cambio de escenas


    }
}
