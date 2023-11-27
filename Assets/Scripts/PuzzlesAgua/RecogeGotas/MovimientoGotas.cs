using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoGotas : MonoBehaviour
{
    private float velocidad;

    // Start is called before the first frame update
    void Awake()
    {
       velocidad = Random.Range(1.5f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * velocidad * Time.deltaTime;
        if (RecogeGotas.instance.MovimientoEmpezado == false) { gameObject.SetActive(false); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            if (this.CompareTag("Gota")) { RecogeGotas.instance.gotasTotales++; }
            Debug.Log("Comió suelo");
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }

    }
}
