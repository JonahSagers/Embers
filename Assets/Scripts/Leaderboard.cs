using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    public Sparks sparks;
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> scores;
    private string publicKey = "941df8ac185290d4cd31278cc4723d4a2ec002fb1fe2f37ee94d64c42f5d1973";
    public bool isReady;
    // Start is called before the first frame update
    public void UpdateLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) => {
            for(int i = 0; i < names.Count; i++){
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }
    public void UploadScore(string username, int score)
    {
        isReady = false;
        LeaderboardCreator.UploadNewEntry(publicKey, username, score, ((msg) => {
            UpdateLeaderboard();
        }));
        isReady = true;
    }
}
