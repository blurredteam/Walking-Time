using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ControladorEventos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oroTxt;
    [SerializeField] private TextMeshProUGUI aguaTxt;
    [SerializeField] private TextMeshProUGUI energiaTxt;
    [SerializeField] private TextMeshProUGUI eventoTxt;

    [SerializeField] private GameObject panelFinal;
    [SerializeField] private GameObject panel;



    private int oro;
    void Update()
    {
        oroTxt.text = LevelManager.instance.gold.ToString();
        aguaTxt.text = LevelManager.instance.teamWater.ToString();
        energiaTxt.text = LevelManager.instance.teamEnergy.ToString();
    }
    //Muy provisional todo esto
    public void botonEvento()
    {
        
        int numEvento = Random.Range(0, 3);
        switch (numEvento)
        {
            case 0:
                EventoCero();
                break;
            case 1:
                EventoUno();
                break;
            case 2:
                EventoDos();
                break;
            default:
                Debug.Log("Número de evento no manejado.");
                break;
        }
        panelFinal.SetActive(true);
        panel.SetActive(false);


    }
    private void EventoCero()
    {
        eventoTxt.text = "MALA SUERTE. PIERDES 50 DE ENERGÍA.";
        LevelManager.instance.teamEnergy -= 50;

    }
    private void EventoUno()
    {
        eventoTxt.text = "MALA SUERTE. PIERDES 1 USO DE AGUA.";
        LevelManager.instance.teamWater -= 1;
    }
    private void EventoDos()
    {
        eventoTxt.text = "ESTA VEZ TUVISTE SUERTE... GANAS 20 DE ORO.";
        LevelManager.instance.gold += 20;
    }

    public void SaltarEvento()
    {
        panelFinal.SetActive(true);
        panel.SetActive(false);
        eventoTxt.text = "TE HAS SALTADO EL EVENTO A CAMBIO DE 30 DE ORO.";
        LevelManager.instance.gold -= 30;
    }

    public void Salir()
    {
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.EventScene);
        LevelManager.instance.ActivateScene();
    }
}
