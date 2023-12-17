using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ControladorEventos : MonoBehaviour
{
    public static ControladorEventos instance;

    [SerializeField] public GameObject panelFinal;
    [SerializeField] public GameObject panel;

    [SerializeField] public GameObject eventoMain;
    [SerializeField] private TextMeshProUGUI _nombreEvento;
    [SerializeField] public TextMeshProUGUI _eventoTxt;
    [SerializeField] public TextMeshProUGUI _opcion1;
    [SerializeField] public TextMeshProUGUI _opcion2;
    [SerializeField] public TextMeshProUGUI _opcion3;
    [SerializeField] public TextMeshProUGUI _opcion4;
    [SerializeField] public Image _eventImg;

    [SerializeField] public GameObject eventoResul;
    [SerializeField] public TextMeshProUGUI _resultadoTxt;

    [SerializeField] private List<Image> _eventImages;
    [SerializeField] private List<Image> _eventObjects;

    private List<Evento> eventos;
    private int seleccionado;       

    private static int previousSelected;

    // --- PLAYER EVAL ---

    private void Awake()
    {
        instance = this;
        GameManager.instance.totalEvents++;

        //1. Se crean los eventos uno a uno y se añaden a la lista de eventos
        ViajeroEvento evento0 = new ViajeroEvento(_eventImages[0]);
        PiedraMalditaEvento evento1 = new PiedraMalditaEvento(_eventImages[1], _eventObjects[0]);
        FinitoEvento evento2 = new FinitoEvento(_eventImages[2]);
        PozoEvento evento3 = new PozoEvento(_eventImages[3]);
        CristalEvento evento4 = new CristalEvento(_eventImages[4]);
        LamentoEvento evento5 = new LamentoEvento(_eventImages[5], _eventObjects[1]);
        BotellaEvento evento6 = new BotellaEvento(_eventImages[6], _eventObjects[2]);
        LucierEvento evento7 = new LucierEvento(_eventImages[7]);
        TopoEvento evento8 = new TopoEvento(_eventImages[8], _eventObjects[3]);

        //eventos = new List<Evento>() { evento0, evento1, evento2, evento3, evento4, evento5, evento6, evento7, evento8 };
        eventos = new List<Evento>() { evento5, evento0 };
    }

    // Asegura que no se repiten dos eventos seguidos y gestiona los eventos eliminados
    private int SelectEvent(int seleccionado)
    {
        while (seleccionado == previousSelected) seleccionado = Random.Range(0, eventos.Count);

        var removedEvents = LevelManager.instance.removedEvents;
        for (int i = 0; i < removedEvents.Count; i++)
        {
            if (removedEvents[i] != null && eventos[seleccionado]._nombre == removedEvents[i]._nombre)
            {
                seleccionado = Random.Range(0, eventos.Count);
                seleccionado = SelectEvent(seleccionado);
            }
        }

        return seleccionado;
    }

    private void Start()
    {
        //2. Se selecciona un evento aleatorio 
        seleccionado = Random.Range(0, eventos.Count);
        seleccionado = SelectEvent(seleccionado);
        previousSelected = seleccionado;

        //3. Se asignan la informacion del evento
        _nombreEvento.text = eventos[seleccionado]._nombre.ToString();
        _eventoTxt.text = eventos[seleccionado]._eventoTxt.ToString();
        _eventImg.sprite = _eventImages[seleccionado].sprite;

        _opcion1.text = eventos[seleccionado]._opcionesList[0].ToString();
        _opcion2.text = eventos[seleccionado]._opcionesList[1].ToString();
        try { _opcion3.text = eventos[seleccionado]._opcionesList[2].ToString(); }
        catch { _opcion3.GetComponentInParent<Button>().gameObject.SetActive(false); }

        try { _opcion4.text = eventos[seleccionado]._opcionesList[3].ToString(); }
        catch { _opcion4.GetComponentInParent<Button>().gameObject.SetActive(false); }
    }

    // En funcion de que opcion se elija 
    public void Option1()
    {
        AudioManager.instance.ButtonSound();
        eventos[seleccionado].Option1();
    }

    public void Option2()
    {
        AudioManager.instance.ButtonSound();
        eventos[seleccionado].Option2();
    }

    public void Option3()
    {
        AudioManager.instance.ButtonSound();
        eventos[seleccionado].Option3();
    }

    public void Option4()
    {
        AudioManager.instance.ButtonSound();
        eventos[seleccionado].Option4();
    }

    public void Salir()
    {
        AudioManager.instance.ButtonSound();
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.EventScene);
        LevelManager.instance.ActivateScene();
    }
}
