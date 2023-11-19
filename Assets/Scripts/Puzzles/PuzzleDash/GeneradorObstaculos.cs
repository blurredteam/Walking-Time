using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObstaculos : MonoBehaviour
{
    public GameObject prefabObstaculo;
    public float velocidadMovimiento = 5.0f;

    void Start()
    {
        StartCoroutine(GenerarObstaculos());
    }

    IEnumerator GenerarObstaculos()
    {
        while (true) 
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0f);
            prefabObstaculo.transform.localScale=new Vector3(1,Random.Range(1, 3),1);
            Instantiate(prefabObstaculo, spawnPosition, Quaternion.identity, transform);
            yield return new WaitForSeconds(Random.Range(1.45f, 2.0f));
        }
    }
}