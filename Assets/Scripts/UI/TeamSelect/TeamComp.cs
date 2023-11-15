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

    [SerializeField] private TextMeshProUGUI _totalEnergyTxt;

    [SerializeField] private Button _continueBtn;

    private List<bool> _slotAvailable;
    private List<int> _slotCharacterId;
    private List<Button> _slotButtons;
    public List<Character> _teamComp = new List<Character>() { null, null, null, null };

    public int _teamMaxEnergy { get; set; }
    public int _teamCurrentEnergy { get; set; }
    public int _teamMaxWater { get; set; } = 3;
    public int _teamCurrentWater { get; set; }
    private float energyPercent = 1; //Para hoguera
    private bool bonfireTile = false;

    private bool ready { get; set; } = false;

    private void Start()
    {
        instance = this;

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
        if (ready && !bonfireTile)
        {
            _continueBtn.onClick.RemoveAllListeners();
            _continueBtn.onClick.AddListener(Continue);
            ready = false;
            return;
        }
    }

    public void SelectCharacter(int characterId)
    {
        Character selectedCharacter = CharacterManager.instance.characterList[characterId];
        //var charEnergy = CharacterManager.instance.characterList[characterId].energy;

        foreach (int slot in _slotCharacterId) { if (slot == characterId) return; }

        for (int i = 0; i < _slotAvailable.Count; i++)
        {
            if (_slotAvailable[i] == true)
            {
                _slotAvailable[i] = false;

                _teamComp[i] = CharacterManager.instance.characterList[characterId];

                _slotCharacterId[i] = characterId;
                _slotButtons[i].image.sprite = selectedCharacter.icon.sprite;

                _teamMaxEnergy += selectedCharacter.energy;
                float currentEnergy = _teamMaxEnergy * energyPercent;
                currentEnergy = (int)currentEnergy;
                _totalEnergyTxt.text = currentEnergy.ToString();

                if (bonfireTile) _teamComp[i].Skill();

                break;

            } else if (i == 3 && _slotAvailable[i] == false) 
            {
                CharacterManager.instance.MakeCharacterAvailable(characterId);
            }

        }

        foreach (bool x in _slotAvailable)
        {
            if (x == false) ready = true;
            else { ready = false; break; }
        }
    }

    private void RemoveSelected(int position)
    {
        if (_slotCharacterId[position] == -1) return;

        _slotAvailable[position] = true;
        _slotButtons[position].image.sprite = _defaultImg.sprite;
        CharacterManager.instance.MakeCharacterAvailable(_slotCharacterId[position]);

        _teamMaxEnergy -= _teamComp[position].energy;
        float currentEnergy = _teamMaxEnergy * energyPercent;
        currentEnergy = (int)currentEnergy;
        _totalEnergyTxt.text = currentEnergy.ToString();

        _slotCharacterId[position] = -1;
        _teamComp[position] = null;
    }
    private void Continue()
    {
        Debug.Log("Aplicando habilidades...");
        foreach (Character c in _teamComp)
        {
            c._map = LevelManager.instance._map;
            c.Skill();
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
        _totalEnergyTxt.text = _teamCurrentEnergy.ToString();

        // Pone el equipo actual en la seleccion
        for (int i= 0; i < team.Count; i++)
        {
            _slotAvailable[i] = false;

            _teamComp[i] = CharacterManager.instance.characterList[team[i]._id];

            _slotCharacterId[i] = team[i]._id;
            _slotButtons[i].image.sprite = team[i].sprite.sprite;
            _teamMaxEnergy += team[i].energy;
        }
        foreach (Character c in _teamComp) c.Skill();

        energyPercent = (float)LevelManager.instance.teamEnergy / (float)_teamMaxEnergy;

        _slotButtons[0].onClick.AddListener(delegate { RemoveBonfire(0); });
        _slotButtons[1].onClick.AddListener(delegate { RemoveBonfire(1); });
        _slotButtons[2].onClick.AddListener(delegate { RemoveBonfire(2); });
        _slotButtons[3].onClick.AddListener(delegate { RemoveBonfire(3); });
        _continueBtn.onClick.AddListener(ExitBonfire);
    }

    private void RemoveBonfire(int position)
    {
        _teamComp[position].RevertSkill();
        RemoveSelected(position);
        foreach (Button btn in _slotButtons) btn.onClick.RemoveAllListeners();
    }

    private void ExitBonfire()
    {
        float teamFinalEnergy = _teamMaxEnergy * energyPercent;

        LevelManager.instance.ActivateScene();
        LevelManager.instance.teamEnergy = (int)teamFinalEnergy;
        LevelManager.instance.teamWater= _teamCurrentWater;
        LevelManager.instance.maxWater = _teamMaxWater;
        LevelManager.instance.SetTeam(_teamComp);

        ScenesManager.instance.UnloadTeamSelect();
    }
}
