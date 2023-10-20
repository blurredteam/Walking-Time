using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{

    [SerializeField] public EventTrigger _clickEvent;
    [SerializeField] public SpriteRenderer _spriteRenderer;

    [SerializeField] private Image _puzzleTypeImage;
    [SerializeField] private Image _obstacleTypeImage;

    [SerializeField] private Image _bonfireImage;
    [SerializeField] private Image _obstacleImage;
    [SerializeField] private Image _puzzleImage;

    private int type;       //El tipo de casilla, 0=puzzle; 1=evento; 2=hoguera
    private int position;   //Posicion x de la casilla en el mapa, las primeras es 0, las siguientes 1...
    private int energyCost; //Coste de viajar a la casilla, depende del tipo

    public int value { get; set; }
    public bool selected = false;
    public List<Tile> AdyacentList = new List<Tile>();

    public void ColorTile(Color color)
    {
        _spriteRenderer.color = color;
    }

    //Asigna el tipo de casilla aleatoriamente en funcion de random (-0.35, 0.35)
    //Ademas, empiezan todas sin ser interactuables ya que eso se gestiona mas adelante al empezar la partida desde LevelManager
    public void AssignType(int position, float random)
    {
        this.position = position;
        _clickEvent.enabled = false;
        //_tileBtn.interactable = false;
        ColorTile(Color.white);

        if (random >= 0)
        {
            energyCost = Random.Range(30, 50);
            PuzzleTile();
        }
        else if(random < -0.30f)
        {
            energyCost = Random.Range(40, 60);
            BonfireTile();
        }
        else
        {
            energyCost = Random.Range(30, 40);
            ObstacleTile();

        }
    }

   public void LoadTile()
    {
        ScenesManager.instance.LoadTileScene(type);

        _clickEvent.enabled = false;

        LevelManager.instance.Travel(position, energyCost);

        _spriteRenderer.color = Color.red;

        LoadNextTiles();
    }

    //Carga las siguientes casillas disponibles en funcion de la lista de adyacencias 
    public void LoadNextTiles()
    {
        if(AdyacentList.Count <= 0) { ScenesManager.instance.LoadScene(ScenesManager.Scene.EndScene); }

        for (int i = 0; i <= AdyacentList.Count - 1; i++)
        {
            AdyacentList[i]._clickEvent.enabled = true;
            AdyacentList[i]._spriteRenderer.color = Color.grey;
        }
    }

    private void PuzzleTile() 
    {
        _spriteRenderer.sprite = _puzzleImage.sprite;

        type = 0;

    }

    private void ObstacleTile() 
    {
        _spriteRenderer.sprite = _obstacleImage.sprite;

        type = 0;
    }

    private void BonfireTile() 
    {
        _spriteRenderer.sprite = _bonfireImage.sprite;

        type = 2;
    }


    public IEnumerator BtnGlow(Button btn)
    {
        if (!btn.interactable) yield break;

        ColorBlock cb = btn.colors;

        if (cb.normalColor == Color.white)
            cb.normalColor = Color.grey;
        else
            cb.normalColor = Color.white;

        btn.colors = cb;

        yield return new WaitForSeconds(1.2f);

        StartCoroutine(BtnGlow(btn));
    }
}
