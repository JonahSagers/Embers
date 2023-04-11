using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    public Vector2 topRightCorner;
    public Vector2 bottomLeftCorner;
    public Transform woodBlock;
    public Transform woodPile;
    public Sparks sparks;
    public BoxCollider2D topBound;
    public BoxCollider2D bottomBound;
    public BoxCollider2D leftBound;
    public BoxCollider2D rightBound;
    // Update is called once per frame
    void Update()
    {
        topRightCorner = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
        topRightCorner = new Vector2(Mathf.Clamp(topRightCorner.x,5,50),Mathf.Clamp(topRightCorner.y,0.5f,15));
        bottomLeftCorner = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        bottomLeftCorner = new Vector2(Mathf.Clamp(bottomLeftCorner.x,-50,-5),Mathf.Clamp(bottomLeftCorner.y,-15,-0.5f));
        if(sparks.gameOver == false && sparks.sparkStrength >= 100){
            topBound.offset = new Vector2(0,topRightCorner.y + 5);
            bottomBound.offset = new Vector2(0,bottomLeftCorner.y - 4);
            leftBound.offset = new Vector2(bottomLeftCorner.x - 5,0);
            rightBound.offset = new Vector2(topRightCorner.x + 5,0);
        }
        Debug.Log(bottomLeftCorner);
    }
}
