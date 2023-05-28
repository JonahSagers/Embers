using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodClick : MonoBehaviour
{
    public LayerMask wood;
    public ParticleSystem woodParticles;
    public float woodCount;
    public Sparks sparks;
    
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if (Input.GetMouseButtonDown(0) && sparks.gameOver == false)
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition, wood);
            if (targetObject)
            {
                if(woodCount < 6)
                {
                    woodParticles.Play();
                    woodCount += 1;
                }
            }
        }
    }
}
