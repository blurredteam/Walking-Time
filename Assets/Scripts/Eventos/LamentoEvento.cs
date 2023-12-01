using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LamentoEvento : Evento
{
    public LamentoEvento(Image imagenEvento, Image objectIcon)
    {
        _nombre = "Roca lamentosa";
        index = 5;
        _eventImage = imagenEvento;
        this.objectIcon = objectIcon;
        objectDescription = "[Jardinera experta] \n[+1 max agua, +10 coste de viajar]";
        _avisoQuitarObj = "Por fin en un lugar seguro, contemplais la opción de dejar a la jardinera aqui. Perdereis" +
            " su compañia, pero os prometió recompensaron con unos deliciosos calabacines.";
        _eventoTxt = "Avanzais por la cueva siguiendo las marcas dejados por otros aventureros, " +
            "en una de las cavernas por las que pasais, escuchais un murmullo, casi un lamento, " +
            "proveniente de detras de una piedra...";

        _opcionesList.Add("1.[Forzudo] Pedir a Berenjeno que destroce la roca.");
        _opcionesList.Add("2.[-70 energía] Mover la piedra entre todos.");
        _opcionesList.Add("3.Ignorar el sonido y seguir con el camino.");

        _resultadosList.Add("1. Al tocar el cristal amarillo, sientes cómo algo fluye dentro de tí, concretamente " +
            "de tu bolsa de monedas. De repente te mareas y caes al suelo inconsciente. " +
            "Cuando el equipo te reanima miras tu bolsa y ¡hay más monedas que antes! " +
            "Por lo menos el mal trago ha merecido la pena.\n[+30 de oro, -15 de energía]");
        _resultadosList.Add("2. Cuando tocas el cristal azul un cansancio enorme se apodera de ti, " +
            "mientras observas cómo tu cantimplora comienza a rellenarse sola. Cuando se rellena, " +
            "sientes que el malestar acaba y valoras cómo la cantimplora está más llena que antes, " +
            "quizá la necesites en un futuro.\n[+1 de agua, -40 energía]");
        _resultadosList.Add("3. Estaís un rato pensado que hacer, no teneis ni el tiempo ni los recursos para " +
            "desenterrar el misterio tras la pesada roca, decidís no darle importancia y seguir con vuestro camino. " +
            "El misterio no resuelto deja una mala sensación en el equipo. [-20 energía]");
    }

    private bool part2 = false;
    private string eventoTxt2 = "Una vez habeis apartado la roca del medio, veis de donde proviene el misterioso" +
        " sonido, una aventurera herida trataba de pedir ayuda. Su cara esboza una pequeña sonrisa" +
        " al ver vuestro fantástico grupo, aliviada de recibir al fin ayuda...";

    private string p2res1 = "Dr. Jáparo se pone manos a la obra, sus 7 años de estudios no son moco de pavo, en un" +
        " pis pas la aventurera esta sanada y con fuerzas para seguir el camino. Extremadamente agradecida, " +
        "la aventurera os cuenta que es jardinera profesional, experta en calabacines, y se niega a dejaros ir sin" +
        " daros algo a cambio. [+30 oro ,max agua] ";
    private string p2res2 = "No teneis los conocimientos médicos suficientes para curarla, sin embargo, fieles al " +
        "código de la aventurería, decidís hacer todo lo posible y le dais una gran parte de vuestra agua. Aunque" +
        " todavía debil, la aventurera recupera buena parte de sus fuerzas, las suficientes para el camino de vuelta. " +
        "Agradecida por salvarle la vida, os regala unos exquisitos calabacines [+50 energía, +10 oro]";
    private string p2res3 = "No podeís abandonar a una persona en apuros, decidís cargarla hasta un lugar más seguro." +
        " Cargar con ella os costará mas energía, sin embargo, una vez recupera la conciencia descubrís que es una " +
        "jardinera profesional, experta en calabacines, con bastos conocimientos sobre la humedad de la cueva. " +
        "[-20 energía, +1 max agua, +10 coste de viajar]";

    public override void Option1()
    {
        var team = LevelManager.instance._team;

        if (!part2)
        {
            foreach (Character c in team)
            {
                if (c.name == "Berenjeno" )
                {
                    part2 = true;
                    ControladorEventos.instance._eventoTxt.text = eventoTxt2;
                    SetPart2();
                    return;
                }
            }
            return;
        }

        foreach (Character c in team)
        {
            if (c.name == "Dr. Japaro" && LevelManager.instance.teamWater > 0)
            {
                LevelManager.instance.gold += 30;
                LevelManager.instance.teamWater = LevelManager.instance.maxWater;
                ControladorEventos.instance._resultadoTxt.text = p2res1;
                FinalizarEvento();
            }
        }
    }

    public override void Option2()
    {
        if (!part2)
        {
            part2 = true;
            LevelManager.instance.teamEnergy -= 70;
            ControladorEventos.instance._eventoTxt.text = eventoTxt2;
            SetPart2();
            return;
        }
        
        if(LevelManager.instance.teamWater > 1)
        {
            LevelManager.instance.teamWater -= 2;
            LevelManager.instance.teamEnergy += 50;
            LevelManager.instance.gold += 15;
            ControladorEventos.instance._resultadoTxt.text = p2res2;
            FinalizarEvento();
        }
    }

    public override void Option3()
    {
        if (!part2)
        {
            LevelManager.instance.teamEnergy -= 20;
            ControladorEventos.instance._resultadoTxt.text = _resultadosList[2].ToString();
            FinalizarEvento();
            return;
        }

        ControladorEventos.instance.RemoveEvent(this, objectIcon);
        LevelManager.instance.teamEnergy -= 20;
        LevelManager.instance.maxWater++;
        LevelManager.instance.travelCostModifier += 10;
        ControladorEventos.instance._resultadoTxt.text = p2res3;
        FinalizarEvento();
    }

    private void SetPart2()
    {
        ControladorEventos.instance._opcion1.text = "[PRECAVIDO, -1 agua] Pedir a Dr. Jáparo que sane a la aventurera";
        ControladorEventos.instance._opcion2.text = "[-2 agua] Dar una buena parte de vuestra agua";
        ControladorEventos.instance._opcion3.text = "Cargarla hasta un lugar seguro";
    }

    public override void RemoveEventoObj()
    {
        LevelManager.instance.travelCostModifier -= 10;
        LevelManager.instance.maxWater--;

        LevelManager.instance.teamEnergy += 30;
    }
}
