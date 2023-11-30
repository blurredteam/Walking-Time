using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiedraMalditaEvento : Evento
{
    private List<Character> team = LevelManager.instance._team;
    private Image objectIcon;
    public PiedraMalditaEvento(Image imagenEvento, Image objectIcon)
    {
        _nombre = "Gema maldita";
        index = 1;
        _eventImage = imagenEvento;
        this.objectIcon = objectIcon;
        _avisoQuitarObj = "¡CUIDADO! Estas a punto de deshacerte del medallón maldito, perderás todo tu oro.";
        _eventoTxt = $"{team[3]} encuentra en el suelo un talismán negro que parece brillar de forma extraña, " +
            "como un brillo apagado. Parece que vale mucho oro pero cuando estas cerca se siente extraño.";

        _opcionesList.Add("1. Coges el extraño medallón y lo llevas durante tu aventura");
        _opcionesList.Add("2. Lo dejas en el suelo y continúas.");

        _resultadosList.Add("1. Tras recoger el medallón y colgártelo del cuello te sientes más pesado al caminar, " +
            "como si cada moneda pesase lo mismo que una roca. Sientes que no puedes llevar una moneda mas " +
            "[¡Has sido maldecido por el medallón! No podrás volver a ganar oro hasta gastar todo tu oro actual][+40 oro]");
        _resultadosList.Add("2. Continuáis vuestro camino mientras miras a la piedra preciosa, aún en el suelo. " +
            $"{team[3]} se queda pensando en qué hubiera podido ser si te la hubieras llevado...");
    }

    public override void Option1()
    {
        ControladorEventos.instance.RemoveEvent(this, objectIcon);

        LevelManager.instance.gold += 40;
        LevelManager.instance.cursed = true;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[0].ToString();
        FinalizarEvento();
    }
    public override void Option2()
    {
        ControladorEventos.instance._resultadoTxt.text = _resultadosList[1].ToString();
        FinalizarEvento();
    }

    public override void RemoveEventoObj()
    {
        LevelManager.instance.gold = 0;
        LevelManager.instance.cursed = false;
    }
}
