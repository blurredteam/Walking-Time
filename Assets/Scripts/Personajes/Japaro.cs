using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Japaro : Character
{
    public Japaro(Image sprite, Image frontCard, Image backCard, Image icon)
    {
        _id = 1;
        name = "Dr. Japaro";
        selected = false;
        this.sprite = sprite;
        this.frontCard = frontCard;
        this.backCard = backCard;
        this.icon = icon;
        skillDesc = "[PRECAVIDO]";
        energy = 100;
    }

    

    private int energyCopy;
    private static float maxEnergyCopy;
    private Image image;

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
        string frase1 = "Mas vale prevenir que curar!";
        string frase2 = "Estudie medicina en Stanford durante 7 años, aprendí que blah blah blah...";
        string frase3 = "Gracias a mis bastos conocimientos ahora me llaman 'el Pelma'";
        string frase4 = "Si te fijas en el coseno de la formación blah blah blah...";
        string frase5 = "Mi nuevo libro, 'Primeros Auxilios', ya está a la venta en blah blah blah...";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
