using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level_UI : MonoBehaviour
{
    public static Level_UI instance;

    // --- RECURSOS ---
    [SerializeField] private Slider _energySlider;
    [SerializeField] private TextMeshProUGUI _energyText;
    [SerializeField] private Slider _waterSlider;
    [SerializeField] private TextMeshProUGUI _waterText;
    [SerializeField] private TextMeshProUGUI goldTxt;
    [SerializeField] private TextMeshProUGUI travelCostTxt;

    // --- PANEL INFO ---
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI infoTxt;

    // --- MOCHILA ---
    [SerializeField] private Button showInvBtn;
    [SerializeField] private List<Image> objectIcons;
    [SerializeField] private List<TextMeshProUGUI> objectDescriptions;

    // --- STATS ---
    [SerializeField] private Button showStatsBtn;
    [SerializeField] private TextMeshProUGUI energyDesc;
    [SerializeField] private TextMeshProUGUI modCoste;
    [SerializeField] private TextMeshProUGUI waterHeal;

    private void Start()
    {
        instance = this;

        showInvBtn.onClick.AddListener(SetEventObjects);
        showStatsBtn.onClick.AddListener(SetEventObjects);
    }

    private void Update()
    {
        _energySlider.maxValue = LevelManager.instance.maxEnergy;
        _energySlider.value = LevelManager.instance.teamEnergy;

        _energyText.text = LevelManager.instance.teamEnergy.ToString() + "/" + LevelManager.instance.maxEnergy.ToString();

        _waterSlider.maxValue = LevelManager.instance.maxWater;
        _waterSlider.value = LevelManager.instance.teamWater;

        _waterText.text = LevelManager.instance.teamWater.ToString() + "/" + LevelManager.instance.maxWater.ToString();

        goldTxt.text = LevelManager.instance.gold.ToString();
        travelCostTxt.text = LevelManager.instance.travelCostModifier.ToString();
    }

    private void SetEventObjects()
    {
        List<Evento> events = LevelManager.instance.removedEvents;

        for (int i = 0; i < events.Count; i++)
        {
            if(events[i] == null) continue;
            else
            {
                objectIcons[i].sprite = events[i].objectIcon.sprite;
                objectDescriptions[i].text = events[i].objectDescription;
            }
        }

        List<Character> team = LevelManager.instance._team;

        string teamEnergy = $"{team[0].defaultEnergy} + {team[1].defaultEnergy} + {team[2].defaultEnergy} + {team[3].defaultEnergy}";
        energyDesc.text = $"Energia máxima: {teamEnergy} = {LevelManager.instance.maxEnergy}";
        foreach(Character c in team)
            if(c.name == "Dr. Japaro") energyDesc.text = $"Energia máxima: ({teamEnergy}) X 1.1 = {LevelManager.instance.maxEnergy}"; ;

        modCoste.text = $"Modificador de coste de viaje = {LevelManager.instance.travelCostModifier}";
        waterHeal.text = $"Curación por uso de cantimplora = {LevelManager.instance.waterRegen}";

    }

    public void HandleWaterUI(int water)
    {
        infoPanel.SetActive(true);

        if (water > 0) infoTxt.text = "-1 AGUA";
        else infoTxt.text = "¡NO QUEDA AGUA!";

        StartCoroutine(EsperarInfo());
    }

    public void HandleEnergyUI(int energy)
    {
        infoPanel.SetActive(true);
        infoTxt.text = $"-{energy} ENERGÍA";
        StartCoroutine(EsperarInfo());
    }

    private IEnumerator EsperarInfo()
    {
        yield return new WaitForSeconds(2);
        infoPanel.SetActive(false);
    }

}
