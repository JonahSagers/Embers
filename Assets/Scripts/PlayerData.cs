using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{ 
    public int highScore;

    public PlayerData(Sparks sparks)
    {
        //STRINGS CANNOT BE SERIALIZED AAAAAAAAAAAAAAAAAAAAAHHHHHHHHHHHHHH
        //playerprefs is used to store username, but it's not very secure, hence highscore being encoded
        highScore = sparks.highScore;
        sparks.username = PlayerPrefs.GetString("Username");
    }
}
