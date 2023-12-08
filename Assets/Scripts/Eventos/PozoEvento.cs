using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PozoEvento : Evento
{
    public PozoEvento(Image imagenEvento)
    {
        _nombre = "Pozo subterráneo";
        index = 3;
        _eventImage = imagenEvento;
        _eventoTxt = "Cuando andáis, se escucha un ruido de agua deslizándose por la piedra, " +
            "al rebuscar un poco más… ¡Encontráis un pozo tras una fina grieta!";

        _opcionesList.Add("1. Coger agua de la grieta.");
        _opcionesList.Add("2. [Forzudo] Le pides a Berenjeno que reviente la pared.");
        _opcionesList.Add("3. Pides un deseo y ¿tiras un poco de oro a la grieta?");

        _resultadosList.Add("1. La grieta es muy estrecha y sólo os deja meter un brazo dentro por lo que sólo podéis " +
            "rellenar un uso de la cantimplora. Continuáis vuestro camino con la cantimplora un poco más llena.\n[+1 Agua]");
        _resultadosList.Add("2. Le explicas a Berenjeno lo que tiene que hacer, este, al ver que hay un manantial de agua" +
            " no se lo piensa dos veces, de un simple puñetazo la grieta se ensancha y aprovecháis para rellenar" +
            " la cantimplora entera. Berenjeno está muy contento de servir al grupo y de poder darse una ducha.\n[+2 agua]");
        _resultadosList.Add("3. Aunque el equipo te mira algo raro propones tirar unas monedas y pedir un deseo. " +
            "Al hacerlo ves que… ¿No pasa nada? Pues claro, es una grieta no un pozo de los deseos, qué iba a pasar." +
            " Os marcháis cabizbajos tras haber perdido dinero de forma tonta.\n[-10 de oro]");
    }

    public override void Option1()
    {
        LevelManager.instance.teamWater++;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[0].ToString();
        FinalizarEvento();
    }
    public override void Option2()
    {
        var team = LevelManager.instance._team;
        foreach(Character c in team)
        {
            if(c.name == "Berenjeno")
            {
                LevelManager.instance.teamWater += 2;
                ControladorEventos.instance._resultadoTxt.text = _resultadosList[1].ToString();
                FinalizarEvento();
                return;
            }
        }
    }
    public override void Option3()
    {
        if (LevelManager.instance.gold >= 10)
        {
            LevelManager.instance.gold -= 10;
            ControladorEventos.instance._resultadoTxt.text = _resultadosList[2].ToString();
            FinalizarEvento();
        }

    }
}
