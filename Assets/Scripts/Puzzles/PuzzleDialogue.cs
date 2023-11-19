using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleDialogue : MonoBehaviour
{
    [SerializeField] private GameObject contenedor;
    [SerializeField] private Image characterSprite;
    [SerializeField] private GameObject contenedorTexto;
    [SerializeField] private TextMeshProUGUI texto;

    private List<Character> _team;

    private void Start()
    {
        _team = LevelManager.instance._team;
    
        int selectCharacter = Random.Range(0, _team.Count);
        characterSprite.sprite = _team[selectCharacter].sprite.sprite;
        texto.text = _team[selectCharacter].PuzzleChooseDialogue();

        StartCoroutine(AnimateDialogue());
    }

    private IEnumerator AnimateDialogue()
    {
        while (contenedor.transform.localPosition.y < 0)
        {
            contenedor.transform.localPosition += new Vector3(0, 1.5f);
            yield return null;
        }
        contenedorTexto.SetActive(true);

        yield return new WaitForSeconds(2);

        contenedorTexto.SetActive(false);
        while (contenedor.transform.localPosition.y > -400)
        {
            contenedor.transform.localPosition -= new Vector3(0, 1.5f);
            yield return null;
        }
    }

    
}
