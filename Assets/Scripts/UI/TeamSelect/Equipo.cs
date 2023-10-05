using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Equipo : MonoBehaviour
{
    public static Equipo instance;

    [SerializeField] private Button _selectedBtn1;
    [SerializeField] private Button _selectedBtn2;
    [SerializeField] private Button _selectedBtn3;
    [SerializeField] private Button _selectedBtn4;

    [SerializeField] private Image _selectedImg1;
    [SerializeField] private Image _selectedImg2;
    [SerializeField] private Image _selectedImg3;
    [SerializeField] private Image _selectedImg4;

    [SerializeField] private Image _characterImg1;
    [SerializeField] private Image _characterImg2;
    [SerializeField] private Image _characterImg3;
    [SerializeField] private Image _characterImg4;
    [SerializeField] private Image _characterImg5;

    private bool _slotAvailable1 = true;
    private bool _slotAvailable2 = true;
    private bool _slotAvailable3 = true;
    private bool _slotAvailable4 = true;

    private void Start()
    {
        instance = this;

        _selectedBtn1.onClick.AddListener(delegate { RemoveSelected(1); });
        _selectedBtn2.onClick.AddListener(delegate { RemoveSelected(2); });
        _selectedBtn3.onClick.AddListener(delegate { RemoveSelected(3); });
        _selectedBtn4.onClick.AddListener(delegate { RemoveSelected(4); });
    }

    public void Team(int personaje)
    {
        Image selectedCharacterImg = null;
        Color color = Color.white;

        switch (personaje)
        {
            case 1:
                selectedCharacterImg = _characterImg1;
                color = Color.white;
                break;
            case 2:
                selectedCharacterImg = _characterImg2;
                color = Color.red;
                break;
            case 3:
                selectedCharacterImg = _characterImg3;
                color = Color.blue;
                break;
            case 4:
                selectedCharacterImg = _characterImg4;
                color = Color.black;
                break;
            case 5:
                selectedCharacterImg = _characterImg5;
                color = Color.green;
                break;
        }

        if( _slotAvailable1)
        {
            _selectedImg1.color = selectedCharacterImg.color;
            _slotAvailable1= false;
        }
        else if(_slotAvailable2)
        {
            _selectedImg2.color = selectedCharacterImg.color;
            _slotAvailable2= false;
        }
        else if(_slotAvailable3)
        {
            _selectedImg3.color = selectedCharacterImg.color;
            _slotAvailable3= false;
        }
        else if(_slotAvailable4)
        {
            _selectedImg4.color = selectedCharacterImg.color;
            _slotAvailable4 = false;
        }
        else
        {
            Debug.Log("No ma");
            Personajes.instance.MakeCharacterAvailable(selectedCharacterImg.color);
        }

    }

    private void RemoveSelected(int position)
    {
        switch (position)
        {
            case 1:
                Personajes.instance.MakeCharacterAvailable(_selectedImg1.color);
                _selectedImg1.color = Color.white;
                _slotAvailable1 = true;
                break;
            case 2:
                Personajes.instance.MakeCharacterAvailable(_selectedImg2.color);
                _selectedImg2.color = Color.white;
                _slotAvailable2 = true;
                break;
            case 3:
                Personajes.instance.MakeCharacterAvailable(_selectedImg3.color);
                _selectedImg3.color = Color.white;
                _slotAvailable3 = true;
                break;
            case 4:
                Personajes.instance.MakeCharacterAvailable(_selectedImg4.color);
                _selectedImg4.color = Color.white;
                _slotAvailable4 = true;
                break;
        }
    }
}
