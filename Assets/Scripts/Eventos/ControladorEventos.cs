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

    [SerializeField] private TextMeshProUGUI _nombreEvento;
    [SerializeField] public TextMeshProUGUI _eventoTxt;
    [SerializeField] public TextMeshProUGUI _resultadoTxt;

    [SerializeField] public TextMeshProUGUI _opcion1;
    [SerializeField] public TextMeshProUGUI _opcion2;
    [SerializeField] public TextMeshProUGUI _opcion3; 
    [SerializeField] public TextMeshProUGUI _opcion4;

    [SerializeField] private List<Image> _eventImages;
    [SerializeField] private List<Image> _eventObjects;

    private List<Evento> eventos;
    private int seleccionado;       

    private static int previousSelected;

    private void Awake()
    {
        instance = this;

        //1. Se crean los eventos uno a uno y se añaden a la lista de eventos
        ViajeroEvento evento0 = new ViajeroEvento(null);
        PiedraMalditaEvento evento1 = new PiedraMalditaEvento(null, _eventObjects[0]);
        FinitoEvento evento2 = new FinitoEvento(null);
        PozoEvento evento3 = new PozoEvento(null);
        CristalEvento evento4 = new CristalEvento(null);
        LamentoEvento evento5 = new LamentoEvento(null, _eventObjects[1]);
        BotellaEvento evento6 = new BotellaEvento(null, _eventObjects[2]);
        LucierEvento evento7 = new LucierEvento(null);
        TopoEvento evento8 = new TopoEvento(null, _eventObjects[3]);

        eventos = new List<Evento>() { evento0, evento1, evento2, evento3, evento4, evento5, evento6, evento7, evento8 };
        
        //1.1 Se quitan los eventos que ya no se pueden jugar
        var removedEvents = LevelManager.instance.removedEvents;
        for(int i = 0; i < removedEvents.Count; i++)
        {
            for(int j = 0; j < eventos.Count; j++)
            {
                if (removedEvents[i] == null || eventos[j] == null) break;
                if (removedEvents[i]._nombre == eventos[j]._nombre) eventos.Remove(eventos[j]);
            }
        }
    }

    public void RemoveEvent(Evento e, Image eventObecjt)
    {
        LevelManager.instance.AddObject(e, eventObecjt);
    }

    private void Start()
    {
        //2. Se selecciona un evento aleatorio
        seleccionado = Random.Range(0, eventos.Count);
        while(seleccionado == previousSelected) seleccionado = Random.Range(0, eventos.Count);
        previousSelected = seleccionado;

        //3. Se asignan la informacion del evento
        _nombreEvento.text = eventos[seleccionado]._nombre.ToString();
        _eventoTxt.text = eventos[seleccionado]._eventoTxt.ToString();

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

    //public void SaltarEvento()
    //{
    //    AudioManager.instance.ButtonSound();
    //    if (LevelManager.instance.gold >= 20) 
    //    {
    //        LevelManager.instance.gold -= 20;
    //        panelFinal.SetActive(true);
    //        panel.SetActive(false);
    //        _resultadoTxt.text = "TE HAS SALTADO EL EVENTO A CAMBIO DE 20 DE ORO.";
    //    }
    //}

    public void Salir()
    {
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.EventScene);
        LevelManager.instance.ActivateScene();
    }
}
