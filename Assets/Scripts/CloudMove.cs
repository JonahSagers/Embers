﻿using System.Collections;
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
    public float direction;

    void Update()
    {
        Position = gameObject.transform.position;
        
        if(sparks.sparked && wind.difficulty > 1.25f)
        {
            if(raining == false)
            {
                RainParticles.Play();
                CloudParticles.Play();
                raining = true;
            }
            gameObject.transform.position += new Vector3(Xvel * Time.timeScale,0,0);
        }
        if(sparks.gameOver == true)
        {
            var CloudPartEmission = CloudParticles.emission;
            var RainPartEmission = RainParticles.emission;
            CloudPartEmission.rateOverTime = 0;
            RainPartEmission.rateOverTime = 0;
        }
        
    }
    void FixedUpdate()
    {
        Below.x = transform.position.x - 1;
        Below.y = -2;
        Pos.x = transform.position.x - 1;
        Pos.y = transform.position.y;
        RaycastHit2D hit = Physics2D.Raycast(Pos, Below, collisions);
        Debug.DrawLine(Pos, Below);
        if(hit.collider == sparksRb && hit.collider != umbrellaRb && sparks.gameOver == false)
        {
            sparks.fireStrength -= wind.difficulty * 10;
        }
        Pos.x = transform.position.x + 1;
        Below.x = transform.position.x + 1;
        Debug.DrawLine(Pos, Below);
        if(hit.collider == sparksRb && hit.collider != umbrellaRb && sparks.gameOver == false)
        {
            sparks.fireStrength -= wind.difficulty * 10;
        }
        if(Position.x < -20f)
        {
            //for direction, 1 is right and -1 is left
            direction = 1;
            
        }
        if(Position.x > 20f)
        {
            direction = -1;
        }
        Xvel += wind.difficulty * 0.003f * Time.timeScale * direction;
        Xvel = Mathf.Clamp(Xvel,-0.07f * wind.difficulty,0.07f * wind.difficulty);
    }
}
