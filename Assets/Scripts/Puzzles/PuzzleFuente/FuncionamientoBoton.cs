using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FuncionamientoBoton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public LlenadoBotella agua;
   

    public void OnPointerDown(PointerEventData eventData)
    {
       
        agua.ComenzarLlenado();
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        agua.DetenerLlenado();
        
    }
}

    // Update is called once per frame
    //void Update()
    //{
    //    if(accionBoton)
    //    {
    //        agua.ComenzarLlenado();
    //    }
    //    else
    //    {
    //        agua.DetenerLlenado();
    //    }
    //}

