using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// BUG MU RARO --> peta si le das a play seleccionando el objeto con este script

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private Button _firstNode;
    [SerializeField] private Button _secondNode;
    [SerializeField] private Button _thirdNode;
    [SerializeField] private Button _fourthNode;
    [SerializeField] private Button _fifthNode;
    [SerializeField] private Button _sixthNode;

    private ColorBlock cb;


    private void Awake()
    {
        instance= this;

        cb = _firstNode.colors;
        cb.disabledColor = Color.grey;
    }

    void Start()
    {
        InitFirstLevelNodes();

        _secondNode.interactable = false;
        _thirdNode.interactable = false;
        _fourthNode.interactable = false;
        _fifthNode.interactable = false;
        _sixthNode.interactable = false;
    }

    private IEnumerator BtnGlow(Button btn)
    {
        ColorBlock cb = btn.colors;

        if(cb.normalColor == Color.white)
            cb.normalColor = Color.grey;
        else
            cb.normalColor = Color.white;

        btn.colors = cb;

        yield return new WaitForSeconds(1.2f);

        StartCoroutine(BtnGlow(btn));
    }

    //Se gestionan los nodos de cada nivel del arbol
    // - Se tiene que añadir cada nodo de uno en uno en el nivel que le corresponde
    // - Si en un nivel se quiere añadir un nodo vale con meterlo dentro del metodo de su nivel
   
    private void InitFirstLevelNodes() 
    {
        StartCoroutine(BtnGlow(_firstNode));

        _firstNode.onClick.AddListener(delegate { FirstLevelCompleted(_firstNode); });
        _firstNode.onClick.AddListener(InitSecondLevelNodes);
    }

    private void FirstLevelCompleted(Button btn)
    {
        //Recibe el nodo que activa el metodo, para ir guardando el recorrido o marcarlos de alguna forma especial por ejemplo

        _firstNode.interactable = false;

        _firstNode.colors = cb;

        _firstNode.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }

    private void InitSecondLevelNodes()
    {
        _secondNode.interactable = true;
        StartCoroutine(BtnGlow(_secondNode));

        _secondNode.onClick.AddListener(delegate { SecondLevelCompleted(_secondNode); });
        _secondNode.onClick.AddListener(InitThirdLevelNodes);
    }

    private void SecondLevelCompleted(Button btn)
    {
        _secondNode.interactable = false;

        _secondNode.colors = cb;

        _secondNode.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }

    private void InitThirdLevelNodes()
    {
        _thirdNode.interactable = true;

        StartCoroutine(BtnGlow(_thirdNode));
        _thirdNode.onClick.AddListener(delegate { ThirdLevelCompleted(_thirdNode); });
        _thirdNode.onClick.AddListener(InitFourthLevelNodes);
    }

    private void ThirdLevelCompleted(Button btn)
    {
        _thirdNode.interactable = false;

        _thirdNode.colors = cb;

        _thirdNode.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }

    private void InitFourthLevelNodes()
    {
        _fourthNode.interactable = true;
        
        StartCoroutine(BtnGlow(_fourthNode));
        _fourthNode.onClick.AddListener(delegate { FourthLevelCompleted(_fourthNode); });
        _fourthNode.onClick.AddListener(InitFifthLevelNodes);
    }

    private void FourthLevelCompleted(Button btn)
    {
        _fourthNode.interactable = false;

        _fourthNode.colors = cb;

        _fourthNode.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }

    private void InitFifthLevelNodes()
    {
        _fifthNode.interactable = true;

        StartCoroutine(BtnGlow(_fifthNode));
        _fifthNode.onClick.AddListener(delegate { FifthLevelCompleted(_fifthNode); });
        _fifthNode.onClick.AddListener(InitSixthLevelNodes);
    }

    private void FifthLevelCompleted(Button btn)
    {
        _fifthNode.interactable = false;

        _fifthNode.colors = cb;

        _fifthNode.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }


    private void InitSixthLevelNodes()
    {
        _sixthNode.interactable = true;

        StartCoroutine(BtnGlow(_sixthNode));
        _sixthNode.onClick.AddListener(delegate { SixthLevelCompleted(_sixthNode); });
    }
    private void SixthLevelCompleted(Button btn)
    {
        _sixthNode.interactable = false;

        _sixthNode.colors = cb;

        _sixthNode.onClick.RemoveAllListeners();
        StopAllCoroutines();
    }

}
