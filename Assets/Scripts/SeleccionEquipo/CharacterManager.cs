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

    [SerializeField] private GameObject characterCard;

    [SerializeField] private Button showInfo;   //Boton para darle la vuelta a la carta

    //Las 5 listas siguientes necesitan ir en orden del ID del personaje
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

    private bool personajeUnlocked = false;

    /* 
    -- PERSONAJES IDs --
    --ID 0 -> berenjeno
    --ID 1 -> japaro
    --ID 2 -> mirabel
    --ID 3 -> seta
    --ID 4 -> fauno
    --ID 5 -> chispa
    */
    
    public Transitioner transition;
    //[SerializeField] private Button continueButon; 

    private void Awake()
    {
        transition = ScenesManager.instance.transitioner;
    
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

        //se llama cuando se inicia la escena de seleccion de personaje para lockear/unlockear a chispa
        desbloqueaPersonaje();

        //Boton de cada personaje
        _btnList[0].onClick.AddListener(delegate { AudioManager.instance.ButtonSound2(); BtnHandler(_berenjeno); });
        _btnList[1].onClick.AddListener(delegate { AudioManager.instance.ButtonSound2(); BtnHandler(_japaro); });
        _btnList[2].onClick.AddListener(delegate { AudioManager.instance.ButtonSound2(); BtnHandler(_mirabel); });
        _btnList[3].onClick.AddListener(delegate { AudioManager.instance.ButtonSound2(); BtnHandler(_seta); });
        _btnList[4].onClick.AddListener(delegate { AudioManager.instance.ButtonSound2(); BtnHandler(_fauno); });
        _btnList[5].onClick.AddListener(delegate { BlockedSound(); BtnHandler(_chispa); });

        for (int i = 0; i < _btnList.Count; i++)
        {
            if (characterList[i].unlocked == false)
            {
                _btnList[i].GetComponent<Image>().color = new Color32(126, 126, 126, 255);
            }
        }


    }

    public void DoFadeTransition()
    {
        StartCoroutine(DoFadeTransitionCo());
    }

    IEnumerator DoFadeTransitionCo()
    {
        //continueButon.enabled = false;
        transition.DoTransitionOnce();
        
        yield return new WaitForSeconds(2f);
        //continueButon.enabled = true;
        transition.DoTransitionOnce();
    }

    private void BtnHandler(Character character)
    {
        if (character.unlocked) ShowCharacter(character._id);

        if (character.unlocked && !character.selected) TeamComp.instance.SelectCharacter(character._id);
    }

    public void ShowCharacter(int id)
    {
        characterCard.SetActive(true);
        Image _characterCard = characterCard.GetComponent<Image>();
        _characterCard.sprite = characterList[id].frontCard.sprite;

        showInfo.onClick.RemoveAllListeners();
        showInfo.onClick.AddListener(delegate { AudioManager.instance.ButtonSound4(); ShowCharacterInfo(id, _characterCard); });
    }

    private void ShowCharacterInfo(int id, Image _characterCard)
    {
        if (_characterCard.sprite == characterList[id].frontCard.sprite)
            _characterCard.sprite = characterList[id].backCard.sprite;
        else
            _characterCard.sprite = characterList[id].frontCard.sprite;
    }


    public void desbloqueaPersonaje()
    {
        personajeUnlocked = UnlockManager.Instance.PersonajeDesbloqueado;   //recivimos el valor desde el singleton

        if (personajeUnlocked == false)
        {
            _chispa.unlocked = false;
        }
        else if (personajeUnlocked == true)
        {
            _chispa.unlocked = true;
            Debug.Log("Chispa ha sido desbloqueada correctamente");
        }

    }

    private void BlockedSound()
    {
        if (_chispa.unlocked)
        {
            AudioManager.instance.ButtonSound2();
        }
        else
        {
            AudioManager.instance.ButtonSoundBlock();
        }
    }
}
