
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    // Lista con todos los puzzles
    public List<string> puzzleScenes = new List<string>() { "PuzleCuadro", "PuzleTimer", "PuzzleFuente" };

    private void Awake()
    {
        instance = this;

    }

    /*
    -- Enum de todas las escenas del juego 
    -- Diferenciar entre escenas y pantallas (p.e settings no es una escena, sino una interfaz de la escena Mainmenu)
    */
    public enum Scene
    {
        MainMenu_Scene,
        TeamSelect,
        Level1,
        PuzleCuadro,
        PuzleTimer,
        PuzzleFuente,
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadTileScene(int type)
    {
        //SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
        // Type =0 -> puzzle; =1 -> event; =2 -> bonfire 

        if (type == 0)
        {
            LoadPuzzle();
        }
        else if (type == 1)
        {
            LoadObstacle();
        }
        else
        {
            LoadBonfire();
        }

    }

    public void UnloadTile(Scene scene)
    {
        SceneManager.UnloadSceneAsync(scene.ToString());
    }

    public void UnloadTeamSelect()
    {
        SceneManager.UnloadSceneAsync(Scene.TeamSelect.ToString());
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(Scene.Level1.ToString());
        SceneManager.LoadScene(Scene.TeamSelect.ToString(), LoadSceneMode.Additive);

    }

    private void LoadPuzzle()
    {
        int selectPuzle = Random.Range(0, puzzleScenes.Count);
        SceneManager.LoadScene(puzzleScenes[selectPuzle], LoadSceneMode.Additive);
    }
    private void LoadObstacle() { Debug.Log("casilla evento"); }
    private void LoadBonfire() 
    {
        SceneManager.LoadScene(Scene.TeamSelect.ToString(), LoadSceneMode.Additive);

        Debug.Log("Casilla hoguera"); 
    }

}
