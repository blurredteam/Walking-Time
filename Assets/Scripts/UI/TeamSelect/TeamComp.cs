using System;
using System.Collections;
using System.Collections.Generic;
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
    public List<Character> _teamComp = new List<Character>() { null, null, null, null};

    private int _totalEnergy = 0;
    private float energyPercent = 1; //Para hoguera

    private void Start()
    {
        instance = this;

        _slotAvailable = new List<bool>() { true, true, true, true};
        _slotCharacterId = new List<int>() { -1, -1, -1, -1 };
        _slotButtons = new List<Button>() { _selectedBtn0, _selectedBtn1, _selectedBtn2, _selectedBtn3 };

        if (LevelManager.instance._team.Count > 0) // Para hogueras
        {
            BonfireTile(LevelManager.instance._team); 
            return; 
        }

        _slotButtons[0].onClick.AddListener(delegate { RemoveSelected(0); });
        _slotButtons[1].onClick.AddListener(delegate { RemoveSelected(1); });
        _slotButtons[2].onClick.AddListener(delegate { RemoveSelected(2); });
        _slotButtons[3].onClick.AddListener(delegate { RemoveSelected(3); });

        _continueBtn.onClick.AddListener(Continue);
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
                _slotButtons[i].image.sprite = selectedCharacter.sprite.sprite;

                _totalEnergy += selectedCharacter.energy;
                float currentEnergy = _totalEnergy * energyPercent;
                currentEnergy = (int)currentEnergy;
                _totalEnergyTxt.text = currentEnergy.ToString();
                
                break;

            } else if (i == 3 && _slotAvailable[i] == false) 
            {
                Debug.Log("No ma");
                CharacterManager.instance.MakeCharacterAvailable(characterId);
            }

        }
    }

    private void RemoveSelected(int position)
    {
        if (_slotCharacterId[position] == -1) return;

        _slotAvailable[position] = true;
        _slotButtons[position].image.sprite = _defaultImg.sprite;
        CharacterManager.instance.MakeCharacterAvailable(_slotCharacterId[position]);
        
        _totalEnergy -= _teamComp[position].energy;
        float currentEnergy = _totalEnergy * energyPercent;
        currentEnergy = (int)currentEnergy;

        _totalEnergyTxt.text = currentEnergy.ToString();

        _slotCharacterId[position] = -1;
        _teamComp[position] = null;
    }

    public void BonfireTile(List<Character> team)
    {
        for(int i= 0; i < team.Count; i++)
        {
            _slotAvailable[i] = false;

            _teamComp[i] = CharacterManager.instance.characterList[team[i]._id];

            _slotCharacterId[i] = team[i]._id;
            _slotButtons[i].image.sprite = team[i].sprite.sprite;
            _totalEnergy += team[i].energy;
        }
        _totalEnergyTxt.text = LevelManager.instance.teamEnergy.ToString();

        energyPercent = (float)LevelManager.instance.teamEnergy / (float)_totalEnergy;
        float currentEnergy = _totalEnergy * (float)energyPercent;
        

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

    private void Continue()
    {
        LevelManager.instance.SetTeam(_teamComp);
        LevelManager.instance.ActivateScene();

        ScenesManager.instance.UnloadTeamSelect();
    }

    private void ExitBonfire()
    {
        float teamFinalEnergy = _totalEnergy * energyPercent;

        LevelManager.instance.ActivateScene();
        LevelManager.instance.SetEnergy((int)teamFinalEnergy);

        ScenesManager.instance.UnloadTeamSelect();
    }
}
