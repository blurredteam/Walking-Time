using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 DedoInicial;
    private Vector3 DedoFinal;
    private Vector3 ClickInicial;
    private Vector3 ClickFinal;

    private Vector3 currentPos;
    private Vector3 lastPos;

    private Touch dedo;

    private bool playerMoving;
    private bool playerTouched;
    private bool playerClicked;
    private bool playerEndClicking;
    private bool clickFinalizado;

    public GameObject player;
    public Rigidbody2D playerBody;
    private Vector2 direccion;
    public float Vel = 2;

    [SerializeField] private Button volverBtn;
    GameObject panel;

    [SerializeField] private AudioClip fondoHielo;



    void Start()
    {
        AudioManager.instance.PlayBackMusic(fondoHielo);
        player = this.gameObject;
        playerBody = player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        currentPos = player.transform.position;

        //////////////////////////////// PARA CONTORL CON DEDO
        if (Input.touchCount > 0)
        {
            dedo = Input.GetTouch(0);
        }

        if (dedo.phase == TouchPhase.Began)
        {
            DedoInicial = dedo.position;
            CheckTouch(DedoInicial);
        }

        if (Input.touchCount > 0 && dedo.phase == TouchPhase.Ended && !playerMoving && playerTouched)
        {

            // Get current touch position       
            DedoFinal = dedo.position;

            // Get distance between current touch position and previous one.
            float distanceDedoX = DedoFinal.x - DedoInicial.x;
            float distanceDedoY = DedoFinal.y - DedoInicial.y;


            // Movimiento Dedo
            if (Mathf.Abs(distanceDedoX) - Mathf.Abs(distanceDedoY) > 0)//nos movemos en la x
            {
                if (distanceDedoX > 0)
                {
                    direccion = new Vector2(1, 0);
                    Movimiento(direccion);
                }
                else if (distanceDedoX < 0)
                {
                    direccion = new Vector2(-1, 0);
                    Movimiento(direccion);
                }

            }
            else
            {
                if (distanceDedoY > 0)
                {
                    direccion = new Vector2(0, 1);
                    Movimiento(direccion);
                }
                else if (distanceDedoY < 0)
                {
                    direccion = new Vector2(0, -1);
                    Movimiento(direccion);
                }
            }
        }
        /////////////////////////////////////////// FIN CONTROL DEDO

        /////////////////////////////////////////// CONTROL CLICK
        if (playerClicked && playerEndClicking)
        {
            playerMoving = true;
            float distanceClickX = ClickFinal.x - ClickInicial.x;
            float distanceClickY = ClickFinal.y - ClickInicial.y;


            // Movimiento Dedo
            if (Mathf.Abs(distanceClickX) - Mathf.Abs(distanceClickY) > 0)//nos movemos en la x
            {
                if (distanceClickX > 0)
                {
                    direccion = new Vector2(1, 0);
                    Movimiento(direccion);
                }
                else if (distanceClickX < 0)
                {
                    direccion = new Vector2(-1, 0);
                    Movimiento(direccion);
                }

            }
            else
            {
                if (distanceClickY > 0)
                {
                    direccion = new Vector2(0, 1);
                    Movimiento(direccion);
                }
                else if (distanceClickY < 0)
                {
                    direccion = new Vector2(0, -1);
                    Movimiento(direccion);
                }
            }
        }

        lastPos = player.transform.position;
    }

    void OnMouseOver()
    {
        //Debug.Log("Estamos encima");
        if (Input.GetMouseButtonDown(0) && !playerMoving)
        {
            playerClicked = true;
            playerEndClicking = false;
            ClickInicial = Input.mousePosition;
            //Debug.Log("clickamos el player? = " + playerClicked);
        }

    }
    private void OnMouseUp()
    {
        playerEndClicking = true;
        ClickFinal = Input.mousePosition;
    }
    public void Movimiento(Vector2 direc)
    {
        playerMoving = true;
        playerBody.MovePosition((Vector2)transform.position + (direc * Vel * Time.deltaTime));

        if (currentPos == lastPos)
        {
            playerMoving = false;
        }
    }

    public void CheckTouch(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider != null)
            {

                if (hit.transform.gameObject == player)
                {
                    playerTouched = true;
                }
                else
                {
                    playerTouched = false;
                }
            }
        }
    }
}
