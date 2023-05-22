using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UmbrellaDrag : MonoBehaviour
{
    public Rigidbody2D rb;
    public WindImpulse wind;
    public BoxCollider2D box;
    public GameObject selectedObject;
    public Sparks sparks;
    public ScreenBounds bounds;
    public Vector3 umbrellaPos;
    public float Xpos;
    public float Ypos;
    public float Xvel;
    public float Yvel;
    Vector3 offset;
    Vector3 rotation;
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
                offset = selectedObject.transform.position - mousePosition;
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
            mousePosition.y += 1.5f;
            if(mousePosition.y < -7.523f)
            {
                mousePosition.y = -7.523f;
            }
            if(mousePosition.y > 8.45f)
            {
                mousePosition.y = 8.45f;
            }
            if(mousePosition.x < bounds.bottomLeftCorner.x + 2.52f)
            {
                mousePosition.x = bounds.bottomLeftCorner.x + 2.52f;
            }
            if(mousePosition.x > bounds.topRightCorner.x - 2.52f)
            {
                mousePosition.x = bounds.topRightCorner.x - 2.52f;
            }
            Xpos = selectedObject.transform.position.x;
            Ypos = selectedObject.transform.position.y;
            selectedObject.transform.position = mousePosition;
            rotation = selectedObject.transform.rotation.eulerAngles;
            if(rotation.z > 180){
                rotation.z = (rotation.z - 360);
            }
            selectedObject.transform.rotation = Quaternion.AngleAxis(rotation.z/1.25f, Vector3.forward);
            Xvel = selectedObject.transform.position.x - Xpos;
            Yvel = selectedObject.transform.position.y - Ypos;
            rotation = selectedObject.transform.rotation.eulerAngles;
            if(rotation.z > 180){
                rotation.z = (rotation.z - 360);
            }
            selectedObject.transform.rotation = Quaternion.AngleAxis(rotation.z + Xvel * 5, Vector3.forward);
        }
        else
        {
            rb.AddForce(new Vector3(wind.windForce * 3,0,0) * Time.deltaTime, ForceMode2D.Impulse);
        }
        umbrellaPos = gameObject.transform.position;
        if(selectedObject)
        {
            umbrellaPos.x += wind.windForce / 3;
        }
        gameObject.transform.position = umbrellaPos;
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            mouseForce = pastForces.Aggregate(new Vector2(0,0), (s,v) => s + v) / (float)pastForces.Count;
            rb.AddForce(mouseForce/5, ForceMode2D.Impulse);
            selectedObject = null;
        }

    }
}