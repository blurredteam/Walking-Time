using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Japaro : Character
{
    public Japaro(Image sprite, Image altSprite, Image frontCard, Image backCard, Image icon)
    {
        _id = 1;
        name = "Dr. Japaro";
        selected = false;
        this.sprite = sprite;
        this.altSprite = altSprite;
        this.frontCard = frontCard;
        this.backCard = backCard;
        this.icon = icon;
        skillDesc = "[PRECAVIDO]";
        skillApplied= false;
        energy = 80;
        //currentEnergy = energy;
        defaultEnergy = energy;
    }

    public override void Skill()
    {
        energy = defaultEnergy;

        var enerPer = (float)TeamComp.instance._teamCurrentEnergy / (float)TeamComp.instance._teamMaxEnergy;

        float teamEnergy = 0;
        float currentEnergy = 0;

        foreach (var c in TeamComp.instance._teamComp)
            if (c != null) // && c.name != "Fausto"
            {
                float charEnergy = c.defaultEnergy * 1.1f;
                c.energy = (int)charEnergy;
                teamEnergy += c.energy;

                float aux = c.energy * enerPer;
                currentEnergy += (int)aux;
            }

        TeamComp.instance._teamCurrentEnergy = (int)currentEnergy;
        TeamComp.instance._teamMaxEnergy = (int)teamEnergy;
    }

    public override void RevertSkill()
    {
        energy = defaultEnergy;

        float teamEnergy = 0;

        foreach (var c in TeamComp.instance._teamComp)
            if (c != null && c.name != this.name)
            {
                if(c.name != "Fausto") c.energy = c.defaultEnergy;
                teamEnergy += c.energy;
            }

        var aux = teamEnergy * 0.1f;
        TeamComp.instance._teamCurrentEnergy -= (int)aux;
        TeamComp.instance._teamMaxEnergy = (int)teamEnergy;
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
