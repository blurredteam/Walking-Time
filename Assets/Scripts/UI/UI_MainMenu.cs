using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private Button _newGameBtn;

    void Start()
    {
        _newGameBtn.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame()
    {
        ScenesManager.instance.StartNewGame();
    }
}
