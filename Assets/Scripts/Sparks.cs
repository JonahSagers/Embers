using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using TMPro;

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
    public Animator textAnim;
    public TextMeshProUGUI text;
    public List<AudioSource> audioSources;
    public float lightOffset;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        fireStrength = 0;
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
            //int audioValue = (int)(sparkStrength/20);
            //audioSources[audioValue].Play();
            sparkStrength += 20;
            textAnim.SetFloat("sparkStrength", sparkStrength);
            if(sparkStrength == 100)
            {
                fireStrength += 1000;
                fireParticles.Play();
            }
        }
    }
    void FixedUpdate()
    {
        if(lightOffset > 0.01f){
            lightOffset /= 1.02f;
        }
    }

    void OnMouseDown()
    {
        if(woodpile.woodCount > 0 && gameOver == false)
        {
            woodpile.woodCount -= 1;
        }
        
    }

    public void StokeFire()
    {
        sparksParticles.Play();
        fireStrength += 10;
        lightOffset = 0.5f;
    }
}