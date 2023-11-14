using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViajeroEvento : Evento
{
    public ViajeroEvento(Image imagenEvento)
    {
        _nombre = "Viajero extraviado";
        _eventImage = imagenEvento;
        _eventoTxt = "Durante tu aventura os encontráis a un hombre tumbado en\r\nel suelo, muy debilitado. Cuándo os acercáis el hombre\r\nhabla tosiendo y con una voz grave:\r\n– Agua, por favor… Agua.";
        
        _opcionesList.Add("1. Darle agua [-1 uso de cantimplora, +100 de oro]");
        _opcionesList.Add("2.Robarle y dejarlo ahí[+25 de oro - 50 energía]");

        _resultadosList.Add("1. Te acercas al hombre y le das agua de tu cantimplora.\r\n–¡Oh, mil gracias! Tomad un detalle por vuestra ayuda.\r\nEl hombre os da una bolsa con 100 de oro y seguís vuestra\r\naventura.\r\n");
        _resultadosList.Add("2. Te acercas al hombre, aún tumbado, para ver si podéis\r\nconseguir algo de valor. Encuentras unas monedas, pero el\r\nhombre se retuerce cuando las intentas coger, haciéndote\r\ncaer al suelo. Te levantas magullado y os vais, siguiendo\r\ncon vuestra aventura.\r\n");
    }

    public override void Option1()
    {
        LevelManager.instance.teamWater--;
        LevelManager.instance.gold += 100;
    }
    public override void Option2()
    {
        LevelManager.instance.gold += 25;
        LevelManager.instance.teamEnergy -= 50;
    }
    public override void Option3()
    {
        Debug.Log("No hace nada");
    }
}
