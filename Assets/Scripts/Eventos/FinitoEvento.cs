using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinitoEvento : Evento
{
    public FinitoEvento(Image imagenEvento)
    {
        _nombre = "Extraño disfrazado";
        _eventImage = imagenEvento;
        _eventoTxt = "Mientras andáis un ruido extraño retumba por la caverna," +
            "\r\nparece una especie de risa…\r\n–JA JA JA JA JA\r\nUn alargado hombre se descuelga del techo y boca abajo," +
            "\r\nos corta el camino.\r\n–Soy el Sr.Finito, que vive en estas cuevas llenas de piedras" +
            "\r\npreciosas. Como habéis decidido pasar por mi cueva tenéis\r\nque pagar…¡una multa por venir sin avisar!" +
            "\r\nMirando al equipo, te dispones a coger el bolsón de oro del" +
            "\r\ncinturón, al misterioso hombre le brillan los ojos al ver ese\r\nmovimiento. En ese momento…\r\n";

        _opcionesList.Add("1. Decides coger la bolsa y pagar, temes lo que Finito\r\npueda hacer si se enfada. [-Todo tu oro]");
        _opcionesList.Add("2. Coges todas vuestras cosas y empezáis a correr a lo\r\nloco. [-50 de energía]\r\n");

        _resultadosList.Add("1. Finito sonríe al ver las monedas de oro salir de la bolsa.\r\n– Mil gracias aventureros, ¡que lo paséis bien paseando por\r\nmis dominios! JA JA JA JA JA\r\nFinito vuelve a la oscuridad del techo de la cueva y ves\r\ncómo sus ojos brillantes desaparecen poco a poco en la\r\noscuridad. Os sentís un poco estafados pero continuáis\r\nvuestro camino mientras se sigue escuchando el eco de la\r\nrisa de Finito.\r\n");
        _resultadosList.Add("2. En un movimiento rápido, ¡recoges tu bolsa de oro y\r\ncomenzáis a correr a toda velocidad por la caverna!\r\nAunque Finito parece que no os persigue, escucháis su voz\r\nretumbar por la cueva.\r\n–JA JA JA JA JA, ¡ME DEBÉIS PAGAR!\r\nEl sprint para huir de aquella situación os ha cansado un\r\npoco pero parece que podéis continuar. Te preguntas si\r\nencontraréis a Finito otra vez antes de acabar vuestra\r\naventura.");
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
