using System.Collections;
using System.Collections.Generic;
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
        energy = 100;
    }

    public override void Skill()
    {
        base.Skill();
    }
    public override void RevertSkill()
    {
        base.RevertSkill();
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
