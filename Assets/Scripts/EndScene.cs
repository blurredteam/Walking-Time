using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    [SerializeField] private Button _menuBtn;
    //public CharacterManager instance;


    private void Start()
    {
        _menuBtn.onClick.AddListener(delegate { ScenesManager.instance.LoadScene(ScenesManager.Scene.EscenaMenu); });

        unlockCharacters();     //cuando se alcanza la escena final se desbloquea automaticamente a chispa
    }

    public void unlockCharacters()
    {
        UnlockManager.Instance.PersonajeDesbloqueado = true;

    }
}
