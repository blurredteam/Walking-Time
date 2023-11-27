using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecogeGotas : MonoBehaviour
{
    public static RecogeGotas instance;

    [SerializeField] private float limiteX = 7.8f;
    [SerializeField] private Animator anim;//Lo dejo por si en algun momento tiene animacion

    [SerializeField] private TextMeshProUGUI aguaTexto;
    [SerializeField] private TextMeshProUGUI venenoTexto;
    [SerializeField] private TextMeshProUGUI timer;

    [SerializeField] private TextMeshProUGUI textoFinal;

    [SerializeField]
    private GameObject panelInicio;
    [SerializeField]
    private GameObject panelFinal;

    [SerializeField] private AudioClip gotaAgua;
    [SerializeField] private AudioClip venenoAudio;

    public int gotasRecogidas = 0;
    public int venenoRecogido = 0;

    public int gotasTotales = 0;
    private float tiempoRestante = 20f; // Tiempo inicial de cuenta regresiva

    //private int direccion = 1;
    private bool movimientoEmpezado = false;


    public bool MovimientoEmpezado { get { return movimientoEmpezado; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        aguaTexto.text = gotasRecogidas.ToString();
        venenoTexto.text = venenoRecogido.ToString();
        timer.text = Mathf.Round(tiempoRestante).ToString();

        //if (Input.GetMouseButtonDown(0) && !movimientoEmpezado)
        //{
        //    movimientoEmpezado = true;
        //}
        if (!movimientoEmpezado) return;

        // Actualizar la cuenta regresiva
        tiempoRestante -= Time.deltaTime;

        // Verificar si se ha agotado el tiempo
        if (tiempoRestante <= 0)
        {
            Debug.Log("Fin del juego");
            FinalJuego();
           
        }

        MoverConMouse();

        //anim.SetBool("Strt", movimientoEmpezado);
    }

    private void MoverConMouse()
    {
        // Obtener la posición del ratón en el mundo
        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Mantener la posición y fija
        posicionMouse.y = transform.position.y;

        // Mantener la posición z fija
        posicionMouse.z = transform.position.z;

        // Limitar la posición en el eje X
        posicionMouse.x = Mathf.Clamp(posicionMouse.x, -limiteX, limiteX);

        // Mover el objeto directamente hacia la posición del ratón
        transform.position = posicionMouse;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            venenoRecogido++;
            Debug.Log("Mala, has cogido veneno, llevas "+venenoRecogido);
            AudioManager.instance.PlaySfx(venenoAudio);

        }
        if (collision.CompareTag("Gota"))
        {
            gotasRecogidas++;
            gotasTotales++;
            Debug.Log("Bien, has cogido agua, llevas " + gotasRecogidas);
            AudioManager.instance.PlaySfx(gotaAgua);

        }

    }
    public void EmpezarJuego()
    {
        panelInicio.SetActive(false);
        movimientoEmpezado = true;
    }
    private void FinalJuego()
    {
        movimientoEmpezado = false;
        panelFinal.SetActive(true);
        textoFinal.text = "DE TODO LO CAÍDO HAS RECOGIDO " + gotasRecogidas + " GOTAS DE AGUA DE LAS " + gotasTotales + " POSIBLES Y " + venenoRecogido + " GOTAS DE VENENO.";
        Recompensas();
    }
    private void Recompensas()
    {

    }
}
    

