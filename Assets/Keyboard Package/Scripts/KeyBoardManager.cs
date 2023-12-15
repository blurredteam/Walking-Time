using UnityEngine;
using TMPro;

public class KeyBoardManager : MonoBehaviour
{
    public static KeyBoardManager Instance;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] TextMeshProUGUI printBox;

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
        InfoPlayer.Instance.SetNombre(textBox.text);
        // Debug.Log("Text submitted successfully!");
    }
}