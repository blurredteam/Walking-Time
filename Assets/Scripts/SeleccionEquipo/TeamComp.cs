using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
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
    private float energyPercent = 1; //Para hoguera
    private bool bonfireTile = false;

    private void Start()
    {
        instance = this;

        _waterTxt.text = "3";

        _slotAvailable = new List<bool>() { true, true, true, true};
        _slotCharacterId = new List<int>() { -1, -1, -1, -1 };
        _slotButtons = new List<Button>() { _selectedBtn0, _selectedBtn1, _selectedBtn2, _selectedBtn3 };

        if (LevelManager.instance._team.Count > 0) { BonfireTile(LevelManager.instance._team); bonfireTile = true; return; }

        _slotButtons[0].onClick.AddListener(delegate { RemoveSelected(0); });
        _slotButtons[1].onClick.AddListener(delegate { RemoveSelected(1); });
        _slotButtons[2].onClick.AddListener(delegate { RemoveSelected(2); });
        _slotButtons[3].onClick.AddListener(delegate { RemoveSelected(3); });
    }

    private void Update()
    {
        ManageResourceText();
    }

    public void SelectCharacter(int characterId)
    {
        Character selectedCharacter = CharacterManager.instance.characterList[characterId];

        foreach (int slot in _slotCharacterId) { if (slot == characterId) return; }

        for (int i = 0; i < _slotAvailable.Count; i++)
        {
            if (_slotAvailable[i] == true)
            {
                _slotAvailable[i] = false;

                _teamComp[i] = CharacterManager.instance.characterList[characterId];
                selectedCharacter.selected = true;

                _slotCharacterId[i] = characterId;
                _slotButtons[i].image.sprite = selectedCharacter.icon.sprite;
                _skillsTxt[i].text = selectedCharacter.skillDesc;

                _teamMaxEnergy += selectedCharacter.energy;

                if (bonfireTile)
                {
                    var currentEnergy = selectedCharacter.energy * energyPercent;
                    _teamCurrentEnergy += (int)currentEnergy;
                }

                if (bonfireTile) _teamComp[i].Skill();

                ApplySkills();
                CheckReady();

                break;

            }
            else if (i == 3 && _slotAvailable[i] == false) 
            {
                selectedCharacter.selected = false;
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

    private void CheckReady()
    {
        if (bonfireTile) { _continueBtn.onClick.AddListener(ExitBonfire); return; }

        foreach (bool available in _slotAvailable)
        {
            if (available) { _continueBtn.onClick.RemoveAllListeners(); break; }
            _continueBtn.onClick.RemoveAllListeners();
            _continueBtn.onClick.AddListener(Continue);
        }
    }

    private void RemoveSelected(int position)
    {
        if (_slotCharacterId[position] == -1) return;

        _teamMaxEnergy -= _teamComp[position].energy;

        if (bonfireTile)
        {
            var currentEnergy = _teamComp[position].energy * energyPercent;
            _teamCurrentEnergy -= (int)currentEnergy;
        }

        _teamComp[position].RevertSkill();

        _slotAvailable[position] = true;
        _slotButtons[position].image.sprite = _defaultImg.sprite;
        _skillsTxt[position].text = " - - - ";

        _teamComp[position].selected = false;
        _slotCharacterId[position] = -1;
        _teamComp[position] = null;

        _continueBtn.onClick.RemoveAllListeners();
    }

    private void Continue()
    { 
        _continueBtn.interactable = false;

        _teamMaxEnergy = 0;
        foreach(Character c in _teamComp)
        {
            c._team = _teamComp;
            if (c.name == "Fausto") c.SkillFinally();
            _teamMaxEnergy += c.energy;
        }

        LevelManager.instance.teamEnergy = _teamMaxEnergy;
        LevelManager.instance.maxEnergy = _teamMaxEnergy;
        LevelManager.instance.teamWater = _teamMaxWater;
        LevelManager.instance.maxWater = _teamMaxWater;

        //Esta corrutina esta para visualizar como se van aplicando las hablidades de cada personaje una a una
        //sobre la energia o el agua del equipo antes de empezar la partida, mostrar visualmente las habilidades
        StartCoroutine(ContinueTimer());
    }

    private IEnumerator ContinueTimer()
    { 
        yield return new WaitForSeconds(1);

        LevelManager.instance.ActivateScene();
        LevelManager.instance.SetTeam(_teamComp);
        LevelManager.instance.StartGame();
        ScenesManager.instance.UnloadTeamSelect();
    }

    public void BonfireTile(List<Character> team)
    {
        _teamCurrentWater = LevelManager.instance.teamWater;
        _teamCurrentEnergy = LevelManager.instance.teamEnergy;
        _teamMaxWater = LevelManager.instance.maxWater;

        // Pone el equipo actual en la seleccion
        for (int i= 0; i < team.Count; i++)
        {
            _slotAvailable[i] = false;

            _teamComp[i] = CharacterManager.instance.characterList[team[i]._id];
            _teamComp[i].skillApplied= true;

            _slotCharacterId[i] = team[i]._id;
            _slotButtons[i].image.sprite = team[i].icon.sprite;
            _teamMaxEnergy += team[i].energy;
        }
        //foreach (Character c in _teamComp) c.Skill();

        energyPercent = (float)LevelManager.instance.teamEnergy / (float)_teamMaxEnergy;

        _slotButtons[0].onClick.AddListener(delegate { RemoveBonfire(0); });
        _slotButtons[1].onClick.AddListener(delegate { RemoveBonfire(1); });
        _slotButtons[2].onClick.AddListener(delegate { RemoveBonfire(2); });
        _slotButtons[3].onClick.AddListener(delegate { RemoveBonfire(3); });
        _continueBtn.onClick.AddListener(ExitBonfire);
    }

    private void RemoveBonfire(int position)
    {
        RemoveSelected(position);
        foreach (Button btn in _slotButtons) btn.onClick.RemoveAllListeners();
    }

    private void ExitBonfire()
    {
        float teamFinalEnergy = _teamMaxEnergy * energyPercent;

        LevelManager.instance.ActivateScene();
        LevelManager.instance.teamEnergy = (int)_teamCurrentEnergy;
        Debug.Log(LevelManager.instance.teamEnergy + ", " + _teamCurrentEnergy);
        LevelManager.instance.teamWater= _teamCurrentWater;
        LevelManager.instance.maxWater = _teamMaxWater;
        LevelManager.instance.SetTeam(_teamComp);

        ScenesManager.instance.UnloadTeamSelect();
    }

    private void ManageResourceText()
    {
        if (bonfireTile)
        {
            _energyTxt.text = _teamCurrentEnergy.ToString() + " / " + _teamMaxEnergy.ToString();
            _waterTxt.text = _teamCurrentWater.ToString() + " / " + _teamMaxWater.ToString();
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
                if(c.energy == 0)
                {
                    _energyTxt.text += "?";
                    _waterTxt.text += "?";
                }
            }
        }
    }
}
