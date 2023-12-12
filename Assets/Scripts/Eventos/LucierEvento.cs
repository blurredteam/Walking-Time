using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LucierEvento : Evento
{
    private List<Character> team = LevelManager.instance._team;
    public LucierEvento(Image imagenEvento)
    {
        _nombre = "Luci�rnagas �ndigas";
        index = 7;
        _eventImage = imagenEvento;
        _eventoTxt = $"Os adentr�is en un tramo oscuro del camino, mientras {team[1]} y {team[3]} debaten " +
            "sobre c�mo superar este tramo sin luz, te das cuenta que una de las paredes de la cueva est� " +
            "debilmente iluminada por una especie de luci�rnagas. " +
            $"\n - {team[0]}: �Increible, Son luci�rnagas �ndigas, se encuentran solo en esta cueva! \n " +
            "Os pregunt�is si existe alguna manera de utilizarlas para iluminar la cueva.";

        _opcionesList.Add("Apreci�is la belleza de las luci�rnagas y segu�s caminando.");
        _opcionesList.Add("[Mirabel] Le pides a mirabel que revele el camino.");
        _opcionesList.Add("[Seta] Le preguntas a Seta si puede hacer algo.");
        _opcionesList.Add("[-20 oro] Pag�is a un gu�a para que os lleve por el camino.");

        _resultadosList.Add("Las luci�rnagas son realemente increibles. Segu�s admir�ndolas un rato, " +
            "pero deb�is seguir por el sendero. Caminar a oscuras por una cueva no es ideal, " +
            "lleg�is al siguiente tramo cansados y con rasgu�os. [-40 energ�a]");
        _resultadosList.Add("Mirabel gu�a al equipo con sus habilidades ps�quicas, no necesita luz para saber " +
            "donde dar el siguiente paso. Tr�s un largo rato caminando lentamente, " +
            "lleg�is al siguiente tramo ilesos. [-10 energ�a]");
        _resultadosList.Add("Resulta que seta se hizo amigo de estas luci�rnagas cuando viv�a en la cueva. " +
            "Adem�s, est� convencido de que Alberto y Felipe, dos de las luci�rnagas que conoce, " +
            "podr�n guiarles sin problema por el camino. Las luci�rnagas parecen desbiarse un poco del tramo, " +
            "pero tras seguirlas un rato descubr�s que os han guiado hac�a un manantial secreto " +
            "antes de seguir con la ruta, recuper�is fuerzas y continu�is el camino. [+1 agua]");
        _resultadosList.Add("Gatotoga, un gato con una excelente visi�n nocturna, os gu�a sin problemas por el" +
            " camino. Lleg�is r�pido a vuestro destino. Una vez all�, gatotoga os mira de forma amenazante. " +
            "Le dais r�pidamente el dinero, qui�n sabe de lo que es capaz... Segu�s por el sendero.");
    }

    public override void Option1()
    {
        LevelManager.instance.teamEnergy -= 40;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[0].ToString();
        FinalizarEvento();
    }
    public override void Option2()
    {
        foreach (Character c in team)
        {
            if (c.name == "Mirabel")
            {
                LevelManager.instance.teamEnergy -= 10;
                ControladorEventos.instance._resultadoTxt.text = _resultadosList[1].ToString();
                FinalizarEvento();
                return;
            }
        }
    }

    public override void Option3()
    {
        foreach (Character c in team)
        {
            if (c.name == "Seta")
            {
                LevelManager.instance.teamWater += 1;
                ControladorEventos.instance._resultadoTxt.text = _resultadosList[2].ToString();
                FinalizarEvento();
                return;
            }
        }
    }

    public override void Option4()
    {
        if (LevelManager.instance.gold < 20) return;

        LevelManager.instance.gold -= 20; 
        ControladorEventos.instance._resultadoTxt.text = _resultadosList[3].ToString();
        FinalizarEvento();
    }
}
