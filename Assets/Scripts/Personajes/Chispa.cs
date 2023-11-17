using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Chispa : Character
{
    public Chispa(Image sprite, Image frontCard, Image backCard, Image icon)
    {
        _id = 5;
        name = "Chispa";
        selected = false;
        this.sprite = sprite;
        this.frontCard = frontCard;
        this.backCard = backCard;
        this.icon = icon;
        skillDesc = "[CHISPA]";
        energy = 60;
        //currentEnergy = energy;
        defaultEnergy = energy;
    }

    public override void Skill()
    {
        if (!skillApplied) LevelManager.instance.gold += 30;
    }

    public override void RevertSkill()
    {
        energy = defaultEnergy;
    }

    public override string PuzzleChooseDialogue()
    {
        string frase1 = "Por donde me lleve el viento...";
        string frase2 = "Dejate llevar... se uno con la naturaleza.";
        string frase3 = "Despeja tu mente, abre tus chakras, aummmmmmm";
        string frase4 = "Los pájaros los controla el gobierno";
        string frase5 = "Todo es contigo uno y un... uno con todo es... como era?";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
