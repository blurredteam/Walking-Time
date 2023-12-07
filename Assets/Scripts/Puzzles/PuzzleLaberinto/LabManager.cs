using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> listaLayouts;
    [SerializeField] private List<GameObject> listaPlayer;

    void Start()
    {
        if (listaLayouts.Count > 0)
        {

            int randomPos = Random.Range(0, listaLayouts.Count);
            listaLayouts[randomPos].SetActive(true);

            for (int i = 0; i < listaLayouts.Count; i++)
            {
                if (i != randomPos)
                {
                    listaLayouts[i].SetActive(false);
                }
            }
        }
        if (listaPlayer.Count > 0)
        {

            int randomPos = Random.Range(0, listaPlayer.Count);
            listaPlayer[randomPos].SetActive(true);

            for (int i = 0; i < listaPlayer.Count; i++)
            {
                if (i != randomPos)
                {
                    listaPlayer[i].SetActive(false);
                }
            }
        }


    }
}
