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
    private bool finLlenado = false;
    private float tiempo = 0.0f;
    private float altura;
    private Quaternion rotacionInicial;
    public GameObject chorroAgua;
    public GameObject panelInicial;
    public GameObject panelFinal;
    public GameObject botonLlenar;
    private float referencia;//El limite donde se ganar� el m�ximo de agua
    public TextMeshProUGUI aguaTexto;
    private int aguaGanada = 0;

    [SerializeField] private Button volverBtn;
    [SerializeField] private GameObject objetivo;

    [SerializeField] private AudioClip fondo;
    [SerializeField] private AudioClip sonidoAgua;
    
    public Transitioner transition;
    public float transitionTime = 1f;

    private void Awake()
    {
        transition = ScenesManager.instance.transitioner;
    }
    private void Start()
    {
        // TODOS LAS CASILLAS TENDRAN QUE TENER ALGO ASI

        volverBtn.onClick.AddListener(delegate
        {
            StartCoroutine(EsperarYSalir());
        });
        // ---------------------------------------------
        AudioManager.instance.PlayBackMusic(fondo); 
        referencia = objetivo.transform.position.y;//Altura del tri�ngulo rojo
        Debug.Log(referencia);
        rotacionInicial = transform.rotation;
        //Para q sea mas probable valores mas bajos si no suele ser demasiado r�pido
        float probabilidad = Random.value; // Valor aleatorio entre 0 y 1

        if (probabilidad < 0.5f)
        {
            // 50% de probabilidad para el rango de 0.5 a 2
            valorMinimo = 0.5f;
            valorMaximo = 2.0f;
        }
        else if (probabilidad >= 0.5f && probabilidad < 0.7f)
        {
            // 20% de probabilidad de q vaya lento
            valorMinimo = 0.1f;
            valorMaximo = 1.0f;
        }
        else
        {
            // 30% de probabilidad de q vaya rapido
            valorMinimo = 2.0f;
            valorMaximo = 4.0f;
        }

        velocidadLlenado = Random.Range(valorMinimo, valorMaximo);
    }
    
    IEnumerator EsperarYSalir()
    {
        AudioManager.instance.ButtonSound();
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        transition.DoTransitionOnce();
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleFuente);
        LevelManager.instance.ActivateScene();
        AudioManager.instance.PlayAmbient();
    }

    void Update()
    {
        if (llenando && !finLlenado)//Con l�mite de altura
        {
            chorroAgua.SetActive(true);
            // Incrementar la posici�n en el eje Y para elevar el Sprite
            transform.position += Vector3.up * velocidadLlenado * Time.deltaTime;
            altura = transform.position.y+3.6683866f;//A�adimos lo ultimo para a�adir la diferencia de tama�o
            // Agitar lateralmente con rotaci�n
            tiempo += Time.deltaTime;
            float angulo = amplitudAgitacion * Mathf.Sin(frecuenciaAgitacion * tiempo);
            transform.rotation = Quaternion.Euler(0, 0, angulo);
        }
        else
        {
            // Cuando no se est� elevando, restablecer la rotaci�n
            transform.rotation = rotacionInicial;
        }
    }

    public void ComenzarJuego()
    {
        AudioManager.instance.ButtonSound();
        panelInicial.SetActive(false);
        botonLlenar.SetActive(true);
    }
    public void ComenzarLlenado()
    {
        llenando = true;
        AudioManager.instance.PlaySfx(sonidoAgua);
    }

    public void DetenerLlenado()
    {
        AudioManager.instance.StopSfx();
        llenando = false;
        finLlenado = true;
        chorroAgua.SetActive(false);
        panelFinal.SetActive(true);
        aguaTexto.text = aguaGanada.ToString();
        //Debug.Log(altura);

        if (altura >= referencia - 0.1f && altura <= referencia + 0.1f)
        {
            aguaTexto.text = "GUAU, EN EL BLANCO!\nHAS GANADO 2 USOS DE AGUA";
            AudioManager.instance.WinMusic();
            Recompensas(2);
        }
        else if ((altura >= -2.0f && altura < referencia - 0.1f) || altura > referencia + 0.1f)
        {
            AudioManager.instance.KindaLoseMusic();
            aguaTexto.text = "NO ESTA MAL, ALGO ES ALGO\nHAS GANADO 1 USO DE AGUA";
            Recompensas(1);
        }
        else
        {
            AudioManager.instance.LoseMusic();
            aguaTexto.text = "VAYA... TE HAS QUEDADO CORTO\nESTA VEZ NO GANAS NADA :(";
        }
    }

    private void Recompensas(int cantAguaGanada)
    {
        int maxAgua = LevelManager.instance.maxWater;
        int agua = LevelManager.instance.teamWater;

        if ((cantAguaGanada + agua) >= maxAgua)
        {
            LevelManager.instance.teamWater = maxAgua;
        }
        else
        {
            LevelManager.instance.teamWater += cantAguaGanada;
        }
    }
    
    
}
