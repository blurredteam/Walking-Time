using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mirabel : Character
{
    public Mirabel(Image sprite, Image altSprite, Image frontCard, Image backCard, RuntimeAnimatorController anim, Image icon)
    {
        _id = 2;
        name = "Mirabel";
        selected = false;
        this.sprite = sprite;
        this.altSprite = altSprite;
        this.frontCard = frontCard;
        this.backCard = backCard;
        this.anim = anim;
        this.icon = icon;
        skillName = "[OBSERVADORA]";
        energy = 100;
        //currentEnergy = energy;
        defaultEnergy = energy;
    }

    public override void Skill()
    {
        skillDescription = $"{name} - {defaultEnergy} energía \n" +
            $"{skillName} - Menos coste de viaje \n" +
            $" - Habilidades psíquicas \n" +
            $" - Viajar cuesta 10 menos de energía";

        if (!skillApplied)
        {
            LevelManager.instance.travelCostModifier -= 10;
            skillApplied= true;
        }
    }
    public override void RevertSkill()
    {
        energy= defaultEnergy;

        LevelManager.instance.travelCostModifier += 10;
        skillApplied = false;
    }

    public override string PuzzleChooseDialogue()
    {
        string frase1 = "A veces veo el futuro, aunque creo que lo vería mejor con unas buenas gafas";
        string frase2 = "Ummmmm veo... veo cosas ummmmmmm";
        string frase3 = "¿Un 7 de picas? ¡¡¿¿Sabes lo que eso significa??!! Ya yo tampoco";
        string frase4 = "Un 4 de treboles... No se que significa, pero lo he visto en las cartas.";
        string frase5 = "Aqui cumpliendo la profecia yatusa.";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
