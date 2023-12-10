using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
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
    [SerializeField] private Image _waterImage;

    //Para animacion de casillas
    [SerializeField] public Animator animatorTile;
    [SerializeField] private RuntimeAnimatorController animationEvent;
    [SerializeField] private RuntimeAnimatorController animationFire;
    [SerializeField] private RuntimeAnimatorController animationWater;
    [SerializeField] private List<RuntimeAnimatorController> _animationPuzzle;

    public int type;        // El tipo de casilla, 0=puzzle; 1=evento; 2=hoguera: 3=fuente
    public int position;    // Posicion x de la casilla en el mapa, las primeras son 0, las siguientes 1...
    private int energyCost; // Coste de viajar a la casilla, depende del tipo
    private int index;      // Indica el puzzle o evento especifico
    private int bonfireProb = 0;    //probabilidad de que la casilla sea una hoguera
    private int waterProb = 0;      //probabilidad de que la casilla sea de agua

    public int value { get; set; }
    public bool selected = false;
    public List<Tile> AdyacentList = new List<Tile>();

    public GameObject casillaInfo;
    public TextMeshProUGUI textoInfo;
    public MovimientoJugador spritesJugador;

    private void Start()
    {
        animatorTile = GetComponent<Animator>();
    }

    //Asigna el tipo de casilla dependiendo de su posicion o aleatoriamente en funcion de random
    public void AssignType(int position)
    {
        this.position = position;
        SetTileInfo();

        // nivel1 buildIndex = 2; nivel2 buildIndex = 3
        bonfireProb = 5 - SceneManager.GetActiveScene().buildIndex;             //nivel 1 = 3, nivel 2 = 2
        waterProb = 11 - (SceneManager.GetActiveScene().buildIndex * 2 - 1);    //nivel 1 = 8, nivel 2 = 6

        EventTile();

        if (position == 0 || position == 5 || position == 7 || position == 10) PuzzleTile();
        else if (position == 6) BonfireTile();
        else
        {
            int random = Random.Range(0, 100);
            if (random >= 55) PuzzleTile();
            else if (random <= bonfireProb) BonfireTile();
            else if (random > bonfireProb && random <= waterProb) WaterTile();
            else EventTile();
        }

        //if (position != 0 && type != 2 && type != 3) animatorTile.enabled = false;
        if (position != 0) animatorTile.enabled = false;
    }

    public void SetTileInfo()
    {
        casillaInfo = GameObject.Find("CasillaInfo");      
        textoInfo = casillaInfo.GetComponentInChildren<TextMeshProUGUI>();
        spritesJugador = GameObject.Find("CharacterSprites").GetComponent<MovimientoJugador>();
        
        _spriteRenderer.sprite = _bonfireImage.sprite; // Imagen/animacion casilla final
        _clickEvent.enabled = false;
    }

    public void LoadTile()
    {
        HideInfo();

        LevelManager.instance.Travel(position, type, index);

        _spriteRenderer.color = Color.black;
        animatorTile.runtimeAnimatorController = null;//Desactivamos la anim
        LoadNextTiles();
    }

    //Carga las siguientes casillas disponibles en funcion de la lista de adyacencias 
    public void LoadNextTiles()
    {
        //if (AdyacentList.Count <= 0) ScenesManager.instance.LoadScene(ScenesManager.Scene.EndScene); 
        if (AdyacentList.Count <= 0) ScenesManager.instance.LoadNextLevel();

        for (int i = 0; i <= AdyacentList.Count - 1; i++)
        {
            AdyacentList[i]._clickEvent.enabled = true;
            AdyacentList[i].animatorTile.enabled = true;
        }
    }

    private void PuzzleTile()
    {
        type = 0;
        energyCost = Random.Range(30, 50);
        var puzzleList = ScenesManager.instance.puzzleScenes;
        index = Random.Range(0, puzzleList.Count);

        _spriteRenderer.sprite = _puzzleImage.sprite;
        animatorTile.runtimeAnimatorController = _animationPuzzle[index];
    }

    private void EventTile()
    {
        type = 1;
        energyCost = Random.Range(30, 40);

        _spriteRenderer.sprite = _obstacleImage.sprite;
        animatorTile.runtimeAnimatorController = animationEvent;
    }

    private void BonfireTile()
    {
        type = 2;
        energyCost = Random.Range(40, 60);

        _spriteRenderer.sprite = _bonfireImage.sprite;
        animatorTile.runtimeAnimatorController = animationFire;
    }

    private void WaterTile()
    {
        type = 3;
        energyCost = Random.Range(10, 30);

        _spriteRenderer.sprite = _waterImage.sprite;
        animatorTile.runtimeAnimatorController = animationWater;
    }

    public void ShowInfo()
    {
        casillaInfo.SetActive(true);
        casillaInfo.transform.position = gameObject.transform.position + new Vector3(1.2f, 0.70f, 0f);

        if (_clickEvent.enabled == true) textoInfo.text = "Coste: " + energyCost; 

        //if (type == 0) textoInfo.text = "Tipo: Puzzle\n Coste: " + this.energyCost;
        //else if (type == 1) textoInfo.text = "Tipo: Evento\n Coste: " + this.energyCost;
        //else if (type == 2) textoInfo.text = "Tipo: Hogera\n Coste: " + this.energyCost;
        //else if(type == 3) textoInfo.text = "Tipo: Fuente\n Coste: " + this.energyCost;
    }

    public void HideInfo()
    {
        try { casillaInfo.SetActive(false); } catch { Debug.Log("Fallo este del pop up"); }
    }

    // Se llama al clickar sobre una casilla
    public void MovimientoPreCarga()
    {
        _clickEvent.enabled = false;
        LevelManager.instance.PreTravel(energyCost);
        spritesJugador.MoverJugador(transform.position, this);  //para el movimiento de los sprites de una casilla a otra
    }

}
