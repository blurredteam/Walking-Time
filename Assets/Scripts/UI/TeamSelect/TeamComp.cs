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

    private List<bool> _slotAvailable;
    private List<int> _slotCharacterId;
    private List<Button> _slotButtons;
    private List<Character> _teamComp = new List<Character>() { null, null, null, null};

    private int _totalEnergy = 0;

    private void Start()
    {
        instance = this;

        _slotAvailable = new List<bool>() { true, true, true, true};
        _slotCharacterId = new List<int>() { -1, -1, -1, -1 };
        _slotButtons = new List<Button>() { _selectedBtn0, _selectedBtn1, _selectedBtn2, _selectedBtn3 };

        _slotButtons[0].onClick.AddListener(delegate { RemoveSelected(0); });
        _slotButtons[1].onClick.AddListener(delegate { RemoveSelected(1); });
        _slotButtons[2].onClick.AddListener(delegate { RemoveSelected(2); });
        _slotButtons[3].onClick.AddListener(delegate { RemoveSelected(3); });
    }

    public void SelectCharacter(int characterId)
    {
        Character selectedCharacter = CharacterManager.instance.characterList[characterId];
        //var charEnergy = CharacterManager.instance.characterList[characterId].energy;

        for (int i = 0; i < _slotAvailable.Count; i++)
        {
            if (_slotAvailable[i] == true)
            {
                _slotAvailable[i] = false;

                _teamComp[i] = CharacterManager.instance.characterList[characterId];

                _slotCharacterId[i] = characterId;
                _slotButtons[i].image.sprite = selectedCharacter.sprite.sprite;
                _totalEnergy += selectedCharacter.energy;
                _totalEnergyTxt.text = _totalEnergy.ToString();
                
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
        _totalEnergyTxt.text = _totalEnergy.ToString();

        _slotCharacterId[position] = -1;
        _teamComp[position] = null;
    }
}
