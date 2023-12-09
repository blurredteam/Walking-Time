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
            UserPerformance.instance.resetStats();
            ScenesManager.instance.LoadScene(ScenesManager.Scene.EscenaMenu);
        });

        _finalEnergy = GameManager.instance.energy;
        Debug.Log(_finalEnergy);

        UserPerfManager = FindObjectOfType<UserPerformance>();
        updateUserPerformance();

        if (_finalEnergy > 0) Victory();
        else Defeat();
    }

    private void Victory()
    {
        _victoryImage.gameObject.SetActive(true);
        _finalText.text = "LLegaste a tu destino!";

        unlockCharacters();     //cuando se alcanza la escena final se desbloquea automaticamente a chispa
    }

    private void Defeat()
    {
        _defeatImage.gameObject.SetActive(true);
        _finalText.text = "Faltaron fuerzas...";
    }

    public void unlockCharacters()
    {
        UnlockManager.Instance.PersonajeDesbloqueado = true;
    }

    private void updateUserPerformance()
    {
        float tiempo = UserPerfManager.GetTimerValue();
        UserPerfManager.changeTimerState();
        float minutos = tiempo / 60f;
        minutos = Mathf.Round(minutos * 10f) / 10f; //redondeamos para mostrar solo 1 decimal

        _timerText.text = "La partida duró " + minutos + " minutos";

        int oro = UserPerformance.instance.totalGoldGained;
        _goldText.text = "Obtuviste un total de " + oro + " de oro";

        int jugados = UserPerformance.instance.puzzlesPlayed;
        int ganados = UserPerformance.instance.puzzlesWon;
        int perdidos = UserPerformance.instance.puzzlesLost;
        _puzzlesPlayedText.text = "Jugaste " + jugados + " puzles de los cuales ganaste " + ganados;

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
