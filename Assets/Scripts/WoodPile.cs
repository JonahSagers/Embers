using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPile : MonoBehaviour
{
    public WoodClick woodpile;
    public Sparks sparks;
    // Start is called before the first frame update
    void OnMouseDown()
    {
        if(woodpile.woodCount > 0 && sparks.gameOver == false)
        {
            woodpile.woodCount -= 1;
        }
    }
}
