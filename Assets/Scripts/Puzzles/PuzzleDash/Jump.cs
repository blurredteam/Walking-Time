using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Jump : MonoBehaviour
{
    public float velocidadMaxima = 10.0f; // Velocidad máxima en el eje Y.
    public float tiempoMaximoSalto = 2.0f; // Tiempo máximo de salto.

    private float tiempoSaltoPresionado = 0.0f;
    private bool enElSuelo = true;
    
    public TextMeshProUGUI  textoTiempo;
    public float tiempoMaximo = 10.0f; // Tiempo en segundos.
    private float tiempoRestante;
    private bool juegoTerminado = false;

    [SerializeField] Button exitBtn;

    private void Start()
    {
        // TODOS LAS CASILLAS TENDRAN QUE TENER ALGO ASI
        exitBtn.onClick.AddListener(delegate {
            ScenesManager.instance.UnloadTile(ScenesManager.Scene.NivelGeometryDash);
            LevelManager.instance.ActivateScene();
        });
        // ---------------------------------------------

        tiempoRestante = tiempoMaximo;
        ActualizarTextoTiempo();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && enElSuelo)
        {
            tiempoSaltoPresionado = 0.0f;
        }

        if (Input.GetMouseButton(0) && enElSuelo)
        {
            tiempoSaltoPresionado += Time.deltaTime;

            // Limita el tiempo presionado al tiempo máximo de salto.
            tiempoSaltoPresionado = Mathf.Clamp(tiempoSaltoPresionado, 0.0f, tiempoMaximoSalto);
        }

        if (Input.GetMouseButtonUp(0) && enElSuelo)
        {
            Salta();
        }
        
        if (!juegoTerminado)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                JuegoTerminado(true);
            }

            ActualizarTextoTiempo();
        }
    }

    void Salta()
    {
        // Calcula la velocidad en función del tiempo presionado.
        float fuerzaSalto = (tiempoSaltoPresionado / tiempoMaximoSalto) * velocidadMaxima;


        // Aplica la velocidad en el eje Y al objeto.
        gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, fuerzaSalto);

        enElSuelo = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enElSuelo = true;
        }
        else
        {
            JuegoTerminado(false);
        }
    }
    
    private void ActualizarTextoTiempo()
    {
        int segundos = Mathf.CeilToInt(tiempoRestante);
        textoTiempo.text = "Tiempo: " + segundos.ToString();
    }
    
    private void JuegoTerminado(bool ganaste)
    {
        juegoTerminado = true;

        if (ganaste)
        {
            textoTiempo.text = "¡Ganaste!";
        }
        else
        {
            textoTiempo.text = "¡Perdiste!";
        }

        EsperarYRecompensa(ganaste);
    }
    
    private void Recompensas(int cantEnergiaGanada)
    {
        int maxEnergia = LevelManager.instance.maxEnergy;
        int energia = LevelManager.instance.teamEnergy;

        if ((cantEnergiaGanada + energia) >= maxEnergia)
        {
            LevelManager.instance.teamEnergy = maxEnergia;
        }
        else
        {
            LevelManager.instance.teamWater += cantEnergiaGanada;
        }
    }

    IEnumerator EsperarYRecompensa(bool ganado)
    {
        // Espera durante 3 segundos.
        yield return new WaitForSeconds(1.5f);
        if (ganado)
        {
            Recompensas(10);
        }
        else
        {
            Recompensas(-10);
        }
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.NivelGeometryDash);
        LevelManager.instance.ActivateScene();
    }
}