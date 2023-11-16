using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fausto : Character
{   
    public Fausto(Image sprite, Image frontCard, Image backCard, Image icon)
    {
        _id = 4;
        name = "Fausto";
        selected = false;
        this.sprite = sprite;
        this.frontCard = frontCard;
        this.backCard = backCard;
        this.icon = icon;
        skillDesc = "[TRILERO]";
        energy = 0;                 //Para que no se muestre cuanta energia tiene, se aplica en skill
        defaultEnergy = energy;
    }

    public override void Skill()
    {
        if (!skillApplied)
        {
            skillApplied = true;
        }
    }
    public override void RevertSkill()
    {
        skillApplied = false;
    }

    public override void SkillFinally()
    {
        energy = Random.Range(30, 120);
        float aux = energy;

        foreach (Character character in _team) 
            if (character.name == "Dr. Japaro") aux *= 1.1f;

        energy = (int)aux;
        
        var randValue = Random.Range(-100, 100);
        if(randValue < 0) TeamComp.instance._teamMaxWater++;
        else TeamComp.instance._teamMaxWater--;
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
