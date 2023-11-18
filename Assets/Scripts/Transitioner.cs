using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitioner : MonoBehaviour
{
    public Animator transition;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void DoTransitionOnce()
    {
        transition.SetTrigger("Start");
    }
    
    public void DoTransitionTwice()
    {
        StartCoroutine(EsperarYSalir());
    }
    
    IEnumerator EsperarYSalir()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);
        
        transition.SetTrigger("Start");
    }
}
