using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CursorImpulse : MonoBehaviour
{
    public Vector3 mousePosition;
    public List<Vector2> pastForces;
    public Vector3 lastPosition;
    public ParticleSystemForceField field;
    public Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pastForces.Add((mousePosition - lastPosition) / Time.deltaTime);
        lastPosition = mousePosition;
        transform.position = new Vector3(mousePosition.x,mousePosition.y,0);
        pastForces.RemoveAt(0);
        force = (pastForces.Aggregate(new Vector2(0,0), (s,v) => s + v) / (float)pastForces.Count);
        field.directionX = force.x;
        field.directionY = force.y;
    }
}
