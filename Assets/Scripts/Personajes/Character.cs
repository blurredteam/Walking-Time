using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Character
{
    public int _id;
    public string name;
    public bool selected;
    public Image sprite;
    public Image altSprite;
    public Image frontCard;
    public Image backCard;
    public RuntimeAnimatorController anim;
    public Image icon;
    public string skillName;
    public string skillDescription;
    public bool skillApplied;
    public int energy;
    //public int currentEnergy;   //Indica la energia propia del personaje
    public int defaultEnergy;   //Se necesita para la habilidad de japaro

    public int _teamMaxEnergy;

    public bool unlocked = true; //para los desbloqueables

    public Tile[,] _map;
    public List<Character> _team;
    public int _teamMaxWater;
    public List<string> _frases;

    public virtual void Skill() { }

    //Esta funcion se usa unicamente en hogueras 
    public virtual void RevertSkill() { }

    //Se usa cuando el jugador se mete en un puzzle
    public virtual string PuzzleChooseDialogue() { return name.ToString() + ": comentario"; }

    //Se usa para las habilidades que se aplican despues de la seleccion (de momento solo el fauno)
    public virtual void SkillFinally() { }
}
