using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Chispa : Character
{
    public Chispa(Image sprite, Image altSprite, Image frontCard, Image backCard, RuntimeAnimatorController anim, Image icon)
    {
        _id = 5;
        name = "Chispa";
        selected = false;
        this.sprite = sprite;
        this.altSprite = altSprite;
        this.frontCard = frontCard;
        this.backCard = backCard;
        this.anim = anim;
        this.icon = icon;
        skillName = "[CHISPA]";
        energy = 80;
        //currentEnergy = energy;
        defaultEnergy = energy;
    }

    public override void Skill()
    {
        skillDescription = $"{name} - {defaultEnergy} energía \n" +
          $"{skillName} - ¡ORO! \n" +
          $" - +30 oro inicial";

        if (!skillApplied)
        {
            skillApplied = true;
            LevelManager.instance.gold += 30;
        }
    }

    public override void RevertSkill()
    {
        skillApplied = false;
        energy = defaultEnergy;
    }

    public override string PuzzleChooseDialogue()
    {
        string frase1 = "Por donde me lleve el viento...";
        string frase2 = "Déjate llevar... sé uno con la naturaleza.";
        string frase3 = "Despeja tu mente, abre tus chakras, aummmmmmm";
        string frase4 = "¡Los pájaros... los controlan, son espías de las megacorporaciones!";
        string frase5 = "Todo es contigo uno y un... uno contigo es... ¿cómo era?";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
