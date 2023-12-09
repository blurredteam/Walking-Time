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

    [SerializeField] private EvalTimerManager timerManager;
    [SerializeField] private TextMeshProUGUI _timerText;

    private int _finalEnergy;

    private void Start()
    {
        _menuBtn.onClick.AddListener(delegate { ScenesManager.instance.LoadScene(ScenesManager.Scene.EscenaMenu); });

        _finalEnergy = GameManager.instance.energy;
        Debug.Log(_finalEnergy);

        timerManager = FindObjectOfType<EvalTimerManager>();
        updateTiempo();

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

    private void updateTiempo()
    {
        float tiempo = timerManager.GetTimerValue();
        timerManager.changeTimerState();
        float minutos = tiempo / 60f;
        minutos = Mathf.Round(minutos * 10f) / 10f; //redondeamos para mostrar solo 1 decimal

        _timerText.text = "La partida duró: " + minutos + " minutos";
    }
}
