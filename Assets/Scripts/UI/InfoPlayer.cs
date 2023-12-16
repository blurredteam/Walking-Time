using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InfoPlayer : MonoBehaviour
{
    public static InfoPlayer Instance;
    [SerializeField] private TextMeshProUGUI textPanelNombre;
    [SerializeField] private TextMeshProUGUI textPanelTeclado;
    [SerializeField] private TextMeshProUGUI textPanelEdad;
    [SerializeField] private TextMeshProUGUI textPanelSexo;

    [SerializeField] private List<GameObject> _paneles = new List<GameObject>();
    [SerializeField] private List<GameObject> _botones = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI textoEdad;

    [SerializeField] private TextMeshProUGUI nombreText;
    [SerializeField] private TextMeshProUGUI nombreText2;
    [SerializeField] private TextMeshProUGUI nombreText3;

    [SerializeField] private Transitioner transition;
    [SerializeField] private float transitionTime = 1f;

    [SerializeField] private GameObject panelNormal;
    [SerializeField] private GameObject panelTeclado;
    [SerializeField] private DatabaseManager database;
    //[SerializeField] private TextMeshProUGUI salidaSexo;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        transition = ScenesManager.instance.transitioner;
    }
    // public void Empezar()
    // {
    //     StartCoroutine(DoTransition(0, 0));
    // }

    public void ActPanelEdad()
    {
        StartCoroutine(DoTransition(0, 1));
    }

    public void ActPanelSexo()
    {
        StartCoroutine(DoTransition(1, 2));
    }

    IEnumerator DoTransition(int from, int to)
    {
        transition.DoTransitionOnce();
        AudioManager.instance.ButtonSound();

        yield return new WaitForSeconds(transitionTime);

        transition.DoTransitionOnce();
        _paneles[from].SetActive(false);
        _paneles[to].SetActive(true);
    }

    public void SetEdad(float valor)
    {
        //AudioManager.instance.ButtonSound2();
        int edad = Mathf.FloorToInt(valor);
        textoEdad.text = edad.ToString();
        GameManager.instance.edadJugador = edad;
        textPanelEdad.text = "Interesante. Veremos como de lejos llegas, espero que me sorprendas "+ GameManager.instance.nombreJugador+", que llevo milenios aburrido.";
        _botones[1].SetActive(true);
    }
    public void SetNombre(string nombre)
    {
        if (nombre.Length >= 2)
        {
            Debug.Log(nombre.Length);
            SinTeclado();
            nombreText.text = nombre;
            nombreText2.text = nombre;
            nombreText3.text = nombre;
            AudioManager.instance.ButtonSound3();
            Debug.Log(nombre);
            GameManager.instance.nombreJugador = nombre;
            textPanelNombre.text = "Vaya vaya, así que te llamas " + nombre + ", no te pega mucho con la cara la verdad, te pegaría más un nombre como...\nHmmm no sé...\n¿Finito quizá?, exxxpléndido.\nBueno, espera, así ya me llamo yo\nJAJAJAJAJAJAJAJA.";
            textPanelTeclado.text = "¿No te convence "+nombre+"?\n Normal a mí tampoco me gusta\nAJAJAJAJAJAJJAJA\nPero date prisa tengo otros seres inteligentes que observar por el universo.";
            _botones[0].SetActive(true);
        }
        else
        {
            textPanelNombre.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
            textPanelTeclado.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
        }
    }

    public void SetSexo(int val)
    {
        try
        {
            switch (val)
            {
                case 2:
                    GameManager.instance.sexoJugador = "Masculino";
                    break;
                case 1:
                    GameManager.instance.sexoJugador = "Femenino";
                    break;
                default:
                    // Lanza una excepci�n si el valor no es 0 ni 1
                    throw new ArgumentOutOfRangeException(nameof(val), "El valor debe ser 0 o 1.");
            }
        }
        catch (Exception e)
        {
            // Maneja la excepci�n aqu� (puedes imprimir un mensaje, registrarla, etc.)
            Debug.LogError($"Error al manejar el valor {val}: {e.Message}");
        }
       
        //if (val == 0) { GameManager.instance.sexoJugador = "Masculino"; }
        //if (val == 1) { GameManager.instance.sexoJugador = "Femenino"; }
        textPanelSexo.text = "Has visto, mi poder es tal que puedo atravesar hasta la cuarta pared\nAJAJAJAJAJAJAJAJA.\nMe retiro, pero estaré vigilándote " + GameManager.instance.nombreJugador + ", quizá nos encontremos por ahí...\nJAJAJAJAJAJAJAJAJAJA";
       _botones[2].SetActive(true);
        AudioManager.instance.RisaFinito();
    }
     public void ConTeclado()
     {
        AudioManager.instance.ButtonSound2();
        panelNormal.SetActive(false);
        panelTeclado.SetActive(true);
     }

    public void SinTeclado()
    {
        AudioManager.instance.ButtonSound2();
        panelNormal.SetActive(true);
        panelTeclado.SetActive(false);
    }
    public void SetDatabaseInfo()
    {
        database.CreatePostLogin("pepe",GameManager.instance.nombreJugador,"tt", GameManager.instance.edadJugador,GameManager.instance.sexoJugador);
    }
}
