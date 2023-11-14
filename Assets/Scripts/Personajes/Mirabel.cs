using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mirabel : Character
{
    public Mirabel(Image sprite, Image info,Image icon)
    {
        _id = 3;
        name = "Mirabel";
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
        string frase1 = "Como me ha dejado el cañon del fausto tu O.O";
        string frase2 = "Puedo incluso ver a traves de ti, y lo que veo me da asco";
        string frase3 = "Te odio.";
        string frase4 = "Muerete.";
        string frase5 = "ahem... voluntarios para sacrificio?";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
