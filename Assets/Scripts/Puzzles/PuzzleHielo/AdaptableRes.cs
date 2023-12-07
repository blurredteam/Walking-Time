using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptableRes : MonoBehaviour
{
    [SerializeField] public Vector2 ResOriginal = new Vector2(1920f, 1080f);
    [SerializeField] private List<GameObject> ObjetosReescalables;
    //[SerializeField] private int numObjetos;
    [SerializeField] private List<Vector3> EscalaOriginal = new List<Vector3>();
    [SerializeField] private List<Vector3> PosicionOriginal = new List<Vector3>();

    private int anchoResPrevia;
    private int altoResPrevia;


    private void Start()
    {
        anchoResPrevia = Screen.width;
        altoResPrevia = Screen.height;
        for (int i = 0; i < ObjetosReescalables.Count; i++)
        {

            EscalaOriginal.Add(ObjetosReescalables[i].transform.localScale);
            PosicionOriginal.Add(ObjetosReescalables[i].transform.localPosition);
        }
    }

    void Update()
    {

        if (Screen.width != anchoResPrevia && Screen.height != altoResPrevia)
        {
            EscalarObjetos();
            anchoResPrevia = Screen.width;
            altoResPrevia = Screen.height;
        }
    }

    void EscalarObjetos()
    {
        float factorDeEscaladoX = Screen.width / ResOriginal.x;
        float factorDeEscaladoY = Screen.height / ResOriginal.y;

        //para mantener el aspect ratio de los objetos
        //float factorDeEscalado = Mathf.Min(factorDeEscaladoX, factorDeEscaladoY);
        Vector3 factorDeEscalado = new Vector3(factorDeEscaladoX, factorDeEscaladoY, 1.0f);


        for (int i = 0; i < ObjetosReescalables.Count; i++)
        {
            //ObjetosReescalables[i].transform.localScale *= factorDeEscalado
            ObjetosReescalables[i].transform.localScale = Vector3.Scale(EscalaOriginal[i], factorDeEscalado);
            ObjetosReescalables[i].transform.localPosition = Vector3.Scale(PosicionOriginal[i], factorDeEscalado);
        }
    }
}
