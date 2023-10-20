using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamaraMovement : MonoBehaviour
{
    [SerializeField] private Camera _cameraRef;
    
    private Vector3 origin;
    private Vector3 difference;
    private Vector3 resetCamera;

    private bool drag = false;

    private void Start()
    {
        resetCamera = _cameraRef.transform.position;
    }

    private void LateUpdate()
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
        if (drag && x > 0 && x < 40) _cameraRef.transform.position = new Vector3(x, 0, z);  //origin - difference;

        if (Input.GetMouseButton(1))
        {
            _cameraRef.transform.position = resetCamera;
        }
    }

    
}
