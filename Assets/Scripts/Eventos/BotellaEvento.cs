using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotellaEvento : Evento
{
    private List<Character> team = LevelManager.instance._team;

    public BotellaEvento(Image imagenEvento, Image objectIcon)
    {
        _nombre = "Botella �pica";
        index = 6;
        _eventImage = imagenEvento;
        this.objectIcon= objectIcon;
        objectDescription = "[Botella �pica] \n[max agua = 10, +20 coste de viajar]";
        _avisoQuitarObj = $"{team[0]} piensa que la botella pesa demasiado como para seguir carg�ndola, pero lo �nico que teneis para" +
            " reemplazarlo es un frasco roto que habeis encontrado por el camino. �Seguro que quieres dejar la" +
            " botella?";
        _eventoTxt = $"Durante el camino, {team[1].name} se tropieza con una piedra y cae en una caverna, " +
            $"estupefacto por lo que encuentra, llama a gritos al resto del equipo. Una vez abajo ves a lo que " +
            $"se refer�a, una botella de cristal de proporciones colosales se alza sobre vosotros. " +
            $"\n -{team[2].name}: No es posible, es la legendaria botella de Finito, podriamos hasta cargar un oceano de agua fresca con ella...";

        _opcionesList.Add("1. Coger la botella.");
        _opcionesList.Add("2. Dejar la botella.");

        _resultadosList.Add(" Segu�s vuestro camino con la nueva �pica botella, mas grande que cualquier otra botella" +
            " de la isla. La sonrisa se os borra tras caminar 5 minutos, el peso de la botella es igual de �pico que " +
            "sus dimensiones. [max agua = 10, +20 coste de viajar]");
        _resultadosList.Add(" �Quien es su sano juicio cargar�a con una botella tan sospechosa? La dejais donde est�" +
            "y seguis por el camino. El desv�o os cuesta un poco de energ�a [-20 energ�a]");
    }

    public override void Option1()
    {     
        LevelManager.instance.maxWater = 10;
        LevelManager.instance.travelCostModifier += 20;

        LevelManager.instance.AddObject(this, objectIcon);

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[0].ToString();
        FinalizarEvento();
    }
    public override void Option2()
    {
        LevelManager.instance.teamEnergy -= 10;
        ControladorEventos.instance._resultadoTxt.text = _resultadosList[1].ToString();
        FinalizarEvento();
    }

    public override void RemoveEventoObj()
    {
        LevelManager.instance.travelCostModifier -= 20;
        LevelManager.instance.maxWater = 1;
    }
}
