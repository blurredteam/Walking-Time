using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasTimer : MonoBehaviour
{
    public static CanvasTimer instance;

    [SerializeField] private GameObject panelInicio;
    [SerializeField] private GameObject panelFinal;
    [SerializeField] private GameObject rueda1;
    [SerializeField] private GameObject rueda2;
    [SerializeField] private GameObject rueda3;
    [SerializeField] private TextMeshProUGUI textoFinal;
    private int energiaPerdida = 0;
    private int puzzleTerminado = 0;

    private bool final = false;
    private bool oroActualizado = false; // Variable para controlar si ya se actualiz� el oro

    private void Awake()
    {
        instance = this;

    }

    public void SetEnergiaPerdida(int energia)
    {
        energiaPerdida += energia;
        puzzleTerminado++;
    }
    public void Empezar()
    {
        panelInicio.SetActive(false);   
        rueda1.SetActive(true);
        rueda2.SetActive(true);
        rueda3.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (puzzleTerminado == 3 && !final)
        {
            panelFinal.SetActive(true);
            if (energiaPerdida == 0 && !oroActualizado)
            {
                textoFinal.text = "�ENHORABUENA, HAS PASADO EL PUZZLE PERFECTO!\nNO PIERDES ENERG�A Y GANAS 10 DE ORO";
                LevelManager.instance.gold += 10;
                oroActualizado = true;
                final = true;
            }
            else if (energiaPerdida == 30)
            {
                textoFinal.text = "VAYA, SE TE HA DADO MAL, HAS PERDIDO 30 DE ENERG�A. APUNTA MEJOR LA PR�XIMA.";
                final = true;
            }
            else
            {
                textoFinal.text = "VAYA, NO HAS CONSEGUIDO HACERLO PERFECTO. \nPIERDES " + energiaPerdida + " DE ENERG�A.";
                final = true;
            }

        }

    }
}
