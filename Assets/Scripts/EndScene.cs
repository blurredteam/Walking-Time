using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    public static EndScene instance;

    [SerializeField] private Button _menuBtn;
    [SerializeField] private Image _victoryImage;
    [SerializeField] private Image _defeatImage;
    [SerializeField] private TextMeshProUGUI _finalText;

    [Header("--------------User Performance--------------")]
    [SerializeField] private UserPerformance UserPerfManager;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _puzzlesPlayedText;

    [SerializeField] public GameObject PanelStats;
    [SerializeField] private Button BotonPanelStats;

    // STATS ----
    public int energyUsed;
    public int waterUsed;
    public int totalEvents;
    public int totalBonfires;


    private int _finalEnergy;

    private void Start()
    {
        instance = this;

        _menuBtn.onClick.AddListener(delegate
        {
            AudioManager.instance.LeaveGame();
            UserPerformance.instance.resetStats();
            
            ScenesManager.instance.LoadScene(ScenesManager.Scene.EscenaMenu);
            ControlPantallaCoompleta.instance.FinDeJuego();
        });

        _finalEnergy = GameManager.instance.energy;

        UserPerfManager = FindObjectOfType<UserPerformance>();

        DatabaseManager.instance.CreatePostSessions();
        UpdateUserPerformance();

        if (_finalEnergy > 0) Victory();
        else Defeat();
    }

    private void Victory()
    {
        _victoryImage.gameObject.SetActive(true);
        _finalText.text = "LLegaste a tu destino!";

        UnlockCharacters();     //cuando se alcanza la escena final se desbloquea automaticamente a chispa
    }

    private void Defeat()
    {
        _defeatImage.gameObject.SetActive(true);
        _finalText.text = "Faltaron fuerzas...";
    }

    public void UnlockCharacters()
    {
        UnlockManager.Instance.PersonajeDesbloqueado = true;
    }

    private void UpdateUserPerformance()
    {
        float tiempo = UserPerfManager.GetTimerValue();
        UserPerfManager.ChangeTimerState();
        float minutos = tiempo / 60f;
        minutos = Mathf.Round(minutos * 10f) / 10f; //redondeamos para mostrar solo 1 decimal

        _timerText.text = "La partida dur� " + minutos + " minutos";

        int oro = UserPerformance.instance.totalGoldGained;
        _goldText.text = "Obtuviste un total de " + oro + " oro";

        int jugados = UserPerformance.instance.puzzlesPlayed;
        int ganados = UserPerformance.instance.puzzlesWon;
        int perdidos = UserPerformance.instance.puzzlesLost;
        _puzzlesPlayedText.text = "Jugaste " + jugados + " puzles de los cuales ganaste " + ganados;

        energyUsed = GameManager.instance.totalEnergyUsed;
        waterUsed = GameManager.instance.totalWaterUsed;
        totalEvents = GameManager.instance.totalEvents;
        totalBonfires = GameManager.instance.bonfiresVisited;
        Debug.Log(energyUsed + "; " + waterUsed + "; " );
        Debug.Log(totalEvents + "; " + totalBonfires+ "; " );
    }

    public void ActivateStatsPanel()
    {
        if (PanelStats.activeSelf) //objeto activo
        {
            PanelStats.SetActive(false);
        }
        else
        {
            PanelStats.SetActive(true);
        }

    }
}
