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

    void FixedUpdate()
    {
        var windParticleEmission = windParticles.emission;
        var windParticleMain = windParticles.main;
        if(sparks.gameOver == false && sparks.sparkStrength == 100)
        {
            windParticleEmission.rateOverTime = 4;
            //windParticleMain.startSpeed = windForce * 5;
            windImpulse.directionX = windForce * 4;
            difficulty += 0.001f;
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
        } else {
            windParticleEmission.rateOverTime = 0;
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
