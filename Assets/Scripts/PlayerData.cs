using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{ 
    // Start is called before the first frame update
    public int highScore;
    public string username;

    public PlayerData(Sparks sparks)
    {
        Debug.Log("Testing for high score...");
        if((int)sparks.score > sparks.highScore){
            sparks.highScore = (int)sparks.score;
            highScore = sparks.highScore;
            Debug.Log("High score found");
        }
        if(sparks.username != null){
            username = sparks.username;
        } else {
            sparks.username = username;
        }
    }
}
