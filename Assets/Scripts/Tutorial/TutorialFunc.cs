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
    public Transitioner transition;

    private void Awake()
    {
        transition = ScenesManager.instance.transitioner;
    }
    // Start is called before the first frame update
    void Start()
    {
        saltarTuto.onClick.AddListener(delegate { AudioManager.instance.ButtonSound(); SaltarTutorial();});

        for (int i = 0; i < 8; i++)
        {
            int currentIndex = i;
            _btnSiguiente[i].onClick.AddListener(delegate {
                AudioManager.instance.ButtonSound();
                _paneles[currentIndex].SetActive(false);
                _paneles[currentIndex + 1].SetActive(true);
            });
        }

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
    
    // public void DoFadeTransition()
    // {
    //     StartCoroutine(DoFadeTransitionCo());
    // }
    //
    // IEnumerator DoFadeTransitionCo()
    // {
    //     transition.DoTransitionOnce();
    //     yield return new WaitForSeconds(1f);
    //     SaltarTutorial();
    //     transition.DoTransitionOnce();
    // }

}
