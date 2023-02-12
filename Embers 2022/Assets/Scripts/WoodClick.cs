using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodClick : MonoBehaviour
{
    public LayerMask wood;
    public ParticleSystem woodParticles;
    public float woodCount;
    public Sparks sparks;
    // Start is called before the first frame update
    void Start()
    {
        sparks = GameObject.Find("Sparks").GetComponent<Sparks>();
        woodParticles = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if (Input.GetMouseButtonDown(0) && sparks.gameOver == false)
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition, wood);
            if (targetObject)
            {
                Debug.Log("Wood Clicked");
                woodParticles.Play();
                if(woodCount < 6)
                {
                    woodCount += 1;
                }
            }
        }
    }
}
