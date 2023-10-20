using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorObjects : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> objects;
    private GameObject winnerObject;
    [SerializeField] private Camera _puzzleCamera;

    public void GenerateObjects()
    {
        foreach (GameObject obj in objects)
        {
            Instantiate(obj, new Vector3(Random.Range(-3,3),Random.Range(-3,3),1), Quaternion.identity);
        }
    }



    void Start()
    {
        GenerateObjects();
        winnerObject = objects[Random.Range(0, objects.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = _puzzleCamera.ScreenToWorldPoint(Input.mousePosition);
        
        if(Input.GetMouseButtonDown(0))
        {
            //Esto es para que si pasas el raton por ecnima del objecto sepa que es ese objeto
           Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                if (targetObject.gameObject == winnerObject)
                {
                    Debug.Log("Has ganado");
                }
            }
        }
    }
}
