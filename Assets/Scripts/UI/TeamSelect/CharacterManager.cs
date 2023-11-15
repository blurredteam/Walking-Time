using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    [SerializeField] private GameObject characterSprite;
    //[SerializeField] private TextMeshProUGUI characterName;
    //[SerializeField] private TextMeshProUGUI characterDescription;
    //[SerializeField] private TextMeshProUGUI characterLore;
    //[SerializeField] private TextMeshProUGUI characterEnergy;

    [SerializeField] private Button showInfo;

    //Las 4 listas siguientes necesitan ir en orden del ID del personaje
    [SerializeField] private List<Button> _btnList;
    [SerializeField] private List<Image> _spriteList;
    [SerializeField] private List<Image> _frontCardsList;    //Cartas de los personajes con su info
    [SerializeField] private List<Image> _backCardsList;
    [SerializeField] private List<Image> _iconList;

    public List<Character> characterList;
    private Japaro _japaro;
    private Berenjeno _berenjeno;
    private Mirabel _mirabel;
    private Fausto _fauno;
    private Seta _seta;
    private Chispa _chispa;

    /* 
    -- PERSONAJES IDs --
    --ID 0 -> berenjeno
    --ID 1 -> japaro
    --ID 2 -> mirabel
    --ID 3 -> seta
    --ID 4 -> fauno
    --ID 5 -> chispa
    */

    private void Awake()
    {
        instance = this;

        _berenjeno = new Berenjeno(_spriteList[0], _frontCardsList[0], _backCardsList[0], _iconList[0]);
        _japaro = new Japaro(_spriteList[1], _frontCardsList[1], _backCardsList[1], _iconList[1]);
        _mirabel = new Mirabel(_spriteList[2], _frontCardsList[2], _backCardsList[2], _iconList[2]);
        _seta = new Seta(_spriteList[3], _frontCardsList[3], _backCardsList[3], _iconList[3]);
        _fauno = new Fausto(_spriteList[4], _frontCardsList[4], _backCardsList[4], _iconList[4]);
        _chispa = new Chispa(_spriteList[5], _frontCardsList[5], _backCardsList[5], _iconList[5]);

        characterList = new List<Character>() { _berenjeno, _japaro, _mirabel, _seta, _fauno, _chispa };
    }

    private void Start()
    {
        //Boton de cada personaje
        _btnList[0].onClick.AddListener(delegate { BtnHandler(_berenjeno); });
        _btnList[1].onClick.AddListener(delegate { BtnHandler(_japaro); });
        _btnList[2].onClick.AddListener(delegate { BtnHandler(_mirabel); });
        _btnList[3].onClick.AddListener(delegate { BtnHandler(_seta); });
        _btnList[4].onClick.AddListener(delegate { BtnHandler(_fauno); });
        _btnList[5].onClick.AddListener(delegate { BtnHandler(_chispa); });
    }

    private void BtnHandler(Character character)
    {
        ShowCharacter(character._id);

        if (character.selected == false) TeamComp.instance.SelectCharacter(character._id);
    }

    public void ShowCharacter(int id)
    {
        characterSprite.SetActive(true);
        Image _characterSprite = characterSprite.GetComponent<Image>();
        _characterSprite.sprite = characterList[id].frontCard.sprite;

        showInfo.onClick.AddListener(delegate { ShowCharacterInfo(id, _characterSprite); });
    }

    private void ShowCharacterInfo(int id, Image _characterSprite)
    {
        if (_characterSprite.sprite == characterList[id].frontCard.sprite)
        {
            _characterSprite.sprite = characterList[id].backCard.sprite;
        }
        else
        {
            _characterSprite.sprite = characterList[id].frontCard.sprite;
        }
    }
    
}
