using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UmbrellaDrag : MonoBehaviour
{
    public Rigidbody2D rb;
    public WindImpulse wind;
    public GameObject selectedObject;
    public Sparks sparks;
    public ScreenBounds bounds;
    public float Xpos;
    public float Xvel;
    public Vector3 rotation;
    Vector2 mouseForce;
    Vector3 lastPosition;
    public LayerMask umbrella;
    public List<Vector2> pastForces;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if (Input.GetMouseButtonDown(0) && sparks.sparkStrength >= 100 && sparks.gameOver == false)
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition, umbrella);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
            }
        }
        if(pastForces.Count > 5 || (pastForces.Count > 0 && !selectedObject)){
            pastForces.RemoveAt(0);
        }
        if (selectedObject)
        {
            pastForces.Add((mousePosition - lastPosition) / Time.deltaTime);
            lastPosition = mousePosition;
            rb.velocity = Vector2.zero;
            mousePosition = new Vector3(Mathf.Clamp(mousePosition.x, bounds.bottomLeftCorner.x + 2.52f, bounds.topRightCorner.x - 2.52f), Mathf.Clamp(mousePosition.y + 1.5f, -7.523f, 8.45f));
            //I know it seems redundant to store position, but this is for saving the previous position of the umbrella to calculate velocity
            Xpos = selectedObject.transform.position.x;
            selectedObject.transform.position = mousePosition;
            rotation = selectedObject.transform.rotation.eulerAngles;
            Xvel = selectedObject.transform.position.x - Xpos;
            if(rotation.z > 180){
                rotation.z -= 360;
            }
            selectedObject.transform.Rotate(0, 0, -rotation.z/4 + Xvel * 5, Space.World);
            transform.position += new Vector3(0,0,(Mathf.Log(Mathf.Abs(wind.windForce/100) + 0.01f, 10) + 2) * Mathf.Sign(wind.windForce));
        }
        else
        {
            rb.AddForce(new Vector3(wind.windForce * 3,0,0) * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            mouseForce = pastForces.Aggregate(new Vector2(0,0), (s,v) => s + v) / (float)pastForces.Count;
            rb.AddForce(mouseForce/5, ForceMode2D.Impulse);
            selectedObject = null;
        }

    }
}