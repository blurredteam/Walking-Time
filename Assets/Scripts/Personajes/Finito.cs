using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finito : Character
{
    public Finito(Image sprite, Image icon)
    {
        _id = -1;
        name = "Finito";
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
        string frase1 = "JEJEJje";
        string frase2 = "AJAJJA";
        string frase3 = "JOJOJO";
        string frase4 = "JIJIJI";
        string frase5 = "JUJUJU";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
