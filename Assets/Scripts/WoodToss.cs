using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodToss : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb.velocity = new Vector2(Random.Range(10,20),Random.Range(8,12));
        rb.angularVelocity = Random.Range(-600, 200);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.name == "Sparks"){
            Debug.Log("Stoked");
            hit.gameObject.GetComponent<Sparks>().StokeFire();
            Destroy(gameObject);
        }
    }
}
