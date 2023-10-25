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

    [SerializeField] private Image characterSprite;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterDescription;
    [SerializeField] private TextMeshProUGUI characterEnergy;

    //Las 4 listas siguientes necesitan ir en orden del ID del personaje
    [SerializeField] private List<Button> _btnList;
    [SerializeField] private List<Image> _spriteList;
    [SerializeField] private List<Image> _iconList;

    public List<Character> characterList;
    private Japaro _japaro;
    private Berenjeno _berenjeno;
    private Finito _finito;   
    private Mirabel _mirabel;
    private Fausto _fauno;
    private Seta _seta;
    private Chispa _chispa;

    /* 
    -- PERSONAJES IDs --
    --ID 0 -> japaro
    --ID 1 -> berenjeno
    --ID 2 -> martin
    --ID 3 -> mirabel
    --ID 4 -> fauno
    --ID 5 -> seta
    --ID 6 -> chispa
    */

    private void Awake()
    {
        instance = this;

        _japaro = new Japaro(_spriteList[0], _iconList[0]);
        _berenjeno = new Berenjeno(_spriteList[1], _iconList[1]);
        _finito = new Finito(_spriteList[2], _iconList[2]);
        _mirabel = new Mirabel(_spriteList[3], _iconList[3]);
        _fauno= new Fausto(_spriteList[4], _iconList[4]);
        _seta = new Seta(_spriteList[5], _iconList[5]);
        _chispa = new Chispa(_spriteList[6], _iconList[6]);

        characterList = new List<Character>() { _japaro, _berenjeno, _finito, _mirabel, _fauno, _seta, _chispa };
    }

    private void Start()
    {
        //Boton de cada personaje
        _btnList[0].onClick.AddListener(delegate { BtnHandler(_japaro); });
        _btnList[1].onClick.AddListener(delegate { BtnHandler(_berenjeno); });
        _btnList[2].onClick.AddListener(delegate { BtnHandler(_finito); });
        _btnList[3].onClick.AddListener(delegate { BtnHandler(_mirabel); });
        _btnList[4].onClick.AddListener(delegate { BtnHandler(_fauno); });
        _btnList[5].onClick.AddListener(delegate { BtnHandler(_seta); });
        _btnList[6].onClick.AddListener(delegate { BtnHandler(_chispa); });
    }

    private void BtnHandler(Character character)
    {
        ShowCharacterInfo(character._id);

        if (character.selected == false) TeamComp.instance.SelectCharacter(character._id);

        character.selected = true;
    }

    public void MakeCharacterAvailable(int characterId)
    {
        characterList[characterId].selected = false;
    }

    public void ShowCharacterInfo(int id)
    {
        characterSprite.sprite = characterList[id].sprite.sprite;
        characterName.text = characterList[id].name;
        characterDescription.text = characterList[id].desc;
        characterEnergy.text = characterList[id].energy.ToString();
    }
}
