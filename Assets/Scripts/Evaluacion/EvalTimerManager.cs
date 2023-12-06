using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvalTimerManager : MonoBehaviour
{
    private static EvalTimerManager instance;
    [SerializeField] public float timer = 0f;
    private bool paused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (paused == false)
        {
            timer += Time.deltaTime;
        }
    }

    public void changeTimerState()
    {
        if (paused == false)
        {
            paused = true;
        }
        else { paused = false; }
    }

    public float GetTimerValue()
    {
        return timer;
    }
}
