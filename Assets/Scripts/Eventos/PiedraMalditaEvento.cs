using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiedraMalditaEvento : Evento
{
    private List<Character> team = LevelManager.instance._team;
    public PiedraMalditaEvento(Image imagenEvento, Image objectIcon)
    {
        _nombre = "Gema maldita";
        index = 1;
        _eventImage = imagenEvento;
        this.objectIcon = objectIcon;
        objectDescription = "[Medall�n maldito] \n[No podr�s volver a ganar oro hasta que te deshagas de �l]";
        _avisoQuitarObj = "�CUIDADO! Estas a punto de deshacerte del medall�n maldito, perder�s todo tu oro.";
        _eventoTxt = $"{team[3]} encuentra en el suelo un talism�n negro que parece brillar de forma extra�a, " +
            "como un brillo apagado. Parece que vale mucho oro pero cuando estas cerca se siente extra�o.";

        _opcionesList.Add("Coges el extra�o medall�n y lo llevas durante tu aventura");
        _opcionesList.Add("Lo dejas en el suelo y contin�as.");

        _resultadosList.Add("Tras recoger el medall�n y colg�rtelo del cuello te sientes m�s pesado al caminar, " +
            "como si cada moneda pesase lo mismo que una roca. Sientes que no puedes llevar una moneda mas " +
            "[�Has sido maldecido por el medall�n! No podr�s volver a ganar oro hasta deshacerte del medall�n][+40 oro]");
        _resultadosList.Add("Continu�is vuestro camino mientras miras a la piedra preciosa, a�n en el suelo. " +
            $"{team[3]} se queda pensando en qu� hubiera podido ser si te la hubieras llevado...");
    }

    public override void Option1()
    {
        LevelManager.instance.gold += 40;
        LevelManager.instance.auxGold = LevelManager.instance.gold;
        LevelManager.instance.cursed = true;

        LevelManager.instance.AddObject(this, objectIcon);

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
