using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopoEvento : Evento
{
    private List<Character> team = LevelManager.instance._team;
    public TopoEvento(Image imagenEvento, Image objectIcon)
    {
        _nombre = "TOPOTIENDA";
        index = 8;
        _eventImage = imagenEvento;
        this.objectIcon = objectIcon;
        objectDescription = "[Topomochila] \n[-10 coste de viajar]";
        _avisoQuitarObj = "¿Seguro que quieres deshacerte de la magnífica topomochila?";
        _eventoTxt = $"Vais caminando alegremente por la caverna, {team[0]} tararea una alegre melodía " +
            $"y {team[2]} le cuenta uno de sus mejores chistes a {team[3]}. Seguís tranquilamente hasta que, " +
            $"de repente, sin previo aviso, de la nada, aparece la TOPOTIENDA, expertos en topos... y en tiendas, " +
            $"supongo. El topocomerciante cuenta con un buen toporepertorio de topobjetos.";

        _opcionesList.Add("[-25 oro] Una bonita topomochila hecha por topoartesanos");
        _opcionesList.Add("[-15 oro] +1 agua");
        _opcionesList.Add("[¡GRATIS!] Una topopiedra");
        _opcionesList.Add("[-60 oro] Topospá");

        _resultadosList.Add("¡Consigues una increible topomochila! [-10 coste de viajar]");
        _resultadosList.Add("Comprais un poco de agua, que os hace falta.");
        _resultadosList.Add("Es una piedra, no se que esperabas.");
        _resultadosList.Add("¡Parece que sois un equipo muy ahorrador! ¿Y para que ahorrabais? Justo para esto! " +
            "Bienvenidos a TOPOSPÁ, expertos en topos... y en spas. Os dan un increible topomasaje, os dais un" +
            " chapuzón en las topotermas y os relajais en la toposauna... Seguis por el camino a tope, o a topo." +
            " [Todo al máximo]");
    }

    public override void Option1()
    {
        if(LevelManager.instance.gold >= 25)
        {
            ControladorEventos.instance.RemoveEvent(this, objectIcon);
            LevelManager.instance.gold -= 25;
            LevelManager.instance.travelCostModifier -= 10;

            ControladorEventos.instance._resultadoTxt.text = _resultadosList[0].ToString();
            FinalizarEvento();
        }

    }
    public override void Option2()
    {
        if (LevelManager.instance.gold >= 15)
        {
            LevelManager.instance.teamWater += 1;

            ControladorEventos.instance._resultadoTxt.text = _resultadosList[1].ToString();
            FinalizarEvento();
        }
    }

    public override void Option3()
    {
        ControladorEventos.instance._resultadoTxt.text = _resultadosList[2].ToString();
        FinalizarEvento();
    }
    public override void Option4()
    {
        if (LevelManager.instance.gold < 60) return;

        LevelManager.instance.teamEnergy = LevelManager.instance.maxEnergy;
        LevelManager.instance.teamWater = LevelManager.instance.maxWater;

        ControladorEventos.instance._resultadoTxt.text = _resultadosList[3].ToString();
        FinalizarEvento();
    }

    public override void RemoveEventoObj()
    {
        LevelManager.instance.travelCostModifier += 10;
    }
}
