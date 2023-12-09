using System.Collections.Generic;
using UnityEngine;

public class MapCamaraMovement1 : MonoBehaviour
{
    [SerializeField] private Camera _cameraRef;
    [SerializeField] private int mapOriginLimit;    // Limite de drag por la izquierda (x = 0)
    [SerializeField] private int mapEndLimit;       // Limite de drag por la derecha (final del mapa)

    private Vector3 origin;
    private Vector3 difference;

    private bool drag = false;

    // Parallax
    [SerializeField] private List<SpriteRenderer> bgImgs= new List<SpriteRenderer>();
    [SerializeField] private List<float> parallaxMultipliers= new List<float>();
    private Vector3 lastCameraPosition;

    private void Start()
    {
        lastCameraPosition = _cameraRef.transform.position;
    }

    private void LateUpdate()
    {
        PlayerMapMovement();
        ParralaxEffect();
    }

    // Gestiona el movimiento de click and drag por el mapa
    private void PlayerMapMovement()
    {
        if (Input.GetMouseButton(0))
        {
            difference = _cameraRef.ScreenToWorldPoint(Input.mousePosition) - _cameraRef.transform.position;
            if (drag == false)
            {
                drag = true;
                origin = _cameraRef.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else drag = false;

        float x = origin.x - difference.x;
        float z = origin.z - difference.z;
        if (drag && x > mapOriginLimit && x < mapEndLimit) _cameraRef.transform.position = new Vector3(x, 0, z);  //origin - difference;

    }

    // Aplica el efecto paralaje a las imagenes que conforman el fondo
    private void ParralaxEffect()
    {
        Vector3 deltaMovement = _cameraRef.transform.position - lastCameraPosition;
        for (int i = 0; i < bgImgs.Count; i++) bgImgs[i].gameObject.transform.position -= new Vector3(deltaMovement.x * parallaxMultipliers[i], deltaMovement.y);
        lastCameraPosition = _cameraRef.transform.position;
    }

}