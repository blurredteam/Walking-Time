using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.PackageManager.Requests;
//using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// BUG MU RARO --> peta si le das a play seleccionando el objeto con este script

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
   
    [SerializeField] private MapCamaraMovement _cameraMovementScript;
    [SerializeField] private GameObject _gridRef;

    [SerializeField] private List<Image> _icons;

    [SerializeField] public List<Image> _eventObjects; //Lista de objetos que se pueden conseguir en eventos
    public List<Evento> removedEvents = new List<Evento>() { null, null, null, null };

    [SerializeField] private GameObject _spritesTeam;
    [SerializeField] private List<Image> _sprites;
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
    
    public Transitioner transition;

    private void Awake()
    {
        transition = ScenesManager.instance.transitioner;
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

        //Asigna los iconos
        for (int i = 0; i < _team.Count; i++)
        {
            _icons[i].sprite = _team[i].icon.sprite;
            _sprites[i].sprite = _team[i].sprite.sprite;
        }

        //_icons[0].gameObject.GetComponentInParent<Button>().onClick.AddListener(delegate { ShowCharacterCard(0); });
        //_icons[1].gameObject.GetComponentInParent<Button>().onClick.AddListener(delegate { ShowCharacterCard(1); });
        //_icons[2].gameObject.GetComponentInParent<Button>().onClick.AddListener(delegate { ShowCharacterCard(2); });
        //_icons[3].gameObject.GetComponentInParent<Button>().onClick.AddListener(delegate { ShowCharacterCard(3); });
    }

    private void Start()
    {
        instance= this;
        AudioManager.instance.PlayAmbient();
        maxWater = teamWater;
        expEnergy = 1;
    }

    private void Update()
    {
        CheckResources();
        CheckObjects();
    }

    private void CheckResources()
    {
        if(teamWater > maxWater) teamWater = maxWater;
        if(teamEnergy > maxEnergy) teamEnergy = maxEnergy;

        if (teamEnergy <= 0) { ScenesManager.instance.LoadScene(ScenesManager.Scene.EndScene); }
    }

    private void CheckObjects()
    {
        if (cursed)
        {
            if (gold <= auxGold) auxGold = gold;
            else if (gold > auxGold) gold = auxGold;
        }
    }

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

        Debug.Log("No caben mas objetos!");
    }

    // Habilita las primeras casillas disponibles al jugador
    public void StartGame()
    {
        for (int y = 0; y < _mapHeight; y++)
            if (_map[0, y].selected) _map[0, y]._clickEvent.enabled = true;
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

    //private void ShowCharacterCard(int position)
    //{
    //    if (cardMoved)
    //    {
    //        StartCoroutine(ChangeCard(position));
    //        return;
    //    }
    //    cardMoved = true;
    //    _characterCard.sprite = _team[position].frontCard.sprite;

    //    SpriteState btnSprites = new SpriteState();
    //    btnSprites.highlightedSprite = _team[position].backCard.sprite;
    //    btnSprites.selectedSprite = _team[position].sprite.sprite;

    //    _characterCard.gameObject.GetComponent<Button>().spriteState = btnSprites;
    //    _characterCard.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
    //    _characterCard.gameObject.GetComponent<Button>().onClick.AddListener(HideCard);
    //    StartCoroutine(ShowCard());
    //}

    //private IEnumerator ShowCard()
    //{
    //    while (_characterCard.rectTransform.localPosition.x > 730)
    //    {
    //        _characterCard.rectTransform.position -= new Vector3(0.1f, 0);
    //        yield return null;
    //    }
    //}

    //private void HideCard()
    //{
    //    cardMoved = false;
    //    StartCoroutine(HideCardCo());
    //}

    //private IEnumerator HideCardCo()
    //{
    //    while (_characterCard.rectTransform.localPosition.x < 1600)
    //    {
    //        _characterCard.rectTransform.position += new Vector3(0.1f, 0);
    //        yield return null;
    //    }
    //}

    //private IEnumerator ChangeCard(int position)
    //{
    //    HideCard();
    //    yield return new WaitForSeconds(0.3f);
    //    ShowCharacterCard(position);
    //}

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
