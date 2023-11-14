using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Hoguera : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energy;
    [SerializeField] private TextMeshProUGUI water;
    [SerializeField] private GameObject panelInicial;
    [SerializeField] private GameObject panelFinal;
    [SerializeField] private Image iconP0;
    [SerializeField] private Image iconP1;
    [SerializeField] private Image iconP2;
    [SerializeField] private Image iconP3;

    private int eneryRegen = 100;

    private void Start()
    {
        iconP0.sprite = LevelManager.instance._team[0].icon.sprite;
        iconP1.sprite = LevelManager.instance._team[1].icon.sprite;
        iconP2.sprite = LevelManager.instance._team[2].icon.sprite;
        iconP3.sprite = LevelManager.instance._team[3].icon.sprite;
    }
    private void Update()
    {
        energy.text = LevelManager.instance.teamEnergy.ToString();
        water.text = LevelManager.instance.teamWater.ToString();
        
    }
    public void RecargarEnergia()
    {
        if ((LevelManager.instance.teamEnergy + eneryRegen) >= LevelManager.instance.maxEnergy)
        {
            LevelManager.instance.teamEnergy = LevelManager.instance.maxEnergy;
        }
        else
        {
            LevelManager.instance.teamEnergy += eneryRegen;
        }
        panelFinal.SetActive(true);
    }

    public void CambiarEquipo()
    {
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.Hoguera);
        SceneManager.LoadScene(ScenesManager.Scene.SeleccionEquipo.ToString(), LoadSceneMode.Additive);
    }

    public void InicioHoguera()
    {
        panelInicial.SetActive(false);
    }
    public void SalirDelJuego()
    {
        
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.Hoguera);
        LevelManager.instance.ActivateScene();
        
    }

}
