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
    public int _teamMaxEnergy;

    public Tile[,] _map;
    public int _teamMaxWater;

    public virtual void Skill() { Debug.Log(name + "; habilidad"); }

    //Esta funcion se usa unicamente en hogueras 
    public virtual void RevertSkill() { Debug.Log(name + "; negar habilidad"); }
}
