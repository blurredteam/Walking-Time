using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObstaculos : MonoBehaviour
{
    public GameObject prefabObstaculo;
    public float velocidadMovimiento = 5.0f;

    [SerializeField] private Sprite rocaBaja;
    [SerializeField] private Sprite estalacBaja;
    [SerializeField] private Sprite rocaAlta;
    [SerializeField] private Sprite estalacAlta;
    [SerializeField] private GameObject SalirBtn;

    void Start()
    {
        SalirBtn.SetActive(true);
        StartCoroutine(GenerarObstaculos());

    }

    IEnumerator GenerarObstaculos()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        bool spawnArriba = false;
        while (true)
        {
            int num=Random.Range(1, 3);
            prefabObstaculo.transform.localScale = new Vector3(1, 1, 1);
            prefabObstaculo.GetComponent<BoxCollider2D>().size = new Vector3(1, num, 1);
            if(Random.Range(1,4)<3 || spawnArriba)
            {

                if (num==1)
                {
                    prefabObstaculo.GetComponent<SpriteRenderer>().sprite = rocaBaja;
                }
                else
                {
                    prefabObstaculo.GetComponent<SpriteRenderer>().sprite = rocaAlta;
                }
                
                prefabObstaculo.GetComponent<Rigidbody2D>().gravityScale = 1;
                spawnPosition.y = transform.position.y;
                
                Instantiate(prefabObstaculo, spawnPosition, Quaternion.identity, transform);
                
                spawnArriba = false;
                yield return new WaitForSeconds(Random.Range(1.45f, 1.6f));
            }
            else
            {
                
                if (num==1)
                {
                    prefabObstaculo.GetComponent<BoxCollider2D>().size = new Vector3(1, 2, 1);
                    prefabObstaculo.GetComponent<SpriteRenderer>().sprite = estalacBaja;
                }
                else
                {
                    prefabObstaculo.GetComponent<BoxCollider2D>().size = new Vector3(1, 3, 1);
                    prefabObstaculo.GetComponent<SpriteRenderer>().sprite = estalacAlta;
                }
                prefabObstaculo.GetComponent<Rigidbody2D>().gravityScale = 0;
                spawnPosition.y = 0.5f;
                
                Instantiate(prefabObstaculo, spawnPosition, Quaternion.identity, transform);
                spawnArriba = true;
                yield return new WaitForSeconds(Random.Range(1f, 1.5f));
            }
        }
    }
}