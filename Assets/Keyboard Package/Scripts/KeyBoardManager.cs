using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class KeyBoardManager : MonoBehaviour
{
    public static KeyBoardManager Instance;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] TextMeshProUGUI printBox;
    [SerializeField] private List<GameObject> _paneles = new List<GameObject>();

    private void Start()
    {
        Instance = this;
        printBox.text = "";
        textBox.text = "";
    }

    public void DeleteLetter()
    {
        if(textBox.text.Length != 0) {
            textBox.text = textBox.text.Remove(textBox.text.Length - 1, 1);
        }
    }

    public void AddLetter(string letter)
    {
        AudioManager.instance.ButtonSound2();
        textBox.text = textBox.text + letter;
    }

    public void SubmitWord()
    {
        printBox.text = textBox.text;
        if(gameObject == _paneles[0])
        {
            InfoPlayer.Instance.SetUsuario(textBox.text);
        }
        if (gameObject == _paneles[1])
        {
            InfoPlayer.Instance.SetPassword(textBox.text);
        }
        if (gameObject == _paneles[2])
        {
            InfoPlayer.Instance.SetUsuario2(textBox.text);
        }
        if (gameObject == _paneles[3])
        {
            InfoPlayer.Instance.SetPassword2(textBox.text);
        }
        if (gameObject == _paneles[4])
        {
            InfoPlayer.Instance.SetNombre(textBox.text);
        }
        
        // Debug.Log("Text submitted successfully!");
    }
}
