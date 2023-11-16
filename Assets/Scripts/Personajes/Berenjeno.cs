using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Berenjeno : Character
{
    public Berenjeno(Image sprite, Image frontCard, Image backCard, Image icon)
    {
        _id = 0;
        name = "Berenjeno";
        selected = false;
        this.sprite = sprite;
        this.frontCard = frontCard;
        this.backCard = backCard;
        this.icon = icon;
        skillDesc = "[FORZUDO]";
        energy = 150;
    }

    public override void Skill() 
    {
        if (TeamComp.instance._teamCurrentWater == TeamComp.instance._teamMaxWater)
        {
            TeamComp.instance._teamCurrentWater--;
        }

        TeamComp.instance._teamMaxWater--;
    }

    public override void RevertSkill()
    {
        TeamComp.instance._teamMaxWater++;
    }

    public override string PuzzleChooseDialogue()
    {
        string frase1 = "Nadie es más fuerte que yo!";
        string frase2 = "Vamos a demostrarles quien es la verdura mas dura.";
        string frase3 = "Uf, este parece complicado compañero...";
        string frase4 = "Ojala mi mente fuese igual de musculosa que estos brazacos";
        string frase5 = "Parece que no podré hacer esto de un puñetazo";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
