using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViajeroEvento : Evento
{
    public ViajeroEvento(Image imagenEvento)
    {
        _nombre = "Viajero extraviado";
        index = 0;
        _eventImage = imagenEvento;
        _eventoTxt = "Durante tu aventura os encontráis a un hombre tumbado en el suelo, muy debilitado. " +
            "Cuándo os acercáis el hombre habla tosiendo y con una voz grave:\r\n– Agua, por favor… Agua.";
        
        _opcionesList.Add("[-1 agua] Darle agua.");
        _opcionesList.Add("Robarle y dejarlo ahí");
        _opcionesList.Add("Ignorarle, vuestros recursos son limitados...");
        _opcionesList.Add("[-15 oro] Darle oro");

        _resultadosList.Add("Te acercas al hombre y le das agua de tu cantimplora.\r\n–¡Oh, mil gracias! " +
            "Tomad un detalle por vuestra ayuda.\r\n El hombre os da una bolsa con 20 de oro y seguís vuestra aventura. \n[+20 de oro]");
        _resultadosList.Add("Te acercas al hombre, aún tumbado, para ver si podéis conseguir algo de valor. " +
            "Encuentras unas monedas, pero el hombre se retuerce cuando las intentas coger, haciéndote caer al suelo." +
            " Te levantas magullado y os vais, siguiendo con vuestra aventura.\n[+10 de oro, - 50 energía]");
        _resultadosList.Add("Ignorais al hombre y seguis el sendero, no todos estan de acuerdo con esta decisión, " +
            "pero sabes que vuestros recursos son limitados. El abondono del hombre deja una mala sensación sobre " +
            "los miembros del equipo... [-25 energía]");
        _resultadosList.Add("Te acercas al hombre y le das algo de oro. No es exactamente lo que necesita " +
            "pero esperas que pase la TOPOTIENDA pronto y pueda comprarse algo de agua. Seguis por el sendero...");
    }

    public override void Option1()
    {
        if(LevelManager.instance.teamWater == 0) return;

        LevelManager.instance.teamWater--;
        LevelManager.instance.gold += 20;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[0].ToString();
        FinalizarEvento();
    }
    public override void Option2()
    {
        LevelManager.instance.gold += 10;
        LevelManager.instance.teamEnergy -= 50;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[1].ToString();
        FinalizarEvento();
    }

    public override void Option3()
    {
        LevelManager.instance.teamEnergy -= 25;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[2].ToString();
        FinalizarEvento();
    }

    public override void Option4()
    {
        if (LevelManager.instance.gold < 15) return;
 
        LevelManager.instance.gold -= 15;
        ControladorEventos.instance._resultadoTxt.text = _resultadosList[3].ToString();
        FinalizarEvento();
    }
}
