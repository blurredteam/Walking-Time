using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
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


    private int _finalEnergy;

    private void Start()
    {
        _menuBtn.onClick.AddListener(delegate
        {
            AudioManager.instance.LeaveGame();
            UserPerformance.instance.resetStats();
            
            ScenesManager.instance.LoadScene(ScenesManager.Scene.EscenaMenu);
            ControlPantallaCoompleta.instance.FinDeJuego();
        });

        _finalEnergy = GameManager.instance.energy;
        //Debug.Log(_finalEnergy);

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

        int energyUsed = GameManager.instance.totalEnergyUsed;
        int waterUsed = GameManager.instance.totalWaterUsed;
        int totalEvents = ControladorEventos.instance.totalEvents;
        int totalBonfires = GameManager.instance.bonfiresVisited;
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
