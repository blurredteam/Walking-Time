using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    //[SerializeField] public Button _tileBtn;
    [SerializeField] public EventTrigger _clickEvent;
    [SerializeField] public SpriteRenderer _spriteRenderer;
    //[SerializeField] public Image _tileTypeImage;
    [SerializeField] private Image _puzzleTypeImage;
    [SerializeField] private Image _obstacleTypeImage;

    [SerializeField] private Image _bonfireImage;
    [SerializeField] private Image _obstacleImage;
    [SerializeField] private Image _puzzleImage;

    private int position;   //Posicion x de la casilla en el mapa, las primeras es 0, las siguientes 1...
    private int energyCost; //Coste de viajar a la casilla, depende del tipo

    public int value { get; set; }
    public bool selected = false;
    public List<Tile> AdyacentList = new List<Tile>();

    private ScenesManager.Scene _tileSceneString;

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
            energyCost = Random.Range(15, 30);
            PuzzleTile();
        }
        else if(random < -0.30f)
        {
            energyCost = Random.Range(30, 50);
            BonfireTile();
        }
        else
        {
            energyCost = Random.Range(10, 20);
            ObstacleTile();

        }
    }

    // Añade un listener a cada casilla, tras presionarlo se inhabilita se pinta como presionado y se cargan las siguientes
    public void LoadTile()
    {
        try { ScenesManager.instance.LoadSelected(_tileSceneString); } catch { Debug.Log("No hay escena todavia"); }

        _clickEvent.enabled = false;

        LevelManager.instance.Travel(energyCost);
        LevelManager.instance.UnloadTiles(position);

        _spriteRenderer.color = Color.red;

        LoadNextTiles();
    }

    //Carga las siguientes casillas disponibles en funcion de la lista de adyacencias 
    public void LoadNextTiles()
    {
        for (int i = 0; i <= AdyacentList.Count - 1; i++)
        {
            AdyacentList[i]._clickEvent.enabled = true;
            AdyacentList[i]._spriteRenderer.color = Color.grey;

        }
    }

    private void PuzzleTile() 
    {
        _spriteRenderer.sprite = _puzzleImage.sprite;

        _tileSceneString = ScenesManager.Scene.PuzleCuadro;

    }

    private void ObstacleTile() 
    {
        _spriteRenderer.sprite = _obstacleImage.sprite;

        _tileSceneString = ScenesManager.Scene.PuzleCuadro;
    }

    private void BonfireTile() 
    {
        _spriteRenderer.sprite = _bonfireImage.sprite;

        _tileSceneString = ScenesManager.Scene.PuzleCuadro;
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
