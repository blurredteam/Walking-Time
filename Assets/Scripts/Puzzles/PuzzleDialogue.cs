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
        float moveTime = 0.45f;
        
        while(moveTime > 0)
        {
            moveTime -= Time.deltaTime;
            contenedor.transform.position += new Vector3(0, 2);
            yield return null;
        }

        contenedorTexto.SetActive(true);

        yield return new WaitForSeconds(2);

        contenedorTexto.SetActive(false);

        float moveBackTime = 0.45f;
        while (moveBackTime > 0)
        {
            moveBackTime -= Time.deltaTime;
            contenedor.transform.position -= new Vector3(0, 2);
            yield return null;
        }
    }

    
}
