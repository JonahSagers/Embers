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
    public float lightOffset;
    public float score;
    public bool isDark;
    public List<AudioSource> audioSources;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        fireStrength = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(score > 150 && isDark == false)
        {
            isDark = true;
            text.text = "It's getting dark";
            textAnim.Play("QuickFade");
        }
        if(fireStrength < 200 && !isDark)
        {
            fog.intensity = fireStrength / 200;
        } else {
            if(isDark)
            {
                fog.intensity = fog.intensity / 1.002f;
            } else {
                fog.intensity = 1;
            }
        }
        if(fireStrength <= 0 && gameOver == false && sparkStrength >= 100)
        {
            resetCooldown = 250;
            gameOver = true;
            text.text = "Score: " + Mathf.Floor(score);
            textAnim.SetFloat("sparkStrength", 0);
        }
        if(resetCooldown < 100 && gameOver == true)
        {
            textAnim.SetFloat("sparkStrength", 100);
        }
        if(Input.GetMouseButtonDown(0) && fireStrength < 1 && sparkStrength < 100 && gameOver == false)
        {
            sparksParticles.Play();
            //int audioValue = (int)(sparkStrength/20);
            //audioSources[audioValue].Play();
            sparkStrength += 20;
            textAnim.SetFloat("sparkStrength", sparkStrength);
            if(sparkStrength >= 100)
            {
                fireStrength += 1000;
                fireParticles.Play();
            }
        }
    }
    void FixedUpdate()
    {
        if(lightOffset > 0){
            if(gameOver == false){
                lightOffset /= 1.02f;
            } else {
                lightOffset /= 2f;
            }
            
        }
        if(fireStrength > 0){
            fireStrength -= 0.4f + wind.difficulty/20;
            fireStrength = Mathf.Clamp(fireStrength,0,1000);
        }
        if(gameOver == true)
        {
            if(resetCooldown > 0)
            {
                resetCooldown -= 1;
                fireStrength = 0;
            }
            else 
            {
                SceneManager.LoadScene("Embers 2022");
            }
        }
        score += 0.04f * wind.difficulty;
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
        fireStrength += 25f + wind.difficulty/20;
        lightOffset = 0.5f;
    }
}