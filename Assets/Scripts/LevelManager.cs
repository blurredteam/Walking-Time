using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
   
    [SerializeField] private MapCamaraMovement1 _cameraMovementScript;
    [SerializeField] private GameObject _gridRef;

    [SerializeField] public List<Image> _eventObjects = new List<Image>(); //Lista de objetos que se pueden conseguir en eventos
    public List<Evento> removedEvents = new List<Evento>() { null, null, null, null };

    public List<Character> _team { get; set; } = new List<Character>();
    public int teamEnergy { get; set; } = 1;
    public int maxEnergy { get; set; } = 1;
    public int teamWater { get; set; }
    public int maxWater { get; set; }
    public int expEnergy { get; set; }
    public int travelCostModifier { get; set; } = 0;  //Modifica el coste de viajar a casillas (de momento solo en la habilidad de mirabel)
    public int gold { get; set; } = 0;

    // Variables para evento de medallon maldito
    public int auxGold = 0; 
    public bool cursed;     //Si esta activo el jugador no gana oro    

    public int waterRegen { get; set; } = 40;   // Cuanto energia regenera la cantimplora en cada uso

    public Tile[,] _map { get; set; }
    public int _mapWidth { get; set; }
    public int _mapHeight { get; set; }
    public int _mapPos { get; set; } = 0;
    
    public Transitioner transition;

    private void Awake()
    {
        instance = this;
        transition = ScenesManager.instance.transitioner;

        if (SceneManager.GetActiveScene().name != "Level1")
        {
            GameManager.instance.SetLevelInfo();
        }
    }

    //Sound
    [SerializeField] private AudioClip losingEnergy;
    [SerializeField] private AudioClip usingWater;
    [SerializeField] private AudioClip noMoreWater;
    public void SetMap(Tile[,] map, int width, int height) 
    {
        _map = map; _mapWidth = width; _mapHeight = height;
    }

    public void SetTeam(List<Character> team)
    {
        this._team = team;
    }

    private void Start()
    {
        AudioManager.instance.PlayAmbient();
        maxWater = teamWater;
        expEnergy = 1;
    }

    private void Update()
    {
        GameManager.instance.GetLevelInfo();
        CheckResources();
        CheckObjects();
    }

    private void CheckResources()
    {
        if(teamWater > maxWater) teamWater = maxWater;
        if(teamEnergy > maxEnergy) teamEnergy = maxEnergy;

        if (teamEnergy <= 0) ScenesManager.instance.LoadScene(ScenesManager.Scene.EndScene); 
    }

    private void CheckObjects()
    {
        if (cursed)
        {
            if (gold <= auxGold) auxGold = gold;
            else if (gold > auxGold) gold = auxGold;
        }
    }

    //Se ejecuta desde ciertos eventos, a�ade un objeto a la mochila y elimina el evento mientras se tenga el objeto
    public void AddObject(Evento e, Image objectIcon)
    {
        for (int i = 0; i < _eventObjects.Count; i++)
        {
            if (!_eventObjects[i].IsActive())
            {
                removedEvents[i] = e;
                _eventObjects[i].sprite = objectIcon.sprite;
                _eventObjects[i].gameObject.SetActive(true);
                return;
            }
        }
    }

    // Habilita las primeras casillas disponibles al jugador, se usa tmb en hoguera al pasar de nivel
    public void StartGame()
    {
        GameManager.instance.GetLevelInfo();

        for (int y = 0; y < _mapHeight; y++) 
            if (_map[_mapPos, y].selected) 
                _map[_mapPos, y]._clickEvent.enabled = true;
    }

    // Se ejecuta antes de cargar la escena de la casilla a la que se viaja
    public void PreTravel(int energyCost)
    {
        _cameraMovementScript.enabled = false;
         
        teamEnergy -= (energyCost + travelCostModifier);

        AudioManager.instance.PlaySfx(losingEnergy);
        Level_UI.instance.HandleEnergyUI(energyCost + travelCostModifier);
    }

    // Carga la escena indicada y reduce la energia total en funcion del coste de viajar a esa casilla
    // Tambien inhabilita las casillas de su misma columna y las pinta 
    public void Travel(int position, int tileType, int index)
    {
        if (tileType != 1 && tileType != 100) //Evento o casilla final
        {
            _gridRef.gameObject.SetActive(false);
            DoFadeTransition(tileType, index);
            _cameraMovementScript.enabled = false;
        } 
        else ScenesManager.instance.LoadTileScene(tileType, index);

        for (int y = 0; y < _mapHeight; y++)
        {
            if (_map[position, y].selected)
            {
                _map[position, y]._clickEvent.enabled = false;
                _map[position, y]._spriteRenderer.color = Color.grey;
                _map[position, y].animatorTile.runtimeAnimatorController = null;
            }
        }

        _mapPos += 1;
    }

    // Se usa al volver de otra escena (puzzles, eventos, hoguera)
    public void ActivateScene()
    {
        _gridRef.gameObject.SetActive(true);
        _cameraMovementScript.enabled = true;
         
    }

    public void UsingWater()
    {
        if(teamWater == 0)
        {
            AudioManager.instance.PlaySfx(noMoreWater);
            Level_UI.instance.HandleWaterUI(0);
            return;
        }

        AudioManager.instance.PlaySfx(usingWater);
        teamEnergy += waterRegen;
        teamWater--;
        Level_UI.instance.HandleWaterUI(1);
    }

    public void DoFadeTransition(int tileType, int index)
    {
        StartCoroutine(DoFadeTransitionCo(tileType, index));
    }

    IEnumerator DoFadeTransitionCo(int tileType, int index)
    {
        transition.DoTransitionOnce();
        yield return new WaitForSeconds(1f);
        ScenesManager.instance.LoadTileScene(tileType, index);
        transition.DoTransitionOnce();
    }
}
