using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinitoEvento : Evento
{
    public FinitoEvento(Image imagenEvento)
    {
        _nombre = "Extra�o disfrazado";
        index = 2;
        _eventImage = imagenEvento;
        _eventoTxt = "Durante el camino un ruido extra�o retumba por la caverna," +
            "parece una especie de risa�\r\n� JA JA JA JA JA\r\nUna alargada criatura se balancea desde el techo boca abajo, " +
            "bloqueando el sendero.\r\n� Soy el Sr.Finito, coleccionador de imposibles. " +
            "Como hab�is decidido pasar por mi cueva ten�is que pagar� �una multa por venir sin avisar!\r\n" +
            "Mirando al equipo, te aferras al bols�n de oro de tu cintur�n. En ese momento�";

        _opcionesList.Add("[-todo tu oro] Decides coger la bolsa y pagar, temes lo que Finito pueda hacer si se enfada.");
        _opcionesList.Add("Coges todas vuestras cosas y empez�is a correr a lo loco.");

        _resultadosList.Add("1. Finito sonr�e al ver las monedas de oro salir de la bolsa." +
            "\r\n� Mil gracias aventureros, �que lo pas�is bien paseando por mis dominios! JA JA JA JA JA\r\n" +
            "Finito vuelve a la oscuridad del techo de la cueva y ves c�mo sus ojos brillantes desaparecen " +
            "poco a poco en la oscuridad. Os sent�s un poco estafados pero continu�is vuestro camino " +
            "mientras se sigue escuchando el eco de la risa de Finito.");
        _resultadosList.Add("2. En un movimiento r�pido, �recoges tu bolsa de oro y comenz�is a correr" +
            " a toda velocidad por la caverna! Aunque Finito parece que no os persigue, escuch�is su " +
            "voz retumbar por la cueva.\r\n�JA JA JA JA JA, �ME DEB�IS PAGAR!\r\n" +
            "El sprint para huir de aquella situaci�n os ha cansado un poco pero parece que pod�is continuar. " +
            "Te preguntas si encontrar�is a Finito otra vez antes de acabar vuestra aventura.\n[-50 de energ�a]");
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
