using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TeamSelect : MonoBehaviour
{
    // - Es una escena
    // - Se activa al empezar la partida y deja montar el equipo 
    // - Se activa en las hogueras y deja cambiar un personaje
    // - En el mapa se puede acceder a la interfaz pero sin cambiar personajes

    // - Boton de continuar 
    [SerializeField] private Button _continueBtn;

    void Start()
    {
        _continueBtn.onClick.AddListener(Continue);
    }

    private void Continue()
    {
        ScenesManager.instance.UnloadTeamSelect();
    }
}
