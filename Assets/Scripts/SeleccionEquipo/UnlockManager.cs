using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : MonoBehaviour
{
    public static UnlockManager Instance;   //basicamente creamos un singleton para mantener que hemos desbloqueado un personaje entre escenas 
                                            //ya que se reinstancia characterManager cada vez que se carga
    public bool PersonajeDesbloqueado = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
