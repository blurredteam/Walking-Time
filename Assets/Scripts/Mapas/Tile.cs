using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEditor.Animations;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{

    [SerializeField] public EventTrigger _clickEvent;
    [SerializeField] public SpriteRenderer _spriteRenderer;

    //Imagenes del tipo de casilla
    [SerializeField] private Image _bonfireImage;
    [SerializeField] private Image _obstacleImage;
    [SerializeField] private Image _puzzleImage;

    //Para animacion de casillas
    [SerializeField] public Animator animatorTile;
    [SerializeField] private AnimatorController animationPuzzleImage;

    private int type;       // El tipo de casilla, 0=puzzle; 1=evento; 2=hoguera
    private int position;   // Posicion x de la casilla en el mapa, las primeras son 0, las siguientes 1...
    private int energyCost; // Coste de viajar a la casilla, depende del tipo
    private int index;      // Indica el puzzle o evento especifico

    public int value { get; set; }
    public bool selected = false;
    public List<Tile> AdyacentList = new List<Tile>();

    GameObject casillaInfo;
    private TextMeshProUGUI textoInfo;


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

        casillaInfo = GameObject.Find("CasillaInfo");       // Asegurar que coincide con el nombre en el editor
        textoInfo = casillaInfo.GetComponentInChildren<TextMeshProUGUI>();

        this.position = position;

        if (random >= 50) PuzzleTile();
        else if (random <= 5) BonfireTile();
        else ObstacleTile();
    }

    public void LoadTile()
    {
        _clickEvent.enabled = false;

        LevelManager.instance.Travel(position, energyCost, type, index);

        _spriteRenderer.color = Color.black;
        animatorTile.runtimeAnimatorController = null;//Desactivamos la anim
        LoadNextTiles();
    }

    //Carga las siguientes casillas disponibles en funcion de la lista de adyacencias 
    public void LoadNextTiles()
    {
        if (AdyacentList.Count <= 0) { ScenesManager.instance.LoadScene(ScenesManager.Scene.EndScene); }

        for (int i = 0; i <= AdyacentList.Count - 1; i++)
        {
            AdyacentList[i]._clickEvent.enabled = true;
            //AdyacentList[i]._spriteRenderer.color = Color.blue;

            //Si es puzzle activa su animacion
            if (AdyacentList[i].type == 0) { AdyacentList[i].animatorTile.runtimeAnimatorController = animationPuzzleImage; }
            else { AdyacentList[i]._spriteRenderer.color = Color.blue; }

        }
    }

    private void PuzzleTile()
    {
        type = 0;
        energyCost = Random.Range(30, 50);
        var puzzleList = ScenesManager.instance.escenasPuzle;
        index = Random.Range(0, puzzleList.Count);


        animatorTile = GetComponent<Animator>();
        _spriteRenderer.sprite = _puzzleImage.sprite;


    }

    private void ObstacleTile()
    {
        _spriteRenderer.sprite = _obstacleImage.sprite;
        energyCost = Random.Range(30, 40);

        type = 1;
    }

    private void BonfireTile()
    {
        _spriteRenderer.sprite = _bonfireImage.sprite;
        energyCost = Random.Range(40, 60);

        type = 2;
    }

    private void ShowInfo()
    {

        casillaInfo.SetActive(true);
        casillaInfo.transform.position = gameObject.transform.position + new Vector3(1.2f, 0.70f, 0f);

        if (_clickEvent.enabled == true)
        {

            if (type == 0)
            {
                textoInfo.text = "Tipo: Puzzle\n Coste: " + this.energyCost;
            }
            if (type == 1)
            {
                textoInfo.text = "Tipo: Evento\n Coste: " + this.energyCost;
            }
            if (type == 2)
            {
                textoInfo.text = "Tipo: Hogera\n Coste: " + this.energyCost;
            }
        }

    }

    public void HideInfo()
    {
        casillaInfo.SetActive(false);
    }

}
