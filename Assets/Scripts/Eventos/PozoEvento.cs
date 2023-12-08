using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PozoEvento : Evento
{
    public PozoEvento(Image imagenEvento)
    {
        _nombre = "Pozo subterr�neo";
        index = 3;
        _eventImage = imagenEvento;
        _eventoTxt = "Cuando and�is, se escucha un ruido de agua desliz�ndose por la piedra, " +
            "al rebuscar un poco m�s� �Encontr�is un pozo tras una fina grieta!";

        _opcionesList.Add("1. Coger agua de la grieta.");
        _opcionesList.Add("2. [Forzudo] Le pides a Berenjeno que reviente la pared.");
        _opcionesList.Add("3. Pides un deseo y �tiras un poco de oro a la grieta?");

        _resultadosList.Add("1. La grieta es muy estrecha y s�lo os deja meter un brazo dentro por lo que s�lo pod�is " +
            "rellenar un uso de la cantimplora. Continu�is vuestro camino con la cantimplora un poco m�s llena.\n[+1 Agua]");
        _resultadosList.Add("2. Le explicas a Berenjeno lo que tiene que hacer, este, al ver que hay un manantial de agua" +
            " no se lo piensa dos veces, de un simple pu�etazo la grieta se ensancha y aprovech�is para rellenar" +
            " la cantimplora entera. Berenjeno est� muy contento de servir al grupo y de poder darse una ducha.\n[+2 agua]");
        _resultadosList.Add("3. Aunque el equipo te mira algo raro propones tirar unas monedas y pedir un deseo. " +
            "Al hacerlo ves que� �No pasa nada? Pues claro, es una grieta no un pozo de los deseos, qu� iba a pasar." +
            " Os march�is cabizbajos tras haber perdido dinero de forma tonta.\n[-10 de oro]");
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
