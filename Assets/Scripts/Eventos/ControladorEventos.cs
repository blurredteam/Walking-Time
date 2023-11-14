using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControladorEventos : MonoBehaviour
{
    public static ControladorEventos instance;

    [SerializeField] public GameObject panelFinal;
    [SerializeField] public GameObject panel;

    [SerializeField] private TextMeshProUGUI _nombreEvento;
    [SerializeField] private TextMeshProUGUI _eventoTxt;
    [SerializeField] public TextMeshProUGUI _resultadoTxt;

    [SerializeField] private TextMeshProUGUI _opcion1;
    [SerializeField] private TextMeshProUGUI _opcion2;
    [SerializeField] private TextMeshProUGUI _opcion3;

    [SerializeField] private List<Image> _imagenes = new List<Image>();

    private List<Evento> eventos;
    private int seleccionado;       

    private void Awake()
    {
        instance = this;

        //1. Se crean los eventos uno a uno y se añaden a la lista de eventos
        ViajeroEvento evento0 = new ViajeroEvento(null);
        PiedraMalditaEvento evento1 = new PiedraMalditaEvento(null);
        FinitoEvento evento2 = new FinitoEvento(null);
        PozoEvento evento3 = new PozoEvento(null);
        CristalEvento evento4 = new CristalEvento(null);

        eventos = new List<Evento>() { evento0, evento1, evento2, evento3, evento4 };
    }

    private void Start()
    {
        //2. Se selecciona un evento aleatorio
        seleccionado = Random.Range(0, eventos.Count);

        //3. Se asignan la informacion del evento
        _nombreEvento.text = eventos[seleccionado]._nombre.ToString();
        _eventoTxt.text = eventos[seleccionado]._eventoTxt.ToString();

        _opcion1.text = eventos[seleccionado]._opcionesList[0].ToString();
        _opcion2.text = eventos[seleccionado]._opcionesList[1].ToString();
        try { _opcion3.text = eventos[seleccionado]._opcionesList[2].ToString(); }
        catch { _opcion3.GetComponentInParent<Button>().gameObject.SetActive(false); } 
    }

    // En funcion de que opcion se elija 
    public void Option1()
    {
        eventos[seleccionado].Option1();
    }

    public void Option2()
    {
        eventos[seleccionado].Option2();
    }

    public void Option3()
    {
        eventos[seleccionado].Option3();
    }

    public void SaltarEvento()
    {
        panelFinal.SetActive(true);
        panel.SetActive(false);
        _resultadoTxt.text = "TE HAS SALTADO EL EVENTO A CAMBIO DE 30 DE ORO.";
        if(LevelManager.instance.gold > 30) LevelManager.instance.gold -= 30;
    }

    public void Salir()
    {
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.EventScene);
        LevelManager.instance.ActivateScene();
    }
}
