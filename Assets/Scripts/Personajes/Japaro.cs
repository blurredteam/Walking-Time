using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Japaro : Character
{
    public Japaro(Image sprite, Image icon)
    {
        _id = 0;
        name = "Dr. Japaro";
        selected = false;
        this.sprite = sprite;
        this.icon = icon;
        desc = "Desc";
        energy = 100;
    }

    private int energyCopy;
    private static float maxEnergyCopy;

    public override void Skill()
    {
        float teamEnergy = TeamComp.instance._teamMaxEnergy;
        teamEnergy *= 1.2f;
        maxEnergyCopy = teamEnergy;
        TeamComp.instance._teamMaxEnergy = (int)teamEnergy;
    }
    public override void RevertSkill()
    {
        float revertEnergy = maxEnergyCopy / 1.2f;
        TeamComp.instance._teamMaxEnergy =  (int) revertEnergy;
    }
    public override string PuzzleChooseDialogue()
    {
        string frase1 = "piopio";
        string frase2 = "*sonido de pajaro*";
        string frase3 = "*sonido de pajaro*";
        string frase4 = "*sonido de pajaro*";
        string frase5 = "*sonido de* SOC pa* ORR *jaroOOOOO*";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
