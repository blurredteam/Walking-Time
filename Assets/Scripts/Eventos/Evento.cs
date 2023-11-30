using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Evento
{
    public string _nombre;
    public int index;
    public Image _eventImage;
    public string _eventoTxt;
    public List<string> _opcionesList = new List<string>();
    public List<string> _resultadosList = new List<string>();
    public string _avisoQuitarObj;

    public virtual void Option1() { }
    public virtual void Option2() { }
    public virtual void Option3() { }

    public virtual void RemoveEventoObj() { }   //Se usa solo en hogueras, quita el objeto con consecuencias

    public void FinalizarEvento() 
    {
        ControladorEventos.instance.panelFinal.SetActive(true);
        ControladorEventos.instance.panel.SetActive(false);
    }

}
