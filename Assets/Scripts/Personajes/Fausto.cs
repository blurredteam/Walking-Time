using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fausto : Character
{   
    public Fausto(Image sprite, Image info, Image icon)
    {
        _id = 4;
        name = "Fausto";
        selected = false;
        this.sprite = sprite;
        this.info = info;
        this.icon = icon;
        desc = "Desc";
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
        string frase1 = "Oh yeah molo mogollon";
        string frase2 = "Tremendo cañon que me estoy talando lokete";
        string frase3 = "Este puzzle me come los cojones";
        string frase4 = "Soy Fausto, el fauno fumón";
        string frase5 = "DOLOR SOLO SIENTO DOLORRRRRRRRRRRR";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
