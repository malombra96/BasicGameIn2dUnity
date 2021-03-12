using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // objetivo que va a seguir la camara
    public Vector3 ofset = new Vector3(0.2f, 0.0f, -10f);
    public float dampingTime = 0.3f;
    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Application.targetFrameRate = 60; // a que frame renderiza la camara 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    void MoveCamera(bool smooth)
    {
        Vector3 destination = new Vector3(target.position.x -ofset.x, ofset.y, ofset.z);
        if (smooth)
        {
            this.transform.position = Vector3.SmoothDamp(// para un barrido suave
                                        this.transform.position,  // posicion de origen
                                        destination, // posicion de destino 
                                        ref velocity, // dato de referencia 
                                        dampingTime); // tiempo  
        }
        else
        {
            this.transform.position = destination;
        }
    }
}
