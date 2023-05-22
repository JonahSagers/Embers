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
    public PauseMenu pauseMenu;
    public List<string> badEndings;
    public Leaderboard leaderboard;
    public bool menuTicking;
    public string username;
    public int highScore;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        fireStrength = 0;
        username = null;
        EncryptData.LoadData(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(score > 500 && isDark == false)
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
            menuTicking = true;
            EncryptData.EncryptScore(this);
            if(score > 100){
                text.text = "Score: " + Mathf.Floor(score);
                StartCoroutine(leaderboard.UploadScore(username, (int)score));
            } else {
                text.text = badEndings[Random.Range(0, badEndings.Count)];
            }
            textAnim.SetFloat("strength", 0);
        }
        if(resetCooldown < 100 && gameOver == true)
        {
            textAnim.SetFloat("strength", 100);
        }
        if(Input.GetMouseButtonDown(0) && fireStrength < 1 && sparkStrength < 100 && gameOver == false && pauseMenu.paused == false)
        {
            sparksParticles.Play();
            //int audioValue = (int)(sparkStrength/20);
            //audioSources[audioValue].Play();
            sparkStrength += 20;
            textAnim.SetFloat("strength", sparkStrength);
            if(sparkStrength >= 100)
            {
                fireStrength += 1000;
                fireParticles.Play();
            }
        }
    }
    void FixedUpdate()
    {
        fireStrength = Mathf.Clamp(fireStrength,0,1000);
        if(lightOffset > 0){
            if(gameOver == false){
                lightOffset /= 1.02f;
            } else {
                lightOffset /= 2f;
            }

        }
        if(fireStrength > 0){
            fireStrength -= 0.4f + wind.difficulty/20;
        }
        if(gameOver == true)
        {
            if(resetCooldown > 0 && menuTicking == true)
            {
                resetCooldown -= 1;
                fireStrength = 0;
            }
            else
            {
                if(menuTicking == true){
                    SceneManager.LoadScene("Embers 2022");
                }
            }
        }
        if(gameOver == false && sparkStrength == 100){
            score += 0.04f * wind.difficulty;
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
        if(gameOver == false){
            sparksParticles.Play();
            fireStrength += 50f + wind.difficulty/20;
            lightOffset = 0.5f;
        }
    }
}