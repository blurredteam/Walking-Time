using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level_UI : MonoBehaviour
{
    public static Level_UI instance;

    // --- SPRITES E ICONOS ---
    [SerializeField] private List<Image> _icons;
    [SerializeField] private GameObject _spritesTeam;
    [SerializeField] private List<Image> _sprites;

    // --- RECURSOS ---
    [SerializeField] private Slider _energySlider;
    [SerializeField] private TextMeshProUGUI _energyText;
    [SerializeField] private Slider _waterSlider;
    [SerializeField] private TextMeshProUGUI _waterText;
    [SerializeField] private TextMeshProUGUI goldTxt;

    // --- PANEL INFO ---
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI infoTxt;

    // --- MOCHILA ---
    [SerializeField] private List<Image> objectIcons;
    [SerializeField] private List<TextMeshProUGUI> objectDescriptions;

    // --- STATS ---
    [SerializeField] private TextMeshProUGUI energyDesc;
    [SerializeField] private TextMeshProUGUI modCoste;
    [SerializeField] private TextMeshProUGUI waterHeal;

    // --- LIBRO BTN ---
    [SerializeField] private Button openBookBtn;
    [SerializeField] private Button showInvBtn;
    [SerializeField] private Button showStatsBtn;

    // --- LIBRO ---
    [SerializeField] private GameObject _libro;
    [SerializeField] private List<Image> _paginas;

    // --- PAGINA EQUIPO ---
    [SerializeField] private List<Image> bookTeamIcons;
    [SerializeField] private List<Image> bookTeamSprites;
    [SerializeField] private List<TextMeshProUGUI> bookTeamDesc;


    private void Start()
    {
        instance = this;

        openBookBtn.onClick.AddListener(SetEventObjects);
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
        //travelCostTxt.text = LevelManager.instance.travelCostModifier.ToString();
    }

    public void OpenBookBtn()
    {
        StartCoroutine(OpenBook());
    }

    private IEnumerator OpenBook()
    {
        var startPos = _libro.GetComponent<RectTransform>().anchoredPosition;
        Vector2 endPos = new Vector2(0, 0);

        while(_libro.GetComponent<RectTransform>().anchoredPosition != endPos)
        {
            _libro.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(
                _libro.GetComponent<RectTransform>().anchoredPosition,
                endPos,
                2000 * Time.deltaTime);
            yield return 0;
        }
    }

    public void SetTeamUI(List<Character> team)
    {
        for (int i = 0; i < team.Count; i++)
        {
            _icons[i].sprite = team[i].icon.sprite;
            _sprites[i].sprite = team[i].sprite.sprite;
            _sprites[i].GetComponent<Animator>().runtimeAnimatorController = team[i].anim;
        }
    }

    // Coge informacion del level manager y la muestra en la pantalla de objetos y stats
    private void SetEventObjects()
    {
        //List<Evento> events = LevelManager.instance.removedEvents;

        //for (int i = 0; i < events.Count; i++)
        //{
        //    if(events[i] == null) continue;
        //    else
        //    {
        //        objectIcons[i].sprite = events[i].objectIcon.sprite;
        //        objectDescriptions[i].text = events[i].objectDescription;
        //    }
        //}

        //List<Character> team = LevelManager.instance._team;

        //string teamEnergy = $"{team[0].defaultEnergy} + {team[1].defaultEnergy} + {team[2].defaultEnergy} + {team[3].defaultEnergy}";
        //energyDesc.text = $"Energia máxima: {teamEnergy} = {LevelManager.instance.maxEnergy}";
        //foreach(Character c in team)
        //    if(c.name == "Dr. Japaro") energyDesc.text = $"Energia máxima: ({teamEnergy}) X 1.1 = {LevelManager.instance.maxEnergy}"; ;

        //modCoste.text = $"Modificador de coste de viaje = {LevelManager.instance.travelCostModifier}";
        //waterHeal.text = $"Curación por uso de cantimplora = {LevelManager.instance.waterRegen}";

        List<Character> team = LevelManager.instance._team;

        for(int i = 0; i < team.Count; i++)
        {
            bookTeamIcons[i].sprite = team[i].icon.sprite;
            bookTeamSprites[i].sprite = team[i].sprite.sprite;
            bookTeamDesc[i].text = team[i].skillDescription;
        }

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
