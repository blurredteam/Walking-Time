using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    [SerializeField] private Button _menuBtn;

    private void Start()
    {
        _menuBtn.onClick.AddListener(delegate { ScenesManager.instance.LoadScene(ScenesManager.Scene.MainMenu_Scene); });
    }
}
