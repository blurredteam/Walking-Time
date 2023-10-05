using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Personajes : MonoBehaviour
{
    public static Personajes instance;

    [SerializeField] private Button _characterBtn1;
    [SerializeField] private Button _characterBtn2;
    [SerializeField] private Button _characterBtn3;
    [SerializeField] private Button _characterBtn4;
    [SerializeField] private Button _characterBtn5;

    //private Image _characterImg1 = Assets_ref.instance._characterImg1;
    //private Image _characterImg2 = Assets_ref.instance._characterImg2;
    //private Image _characterImg3 = Assets_ref.instance._characterImg3;
    //private Image _characterImg4 = Assets_ref.instance._characterImg4;
    //private Image _characterImg5 = Assets_ref.instance._characterImg5;


    private void Start()
    {
        instance= this;

        _characterBtn1.onClick.AddListener(delegate { BtnHandler("personaje1"); });
        _characterBtn2.onClick.AddListener(delegate { BtnHandler("personaje2"); });
        _characterBtn3.onClick.AddListener(delegate { BtnHandler("personaje3"); });
        _characterBtn4.onClick.AddListener(delegate { BtnHandler("personaje4"); });
        _characterBtn5.onClick.AddListener(delegate { BtnHandler("personaje5"); });
    }

    private void BtnHandler(string personaje)
    {
        int selected = 0;

        switch (personaje)
        {
            case "personaje1":
                selected = 1;
                _characterBtn1.enabled = false;
                break;
            case "personaje2":
                selected = 2;
                _characterBtn2.enabled = false;
                break;
            case "personaje3":
                selected = 3;
                _characterBtn3.enabled = false;
                break;
            case "personaje4":
                selected = 4;
                _characterBtn4.enabled = false;
                break;
            case "personaje5":
                selected = 5;
                _characterBtn5.enabled = false;
                break;
        }

        Equipo.instance.Team(selected);
    }

    public void MakeCharacterAvailable(Color color)
    {
        Debug.Log(color);
        Debug.Log(Color.yellow);

        if(color == Color.white)
        {
            _characterBtn1.enabled = true;
        }
        else if(color == Color.red) 
        {
            _characterBtn2.enabled = true;
        }
        else if (color == Color.blue)
        {
            _characterBtn3.enabled = true;
        }
        else if (color == Color.black)
        {
            _characterBtn4.enabled = true;
        }
        else if (color == Color.green)
        {
            _characterBtn5.enabled = true;
        }
    }
}
