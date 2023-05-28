using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Sparks sparks;
    public Camera cam;
    public Vector3 pos;

    void Start()
    {
        cam.orthographicSize = 5;
    }
    //if the camera is not in the right place, set the y position to -4.5
    void Update()
    {
        if(sparks.sparked && cam.orthographicSize < 11)
        {
            pos = gameObject.transform.position;
            cam.orthographicSize += (11.1f - cam.orthographicSize) / 100;
            pos.y += (0 - pos.y) / 90;
            gameObject.transform.position = pos;
        }
    }
}
