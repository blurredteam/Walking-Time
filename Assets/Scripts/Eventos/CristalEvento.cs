using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CristalEvento : Evento
{
    public CristalEvento(Image imagenEvento)
    {
        _nombre = "Cristales del futuro";
        _eventImage = imagenEvento;
        _eventoTxt = "En la cueva aparecen unos extraños brillos de colores\r\nreflejados en la pared. Al acercaros veis que son cristales\r\nde vivos colores, azul, rojo, amarillo… y que parpadean,\r\ncomo instando a que los toques. Decides tocar los cristales\r\npero… ¿En qué orden?\r\n";

        _opcionesList.Add("1. Amarillo primero. [+30 de oro, -15 de energía]");
        _opcionesList.Add("2.Azul primero. [+1 de agua, -40 energía]");
        _opcionesList.Add("3. Rojo primero. [+15 de energía, -30 de oro]");

        _resultadosList.Add("1. Al tocar el cristal amarillo, sientes cómo algo fluye dentro\r\nde tí, concretamente de tu bolsa de monedas. De repente te\r\nmareas y caes al suelo inconsciente. Cuando el equipo te\r\nreanima miras tu bolsa y ¡hay más monedas que antes! Por\r\nlo menos el mal trago ha merecido la pena.\r\n");
        _resultadosList.Add("2. Cuando tocas el cristal azul un cansancio enorme se\r\napodera de ti, mientras observas cómo tu cantimplora\r\ncomienza a rellenarse sola. Cuando se rellena, sientes que\r\nel malestar acaba y valoras cómo la cantimplora está más\r\nllena que antes, quizá la necesites en un futuro.");
        _resultadosList.Add("3. Tocas primero el cristal rojo. De repente un poder que\r\nnunca habías sentido se apodera de ti y sientes cómo\r\npodrías llevar en peso a todo tu equipo si fuera necesario.\r\nMientras disfrutas de tu estado eufórico te das cuenta de\r\nque tu bolsa de oro se encuentra más vacía. ¿Es cosa de\r\nesta fuerza repentina que pesa tan poco o hay menos oro\r\nque antes?");
    }

    public override void Option1()
    {
        LevelManager.instance.teamEnergy -= 15;
        LevelManager.instance.gold += 30;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[0].ToString();
        FinalizarEvento();
    }
    public override void Option2()
    {
        LevelManager.instance.teamWater++;
        LevelManager.instance.teamEnergy -= 40;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[1].ToString();
        FinalizarEvento();
    }
    public override void Option3()
    {
        if(LevelManager.instance.gold > 30)
        {
            LevelManager.instance.teamEnergy += 30;
            LevelManager.instance.gold -= 30;

            ControladorEventos.instance._resultadoTxt.text = _resultadosList[2].ToString();
            FinalizarEvento();
        }
    }
}
