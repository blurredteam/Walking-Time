using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo_UI : MonoBehaviour
{
    public static CharacterInfo_UI instance;

    [SerializeField] private Image characterSprite;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterDescription;
    [SerializeField] private TextMeshProUGUI characterEnergy;

    private void Start()
    {
        instance = this;
    }

    public void ShowCharacterInfo(int id)
    {
        characterSprite.sprite = CharacterManager.instance.characterList[id].sprite.sprite;
        characterName.text = CharacterManager.instance.characterList[id].name;
        characterDescription.text = CharacterManager.instance.characterList[id].desc;
        characterEnergy.text = CharacterManager.instance.characterList[id].energy.ToString();
    }
}
