using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public Volume post;
    public DepthOfField blur;
    public float blurStrength;
    public bool paused;
    public TextMeshProUGUI text;
    public Animator textAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
            textAnim.SetBool("shown", paused);
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
    }

    public void QuitGame(){
        if(paused){
            Debug.Log("Game Quit");
            Application.Quit();
        }
    }
}
