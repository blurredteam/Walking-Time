using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CristalEvento : Evento
{
    private List<Character> team = LevelManager.instance._team;
    public CristalEvento(Image imagenEvento)
    {
        _nombre = "Cristales del futuro";
        index = 4;
        _eventImage = imagenEvento;
        _eventoTxt = "En la cueva aparecen unos extraños brillos de colores reflejados en la pared." +
            " Al acercaros veis que son cristales de vivos colores, azul, rojo, amarillo… y que parpadean," +
            "como instando a que los toques. Decides tocar los cristales pero… ¿En qué orden?";

        _opcionesList.Add("Amarillo primero.");
        _opcionesList.Add("Azul primero.");
        _opcionesList.Add("Rojo primero.");
        _opcionesList.Add("Ignorar los cristales");

        _resultadosList.Add("Al tocar el cristal amarillo, sientes cómo algo fluye dentro de tí, concretamente " +
            "de tu bolsa de monedas. De repente te mareas y caes al suelo inconsciente. " +
            "Cuando el equipo te reanima miras tu bolsa y ¡hay más monedas que antes! " +
            "Por lo menos el mal trago ha merecido la pena.\n[+30 de oro, -15 de energía]");
        _resultadosList.Add("Cuando tocas el cristal azul un cansancio enorme se apodera de ti, " +
            "mientras observas cómo tu cantimplora comienza a rellenarse sola. Cuando se rellena, " +
            "sientes que el malestar acaba y valoras cómo la cantimplora está más llena que antes, " +
            "quizá la necesites en un futuro.\n[+1 de agua, -40 energía]");
        _resultadosList.Add("Tocas primero el cristal rojo. De repente un poder que nunca habías sentido se " +
            "apodera de ti y sientes cómo podrías llevar en peso a todo tu equipo si fuera necesario.Mientras " +
            "disfrutas de tu estado eufórico te das cuenta de que tu bolsa de oro se encuentra más vacía. " +
            "¿Es cosa de esta fuerza repentina que pesa tan poco o hay menos oro que antes?\n[+15 de energía, -30 de oro]");
        _resultadosList.Add("Muy bonitos pero sospechosos... decidis no tocar ningún cristal y seguir adelante, " +
            $"{team[2]} deseaba mucho conseguir uno de esos cristales, sigue el camino desanimado [-10 energía]");
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
        if(LevelManager.instance.gold > 15)
        {
            LevelManager.instance.teamEnergy += 30;
            LevelManager.instance.gold -= 15;

            ControladorEventos.instance._resultadoTxt.text = _resultadosList[2].ToString();
            FinalizarEvento();
        }
    }
    public override void Option4()
    {
        LevelManager.instance.teamEnergy -= 15;
        ControladorEventos.instance._resultadoTxt.text = _resultadosList[3].ToString();
        FinalizarEvento();
    }
}
