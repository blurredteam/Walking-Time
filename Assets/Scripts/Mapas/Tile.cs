using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{

    [SerializeField] public EventTrigger _clickEvent;
    [SerializeField] public SpriteRenderer _spriteRenderer;

    //Imagenes del tipo de casilla
    [SerializeField] private Image _bonfireImage;
    [SerializeField] private Image _obstacleImage;
    [SerializeField] private Image _puzzleImage;

    private int type;       // El tipo de casilla, 0=puzzle; 1=evento; 2=hoguera
    private int position;   // Posicion x de la casilla en el mapa, las primeras son 0, las siguientes 1...
    private int energyCost; // Coste de viajar a la casilla, depende del tipo
    private int index;      // Indica el puzzle o evento especifico

    public int value { get; set; }
    public bool selected = false;
    public List<Tile> AdyacentList = new List<Tile>();

    public void ColorTile(Color color)
    {
        _spriteRenderer.color = color;
    }

    //Asigna el tipo de casilla aleatoriamente en funcion de random 
    //Ademas, empiezan todas sin ser interactuables ya que eso se gestiona mas adelante al empezar la partida desde LevelManager
    public void AssignType(int position)
    {
        int random = Random.Range(0, 100);
        _clickEvent.enabled = false;

        this.position = position;

        if (random >= 50) PuzzleTile();
        else if(random <= 5) BonfireTile();
        else ObstacleTile();
    }

   public void LoadTile()
    {
        _clickEvent.enabled = false;

        LevelManager.instance.Travel(position, energyCost, type, index);

        _spriteRenderer.color = Color.black;
        LoadNextTiles();
    }

    //Carga las siguientes casillas disponibles en funcion de la lista de adyacencias 
    public void LoadNextTiles()
    {
        if(AdyacentList.Count <= 0) { ScenesManager.instance.LoadScene(ScenesManager.Scene.EndScene); }

        for (int i = 0; i <= AdyacentList.Count - 1; i++)
        {
            AdyacentList[i]._clickEvent.enabled = true;
            AdyacentList[i]._spriteRenderer.color = Color.blue;
        }
    }

    private void PuzzleTile()
    {
        type = 0;
        energyCost = Random.Range(30, 50);
        var puzzleList = ScenesManager.instance.escenasPuzle;
        index = Random.Range(0, puzzleList.Count);

        // Aqui falta poner la imagen en funcion de que puzzle es
        // Se necesitara una lista con las imagenes de cada tipo de puzzle
        // Quiza refactorizar los puzzles y hacer que todos hereden de una clase base puzzle (?
        // _spriteRenderer.sprite = _puzzleImages[index].sprite;
        _spriteRenderer.sprite = _puzzleImage.sprite;

    }

    private void ObstacleTile() 
    {
        _spriteRenderer.sprite = _obstacleImage.sprite;
        energyCost = Random.Range(30, 40);

        type = 0;
    }

    private void BonfireTile() 
    {
        _spriteRenderer.sprite = _bonfireImage.sprite;
        energyCost = Random.Range(40, 60);

        type = 2;
    }
}
