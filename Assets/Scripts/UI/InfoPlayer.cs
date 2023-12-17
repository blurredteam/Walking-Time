using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InfoPlayer : MonoBehaviour
{
    public static InfoPlayer Instance;
    [SerializeField] private TextMeshProUGUI textPanelLogin;
    [SerializeField] private TextMeshProUGUI textPanelReg;
    [SerializeField] private TextMeshProUGUI textPanelNombre;
    [SerializeField] private TextMeshProUGUI textPanelTeclado;
    [SerializeField] private TextMeshProUGUI textPanelEdad;
    [SerializeField] private TextMeshProUGUI textPanelSexo;

    [SerializeField] private List<GameObject> _paneles = new List<GameObject>();
    [SerializeField] private List<GameObject> _botones = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI textoEdad;

    //I de inicio de sesion y R de registro
    [SerializeField] private TextMeshProUGUI nombreText;
    [SerializeField] private TextMeshProUGUI nombreText2;
    [SerializeField] private TextMeshProUGUI nombreText3;
    [SerializeField] private TextMeshProUGUI userTextI;
    [SerializeField] private TextMeshProUGUI userTextI2;
    [SerializeField] private TextMeshProUGUI userTextI3;
    [SerializeField] private TextMeshProUGUI passTextI;
    [SerializeField] private TextMeshProUGUI passTextI2;
    [SerializeField] private TextMeshProUGUI passTextI3;
    [SerializeField] private TextMeshProUGUI userTextR;
    [SerializeField] private TextMeshProUGUI userTextR2;
    [SerializeField] private TextMeshProUGUI userTextR3;
    [SerializeField] private TextMeshProUGUI passTextR;
    [SerializeField] private TextMeshProUGUI passTextR2;
    [SerializeField] private TextMeshProUGUI passTextR3;

    [SerializeField] private Transitioner transition;
    [SerializeField] private float transitionTime = 1f;

    [SerializeField] private GameObject panelNormal;
    [SerializeField] private GameObject panelTeclado;
    [SerializeField] private GameObject panelUsuario;
    [SerializeField] private GameObject panelUsuarioTeclado;
    [SerializeField] private GameObject panelContraseñaTeclado;
    [SerializeField] private GameObject panelRegistro;
    [SerializeField] private GameObject panelUsuarioTeclado2;
    [SerializeField] private GameObject panelContraseñaTeclado2;

    [SerializeField]
    private Button inicioSesion;
    [SerializeField]
    private Button registrarse;

    [SerializeField] private DatabaseManager database;
    //[SerializeField] private TextMeshProUGUI salidaSexo;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        transition = ScenesManager.instance.transitioner;
    }
    // public void Empezar()
    // {
    //     StartCoroutine(DoTransition(0, 0));
    // }

    public void ActPanelEdad()
    {
        StartCoroutine(DoTransition(0, 1));
    }

    public void ActPanelSexo()
    {
        StartCoroutine(DoTransition(1, 2));
    }
    
    public void ActPanelLogin()
    {
        StartCoroutine(DoTransition(4, 5));
    }
    
    public void ActPanelReg()
    {
        StartCoroutine(DoTransition(4, 6));
    }
    
    public void ActPanelNombre()
    {
        StartCoroutine(DoTransition(5, 0));
    }
    public void ActPanelNombre2()
    {
        StartCoroutine(DoTransition(6, 0));
    }

    IEnumerator DoTransition(int from, int to)
    {
        transition.DoTransitionOnce();
        AudioManager.instance.ButtonSound();

        yield return new WaitForSeconds(transitionTime);

        transition.DoTransitionOnce();
        _paneles[from].SetActive(false);
        _paneles[to].SetActive(true);
    }

    public void SetEdad(float valor)
    {
        //AudioManager.instance.ButtonSound2();
        int edad = Mathf.FloorToInt(valor);
        textoEdad.text = edad.ToString();
        GameManager.instance.edadJugador = edad;
        textPanelEdad.text = "Interesante. Veremos como de lejos llegas, espero que me sorprendas "+ GameManager.instance.nombreJugador+", que llevo milenios aburrido.";
        _botones[1].SetActive(true);
    }
    public void SetNombre(string nombre)
    {
        if (nombre.Length >= 2)
        {
            Debug.Log(nombre.Length);
            SinTeclado();
            nombreText.text = nombre;
            nombreText2.text = nombre;
            nombreText3.text = nombre;
            AudioManager.instance.ButtonSound3();
            Debug.Log(nombre);
            GameManager.instance.nombreJugador = nombre;
            textPanelNombre.text = "Vaya vaya, así que te llamas " + nombre + ", no te pega mucho con la cara la verdad, te pegaría más un nombre como...\nHmmm no sé...\n¿Finito quizá?, exxxpléndido.\nBueno, espera, así ya me llamo yo\nJAJAJAJAJAJAJAJA.";
            textPanelTeclado.text = "¿No te convence "+nombre+"?\n Normal a mí tampoco me gusta\nAJAJAJAJAJAJJAJA\nPero date prisa tengo otros seres inteligentes que observar por el universo.";
            _botones[0].SetActive(true);
        }
        else
        {
            textPanelNombre.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
            textPanelTeclado.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
        }
    }

    public void SetSexo(int val)
    {
        try
        {
            switch (val)
            {
                case 2:
                    GameManager.instance.sexoJugador = "Masculino";
                    break;
                case 1:
                    GameManager.instance.sexoJugador = "Femenino";
                    break;
                default:
                    // Lanza una excepci�n si el valor no es 0 ni 1
                    throw new ArgumentOutOfRangeException(nameof(val), "El valor debe ser 0 o 1.");
            }
        }
        catch (Exception e)
        {
            // Maneja la excepci�n aqu� (puedes imprimir un mensaje, registrarla, etc.)
            Debug.LogError($"Error al manejar el valor {val}: {e.Message}");
        }
       
        //if (val == 0) { GameManager.instance.sexoJugador = "Masculino"; }
        //if (val == 1) { GameManager.instance.sexoJugador = "Femenino"; }
        textPanelSexo.text = "Has visto, mi poder es tal que puedo atravesar hasta la cuarta pared\nAJAJAJAJAJAJAJAJA.\nMe retiro, pero estaré vigilándote " + GameManager.instance.nombreJugador + ", quizá nos encontremos por ahí...\nJAJAJAJAJAJAJAJAJAJA";
       _botones[2].SetActive(true);
        AudioManager.instance.RisaFinito();
    }
     public void ConTeclado()
     {
        AudioManager.instance.ButtonSound2();
        panelNormal.SetActive(false);
        panelTeclado.SetActive(true);
     }

    public void SinTeclado()
    {
        AudioManager.instance.ButtonSound2();
        panelNormal.SetActive(true);
        panelTeclado.SetActive(false);
    }
    public void ConTecladoUsuario()
    {
        AudioManager.instance.ButtonSound2();
        panelUsuario.SetActive(false);
        panelUsuarioTeclado.SetActive(true);
    }

    public void SinTecladoUsuario()
    {
        AudioManager.instance.ButtonSound2();
        panelUsuario.SetActive(true);
        panelUsuarioTeclado.SetActive(false);
    }
    public void ConTecladoContraseña()
    {
        AudioManager.instance.ButtonSound2();
        panelUsuario.SetActive(false);
        panelContraseñaTeclado.SetActive(true);
    }

    public void SinTecladoContraseña()
    {
        AudioManager.instance.ButtonSound2();
        panelUsuario.SetActive(true);
        panelContraseñaTeclado.SetActive(false);
    }
    public void ConTecladoUsuario2()
    {
        AudioManager.instance.ButtonSound2();
        panelRegistro.SetActive(false);
        panelUsuarioTeclado2.SetActive(true);
    }

    public void SinTecladoUsuario2()
    {
        AudioManager.instance.ButtonSound2();
        panelRegistro.SetActive(true);
        panelUsuarioTeclado2.SetActive(false);
    }
    public void ConTecladoContraseña2()
    {
        AudioManager.instance.ButtonSound2();
        panelRegistro.SetActive(false);
        panelContraseñaTeclado2.SetActive(true);
    }

    public void SinTecladoContraseña2()
    {
        AudioManager.instance.ButtonSound2();
        panelRegistro.SetActive(true);
        panelContraseñaTeclado2.SetActive(false);
    }
    public void SetDatabaseInfo()
    {
        database.CreatePostLogin(GameManager.instance.user,GameManager.instance.nombreJugador,GameManager.instance.password, GameManager.instance.edadJugador,GameManager.instance.sexoJugador);
    }

    public void SetUsuario(string usuario)
    {
        if (usuario.Length >= 2)
        {
            Debug.Log(usuario.Length);
            SinTecladoUsuario();
            userTextI.text = usuario;
            userTextI2.text = usuario;
            userTextI3.text = usuario;
            AudioManager.instance.ButtonSound3();
            Debug.Log(usuario);
            GameManager.instance.user = usuario;
            CheckLogin();
            //textPanelLogin.text=textPanelReg.text = "Vaya vaya, así que te llamas " + usuario + ", no te pega mucho con la cara la verdad, te pegaría más un nombre como...\nHmmm no sé...\n¿Finito quizá?, exxxpléndido.\nBueno, espera, así ya me llamo yo\nJAJAJAJAJAJAJAJA.";
            //textPanelTeclado.text = "¿No te convence "+usuario+"?\n Normal a mí tampoco me gusta\nAJAJAJAJAJAJJAJA\nPero date prisa tengo otros seres inteligentes que observar por el universo.";
            // _botones[0].SetActive(true);
        }
        else
        {
            //textPanelLogin.text=textPanelReg.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
            //textPanelTeclado.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
        }
    }
    
    public void SetPassword(string pass)
    {
        if (pass.Length >= 2)
        {
            Debug.Log(pass.Length);
            SinTecladoContraseña();
            passTextI.text = pass;
            passTextI2.text = pass;
            passTextI3.text = pass;
            AudioManager.instance.ButtonSound3();
            Debug.Log(pass);
            GameManager.instance.password = pass;
            CheckLogin();
            //textPanelLogin.text=textPanelReg.text = "Vaya vaya, así que te llamas " + pass + ", no te pega mucho con la cara la verdad, te pegaría más un nombre como...\nHmmm no sé...\n¿Finito quizá?, exxxpléndido.\nBueno, espera, así ya me llamo yo\nJAJAJAJAJAJAJAJA.";
            //textPanelTeclado.text = "¿No te convence "+pass+"?\n Normal a mí tampoco me gusta\nAJAJAJAJAJAJJAJA\nPero date prisa tengo otros seres inteligentes que observar por el universo.";
            //_botones[3].SetActive(true);
            //Login();
        }
        else
        {
            //textPanelLogin.text=textPanelReg.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
            //textPanelTeclado.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
        }
    }
    public void SetUsuario2(string usuario)
    {
        if (usuario.Length >= 2)
        {
            Debug.Log(usuario.Length);
            SinTecladoUsuario2();
            userTextR.text = usuario;
            userTextR2.text = usuario;
            userTextR3.text = usuario;
            AudioManager.instance.ButtonSound3();
            Debug.Log(usuario);
            GameManager.instance.user = usuario;
            CheckReg();
            //textPanelLogin.text=textPanelReg.text = "Vaya vaya, así que te llamas " + usuario + ", no te pega mucho con la cara la verdad, te pegaría más un nombre como...\nHmmm no sé...\n¿Finito quizá?, exxxpléndido.\nBueno, espera, así ya me llamo yo\nJAJAJAJAJAJAJAJA.";
            //textPanelTeclado.text = "¿No te convence "+usuario+"?\n Normal a mí tampoco me gusta\nAJAJAJAJAJAJJAJA\nPero date prisa tengo otros seres inteligentes que observar por el universo.";
            // _botones[0].SetActive(true);
        }
        else
        {
            //textPanelLogin.text=textPanelReg.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
            //textPanelTeclado.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
        }
    }

    public void SetPassword2(string pass)
    {
        if (pass.Length >= 2)
        {
            Debug.Log(pass.Length);
            SinTecladoContraseña2();
            passTextR.text = pass;
            passTextR2.text = pass;
            passTextR3.text = pass;
            AudioManager.instance.ButtonSound3();
            Debug.Log(pass);
            GameManager.instance.password = pass;
            CheckReg();
            //textPanelLogin.text=textPanelReg.text = "Vaya vaya, así que te llamas " + pass + ", no te pega mucho con la cara la verdad, te pegaría más un nombre como...\nHmmm no sé...\n¿Finito quizá?, exxxpléndido.\nBueno, espera, así ya me llamo yo\nJAJAJAJAJAJAJAJA.";
            //textPanelTeclado.text = "¿No te convence "+pass+"?\n Normal a mí tampoco me gusta\nAJAJAJAJAJAJJAJA\nPero date prisa tengo otros seres inteligentes que observar por el universo.";
            //_botones[3].SetActive(true);
            //Login();
        }
        else
        {
            //textPanelLogin.text=textPanelReg.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
            //textPanelTeclado.text = "Venga humano, pon algo reconocible, puedes engañarme si quieres, pero al menos pon algo.";
        }
    }

    public void Login()
    {
        textPanelLogin.text = "Has iniciado sesión corréctamente";
        database.CreateGetLogin(GameManager.instance.user);
        _botones[3].SetActive(true);
    }
    public void Reg()
    {
        textPanelReg.text = "Te has registrado corréctamente";
        database.CreateGetLogin(GameManager.instance.user);
        _botones[4].SetActive(true);
    }

    public void CheckLogin()
    {
        if ((userTextI2.text.Length >= 2 && passTextI2.text.Length >= 2) || (userTextI3.text.Length >= 2 && passTextI3.text.Length >= 2))
        {
            inicioSesion.interactable = true;

        }
    }
    public void CheckReg()
    {
        if ((userTextR2.text.Length >= 2 && passTextR2.text.Length >= 2) || (userTextR3.text.Length >= 2 && passTextR3.text.Length >= 2))
        {
            registrarse.interactable = true;

        }
    }

    private void Update()
    {
        
        if (GameManager.instance.passCorrecta)
        {
            print(true);
            _botones[3].SetActive(true);
            _botones[4].SetActive(false);
            GameManager.instance.passCorrecta = false;
        }

        if (GameManager.instance.regCorrecto)
        {
            _botones[4].SetActive(true);
            _botones[3].SetActive(false);
            GameManager.instance.regCorrecto = false;
        }
    }
}
