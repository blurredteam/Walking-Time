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
        skillApplied= false;
        energy = 100;
        defaultEnergy = energy;
    }

    public override void Skill()
    {
        float teamEnergy = 0;

        Debug.Log(name + ": Habilidad APLICADA");
        foreach (var c in TeamComp.instance._teamComp)
            if (c != null && c.name != "Fausto")
            {
                float charEnergy = c.defaultEnergy * 1.1f;
                c.energy = (int)charEnergy;
                teamEnergy += c.energy;
            }

        TeamComp.instance._teamMaxEnergy = (int)teamEnergy;
    }

    public override void RevertSkill()
    {
        energy = defaultEnergy;

        float teamEnergy = 0;

        foreach (var c in TeamComp.instance._teamComp)
            if (c != null && c.name != this.name)
            {
                c.energy = c.defaultEnergy;
                teamEnergy += c.energy;
            }

        TeamComp.instance._teamMaxEnergy = (int)teamEnergy;
    }
    public override string PuzzleChooseDialogue()
    {
        string frase1 = "Mas vale prevenir que curar!";
        string frase2 = "Estudie medicina en Stanford durante 7 a�os, aprend� que blah blah blah...";
        string frase3 = "Gracias a mis bastos conocimientos ahora me llaman 'el Pelma'";
        string frase4 = "Si te fijas en el coseno de la formaci�n blah blah blah...";
        string frase5 = "Mi nuevo libro, 'Primeros Auxilios', ya est� a la venta en blah blah blah...";

        _frases = new List<string> { frase1, frase2, frase3, frase4, frase5 };

        int index = Random.Range(0, _frases.Count);
        return (_frases[index]);
    }
}
