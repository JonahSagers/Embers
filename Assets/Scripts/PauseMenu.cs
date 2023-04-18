using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Volume post;
    public DepthOfField blur;
    public float blurStrength;
    public bool paused;
    public TextMeshProUGUI text;
    public Animator textAnim;
    public Sparks sparks;
    public Animator pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
        }
        if(Input.GetMouseButtonDown(0)){
                paused = false;
        }
        post.profile.TryGet<DepthOfField>(out var blur);
        blur.focalLength.value = blurStrength;
        if(blurStrength > 1){
            blur.active = true;
        } else {
            blur.active = false;
        }
        if(paused){
            blurStrength += (30 - blurStrength) * Time.unscaledDeltaTime * 10;
        } else {
            blurStrength -= blurStrength * Time.unscaledDeltaTime * 5;
        }
        Time.timeScale = Mathf.Clamp(1 - (blurStrength/30),0,1);
        textAnim.SetBool("shown", paused);
        if(sparks.sparkStrength == 100 && sparks.gameOver == false && paused == false){
            pauseButton.SetBool("shown", true);
        } else {
            pauseButton.SetBool("shown", false);
        }
    }
    public void Pause(){
        if(paused == false){
            paused = !paused;
        }
        
    }
    public void QuitGame(){
        if(paused){
            Debug.Log("Game Quit");
            Application.Quit();
        }
    }
}
