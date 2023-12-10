using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaberintoMove : MonoBehaviour
{
    [SerializeField] Camera camaraPuzle;
    private bool isClicked = false;
    public Rigidbody2D playerBody;

    private void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
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
        playerBody.MovePosition(new Vector2(mousePos.x, mousePos.y));
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -camaraPuzle.transform.position.z;
        return camaraPuzle.ScreenToWorldPoint(mousePos);
    }



}
