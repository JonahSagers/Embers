using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSprite : MonoBehaviour
{
    public WoodClick woodpile;
    public float woodNumber;
    public Renderer visible;
    public bool armed;
    public GameObject woodBlock;
    // Start is called before the first frame update
    void Start()
    {
        woodpile = GameObject.FindGameObjectWithTag("Wood").GetComponent<WoodClick>();
        visible = gameObject.GetComponent<SpriteRenderer>();
        visible.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(woodpile.woodCount >= woodNumber)
        {
            visible.enabled = true;
            armed = true;
        }
        else
        {
            visible.enabled = false;
            if(armed == true){
                Instantiate(woodBlock, transform.localPosition + transform.parent.position, Quaternion.Euler(0,0,90));
                armed = false;
            }
        }
    }
}
