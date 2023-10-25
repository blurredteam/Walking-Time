using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Berenjeno : Character
{
    public Berenjeno(Image sprite, Image icon)
    {
        _id = 1;
        name = "Berenjeno";
        selected = false;
        this.sprite = sprite;
        this.icon = icon;
        desc = "Berenjeno se despertó en la isla sin saber muy bien donde estaba, confuso y un poco asustado, pero sobre todo confuso. Siempre se ha dicho que las verduras ayudan a crecer sano y fuerte pero no sabía que era tan real hasta que vi a Berenjeno.";
        energy = 150;
    }

    public override void Skill() 
    {
        if (TeamComp.instance._teamCurrentWater == TeamComp.instance._teamMaxWater)
        {
            TeamComp.instance._teamCurrentWater--;
        }

        TeamComp.instance._teamMaxWater--;
    }

    public override void RevertSkill()
    {
        TeamComp.instance._teamMaxWater++;
    }
}
