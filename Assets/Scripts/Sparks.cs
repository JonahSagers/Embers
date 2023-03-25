using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Sparks : MonoBehaviour
{
    public ParticleSystem sparksParticles;
    public float fireStrength;
    public float sparkStrength;
    public WoodClick woodpile;
    public WindImpulse wind;
    public bool gameOver;
    public Light2D backlight;
    public Light2D fog;
    public float resetCooldown;
    public ParticleSystem fireParticles;
    public Animator sampleText;
    // Start is called before the first frame update
    void Start()
    {
        sampleText = GameObject.Find("Press Space").GetComponent<Animator>();
        backlight = GameObject.Find("Backlight").gameObject.GetComponent<Light2D>();
        fog = GameObject.Find("NightFog").gameObject.GetComponent<Light2D>();
        gameOver = false;
        fireStrength = 0;
        wind = GameObject.Find("WindImpulse").GetComponent<WindImpulse>();
        sparksParticles = GetComponent<ParticleSystem>();
        woodpile = GameObject.FindGameObjectWithTag("Wood").GetComponent<WoodClick>();
        fireParticles = GameObject.Find("Fire").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fireStrength > 1000)
        {
            fireStrength = 1000;
        }
        if(fireStrength < 200)
        {
            //backlight.intensity = fireStrength / 200;
            fog.intensity = fireStrength / 200;
        }
        else
        {
            fog.intensity = 1;
            //backlight.intensity = 1;
        }
        if(fireStrength < 0 && gameOver == false)
        {
            gameOver = true;
            Debug.Log("Game Over");
            resetCooldown = 300;
        }
        if(gameOver == true)
        {
            fireStrength = 0;
            if(resetCooldown > 0)
            {
                resetCooldown -= 1;
            }
            else 
            {
                SceneManager.LoadScene("Embers 2022");
            }
        }
        if(Input.GetMouseButtonDown(0) && fireStrength < 1 && sparkStrength < 100)
        {
            sparksParticles.Play();
            sparkStrength += 20;
            sampleText.SetFloat("sparkStrength", sparkStrength);
            if(sparkStrength == 100)
            {
                fireStrength += 1000;
                fireParticles.Play();
            }
        }
    }
    void FixedUpdate()
    {
        if(sparkStrength == 100)
        {
            //fireStrength -= 0.1f + wind.windForce;
        }
    }

    void OnMouseDown()
    {
        if(woodpile.woodCount > 0 && gameOver == false)
        {
            sparksParticles.Play();
            woodpile.woodCount -= 1;
            fireStrength += 100;
        }
    }
}