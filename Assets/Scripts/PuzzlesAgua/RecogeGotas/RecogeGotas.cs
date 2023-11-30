using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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
    private GameObject panelInfo;
    [SerializeField]
    private GameObject panelInicio;
    [SerializeField]
    private GameObject panelFinal;

    [SerializeField] private AudioClip fondo;
    [SerializeField] private AudioClip gotaAgua;
    [SerializeField] private AudioClip venenoAudio;

    [SerializeField] private Button volverBtn;

    public int gotasRecogidas = 0;
    public int venenoRecogido = 0;

    public int gotasTotales = 0;
    private float tiempoRestante = 20f; // Tiempo inicial de cuenta regresiva

    private Transitioner transition;
    public float transitionTime = 1f;

    //private int direccion = 1;
    private bool movimientoEmpezado = false;


    public bool MovimientoEmpezado { get { return movimientoEmpezado; } }

    private void Awake()
    {
        transition = ScenesManager.instance.transitioner;
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        volverBtn.onClick.AddListener(delegate
        {
            StartCoroutine(EsperarYSalir());
        });
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
        AudioManager.instance.ButtonSound();
        AudioManager.instance.PlayBackMusic(fondo);
        panelInicio.SetActive(false);
        panelInfo.SetActive(true);
        movimientoEmpezado = true;
    }
    private void FinalJuego()
    {
        movimientoEmpezado = false;
        panelFinal.SetActive(true);
        panelInfo.SetActive(false);
        textoFinal.text = "DE TODO LO CAÍDO HAS RECOGIDO " + gotasRecogidas + " GOTAS DE AGUA DE LAS " + gotasTotales + " POSIBLES Y " + venenoRecogido + " GOTAS DE VENENO.";
        Recompensas();
    }
    private void Recompensas()
    {
        int aguaMinima = gotasTotales - Mathf.RoundToInt(gotasTotales / 3);//Si consigue el 66% de las gotas consigue un uso
        if((gotasRecogidas==gotasTotales) && (venenoRecogido == 0))
        {
            textoFinal.text = "ENHORABUENA, HAS RECOGIDO TODAS LAS GOTAS POSIBLES DE AGUA LIMPIA. ¡GANAS 2 USOS DE AGUA!";
            AudioManager.instance.WinMusic();
            AguaGanada(2);

        }
        else if(venenoRecogido > 0)
        {
            textoFinal.text = "VAYA... HAS MEZCLADO TU AGUA CON VENENO, HAS CONTAMINADO TODA TU AGUA POR LO QUE DEBES TIRARLA.";
            AudioManager.instance.LoseMusic();
            LevelManager.instance.teamWater = 0;
        }
        else if(gotasRecogidas >= aguaMinima)
        {
            textoFinal.text = "HAS CONSEGUIDO AGUA SUFICIENTE PARA RECUPERAR UN USO DE AGUA PERO PODRÍAS RECOGER MÁS.";
            AudioManager.instance.KindaLoseMusic();
            AguaGanada(1);
        }
        else
        {
            textoFinal.text = "VAYA... NO HAS CONSEGUIDO RECOGER SUFICIENTE AGUA.";
            AudioManager.instance.LoseMusic();
        }
    }

    private void AguaGanada(int cantAguaGanada)
    {
        int maxAgua = LevelManager.instance.maxWater;
        int agua = LevelManager.instance.teamWater;

        if ((cantAguaGanada + agua) >= maxAgua)
        {
            LevelManager.instance.teamWater = maxAgua;
        }
        else
        {
            LevelManager.instance.teamWater += cantAguaGanada;
        }
    }

    IEnumerator EsperarYSalir()
    {
        AudioManager.instance.ButtonSound();
        transition.DoTransitionOnce();

        yield return new WaitForSeconds(transitionTime);
        transition.DoTransitionOnce();
        ScenesManager.instance.UnloadTile(ScenesManager.Scene.PuzzleGotas);
        LevelManager.instance.ActivateScene();
        AudioManager.instance.PlayAmbient();
    }
}
    

