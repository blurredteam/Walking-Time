using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Evento : MonoBehaviour
{
    public string _nombre;
    public Image _eventImage;
    public string _eventoTxt;
    public List<string> _opcionesList = new List<string>();
    public List<string> _resultadosList = new List<string>();

    public virtual void Option1() { }
    public virtual void Option2() { }
    public virtual void Option3() { }

}
