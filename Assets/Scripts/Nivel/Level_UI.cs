using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level_UI : MonoBehaviour
{
    [SerializeField] private Slider _energySlider;
    [SerializeField] private TextMeshProUGUI _energyText;

    [SerializeField] private Slider _waterSlider;
    [SerializeField] private TextMeshProUGUI _waterText;

    private void Update()
    {
        _energySlider.maxValue = LevelManager.instance.maxEnergy;
        _energySlider.value = LevelManager.instance.teamEnergy;

        _energyText.text = LevelManager.instance.teamEnergy.ToString() + "/" + LevelManager.instance.maxEnergy.ToString();

        _waterSlider.maxValue = LevelManager.instance.maxWater;
        _waterSlider.value = LevelManager.instance.teamWater;

        _waterText.text = LevelManager.instance.teamWater.ToString() + "/" + LevelManager.instance.maxWater.ToString();

    }

}
