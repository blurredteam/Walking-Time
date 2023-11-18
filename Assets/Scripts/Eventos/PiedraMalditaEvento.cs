using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiedraMalditaEvento : Evento
{
    public PiedraMalditaEvento(Image imagenEvento)
    {
        _nombre = "Gema maldita";
        _eventImage = imagenEvento;
        _eventoTxt = "Encontráis en el suelo un talismán negro que parece brillar de forma extraña, " +
            "como un brillo apagado. Parece que vale mucho oro pero al estar cerca suya te sientes extraño.";

        _opcionesList.Add("1. Coges el extraño medallón y lo llevas durante tu aventura [+200 oro]");
        _opcionesList.Add("2. Lo dejas en el suelo y continúas.");

        _resultadosList.Add("1. Tras recoger el medallón y colgártelo del cuello te sientes más pesado al caminar, " +
            "como si cada moneda pesase lo mismo que una roca. Sientes que no puedes llevar una moneda mas " +
            "[¡Has sido maldecido por el medallón! No podrás volver a ganar oro hasta gastar todo tu oro actual]");
        _resultadosList.Add("2. Continuáis vuestro camino mientras miras a la piedra preciosa, aún en el suelo. " +
            "Piensas en qué hubiera podido ser si te la hubieras llevado.");
    }

    public override void Option1()
    {
        LevelManager.instance.gold += 200;
        LevelManager.instance.cursed = true;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[0].ToString();
        FinalizarEvento();
    }
    public override void Option2()
    {
        ControladorEventos.instance._resultadoTxt.text = _resultadosList[1].ToString();
        FinalizarEvento();
    }
}
