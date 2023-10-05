using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assets_ref : MonoBehaviour
{
    public static Assets_ref instance;

    // -- ASSETS DEL EQUIPO SELECCIONADO -- 
    [SerializeField] public Button _selectedBtn1 { get; set; }
    [SerializeField] public Button _selectedBtn2;
    [SerializeField] public Button _selectedBtn3;
    [SerializeField] public Button _selectedBtn4;

    [SerializeField] public Image _selectedImg1;
    [SerializeField] public Image _selectedImg2;
    [SerializeField] public Image _selectedImg3;
    [SerializeField] public Image _selectedImg4;

    // -- ASSETS PERSONAJES -- 
    [SerializeField] public Button _characterBtn1;
    [SerializeField] public Button _characterBtn2;
    [SerializeField] public Button _characterBtn3;
    [SerializeField] public Button _characterBtn4;
    [SerializeField] public Button _characterBtn5;

    [SerializeField] public Image _characterImg1;
    [SerializeField] public Image _characterImg2;
    [SerializeField] public Image _characterImg3;
    [SerializeField] public Image _characterImg4;
    [SerializeField] public Image _characterImg5;

    // -- ASSETS DE INFO --

    private void Awake()
    {
        instance = this;
    }

}
