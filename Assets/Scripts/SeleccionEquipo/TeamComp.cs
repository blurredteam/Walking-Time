using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class TeamComp : MonoBehaviour
{
    public static TeamComp instance;
    
    [SerializeField] private Button _selectedBtn0;
    [SerializeField] private Button _selectedBtn1;
    [SerializeField] private Button _selectedBtn2;
    [SerializeField] private Button _selectedBtn3;
    [SerializeField] private Image _defaultImg;

    [SerializeField] private List<TextMeshProUGUI> _skillsTxt;

    [SerializeField] public TextMeshProUGUI _energyTxt;
    [SerializeField] public TextMeshProUGUI _waterTxt;

    [SerializeField] private Button _continueBtn;

    private List<bool> _slotAvailable;  //Indica si la posicion esta libre o no
    private List<int> _slotCharacterId; //Indica el ID del personaje de cada posicion (id: -1 si esta vacia)
    private List<Button> _slotButtons;  //Sirven para desseleccionar personajes
    public List<Character> _teamComp = new List<Character>() { null, null, null, null }; //Lista de integrantes del equipo

    public int _teamMaxEnergy { get; set; }
    public int _teamCurrentEnergy { get; set; }
    public int _teamMaxWater { get; set; } = 3;
    public int _teamCurrentWater { get; set; }

    //Variables para hoguera
    public bool bonfireTile = false;
    private float energyPercent;     //indica el porcentaje de energia con relacion a la enegia maxima del equipo
    
    

    private void Start()
    {
        instance = this;
        AudioManager.instance.OnSelection();
        _waterTxt.text = "3"; //Agua por defecto

        _slotAvailable = new List<bool>() { true, true, true, true};
        _slotCharacterId = new List<int>() { -1, -1, -1, -1 };
        _slotButtons = new List<Button>() { _selectedBtn0, _selectedBtn1, _selectedBtn2, _selectedBtn3 };

        if (GameManager.instance.team.Count > 0) { BonfireTile(GameManager.instance.team); bonfireTile = true; return; }

        _slotButtons[0].onClick.AddListener(delegate { RemoveSelected(0); });
        _slotButtons[1].onClick.AddListener(delegate { RemoveSelected(1); });
        _slotButtons[2].onClick.AddListener(delegate { RemoveSelected(2); });
        _slotButtons[3].onClick.AddListener(delegate { RemoveSelected(3); });
    }

    private void Update()
    {
        ManageResourceText();
        CheckReady();
    }

    public void SelectCharacter(int characterId)
    {
        Character selectedCharacter = CharacterManager.instance.characterList[characterId];

        for (int i = 0; i < _slotAvailable.Count; i++)
        {
            if (_slotAvailable[i] == true)
            {
                //Se gestiona la informacion por pantalla 
                _slotAvailable[i] = false;
                _slotCharacterId[i] = characterId;
                _slotButtons[i].image.sprite = selectedCharacter.icon.sprite;
                _skillsTxt[i].text = selectedCharacter.skillName;
                _teamMaxEnergy += selectedCharacter.energy;

                //Se gestiona la informacion del personaje
                selectedCharacter.selected = true;
                _teamComp[i] = selectedCharacter;

                //Se gestiona si es hoguera
                if (bonfireTile)
                {
                    var aux = selectedCharacter.energy * energyPercent;
                    _teamCurrentEnergy += (int)aux; 
                }
                else
                {
                    _teamCurrentEnergy = _teamMaxEnergy;
                    _teamCurrentWater = _teamMaxWater;
                }

                ApplySkills();

                break;
            }
        }
    }

    private void ApplySkills()
    {
        foreach (Character c in _teamComp)
        {
            if (c != null)
            {
                c._map = LevelManager.instance._map;
                c.Skill();
            }
        }
    }

    private void RemoveSelected(int position)
    {
        AudioManager.instance.ButtonSound3();
        //Si se quita un personaje el equipo no esta lleno
        _continueBtn.onClick.RemoveAllListeners();  

        if (_slotCharacterId[position] == -1) return;

        //Se gestiona informacion en pantalla
        _teamMaxEnergy -= _teamComp[position].energy;
        _slotAvailable[position] = true;
        _slotCharacterId[position] = -1;
        _slotButtons[position].image.sprite = _defaultImg.sprite;
        _skillsTxt[position].text = " - - - ";

        //Se gestiona si es hoguera
        if(bonfireTile)
        {
            foreach (Button btn in _slotButtons) btn.onClick.RemoveAllListeners();
            var aux = _teamComp[position].energy * energyPercent;
            _teamCurrentEnergy -= (int)aux;
        }

        //Se gestiona la informacion del personaje y equipo
        _teamComp[position].RevertSkill();
        _teamComp[position].selected = false;
        _teamComp[position] = null;
    }

    public void BonfireTile(List<Character> team)
    {
        AudioManager.instance.OnSelection();
        _teamCurrentWater = GameManager.instance.water;
        _teamMaxWater = GameManager.instance.maxWater;
        _teamCurrentEnergy = GameManager.instance.energy;
        _teamMaxEnergy = GameManager.instance.maxEnergy;
        energyPercent = (float)_teamCurrentEnergy / (float)_teamMaxEnergy;

        // Pone el equipo actual en la seleccion
        for (int i = 0; i < team.Count; i++)
        {
            //Se gestiona info por pantalla
            _slotAvailable[i] = false;
            _slotCharacterId[i] = team[i]._id;
            _slotButtons[i].image.sprite = team[i].icon.sprite;
            _skillsTxt[i].text = team[i].skillName;

            //Se gestiona info del personaje
            CharacterManager.instance.characterList[team[i]._id].selected = true;
            CharacterManager.instance.characterList[team[i]._id].skillApplied = true;
            _teamComp[i] = team[i];
            _teamComp[i].selected = true;
            _teamComp[i].skillApplied = true;
        }

        _slotButtons[0].onClick.AddListener(delegate { RemoveSelected(0); });
        _slotButtons[1].onClick.AddListener(delegate { RemoveSelected(1); });
        _slotButtons[2].onClick.AddListener(delegate { RemoveSelected(2); });
        _slotButtons[3].onClick.AddListener(delegate { RemoveSelected(3); });
    }

    private void Continue()
    {
        foreach (Button b in _slotButtons) b.interactable = false;
        AudioManager.instance.ButtonSound();
        _continueBtn.interactable = false;

        foreach (Character c in _teamComp)
        {
            c._team = _teamComp;
            c.SkillFinally();           //Para habilidades que se aplican tras elegir el equipo (como Fausto)
        }
        
        StartCoroutine(ContinueTimer());
    }

    //Sirve para visualizar las habilidades que se aplican al final (SkillFinally)
    private IEnumerator ContinueTimer()
    {
        yield return new WaitForSeconds(1); // En este primer yield se visualizan las habilidades (por implementar)
        CharacterManager.instance.transition.DoTransitionTwice();
        yield return new WaitForSeconds(1);

        Level_UI.instance.SetTeamUI(_teamComp);
        Level_UI.instance.StartImage();

        LevelManager.instance.SetTeam(_teamComp);
        LevelManager.instance.teamEnergy = _teamCurrentEnergy;
        LevelManager.instance.maxEnergy = _teamMaxEnergy;
        LevelManager.instance.teamWater = _teamCurrentWater;
        LevelManager.instance.maxWater = _teamMaxWater;
        LevelManager.instance.StartGame();
        LevelManager.instance.ActivateScene();

        ScenesManager.instance.UnloadTeamSelect();

        AudioManager.instance.PlayAmbient();
    }

    private void ManageResourceText()
    {
        if (bonfireTile)
        {
            _energyTxt.text = _teamCurrentEnergy.ToString() + "/" + _teamMaxEnergy.ToString();
            _waterTxt.text = _teamCurrentWater.ToString() + "/" + _teamMaxWater.ToString();
        }
        else
        {
            _energyTxt.text = _teamMaxEnergy.ToString();
            _waterTxt.text = _teamMaxWater.ToString();
        }

        foreach (Character c in _teamComp)
        {
            if (c == null) return;
            else if (c.name == "Fausto")
            {
                if(c.energy == 0 || (bonfireTile && c.energy == 0))
                {
                    _energyTxt.text += "?";
                    _waterTxt.text += "?";
                }
            }
        }
    }

    private void CheckReady()
    {
        foreach (bool available in _slotAvailable)
        {
            if (available)
            {
                _continueBtn.onClick.RemoveAllListeners();
                return;
            }
        }

        _continueBtn.onClick.RemoveAllListeners();
        _continueBtn.onClick.AddListener(Continue);
    }

    //Para el boton del tuto
    public void ButtonTuto()
    {
        AudioManager.instance.ButtonSound();
    }
}
