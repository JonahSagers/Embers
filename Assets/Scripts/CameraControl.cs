using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Sparks sparks;
    public new Camera camera;
    public new Vector3 transform;

    void Start()
    {
        sparks = GameObject.Find("Sparks").GetComponent<Sparks>();
        camera = gameObject.GetComponent<Camera>();
        camera.orthographicSize = 5;
    }
    //DEV NOTE: if the camera is not in the right place, set the y position to -4.5
    void Update()
    {
        if(sparks.sparkStrength == 100 && camera.orthographicSize < 10)
        {
            transform = gameObject.transform.position;
            Debug.Log("Camera Triggered");
            camera.orthographicSize += (10.1f - camera.orthographicSize) / 100;
            transform.y += (0 - transform.y) / 90;
            gameObject.transform.position = transform;
        }
    }
}
