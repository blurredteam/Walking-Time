using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    private void Awake()
    {
        instance = this;
    }

    /*
    -- Lista de todas las escenas del juego 
    -- Diferenciar entre escenas y pantallas (p.e settings no es una escena, sino una interfaz de la escena Mainmenu)
    */
    public enum Scene
    {
        MainMenu_Scene,
        TeamSelect,
        Level_1,
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(Scene.Level_1.ToString());
        SceneManager.LoadScene(Scene.TeamSelect.ToString(), LoadSceneMode.Additive);
    
    }

    public void UnloadTeamSelect()
    {
        SceneManager.UnloadSceneAsync(Scene.TeamSelect.ToString());
    }

    public void PrintActiveScenes()
    {
        Debug.Log(SceneManager.sceneCount.ToString());
        Debug.Log(SceneManager.GetActiveScene().name);
    }

}
