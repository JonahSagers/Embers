using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class Sparks : MonoBehaviour
{
    public ParticleSystem sparksParticles;
    public float fireStrength;
    public bool sparked;
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
    public PauseMenu pauseMenu;
    public List<string> badEndings;
    public Leaderboard leaderboard;
    public bool menuTicking;
    public string username;
    public int highScore;
    public float marshmalloCD;
    public GameObject marshmalloPre;
    // Start is called before the first frame update
    void Start()
    {
        // if (!System.IO.File.Exists(Application.persistentDataPath + "/player.dat"))
        // {
        //     EncryptData.EncryptScore(this);
        // }
        gameOver = false;
        fireStrength = 0;
        marshmalloCD = 0;
        // PlayerData data = EncryptData.LoadData(this);
        // highScore = data.highScore;
    }

    // Update is called once per frame
    void Update()
    {
        // if(score > 250)
        // {
        //     if(marshmalloCD > 0){
        //         marshmalloCD -= Time.deltaTime;
        //     } else {
        //         Instantiate(marshmalloPre);
        //         marshmalloCD += 20;
        //     }
        // }
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
        if(fireStrength <= 0 && gameOver == false && sparked)
        {
            resetCooldown = 250;
            gameOver = true;
            menuTicking = true;
            text.text = "Score: " + Mathf.Floor(score);
            if(score > highScore){
                highScore = (int)score;
                EncryptData.EncryptScore(this);
                if(highScore > 1000){
                    // PlayerData data = EncryptData.LoadData(this);
                    StartCoroutine(leaderboard.UploadScore(username, (int)highScore));
                }
            } else {
                if(highScore > 500){
                    StartCoroutine(leaderboard.UpdateLeaderboard(true));
                }
            }
            if(highScore < 100){
                text.text = badEndings[Random.Range(0, badEndings.Count)];
            }
            textAnim.SetFloat("strength", 0);
        }
        if(resetCooldown < 100 && gameOver == true)
        {
            textAnim.SetFloat("strength", 100);
        }
        if(Input.GetMouseButtonDown(0) && fireStrength < 1 && gameOver == false && pauseMenu.paused == false)
        {
            sparked = true;
            textAnim.SetBool("sparked", true);
            fireStrength += 1000;
            fireParticles.Play();
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
            score += 0.04f * wind.difficulty;
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