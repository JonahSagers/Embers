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
    public Animator bottomText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateLeaderboard(false);
    }
    public IEnumerator UpdateLeaderboard(bool display)
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) => {
            for(int i = 0; i < names.Count; i++){
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
        yield return new WaitForSeconds(0.5f);
        if(display){
            for(int i = 0; i < names.Count; i++){
                names[i].gameObject.GetComponent<Animator>().SetBool("active", true);
                scores[i].gameObject.GetComponent<Animator>().SetBool("active", true);
                yield return new WaitForSeconds(0.15f);
            }
        }
        yield return new WaitForSeconds(0.15f);
        bottomText.SetBool("active", true);
        sparks.menuTicking = false;
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        if(display){
            for(int i = 0; i < names.Count; i++){
                names[i].gameObject.GetComponent<Animator>().SetBool("active", false);
                scores[i].gameObject.GetComponent<Animator>().SetBool("active", false);
                yield return new WaitForSeconds(0.15f);
            }
        }
        sparks.menuTicking = true;
        yield return new WaitForSeconds(0.15f);
        bottomText.SetBool("active", false);
    }
    public void UploadScore(string username, int score)
    {
        isReady = false;
        //LeaderboardCreator.UploadNewEntry(publicKey, username, score, ((msg) => {
            StartCoroutine(UpdateLeaderboard(true));
        //}));
        isReady = true;
    }
}
