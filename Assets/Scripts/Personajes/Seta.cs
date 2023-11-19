using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seta : Character
{
    public Seta(Image sprite, Image frontCard, Image backCard, Image icon)
    {
        _id = 3;
        name = "Seta";
        selected = false;
        this.sprite = sprite;
        this.frontCard = frontCard;
        this.backCard = backCard;
        this.icon = icon;
        skillDesc = "[ESPORAS CURATIVAS]";
        energy = 30;
        //currentEnergy = energy;
        defaultEnergy = energy;
    }

    public override void Skill()
    {
        if (!skillApplied)
        {
            LevelManager.instance.waterRegen = 60;
            skillApplied = true;
        }
    }
    public override void RevertSkill()
    {
        energy = defaultEnergy;

        LevelManager.instance.waterRegen = 40;
        skillApplied = false;
    }

    public override string PuzzleChooseDialogue()
    {
        string frase1 = "JAJAJAJAJ soy una seta";
        string frase2 = "Mi comida favorita? Rissoto!";
        string frase3 = "Soy una seta, no se que decir";
        string frase4 = "Jeje no, no soy comestible jeje";
        string frase5 = "Que soy una seta AJAJAJAJAJA";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
