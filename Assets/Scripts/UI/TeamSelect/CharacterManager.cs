using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    //Un boton por cada personaje
    [SerializeField] private Button _pajaroBtn;
    [SerializeField] private Button _berenjenoBtn;
    [SerializeField] private Button _misteriosoBtn;
    [SerializeField] private Button _ojosBtn;
    [SerializeField] private Button _characterBtn5;

    private List<Button> _btnList;

    [SerializeField] private Image _pajaroSprite;
    [SerializeField] private Image _berenjenoSprite;
    [SerializeField] private Image _misteriosoSprite;
    [SerializeField] private Image _ojosSprite;

    [SerializeField] private Image _pajaroIcon;
    [SerializeField] private Image _berenjenoIcon;
    [SerializeField] private Image _misteriosoIcon;
    [SerializeField] private Image _ojosIcon;

    private Character _pajaro;
    private Character _berenjeno;
    private Character _misterioso;
    private Character _ojos;

    public List<Character> characterList;

    /* -- PERSONAJES IDs --
    --ID 0 -> pajaro
    --ID 1 -> berenjeno
    --ID 2 -> misterioso
    --ID 3 -> ojos
    --ID 5 -> nadie
    */

    private void Awake()
    {
        instance = this;

        string descPajaro = "Hola que tal soy un pajaro";
        string descBerenjeno = "berenjeno";
        string descMisterioso = "ey yeye";
        string descOjos = "O.O";

        _pajaro = new Character(0, "pajaro", false, _pajaroSprite, _pajaroIcon, descPajaro, 100);
        _berenjeno = new Character(1, "berenjeno", false, _berenjenoSprite, _berenjenoIcon, descBerenjeno, 150);
        _misterioso = new Character(2, "misterioso", false, _misteriosoSprite, _misteriosoIcon, descMisterioso, 50);
        _ojos = new Character(3, "ojos", false, _ojosSprite, _ojosIcon, descOjos, 50);

        characterList = new List<Character>() { _pajaro, _berenjeno, _misterioso, _ojos};
    }

    private void Start()
    {
        _btnList = new List<Button>() { _pajaroBtn, _berenjenoBtn, _misteriosoBtn, _ojosBtn };

        //Boton de cada personaje, envia su ID al metodo
        _pajaroBtn.onClick.AddListener(delegate { BtnHandler(_pajaro); });
        _berenjenoBtn.onClick.AddListener(delegate { BtnHandler(_berenjeno); });
        _misteriosoBtn.onClick.AddListener(delegate { BtnHandler(_misterioso); });
        _ojosBtn.onClick.AddListener(delegate { BtnHandler(_ojos); });
    }

    private void BtnHandler(Character character)
    {
        CharacterInfo_UI.instance.ShowCharacterInfo(character._id);

        if (character.selected == false)
            TeamComp.instance.SelectCharacter(character._id);

        character.selected = true;
    }

    public void MakeCharacterAvailable(int characterId)
    {
        characterList[characterId].selected = false;
    }
}
