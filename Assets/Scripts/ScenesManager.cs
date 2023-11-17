
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    public Animator transition;

    public float transitionTime = 1f;

    // Lista con todos los puzzles
    public List<string> escenasPuzle = new List<string>() {
        "PuzzleFinder", "PuzleSumarFiguras", "PuzzleHielo", "PuzleCuadro", "PuzleTimer",
        "PuzzleFuente", "NivelGeometryDash",
    };

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
        EscenaMenu,
        SeleccionEquipo,
        Level1,
        PuzleCuadro,
        PuzleTimer,
        PuzzleFuente,
        PuzzleFinder,
        NivelGeometryDash,
        PuzleSumarFiguras,
        PuzzleHielo,
        Hoguera,
        EndScene,
        EventScene,
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    public void LoadTileScene(int type, int index)
    {
        //SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
        // Type =0 -> puzzle; =1 -> event; =2 -> bonfire 

        if (type == 0)
        {
            LoadPuzzle(index);
        }
        else if (type == 1)
        {
            LoadObstacle(index);
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
        SceneManager.UnloadSceneAsync(Scene.SeleccionEquipo.ToString());
    }

    public void StartNewGame()
    {
        StartCoroutine(StartNewGameTransition());
        
        // SceneManager.LoadScene(Scene.Level1.ToString());
        // SceneManager.LoadScene(Scene.SeleccionEquipo.ToString(), LoadSceneMode.Additive);

    }
    
    IEnumerator StartNewGameTransition()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(Scene.Level1.ToString());
        SceneManager.LoadScene(Scene.SeleccionEquipo.ToString(), LoadSceneMode.Additive);
    }

    private void LoadPuzzle(int index)
    {
        SceneManager.LoadScene(escenasPuzle[index], LoadSceneMode.Additive);
        //SceneManager.LoadScene(Scene.PuzzleFuente.ToString(), LoadSceneMode.Additive);
    }
    private void LoadObstacle(int index)
    {
        SceneManager.LoadScene(Scene.EventScene.ToString(), LoadSceneMode.Additive);
        Debug.Log("casilla evento");
    }
    private void LoadBonfire()
    {
        SceneManager.LoadScene(Scene.Hoguera.ToString(), LoadSceneMode.Additive);

        Debug.Log("Casilla hoguera");
    }
    
    

}
