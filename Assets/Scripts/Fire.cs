using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Fire : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public ParticleSystemForceField windImpulse;
    public WindImpulse wind;
    public Sparks sparks;
    public Light2D red;
    public Light2D orange;
    public Light2D yellow;
    public float fireOffset;
    public float lightOffset;

    void Update()
    {
        var fireParticleRate = fireParticles.emission;
        var fireParticleDuration = fireParticles.main;
        var fireParticleShape = fireParticles.shape;
        windImpulse.directionX = wind.windForce * 4;
        fireParticleRate.rateOverTime = Mathf.Pow(2,sparks.fireStrength/100);
        fireParticleDuration.startLifetime = sparks.fireStrength/800;
        fireParticleShape.radius = 1 + sparks.fireStrength/1000;
        fireParticleShape.randomPositionAmount = 1 + sparks.fireStrength/500;
        red.pointLightOuterRadius += (sparks.fireStrength/300 - red.pointLightOuterRadius)/3 + sparks.lightOffset/2;
        orange.pointLightOuterRadius += (sparks.fireStrength/200 - orange.pointLightOuterRadius)/3 + sparks.lightOffset;
        yellow.pointLightOuterRadius += (sparks.fireStrength/40 - yellow.pointLightOuterRadius)/3 + sparks.lightOffset*2;
        //Debug.Log("Fire Strength: " + sparks.fireStrength);
    }
}