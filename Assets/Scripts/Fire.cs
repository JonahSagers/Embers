using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Fire : MonoBehaviour
{
    private ParticleSystem fireParticles;
    public ParticleSystemForceField windImpulse;
    public WindImpulse wind;
    public Sparks sparks;
    public Light2D red;
    public Light2D orange;
    public Light2D yellow;
    public float fireOffset;
    public float lightOffset;
    // Start is called before the first frame update
    void Start()
    {
        wind = GameObject.Find("WindImpulse").GetComponent<WindImpulse>();
        windImpulse = gameObject.GetComponent<ParticleSystemForceField>();
        Debug.Log("Hello World");
        sparks = GameObject.FindGameObjectWithTag("Sparks").GetComponent<Sparks>();
        red = gameObject.transform.GetChild(0).gameObject.GetComponent<Light2D>();
        orange = gameObject.transform.GetChild(1).gameObject.GetComponent<Light2D>();
        yellow = gameObject.transform.GetChild(2).gameObject.GetComponent<Light2D>();
        fireParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
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