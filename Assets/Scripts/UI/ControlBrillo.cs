using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControlBrillo : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float valorSlider;
    [SerializeField] private Image panelBrillo;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CambiarBrillo(float valor)
    {
        valorSlider = valor;
        PlayerPrefs.SetFloat("brillo", slider.value);
        panelBrillo.color= new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);
    }
}
