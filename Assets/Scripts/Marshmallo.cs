using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marshmallo : MonoBehaviour
{
    public float cooked;
    public float optimalStrength;
    public Sparks sparks;
    public float heat;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        sparks = GameObject.Find("Sparks").GetComponent<Sparks>();
        optimalStrength = Random.Range(500, 750);
        transform.position = new Vector3(Random.Range(-2, 2),-2 + optimalStrength / 100,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        heat = optimalStrength - sparks.fireStrength;
        if(Mathf.Abs(heat) < 50){
            Debug.Log("Marshmallo Cooking");
            cooked += 0.25f;
        } else {
            if(heat > 50){
                Debug.Log("Marshmallo Burned");
                cooked = 100;
            }
        }
    }

    void OnMouseDown()
    {
        if(cooked > 75 && cooked < 100)
        {
            Debug.Log("Marshmallo Collected");
        }
    }
}
