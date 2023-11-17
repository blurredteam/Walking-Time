using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mirabel : Character
{
    public Mirabel(Image sprite, Image frontCard, Image backCard, Image icon)
    {
        _id = 2;
        name = "Mirabel";
        selected = false;
        this.sprite = sprite;
        this.frontCard = frontCard;
        this.backCard = backCard;
        this.icon = icon;
        skillDesc = "[OBSERVADORA]";
        energy = 90;
        //currentEnergy = energy;
        defaultEnergy = energy;
    }

    public override void Skill()
    {
        if (!skillApplied)
        {
            LevelManager.instance.travelCostModifier = -10;
            skillApplied= true;
        }
    }
    public override void RevertSkill()
    {
        energy= defaultEnergy;

        LevelManager.instance.travelCostModifier = 0;
        skillApplied = false;
    }

    public override string PuzzleChooseDialogue()
    {
        string frase1 = "A veces veo el futuro, aunque creo que lo ver�a mejor con unas buenas gafas";
        string frase2 = "Ummmmm veo... veo cosas ummmmmmm";
        string frase3 = "�Un 7 de picas? ����Sabes lo que eso significa??!! Ya yo tampoco";
        string frase4 = "Un 4 de treboles... No se que significa, pero lo he visto en las cartas.";
        string frase5 = "Aqui cumpliendo la profecia yatusa.";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
