using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaSpawner : MonoBehaviour
{
    [SerializeField] private float limiteX;
    [SerializeField] private float[] posicionesX;

    [SerializeField] private GameObject[] _prefab;
    [SerializeField] private Oleada[] _oleada;

    private float currentTime;
    List<float> posicionesRestantes = new List<float>();
    private int tipoOleada;//Voy a hacer varios tipos de oleadas para q tengan dificultades diferentes
    float posX = 0;
    int rand;
    private void Start()
    {
        currentTime = 0;
        posicionesRestantes.AddRange(posicionesX);
    }

    private void Update()
    {
        if (RecogeGotas.instance.MovimientoEmpezado)
        {
            //EleccionOleada();
            currentTime -= Time.deltaTime;
            if (currentTime <= 0) { EleccionOleada(); }
        }
    }
    private void SpawnGotas(float posX)
    {
        int r = Random.Range(0, 2);//Porq hay dos tipos de gotas
        GameObject gota = Instantiate(_prefab[r], new Vector3(posX, transform.position.y), Quaternion.identity);
    }

    private void EleccionOleada()//Cada oleada representa una dificultad
    {
        posicionesRestantes = new List<float>();
        posicionesRestantes.AddRange(posicionesX);

        tipoOleada = Random.Range(0, _oleada.Length);

        currentTime = _oleada[tipoOleada].tiempoDelay;

        if (_oleada[tipoOleada].cantSpawn == 1)
        {
            posX = Random.Range(-limiteX, limiteX);
        }
        else if (_oleada[tipoOleada].cantSpawn > 1)
        {
            rand = Random.Range(0, posicionesRestantes.Count);
            posX = posicionesRestantes[rand];
            posicionesRestantes.RemoveAt(rand);
        }

        for (int i = 0; i < _oleada[tipoOleada].cantSpawn; i++)
        {
            SpawnGotas(posX);
            rand = Random.Range(0, posicionesRestantes.Count);
            posX = posicionesRestantes[rand];
            posicionesRestantes.RemoveAt(rand);
        }
        //Debug.Log("Estamos con la oleada " + tipoOleada);
    }

}

[System.Serializable]//Para verlo en el inspector
public class Oleada
{
    public float tiempoDelay;
    public float cantSpawn;
}