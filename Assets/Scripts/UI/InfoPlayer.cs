using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InfoPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPanelNombre;
    [SerializeField] private TextMeshProUGUI textPanelEdad;
    [SerializeField] private TextMeshProUGUI textPanelSexo;

    [SerializeField] private List<GameObject> _paneles = new List<GameObject>();
    [SerializeField] private List<GameObject> _botones = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI textoEdad;
    
    [SerializeField] private Transitioner transition;
    [SerializeField] private float transitionTime = 1f;
    //[SerializeField] private TextMeshProUGUI salidaSexo;
    // Start is called before the first frame update
    private void Awake()
    {
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
        AudioManager.instance.ButtonSound3();
        Debug.Log(nombre);
        GameManager.instance.nombreJugador = nombre;
        textPanelNombre.text = "Vaya vaya, as� que te llamas " + nombre + ", no te pega mucho con la cara la verdad, te pegar�a m�s un nombre como... Hmmm no s�... �Finito quiz�?, espl�ndido. Bueno, espera, as� ya me llamo yo JAJAJAJAJAJAJAJA.";
        _botones[0].SetActive(true);
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
        textPanelSexo.text = "Has visto, mi poder es tal que puedo atravesar hasta la cuarta pared AJAJAJAJAJAJAJAJA.\nMe retiro, pero estar� vigil�ndote " + GameManager.instance.nombreJugador + ", quiz� nos encontremos por ah�...\nJAJAJAJAJAJAJAJAJAJA";
       _botones[2].SetActive(true);
        AudioManager.instance.RisaFinito();
    }
}
