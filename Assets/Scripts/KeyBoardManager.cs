using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Character> team = new List<Character>();
    public int energy;
    public int maxEnergy;
    public int totalEnergyUsed = 0;
    public int water;
    public int maxWater;
    public int totalWaterUsed = 0;
    public int gold;
    public int bonfiresVisited = 0;

    public List<Image> eventObjects = new List<Image>();
    public List<Evento> removedEvents = new List<Evento>();
    public int modViajar;
    public int waterRegen;
    public int expEnergy;

    //Atributos jugador
    public string nombreJugador;
    public int edadJugador;
    public string sexoJugador;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetLevelInfo()
    {
        LevelManager.instance.SetTeam(team);
        LevelManager.instance.teamEnergy= energy;
        LevelManager.instance.maxEnergy= maxEnergy;
        LevelManager.instance.teamWater = water;
        LevelManager.instance.maxWater= maxWater;
        LevelManager.instance.waterRegen = waterRegen;
        LevelManager.instance.travelCostModifier= modViajar;
        LevelManager.instance.expEnergy = expEnergy;      

        // Objetos
        for (int i = 0; i < removedEvents.Count; i++)
            if(removedEvents[i] != null) LevelManager.instance.AddObject(removedEvents[i], eventObjects[i]);
        
    }

    public void GetLevelInfo()
    {
        var lvl = LevelManager.instance;

        team = lvl._team;
        energy = lvl.teamEnergy;
        maxEnergy = lvl.maxEnergy;
        water = lvl.teamWater;
        maxWater = lvl.maxWater;
        totalEnergyUsed = lvl.totalEnergyUsed;
        totalWaterUsed = lvl.totalWaterUsed;

        modViajar = lvl.travelCostModifier;
        waterRegen = lvl.waterRegen;
        expEnergy = lvl.expEnergy;
        eventObjects = lvl._eventObjects;
        removedEvents = lvl.removedEvents;
    }
}
