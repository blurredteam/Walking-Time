using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int _id;
    public string name;
    public bool selected;
    public Image sprite;
    public Image icon;
    public string desc;
    public int energy;

    public Character(int id, string name, bool selected, Image sprite, Image icon, string desc, int energy)
    {
        _id = id;
        this.name = name;
        this.selected = selected;
        this.sprite = sprite;
        this.icon = icon;
        this.desc = desc;
        this.energy = energy;
    }
}
