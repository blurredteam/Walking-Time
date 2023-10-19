using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LlenadoBotella : MonoBehaviour
{

    public float velocidadLlenado = 1.0f;
    float valorMinimo;
    float valorMaximo;

    public float amplitudAgitacion = 10.0f;
    public float frecuenciaAgitacion = 2.0f;
    private bool llenando = false;
    private bool  finLlenado = false;
    private float tiempo = 0.0f;
    private float altura;
    private Quaternion rotacionInicial;
    public GameObject chorroAgua;
    public GameObject panelInicial;
    public GameObject panelFinal;
    public GameObject botonLlenar;
    private float referencia;//El limite donde se ganará el máximo de agua
    public TextMeshProUGUI aguaTexto;
    private int aguaGanada = 0;

    [SerializeField] private Button volverBtn;

    private void Start()
    {
        // TODOS LAS CASILLAS TENDRAN QUE TENER ALGO ASI
        volverBtn.onClick.AddListener(delegate {
            ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleFuente);
            LevelManager.instance.ActivateScene();
        });
        // ---------------------------------------------

        referencia = 2.62f;//Altura del triángulo rojo
        rotacionInicial = transform.rotation;
        //Para q sea mas probable valores mas bajos si no suele ser demasiado rápido
        float probabilidad = Random.value; // Valor aleatorio entre 0 y 1

        if (probabilidad < 0.5f)
        {
            // 50% de probabilidad para el rango de 0.5 a 2
            valorMinimo = 0.5f;
            valorMaximo = 2.0f;
        }
        else if(probabilidad>=0.5f && probabilidad< 0.7f)
        {
            // 20% de probabilidad de q vaya lento
            valorMinimo= 0.1f;
            valorMaximo= 1.0f;
        }
        else
        {
            // 30% de probabilidad de q vaya rapidp
            valorMinimo = 2.0f;
            valorMaximo = 4.0f;
        }

        velocidadLlenado = Random.Range(valorMinimo, valorMaximo);
    }

    void Update()
    {
        if (llenando && !finLlenado && altura<=3.2f)//Con límite de altura
        {
            chorroAgua.SetActive(true);
            // Incrementar la posición en el eje Y para elevar el Sprite
            transform.position += Vector3.up * velocidadLlenado * Time.deltaTime;
            altura = transform.position.y + 4.46f;//Añadimos lo ultimo para añadir la diferencia de tamaño
            // Agitar lateralmente con rotación
            tiempo += Time.deltaTime;
            float angulo = amplitudAgitacion * Mathf.Sin(frecuenciaAgitacion * tiempo);
            transform.rotation = Quaternion.Euler(0, 0, angulo); 
        }
        else
        {
            // Cuando no se está elevando, restablecer la rotación
            transform.rotation = rotacionInicial;          
        }
    }

    public void ComenzarJuego()
    {
        panelInicial.SetActive(false);
        botonLlenar.SetActive(true);
    }
    public void ComenzarLlenado()
    {
        llenando = true;      
    }

    public void DetenerLlenado()
    {
        llenando = false;
        finLlenado = true;
        chorroAgua.SetActive(false);
        panelFinal.SetActive(true);
        aguaTexto.text = aguaGanada.ToString();
        

        if (altura >= referencia - 0.1 && altura <= referencia + 0.1)
        {
            aguaTexto.text = "¡GUAU, EN EL BLANCO!\nHAS GANADO 2 USOS DE AGUA";
        }
        else if((altura >= 0.55 && altura < referencia - 0.1) || altura > referencia + 0.1)
        {
            aguaTexto.text = "NO ESTÁ MAL, ALGO ES ALGO\nHAS GANADO 1 USO DE AGUA";
        }
        else
        {
            aguaTexto.text = "VAYA... TE HAS QUEDADO CORTO\nESTA VEZ NO GANAS NADA :(";
        }      
    }
}
