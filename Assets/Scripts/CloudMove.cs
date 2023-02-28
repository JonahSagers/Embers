using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CloudMove : MonoBehaviour
{
    //public ParticleSystem fire;
    public Sparks sparks;
    public BoxCollider2D sparksRb;
    public BoxCollider2D umbrellaRb;
    public float Xvel;
    public float Xpos;
    public ParticleSystem RainParticles;
    public ParticleSystem CloudParticles;
    Vector3 Position;
    public LayerMask collisions;
    Vector2 Below;
    Vector2 Pos;
    public WindImpulse wind;
    public bool raining;
    public Light2D lightning;
    public float lightningTime;
    public int lightningPoint;
    // Start is called before the first frame update
    void Start()
    {
        raining = false;
        //fire = GameObject.FindGameObjectWithTag("Fire").GetComponent<ParticleSystem>();
        lightning = gameObject.GetComponent<Light2D>();
        sparks = GameObject.FindGameObjectWithTag("Sparks").GetComponent<Sparks>();
        sparksRb = GameObject.FindGameObjectWithTag("Sparks").GetComponent<BoxCollider2D>();
        umbrellaRb = GameObject.FindGameObjectWithTag("Umbrella").GetComponent<BoxCollider2D>();
        CloudParticles = GetComponent<ParticleSystem>();
        RainParticles = GameObject.Find("Rain").GetComponent<ParticleSystem>();
        wind = GameObject.Find("WindImpulse").GetComponent<WindImpulse>();
        lightningTime = 0;
        lightningPoint = Random.Range(100, 200);
    }

    // Update is called once per frame
    void Update()
    {
        Position = gameObject.transform.position;
        
        if(sparks.sparkStrength >= 100 && wind.difficulty > 1.05f)
        {
            if(raining == false)
            {
                RainParticles.Play();
                CloudParticles.Play();
                raining = true;
            }
            
            
            if(Position.x < -20f)
            {
                Xvel += wind.difficulty * 0.001f;
            }
            if(Position.x > 20f)
            {
                Xvel -= wind.difficulty * 0.001f;
            }
            
            gameObject.transform.position += new Vector3(Xvel,0,0);
            lightningTime += 1;
            if(lightningTime == lightningPoint)
            {
                lightning.intensity = 10;
                lightning.pointLightOuterRadius = 10;
            }
            lightning.intensity /= 1.5f;
            lightning.pointLightOuterRadius /=1.5f;
        }
        if(sparks.gameOver == true)
        {
            CloudParticles.Stop();
        }
        
    }
    void FixedUpdate()
    {
        Below.x = transform.position.x - 1;
        Below.y = transform.position.y - 7;
        Pos.x = transform.position.x - 1;
        Pos.y = transform.position.y;
        RaycastHit2D hit = Physics2D.Raycast(Pos, Below, collisions);
        Debug.DrawLine(Pos, Below);
        if(hit.collider == sparksRb && hit.collider != umbrellaRb)
        {
            sparks.fireStrength -= wind.difficulty * 10;
        }
        Pos.x = transform.position.x + 1;
        Below.x = transform.position.x + 1;
        Debug.DrawLine(Pos, Below);
        if(hit.collider == sparksRb && hit.collider != umbrellaRb)
        {
            sparks.fireStrength -= wind.difficulty * 10;
        }
    }
}
