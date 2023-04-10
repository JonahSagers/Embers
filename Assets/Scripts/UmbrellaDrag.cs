using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaDrag : MonoBehaviour
{
    public Rigidbody2D rb;
    public WindImpulse wind;
    public BoxCollider2D box;
    public GameObject selectedObject;
    public Sparks sparks;
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
        if (selectedObject)
        {
            mouseForce = (mousePosition - lastPosition) / Time.deltaTime;
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
            if(mousePosition.x < -17.1f)
            {
                mousePosition.x = -17.1f;
            }
            if(mousePosition.x > 17.1f)
            {
                mousePosition.x = 17.1f;
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
            rb.AddForce(new Vector3(wind.windForce / 4,0,0), ForceMode2D.Impulse);
        }
        umbrellaPos = gameObject.transform.position;
        if(selectedObject)
        {
            umbrellaPos.x += wind.windForce / 2;
        }
        
        //if(umbrellaPos.y < -6.5f)
        //{
        //    umbrellaPos.y = -6.5f;
        //}
        //if(umbrellaPos.y > 7.5f)
        //{
        //    umbrellaPos.y = 7.5f;
        //}
        if(umbrellaPos.x < -21.5f)
        {
            umbrellaPos.x = -21.5f;
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
        if(umbrellaPos.x > 21.5f)
        {
            umbrellaPos.x = 21.5f;
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
        gameObject.transform.position = umbrellaPos;
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            rb.AddForce(mouseForce/5, ForceMode2D.Impulse);
            selectedObject = null;
        }

    }
}