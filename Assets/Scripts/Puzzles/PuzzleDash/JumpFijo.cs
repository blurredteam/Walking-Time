using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JumpFijo : MonoBehaviour
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
    public GameObject panFinal;
    public TextMeshProUGUI textoFinal;

    [SerializeField] private AudioClip fondo;
    [SerializeField] private AudioClip sonidoSaltar;
    
    // private Transitioner transition;
    // public float transitionTime = 1f;
    //
    // private void Awake()
    // {
    //     transition = ScenesManager.instance.transitioner;
    // }
    private void Start()
    {
         AudioManager.instance.ButtonSound();
        // TODOS LAS CASILLAS TENDRAN QUE TENER ALGO ASI
        exitBtn.onClick.AddListener(delegate
        {
            AudioManager.instance.ButtonSound();
            //AudioManager.instance.LoseMusic();

            LevelManager.instance.teamEnergy -= 10 * LevelManager.instance.expEnergy;
            LevelManager.instance.expEnergy += 1;
            ScenesManager.instance.UnloadTile(ScenesManager.Scene.NivelGeometryDash);
            LevelManager.instance.ActivateScene();
            AudioManager.instance.PlayAmbient();
        });
        // ---------------------------------------------
        AudioManager.instance.PlayBackMusic(fondo);
        tiempoRestante = tiempoMaximo;
        ActualizarTextoTiempo();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && enElSuelo)
        {
            Salta();
        }

        // if (Input.GetMouseButton(0) && enElSuelo)
        // {
        //     tiempoSaltoPresionado += Time.deltaTime;
        //
        //     // Limita el tiempo presionado al tiempo máximo de salto.
        //     tiempoSaltoPresionado = Mathf.Clamp(tiempoSaltoPresionado, 0.0f, tiempoMaximoSalto);
        // }
        //
        // if (Input.GetMouseButtonUp(0) && enElSuelo)
        // {
        //     
        // }
        
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
        AudioManager.instance.PlaySfx(sonidoSaltar);
        // Calcula la velocidad en función del tiempo presionado.
        float fuerzaSalto = velocidadMaxima;


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
            if(!juegoTerminado){
                JuegoTerminado(false);
            }
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
        panFinal.SetActive(true);

        if (ganaste)
        {
            AudioManager.instance.WinMusic();

            textoTiempo.text = "¡Ganaste!";
            textoFinal.text = "Enhorabuena lo has conseguido! Ganas 10 de oro";
        }
        else
        {
            AudioManager.instance.LoseMusic();
            textoTiempo.text = "¡Perdiste!";
            textoFinal.text = "Una pena, a ver si la próxima te va mejor. Pierdes 10 de energía.";
        }

        StartCoroutine(EsperarYRecompensa(ganaste));
    }
    
    private void Recompensas(int recompensa)
    {
        if(recompensa>0)
            LevelManager.instance.gold += recompensa;
        else
        {
            LevelManager.instance.teamEnergy -= recompensa;
        }
    }

    IEnumerator EsperarYRecompensa(bool ganado)
    {
        
        yield return new WaitForSeconds(1.5f);
        
        if (ganado)
        {
            Recompensas(10);
            
        }
        else
        {
            Recompensas(-10);
            
        }
        // transition.DoTransitionOnce();
        //
        // yield return new WaitForSeconds(transitionTime);
        // transition.DoTransitionOnce();
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.NivelGeometryDash);
        LevelManager.instance.ActivateScene();
        AudioManager.instance.PlayAmbient();
    }
}