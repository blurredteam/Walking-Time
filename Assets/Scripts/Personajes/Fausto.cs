using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fausto : Character
{   
    public Fausto(Image sprite, Image altSprite, Image frontCard, Image backCard, RuntimeAnimatorController anim, Image icon)
    {
        _id = 4;
        name = "Fausto";
        selected = false;
        this.sprite = sprite;
        this.altSprite = altSprite;
        this.frontCard = frontCard;
        this.backCard = backCard;
        this.anim = anim;
        this.icon = icon;
        skillName = "[TRILERO]";
        energy = 0;                 //Para que no se muestre cuanta energia tiene, se aplica en skill
        defaultEnergy = energy;
    }

    private static int waterModi;

    public override void RevertSkill()
    {
        if (skillApplied)
        {
            skillApplied = false;
            
            if (waterModi < 0)
            {
                if (TeamComp.instance._teamMaxWater == TeamComp.instance._teamCurrentWater) TeamComp.instance._teamCurrentWater--;

                TeamComp.instance._teamMaxWater--;
            }
            else TeamComp.instance._teamMaxWater++;
        }
    }

    public override void SkillFinally()
    {
        if (!skillApplied)
        {
            skillApplied = true;

            energy = Random.Range(40, 120);
            float aux = energy;

            skillDescription = $"{name} - {energy} energía \n" +
                $"{skillName} - Energía y agua aleatoria \n" +
                $" - Añade o quita uno de agua máxima \n" +
                $" - Energía aleatoria entre [30-120]"; 

            foreach (Character character in _team)
                if (character.name == "Dr. Japaro") aux *= 1.1f;

            energy = (int)aux;
            defaultEnergy = energy;

            float currentTeamEnergy = TeamComp.instance._teamCurrentEnergy;
            float maxTeamEnergy = TeamComp.instance._teamMaxEnergy;

            float currentEnergy = (currentTeamEnergy / maxTeamEnergy) * energy;
            TeamComp.instance._teamCurrentEnergy += (int)currentEnergy;
            TeamComp.instance._teamMaxEnergy += energy;

            waterModi = Random.Range(-100, 100);
            if (waterModi < 0) TeamComp.instance._teamMaxWater++;
            else
            {
                if (TeamComp.instance._teamMaxWater == TeamComp.instance._teamCurrentWater) TeamComp.instance._teamCurrentWater--;
                TeamComp.instance._teamMaxWater--;
            }
        }
    }

    public override string PuzzleChooseDialogue()
    {
        string frase1 = "Oh yeah molo mogollon!";
        string frase2 = "Este puzzle parece divertido!";
        string frase3 = "A toda mecha! Vamos flying loquete!";
        string frase4 = "Voy motivadisimo, siempre motivadisimo";
        string frase5 = "Vamos parrrriba pim pam!";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
