using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodToss : MonoBehaviour
{
    public Rigidbody2D rb;

    void Awake()
    {
        rb.velocity = new Vector2(Random.Range(6,12),Random.Range(8,12));
        rb.angularVelocity = Random.Range(-720, 180);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.name == "Sparks"){
            hit.gameObject.GetComponent<Sparks>().StokeFire();
            Destroy(gameObject);
        }
    }
}
