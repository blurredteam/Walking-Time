using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinitoEvento : Evento
{
    public FinitoEvento(Image imagenEvento)
    {
        _nombre = "Extraño disfrazado";
        index = 2;
        _eventImage = imagenEvento;
        _eventoTxt = "Durante el camino un ruido extraño retumba por la caverna," +
            "parece una especie de risa…\r\n– JA JA JA JA JA\r\nUna alargada criatura se balancea desde el techo boca abajo, " +
            "bloqueando el sendero.\r\n– Soy el Sr.Finito, coleccionador de imposibles. " +
            "Como habéis decidido pasar por mi cueva tenéis que pagar… ¡una multa por venir sin avisar!\r\n" +
            "Mirando al equipo, te aferras al bolsón de oro de tu cinturón. En ese momento…";

        _opcionesList.Add("[-todo tu oro] Decides coger la bolsa y pagar, temes lo que Finito pueda hacer si se enfada.");
        _opcionesList.Add("Coges todas vuestras cosas y empezáis a correr a lo loco.");

        _resultadosList.Add("1. Finito sonríe al ver las monedas de oro salir de la bolsa." +
            "\r\n– Mil gracias aventureros, ¡que lo paséis bien paseando por mis dominios! JA JA JA JA JA\r\n" +
            "Finito vuelve a la oscuridad del techo de la cueva y ves cómo sus ojos brillantes desaparecen " +
            "poco a poco en la oscuridad. Os sentís un poco estafados pero continuáis vuestro camino " +
            "mientras se sigue escuchando el eco de la risa de Finito.");
        _resultadosList.Add("2. En un movimiento rápido, ¡recoges tu bolsa de oro y comenzáis a correr" +
            " a toda velocidad por la caverna! Aunque Finito parece que no os persigue, escucháis su " +
            "voz retumbar por la cueva.\r\n–JA JA JA JA JA, ¡ME DEBÉIS PAGAR!\r\n" +
            "El sprint para huir de aquella situación os ha cansado un poco pero parece que podéis continuar. " +
            "Te preguntas si encontraréis a Finito otra vez antes de acabar vuestra aventura.\n[-50 de energía]");
    }

    public override void Option1()
    {
        LevelManager.instance.gold = 0;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[0].ToString();
        FinalizarEvento();
    }
    public override void Option2()
    {
        LevelManager.instance.teamEnergy -= 50;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[1].ToString();
        FinalizarEvento();
    }
}
