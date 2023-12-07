using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaberintoMove : MonoBehaviour
{


    //[SerializeField] private Button volverBtn;
    GameObject panel;

    //[SerializeField] private AudioClip fondoHielo;

    private bool isClicked = false;
    private string tagMuro = "Obstaculo";
    public Rigidbody2D playerBody;

    private void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = GetMouseWorldPos();
            Collider2D collider = Physics2D.OverlapPoint(mousePos);

            if (collider != null && collider.gameObject == gameObject)
            {
                isClicked = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }

        if (isClicked == true)
        {
            Movement();
        }

    }

    private void Movement()
    {
        Vector3 mousePos = GetMouseWorldPos();
        //transform.position = ClampToMazeBounds(mousePos);

        Collider2D wallCollider = Physics2D.OverlapPoint(mousePos);
        if (wallCollider != null && wallCollider.CompareTag(tagMuro))
        {
            print("chocado");
            return;
        }

        //transform.position = mousePos;
        playerBody.MovePosition(new Vector2(mousePos.x, mousePos.y));
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }



}
