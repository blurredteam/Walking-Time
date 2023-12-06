using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{


    [SerializeField] private List<GameObject> listaLayouts;

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


    }


}
