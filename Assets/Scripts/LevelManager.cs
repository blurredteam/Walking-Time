using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// BUG MU RARO --> peta si le das a play seleccionando el objeto con este script

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private Camera m_Camera;

    [SerializeField] private TextMeshProUGUI energy;
    [SerializeField] private TextMeshProUGUI water;
    [SerializeField] private Image iconP0;
    [SerializeField] private Image iconP1;
    [SerializeField] private Image iconP2;
    [SerializeField] private Image iconP3;

    [SerializeField] private GameObject _spritesTeam;
    [SerializeField] private Image spriteP0;
    [SerializeField] private Image spriteP1;
    [SerializeField] private Image spriteP2;
    [SerializeField] private Image spriteP3;

    private List<Character> _team;
    private int teamEnergy;
    private int maxEnergy;
    public int teamWater;
    private int waterRegen = 50;

    public void SetTeam(List<Character> team) { _team = team; HandleTeam(); }

    private Tile[,] _map;
    private List<GameObject> _lines;
    private int _mapWidth;
    private int _mapHeight;
    public void SetMap(Tile[,] map, int width, int height, List<GameObject> lines) 
    {
        _map = map; _mapWidth = width; _mapHeight = height; StartGame();
        _lines = lines;
    }

    private void Start()
    {
        instance= this;
    }

    private void Update()
    {
        energy.text = teamEnergy.ToString();
        water.text = teamWater.ToString();
    }

    public void HandleTeam() 
    {
        iconP0.sprite = _team[0].icon.sprite;
        iconP1.sprite = _team[1].icon.sprite;
        iconP2.sprite = _team[2].icon.sprite;
        iconP3.sprite = _team[3].icon.sprite;

        spriteP0.sprite = _team[0].sprite.sprite;
        spriteP1.sprite = _team[1].sprite.sprite;
        spriteP2.sprite = _team[2].sprite.sprite;
        spriteP3.sprite = _team[3].sprite.sprite;

        for(int i = 0; i < _team.Count; i++)
        {
            teamEnergy += _team[i].energy;
        }
        maxEnergy = teamEnergy;
        teamWater = 3;
    }

    // Habilita las primeras casillas disponibles al jugador
    private void StartGame()
    {
        for (int y = 0; y < _mapHeight; y++)
        {
            if (_map[0, y].selected)
            {
                _map[0, y]._clickEvent.enabled = true;
            }
        }
    }

    // Tras presionar una casilla, inhabilita las casillas de su misma columna y las pinta 
    public void UnloadTiles(int position)
    {
        for (int y = 0; y < _mapHeight; y++)
        {
            if (_map[position, y].selected)
            {
                _map[position, y]._clickEvent.enabled = false;
                _map[position, y]._spriteRenderer.color = Color.grey;
            }
        }
    }

    //Reduce la energia total en funcion del coste de viajar a una casilla
    public void Travel(int energyCost)
    {
        teamEnergy -= energyCost;
        m_Camera.transform.position += new Vector3(3.5f,0);
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
