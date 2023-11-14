using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PozoEvento : Evento
{
    public PozoEvento(Image imagenEvento)
    {
        _nombre = "Pozo subterr�neo";
        _eventImage = imagenEvento;
        _eventoTxt = "Cuando and�is, se escucha un ruido de agua desliz�ndose\r\npor la piedra, al rebuscar un poco m�s� �Encontr�is un\r\npozo tras una fina grieta!\r\n";

        _opcionesList.Add("1. Coger agua de la grieta [Rellenar un uso de la\r\ncantimplora.");
        _opcionesList.Add("2. [Solo disponible si ten�is un personaje con la\r\nhabilidad �Forzudo�] Le pides a Berenjeno que\r\nreviente la pared. [Rellen�is todos los usos de la\r\ncantimplora]\r\n");
        _opcionesList.Add("3. Pides un deseo y �tiras un poco de oro a la grieta?\r\n[-15 de oro]\r\n");

        _resultadosList.Add("1. La grieta es muy estrecha y s�lo os deja meter un brazo\r\ndentro por lo que s�lo pod�is rellenar un uso de la\r\ncantimplora. Continu�is vuestro camino con la cantimplora\r\nun poco m�s llena.\r\n");
        _resultadosList.Add("2. Le explicas a Berenjeno lo que tiene que hacer, este, al\r\nver que hay un manantial de agua no se lo piensa dos\r\nveces, de un simple pu�etazo la grieta se ensancha y\r\naprovech�is para rellenar la cantimplora entera. Berenjeno\r\nest� muy contento de servir al grupo y de poder darse una\r\nducha.\r\n");
        _resultadosList.Add("3. Aunque el equipo te mira algo raro propones tirar unas\r\nmonedas y pedir un deseo. Al hacerlo ves que� �No pasa\r\nnada? Pues claro, es una grieta no un pozo de los deseos,\r\nqu� iba a pasar. Os march�is cabizbajos tras haber perdido\r\ndinero de forma tonta.\r\n");
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
                LevelManager.instance.teamWater = LevelManager.instance.maxWater;
                ControladorEventos.instance._resultadoTxt.text = _resultadosList[1].ToString();
                FinalizarEvento();
                return;
            }
        }

        Debug.Log("No tienes al beren");
        //Hacer algo que indique que puedes elegir esa opcion
        //Poner el boton en rojo o algo asi
    }
    public override void Option3()
    {
        if (LevelManager.instance.gold >= 15)
        {
            LevelManager.instance.gold -= 15;
            ControladorEventos.instance._resultadoTxt.text = _resultadosList[2].ToString();
            FinalizarEvento();
        }

    }
}
