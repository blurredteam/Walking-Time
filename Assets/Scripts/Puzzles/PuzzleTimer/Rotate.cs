
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Rotate : MonoBehaviour
{
    [SerializeField] private Camera _puzzleCamera;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private GameObject circulo;

    public Transform objetoCentral; // El objeto alrededor del cual quieres rotar.
    [SerializeField] private int velocidadRotacion; // Velocidad de rotación en grados por segundo.
    [SerializeField] private int velocidadRotacionPropia;
    public Transform puntoObjetivo;

    private bool pulsado;

    private int energiaPerdida = 0;//Si hace mal el puzzle perderá

    public Transitioner transition;
    public float transitionTime = 1f;

    private void Awake()
    {
        transition = ScenesManager.instance.transitioner;
        int velocidad = Random.Range(50, 200);
        velocidadRotacion = velocidad;
        velocidadRotacionPropia = velocidad;

    }

    private void Start()
    {
        // TODOS LAS CASILLAS TENDRAN QUE TENER ALGO ASI
        _continueBtn.onClick.AddListener(delegate
        {
            StartCoroutine(EsperarYSalir());
        });
        // ---------------------------------------------
       
        pulsado = false;
    }

    IEnumerator EsperarYSalir()
    {
        //AudioManager.instance.ButtonSound();
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        transition.DoTransitionOnce();

        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzleTimer);
        LevelManager.instance.ActivateScene();
        AudioManager.instance.PlayAmbient();
    }

    void Update()
    {
        Vector3 mousePosition = _puzzleCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.ButtonSound2();
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject && targetObject.transform == gameObject.transform.parent)
            {
                // Calcular la posición relativa del objeto con respecto al punto objetivo
                Vector3 posicionRelativaObjetivo = transform.position - puntoObjetivo.position;

                // Calcular la distancia del objeto al punto objetivo
                float distanciaAlObjetivo = posicionRelativaObjetivo.magnitude;

                // Asignar una puntuación en función de la distancia
                int puntuacion = CalcularPuntuacion(distanciaAlObjetivo);
                Debug.Log("Puntuación: " + puntuacion);

                circulo.GetComponent<CircleCollider2D>().enabled = false;

                pulsado = true;
            }
        }
        else if (!pulsado)
        {
            // Calcula la posición relativa del objeto al objeto central.
            Vector3 posicionRelativa = transform.position - objetoCentral.position;

            // Calcula el ángulo de rotación basado en la velocidad y el tiempo.
            float anguloRotacion = velocidadRotacion * Time.deltaTime;

            // Rota la posición relativa alrededor del objeto central.
            Vector3 nuevaPosicionRelativa = Quaternion.Euler(0, 0, anguloRotacion) * posicionRelativa;

            // Establece la nueva posición del objeto basada en la posición relativa.
            transform.position = objetoCentral.position + nuevaPosicionRelativa;

            float anguloRotacionPropia = velocidadRotacionPropia * Time.deltaTime;
            transform.Rotate(Vector3.forward * anguloRotacionPropia);
        }

    }

    int CalcularPuntuacion(float distancia)
    {
        // Define tu lógica para asignar puntuaciones en función de la distancia.
        // Puedes usar una función matemática o establecer rangos de distancia para diferentes puntuaciones.
        if (distancia < 0.5f)
        {
            CanvasTimer.instance.SetEnergiaPerdida(0);
            return 100;

        }
        else if (distancia < 0.7f)
        {
            energiaPerdida += 5;
            LevelManager.instance.teamEnergy -= 5;
            CanvasTimer.instance.SetEnergiaPerdida(5);
            return 50;

        }
        else
        {
            LevelManager.instance.teamEnergy -= 10;
            CanvasTimer.instance.SetEnergiaPerdida(10);
            return 10;
        }
    }


}