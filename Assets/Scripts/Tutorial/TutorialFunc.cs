using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialFunc : MonoBehaviour
{
    [SerializeField] private GameObject panelTuto;
    [SerializeField] private List<GameObject> _paneles;
    [SerializeField] private List<Button> _btnSiguiente;
    [SerializeField] private List<Button> _btnAnterior;
    [SerializeField] private Button saltarTuto;
    // Start is called before the first frame update
    void Start()
    {
        saltarTuto.onClick.AddListener(delegate { AudioManager.instance.ButtonSound(); SaltarTutorial(); });

        for (int i = 0; i < 8; i++)
        {
            int currentIndex = i;
            _btnSiguiente[i].onClick.AddListener(delegate {
                AudioManager.instance.ButtonSound();
                _paneles[currentIndex].SetActive(false);
                _paneles[currentIndex + 1].SetActive(true);
            });
        }
        //_btnSiguiente[0].onClick.AddListener(delegate { _paneles[0].SetActive(false); _paneles[1].SetActive(true); });
        //_btnSiguiente[1].onClick.AddListener(delegate { _paneles[1].SetActive(false); _paneles[2].SetActive(true); });
        //_btnSiguiente[2].onClick.AddListener(delegate { _paneles[1].SetActive(false); _paneles[2].SetActive(true); });
        //_btnSiguiente[3].onClick.AddListener(delegate { _paneles[1].SetActive(false); _paneles[2].SetActive(true); });
        //_btnSiguiente[4].onClick.AddListener(delegate { _paneles[1].SetActive(false); _paneles[2].SetActive(true); });
        //_btnSiguiente[5].onClick.AddListener(delegate { _paneles[1].SetActive(false); _paneles[2].SetActive(true); });
        //_btnSiguiente[6].onClick.AddListener(delegate { _paneles[1].SetActive(false); _paneles[2].SetActive(true); });
        //_btnSiguiente[7].onClick.AddListener(delegate { _paneles[1].SetActive(false); _paneles[2].SetActive(true); });
        //_btnSiguiente[8].onClick.AddListener(delegate { _paneles[1].SetActive(false); _paneles[2].SetActive(true); });
        //_btnSiguiente[2].onClick.AddListener(delegate { _paneles[0].SetActive(false); _paneles[1].SetActive(true); });
        //_btnAnterior[1].onClick.AddListener(delegate { _paneles[0].SetActive(true); _paneles[1].SetActive(false); });
        //_btnAnterior[2].onClick.AddListener(delegate { _paneles[1].SetActive(true); _paneles[2].SetActive(false); });

        for (int i = 1; i <= 8; i++)
        {
            int currentIndex = i;
            _btnAnterior[i].onClick.AddListener(delegate {
                AudioManager.instance.ButtonSound();
                _paneles[currentIndex - 1].SetActive(true);
                _paneles[currentIndex].SetActive(false);
            });
        }

    }


    private void SaltarTutorial()
    {
        panelTuto.SetActive(false);
    }

}
