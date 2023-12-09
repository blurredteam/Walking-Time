using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlAjustes : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] TMP_Dropdown opcionesResolucion;
    Resolution[] resoluciones;
   

    //Brillo
    [SerializeField] private Slider slider;
    [SerializeField] private float valorSlider;
    [SerializeField] private Image panelBrillo;

    //private void Awake()
    //{
       
    //    DontDestroyOnLoad(gameObject);
    //}
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, 1 - slider.value);

        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        RevisarResolucion();
    }

    public void ActivarPantallaCompeta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
    private void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        opcionesResolucion.ClearOptions();
        List<string> opc = new List<string>();
        int resolucionActual = 0;
        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opc.Add(opcion);

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            { resolucionActual = i; }
        }

        opcionesResolucion.AddOptions(opc);
        opcionesResolucion.value = resolucionActual;
        opcionesResolucion.RefreshShownValue();
        opcionesResolucion.value = PlayerPrefs.GetInt("numeroResolucion", 0);

    }
    public void CambiarResolucion(int id)
    {
        PlayerPrefs.GetInt("numeroResolucion", opcionesResolucion.value);
        Resolution resolucion = resoluciones[id];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }
    public void CambiarBrillo(float valor)
    {
        valorSlider = valor;
        PlayerPrefs.SetFloat("brillo", slider.value);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, 1 - slider.value);
    }
}
