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
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        bool spawnArriba = false;
        while (true) 
        {
            prefabObstaculo.transform.localScale = new Vector3(1, Random.Range(1, 3), 1);
            if(Random.Range(1,4)<3 || spawnArriba)
            {
                
                prefabObstaculo.GetComponent<Rigidbody2D>().gravityScale = 1;
                spawnPosition.y = transform.position.y;
                Instantiate(prefabObstaculo, spawnPosition, Quaternion.identity, transform);
                spawnArriba = false;
                yield return new WaitForSeconds(Random.Range(1.45f, 1.6f));
            }
            else
            {
                prefabObstaculo.GetComponent<Rigidbody2D>().gravityScale = 0;
                spawnPosition.y = -0.5f;
                Instantiate(prefabObstaculo, spawnPosition, Quaternion.identity, transform);
                spawnArriba = true;
                yield return new WaitForSeconds(Random.Range(1f, 1.5f));
            }
        }
    }
}