using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// BUG MU RARO --> peta si le das a play seleccionando el objeto con este script

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private MapCamaraMovement _cameraMovementScript;
    [SerializeField] private GameObject _gridRef;

    [SerializeField] private TextMeshProUGUI energyTxt;
    [SerializeField] private TextMeshProUGUI waterTxt;
    [SerializeField] private TextMeshProUGUI goldTxt;
    [SerializeField] private List<Image> _icons;

    [SerializeField] private GameObject _spritesTeam;
    [SerializeField] private List<Image> _sprites;

    public List<Character> _team { get; set; } = new List<Character>();
    public int teamEnergy { get; set; } = 1;
    public int maxEnergy { get; set; }
    public int teamWater { get; set; }
    public int maxWater { get; set; }

    public int gold { get; set; } = 0;

    private int waterRegen = 50;

    public Tile[,] _map { get; set; }
    private int _mapWidth;
    private int _mapHeight;
    public void SetMap(Tile[,] map, int width, int height) 
    {
        _map = map; _mapWidth = width; _mapHeight = height;
    }

    public void SetTeam(List<Character> team)
    {
        this._team = team;

        //Asigna los iconos
        for (int i = 0; i < _team.Count; i++)
        {
            _icons[i].sprite = _team[i].icon.sprite;
            _sprites[i].sprite = _team[i].sprite.sprite;
        }
    }

    private void Start()
    {
        instance= this;

        maxWater = teamWater;
    }

    private void Update()
    {
        energyTxt.text = teamEnergy.ToString();
        waterTxt.text = teamWater.ToString();
        goldTxt.text = gold.ToString();

        CheckEnergy();
    }

    private void CheckEnergy()
    {
        if(teamEnergy <= 0) { ScenesManager.instance.LoadScene(ScenesManager.Scene.EndScene); }
    }

    // Habilita las primeras casillas disponibles al jugador
    public void StartGame()
    {

        for (int y = 0; y < _mapHeight; y++)
        {
            if (_map[0, y].selected)
            {
                _map[0, y]._clickEvent.enabled = true;
            }
        }
    }

    // Carga la escena indicada y reduce la energia total en funcion del coste de viajar a esa casilla
    // Tambien inhabilita las casillas de su misma columna y las pinta 
    public void Travel(int position, int energyCost, int tileType, int index)
    {
        _gridRef.gameObject.SetActive(false); 
        _cameraMovementScript.enabled =false;

        ScenesManager.instance.LoadTileScene(tileType, index);

        teamEnergy -= energyCost; 

        for (int y = 0; y < _mapHeight; y++)
        {
            if (_map[position, y].selected)
            {
                _map[position, y]._clickEvent.enabled = false;
                _map[position, y]._spriteRenderer.color = Color.grey;
                _map[position, y].animatorTile.runtimeAnimatorController = null;
            }
        }
    }

    // Se usa al volver de otra escena (puzzles, eventos, hoguera)
    public void ActivateScene()
    {
        _gridRef.gameObject.SetActive(true);
        _cameraMovementScript.enabled = true;   
    }

    public void UsingWater()
    {
        if (teamWater > 0)
        {
            if ((teamEnergy + waterRegen) >= maxEnergy)
            {
                teamEnergy = maxEnergy;
                teamWater--;
            }
            else
            {
                teamEnergy += 50;
                teamWater--;
            }
        }
    }
}
