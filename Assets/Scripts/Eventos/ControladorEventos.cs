 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class ControladorEventos : MonoBehaviour
{
    public static ControladorEventos instance;

    [SerializeField] private GameObject panelFinal;
    [SerializeField] private GameObject panel;

    [SerializeField] private TextMeshProUGUI _nombreEvento;
    [SerializeField] private TextMeshProUGUI _eventoTxt;
    [SerializeField] private TextMeshProUGUI _resultadoTxt;

    [SerializeField] private TextMeshProUGUI _opcion1;
    [SerializeField] private TextMeshProUGUI _opcion2;
    [SerializeField] private TextMeshProUGUI _opcion3;

    [SerializeField] private List<Image> _imagenes = new List<Image>();

    private List<Evento> eventos;
    private int seleccionado;

    private void Awake()
    {
        instance = this;

        ViajeroEvento evento0 = new ViajeroEvento(null);
        //PiedraMalditaEvento evento1 = new PiedraMalditaEvento(null);

        eventos = new List<Evento>() { evento0 };
    }

    private void Start()
    {
        seleccionado = Random.Range(0, eventos.Count);

        _nombreEvento.text = eventos[seleccionado]._nombre.ToString();
        _eventoTxt.text = eventos[seleccionado]._eventoTxt.ToString();

        _opcion1.text = eventos[seleccionado]._opcionesList[0].ToString();
        _opcion2.text = eventos[seleccionado]._opcionesList[1].ToString();

        try { _opcion3.text = eventos[seleccionado]._opcionesList[2].ToString(); }
        catch { _opcion3.text = "no hay opcion en este botoncito"; } //En vez de este texto deshabilitar el boton
    }

    public void Option1()
    {
        eventos[0].Option1();
    }

    public void Option2()
    {
        eventos[0].Option2();
    }

    public void Option3()
    {
        eventos[0].Option3();
    }

    public void SaltarEvento()
    {
        panelFinal.SetActive(true);
        panel.SetActive(false);
        _resultadoTxt.text = "TE HAS SALTADO EL EVENTO A CAMBIO DE 30 DE ORO.";
        LevelManager.instance.gold -= 30;
    }

    public void Salir()
    {
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.EventScene);
        LevelManager.instance.ActivateScene();
    }
}
