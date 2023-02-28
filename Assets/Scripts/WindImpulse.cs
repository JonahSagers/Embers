using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindImpulse : MonoBehaviour
{
    public int windDelay;
    public float windDuration;
    public float windForce;
    public ParticleSystem windParticles;
    public bool windDirection;
    public float difficulty;
    public Sparks sparks;
    public ParticleSystemForceField windImpulse;
    // Start is called before the first frame update
    void Start()
    {
        windImpulse = gameObject.GetComponent<ParticleSystemForceField>();
        sparks = GameObject.FindGameObjectWithTag("Sparks").GetComponent<Sparks>();
        windParticles = GetComponent<ParticleSystem>();
        //windDelay = Random.Range(500f, 1000f);
        windDelay = 10;
        windDuration = 0;
        difficulty = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(sparks.gameOver == false && sparks.sparkStrength == 100)
        {
            var windParticleMain = windParticles.main;
            //windParticleMain.startSpeed = windForce * 5;
            windImpulse.directionX = windForce * 4;
            difficulty += 0.0002f;
            if(windDelay > 0)
            {
                //windParticleMain.startLifetime = 0;
                if(difficulty > 1.2f)
                {
                    windDelay -= 1;
                }
            }
            else
            {
                if(windDuration < 2 && sparks.gameOver != true)
                {
                    //windParticleMain.startLifetime = 5;
                }
                else
                {
                    //windParticleMain.startLifetime = 0;
                }
                if(windDuration < 3.14f)
                {

                    windDuration += 0.02f;
                    windForce = Mathf.Sin(windDuration) * difficulty/2;
                    if(windDirection)
                    {
                        windForce = windForce * -1;
                    }
                }
                else
                {
                    windDelay = Random.Range(500, 1000) - (int)difficulty * 100;
                    if(Random.Range(0,2) < 0.5)
                    {
                        windDirection = true;
                    }
                    else
                    {
                        windDirection = false;
                    }
                    if(windDelay < 0)
                    {
                        windDelay = 25;
                    }
                    windDuration = 0;
                    windForce = 0;
                }
            }
        }
    }
    void CreateWind()
    {
        windDuration = 0;
        while(windDuration < 3.14f)
        {
            windDuration += 0.01f;
            windForce = Mathf.Sin(windDuration);
        }
    }
}
