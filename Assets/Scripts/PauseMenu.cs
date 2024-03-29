using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public GraphicRaycaster ray;
    public PointerEventData pointer;
    public EventSystem eventSystem;
    public LayerMask buttons;
    // Start is called before the first frame update
    void Start()
    {
        ray = GameObject.FindObjectOfType<GraphicRaycaster>();
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
        }
        if(Input.GetMouseButtonDown(0) && paused == true){
            pointer = new PointerEventData(eventSystem);
            pointer.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            ray.Raycast(pointer, results);
            if(results.Count == 0){
                paused = false;
            } else {
                bool buffer = true;
                foreach(RaycastResult result in results){
                    if(buttons == (buttons | (1 << result.gameObject.layer))){
                        buffer = false;
                    }
                }
                if(buffer == true){
                    paused = false;
                }
            }
            
            
        }
        post.profile.TryGet<DepthOfField>(out var blur);
        blur.focalLength.value = blurStrength;
        if(blurStrength > 1){
            blur.active = true;
        } else {
            blur.active = false;
        }
        if(paused){
            blurStrength += (30 - blurStrength) * Time.unscaledDeltaTime * 8;
        } else {
            blurStrength -= blurStrength * Time.unscaledDeltaTime * 3;
        }
        Time.timeScale = Mathf.Clamp(1 - (blurStrength/30),0,1);
        textAnim.SetBool("shown", paused);
        if(sparks.sparked && sparks.gameOver == false && paused == false){
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
