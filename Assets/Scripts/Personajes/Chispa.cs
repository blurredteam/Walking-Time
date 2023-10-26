using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Chispa : Character
{
    public Chispa(Image sprite, Image icon)
    {
        _id = 6;
        name = "Chispa";
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
        string frase1 = "Yo voy chill por donde me lleve el viento";
        string frase2 = "Tengo miedo de que Fausto me convierta en porro";
        string frase3 = "Vamos compañero! Tu puedes!";
        string frase4 = "El otro dia me saque unos cuartos en la rule";
        string frase5 = "Berenjeno lo ha pasado mal... no tomeis drogas niños";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
