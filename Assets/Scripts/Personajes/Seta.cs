using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seta : Character
{
    public Seta(Image sprite, Image icon)
    {
        _id = 5;
        name = "Seta";
        selected = false;
        this.sprite = sprite;
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
        string frase1 = "...";
        string frase2 = "...";
        string frase3 = "Que las setas no hablan joderrr";
        string frase4 = "...";
        string frase5 = "...";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
