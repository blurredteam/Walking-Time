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

    public Transitioner transitioner;

    private int nextWaterPuzzle;

    


    // Lista con todos los puzzles
    public List<string> puzzleScenes = new List<string>()
    {
        "PuzzleFinder", "PuzleSumarFiguras", "PuzzleHielo", "PuzleCuadro", "PuzleTimer", "NivelGeometryDash", "PuzzleLaberinto"
    };

    public List<string> waterScenes = new List<string>()
    {
        "PuzzleFuente", "PuzzleGotas"
    };

    private void Awake()
    {
        instance = this;
        nextWaterPuzzle = Random.Range(0, 2);

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
        Level2,
        PuzleCuadro,
        PuzleTimer,
        PuzzleFuente,
        PuzzleFinder,
        NivelGeometryDash,
        PuzleSumarFiguras,
        PuzzleHielo,
        PuzzleLaberinto,
        Hoguera,
        EndScene,
        EventScene,
        PuzzleGotas,
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadTileScene(int type, int index)
    {
        //SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
        // Type =0 -> puzzle; =1 -> event; =2 -> bonfire; =3 -> fuente

        if (type == 0) LoadPuzzle(index);
        else if (type == 1) LoadEvent();
        else if (type == 2) LoadBonfire();
        else if (type == 3) LoadWater();
    }

    public void UnloadTile(Scene scene)
    {
        SceneManager.UnloadSceneAsync(scene.ToString());
        LevelManager.instance.DesactivarLibro();

        UserPerformance.instance.UpdateResources();
    }

    public void UnloadTeamSelect()
    {
        SceneManager.UnloadSceneAsync(Scene.SeleccionEquipo.ToString());
    }

    public void StartNewGame()
    {
        StartCoroutine(StartNewGameTransition());
    }

    public void StartPanel(GameObject objeto)
    {
        StartCoroutine(StartPanelTransition(objeto));
    }

    IEnumerator StartNewGameTransition()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(Scene.Level1.ToString());
        SceneManager.LoadScene(Scene.SeleccionEquipo.ToString(), LoadSceneMode.Additive);
        transitioner.DoTransitionOnce();
        AudioManager.instance.OnLevel1();
    }
    
    IEnumerator StartPanelTransition(GameObject objeto)
    {
        transitioner.DoTransitionOnce();
        
        yield return new WaitForSeconds(transitionTime);

        objeto.SetActive(!objeto.activeSelf);
        transitioner.DoTransitionOnce();
    }

    private void LoadPuzzle(int index)
    {
        SceneManager.LoadScene(puzzleScenes[index], LoadSceneMode.Additive);
    }

    private void LoadEvent()
    {
        SceneManager.LoadScene(Scene.EventScene.ToString(), LoadSceneMode.Additive);
    }

    private void LoadBonfire()
    {
        SceneManager.LoadScene(Scene.Hoguera.ToString(), LoadSceneMode.Additive);
    }
    private void LoadWater()
    {
        //Si lo queremos aleatorio:
        nextWaterPuzzle = Random.Range(0, 2);
        SceneManager.LoadScene(waterScenes[nextWaterPuzzle], LoadSceneMode.Additive);
        if (nextWaterPuzzle == 0) { nextWaterPuzzle = 1; }
        else { nextWaterPuzzle = 0; }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(Scene.EndScene.ToString());

        //if (SceneManager.GetActiveScene().name == "Level1")
        //{
        //    AudioManager.instance.OnLevel2();
        //    SceneManager.LoadScene(Scene.Level2.ToString());
        //    SceneManager.LoadScene(Scene.SeleccionEquipo.ToString(), LoadSceneMode.Additive);
        //}
        //else if (SceneManager.GetActiveScene().name == "Level2")
        //{
        //    SceneManager.LoadScene(Scene.EndScene.ToString());
        //}
    }
    public void Destruir()
    {
        Destroy(gameObject);
    }

}
