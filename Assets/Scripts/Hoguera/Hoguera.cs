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
    [SerializeField] private Slider energySlider;
    [SerializeField] private Slider waterSlider;

    //Objetos
    [SerializeField] private List<Button> objectIcons;
    [SerializeField] private GameObject panelQuitarObjeto;
    [SerializeField] private TextMeshProUGUI textoAviso;
    [SerializeField] private Button _confirmarBtn;

    private int eneryRegen = 100;
    
    public Transitioner transition;
    public float transitionTime = 1f;

    private void Awake()
    {
        for (int i = 0; i < objectIcons.Count; i++)
        {
            objectIcons[i].image.sprite = LevelManager.instance._eventObjects[i].sprite;
            if(objectIcons[i].image.sprite != null) objectIcons[i].gameObject.SetActive(true);
        }

        objectIcons[0].onClick.AddListener(delegate { Warning(LevelManager.instance.removedEvents[0], 0); });
        objectIcons[1].onClick.AddListener(delegate { Warning(LevelManager.instance.removedEvents[1], 1); });
        objectIcons[2].onClick.AddListener(delegate { Warning(LevelManager.instance.removedEvents[2], 2); });
        objectIcons[3].onClick.AddListener(delegate { Warning(LevelManager.instance.removedEvents[3], 3); });

        transition = ScenesManager.instance.transitioner;
    }

    private void Start()
    {
        iconP0.sprite = LevelManager.instance._team[0].icon.sprite;
        iconP1.sprite = LevelManager.instance._team[1].icon.sprite;
        iconP2.sprite = LevelManager.instance._team[2].icon.sprite;
        iconP3.sprite = LevelManager.instance._team[3].icon.sprite;
    }
    private void Update()
    {
        energySlider.maxValue = LevelManager.instance.maxEnergy;
        energySlider.value = LevelManager.instance.teamEnergy;

        waterSlider.maxValue = LevelManager.instance.maxWater;
        waterSlider.value = LevelManager.instance.teamWater;

        energy.text = LevelManager.instance.teamEnergy.ToString() + "/" + LevelManager.instance.maxEnergy.ToString();
        water.text = LevelManager.instance.teamWater.ToString() + "/" + LevelManager.instance.maxWater.ToString();
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

        StartCoroutine(Esperar());
    }

    public void CambiarEquipo()
    {
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.Hoguera);
        SceneManager.LoadScene(ScenesManager.Scene.SeleccionEquipo.ToString(), LoadSceneMode.Additive);
    }

    private void Warning(Evento e, int index)
    {
        panelQuitarObjeto.SetActive(true);
        textoAviso.text = LevelManager.instance.removedEvents[index]._avisoQuitarObj;

        _confirmarBtn.onClick.RemoveAllListeners();
        _confirmarBtn.onClick.AddListener(delegate { RemoveObject(e, index); });
    }

    private void RemoveObject(Evento e, int index)
    {
        objectIcons[index].gameObject.SetActive(false);
        objectIcons[index].image.sprite = null;

        LevelManager.instance.removedEvents[index].RemoveEventoObj();
        LevelManager.instance.removedEvents[index] = null;
        LevelManager.instance._eventObjects[index].gameObject.SetActive(false);
        LevelManager.instance._eventObjects[index].sprite = null;
    }

    IEnumerator Esperar()
    {
        
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        panelFinal.SetActive(true);
        transition.DoTransitionOnce();
    }
    public void InicioHoguera()
    {
        panelInicial.SetActive(false);
    }
    public void SalirDelJuego()
    {
        StartCoroutine(EsperarYSalir());


    }
    
    IEnumerator EsperarYSalir()
    {
        
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.Hoguera);
        LevelManager.instance.ActivateScene();
        transition.DoTransitionOnce();
    }

}
