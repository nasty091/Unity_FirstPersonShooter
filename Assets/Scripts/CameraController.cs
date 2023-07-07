using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;

    //FOV is Field Of View in Main Camera
    private float startFOV, targetFOV;

    public float zoomSpeed = 1f;

    public Camera theCam;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        startFOV = theCam.fieldOfView;
        targetFOV = startFOV;
    }

    //LateUpdate will be called as soon as Update done
    void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
        //Debug.Log(Time.deltaTime);
        //Debug.Log(theCam.fieldOfView);
    }

    public void ZoomIn(float newZoom)
    {
        targetFOV = newZoom;
    }

    public void ZoomOut()
    {
        targetFOV = startFOV;
    }
}
