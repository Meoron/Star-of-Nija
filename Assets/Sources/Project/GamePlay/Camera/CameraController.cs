using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private int pixelPerUnit = 16;
    private Camera cameraSpecification;

    private void Awake(){
        //cameraSpecification = GetComponent<Camera>();
        //cameraSpecification.orthographicSize = Screen.height / pixelPerUnit / 2;
    }
}
