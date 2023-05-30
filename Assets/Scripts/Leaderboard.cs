using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    public Sparks sparks;
    public List<string> namesRaw;
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> scores;
    //4e79e0abad1827143f8d784ccae486ed62079553a2d5be292a306c0d177f28424068c5b25f6a28dda8dad08df2d7eeb131f0e925612d9357ccc154b1e65947f53302bb27eb60266a6f566c85447c001b66f56566e4ac855f61a256fcf4ad16abceffb2b2448422d82aae8b51d4dec089ba93de9e993b98b3c211460223a7044b
    private string publicKey = "0c352c7ca7c60d150026c9d6e25cf7ed36f3e05dc62498012c0635304383311f";
    public bool isReady;
    public Animator bottomAnim;
    public TextMeshProUGUI bottomText;
    public TextMeshProUGUI topText;
    public TMP_InputField input;
    public TextMeshProUGUI notice; 
    public float noticeCD;
    public bool proceed;
    public bool mobile;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateLeaderboard(false));
    }
    public IEnumerator UpdateLeaderboard(bool display)
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) => {
            int i = 0;
            if(namesRaw.Count == 0){
                while(true){
                    //always be careful around while true loops.  If you edit this, change it to a for loop or something that won't crash you
                    try{
                        namesRaw.Add(msg[i].Username);
                    } catch{
                        return;
                    }
                    i++;
                }
            }
            for(i = 0; i < names.Count; i++){
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
            yield return new WaitForSeconds(0.15f);
            bottomText.text = "Click anywhere to continue";
            bottomAnim.SetBool("active", true);
            sparks.menuTicking = false;
            while (!Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.Return))
            {
                yield return null;
            }
            for(int i = 0; i < names.Count; i++){
                names[i].gameObject.GetComponent<Animator>().SetBool("active", false);
                scores[i].gameObject.GetComponent<Animator>().SetBool("active", false);
                yield return new WaitForSeconds(0.15f);
            }
            sparks.menuTicking = true;
            yield return new WaitForSeconds(0.15f);
            bottomAnim.SetBool("active", false);
        }
    }
    public IEnumerator UploadScore(string username, int score)
    {
        if(username == ""){
            proceed = false;
            topText.text = "What should we call you?";
            input.interactable = true;
            yield return new WaitForSeconds(0.15f);
            input.GetComponent<Animator>().SetBool("active", true);
            yield return new WaitForSeconds(0.15f);
            bottomText.text = "Click here to continue";
            sparks.menuTicking = false;
            bottomAnim.SetBool("active", true);
            input.Select();
            while ((!Input.GetKey(KeyCode.Return) && !proceed) || input.text == "" || namesRaw.Contains(input.text))
            {
                input.Select();
                if(Input.GetKey(KeyCode.Return) && namesRaw.Contains(input.text)){
                    noticeCD = 100;
                }
                if(noticeCD > 0){
                    notice.enabled = true;
                    noticeCD -= 1;
                } else {
                    notice.enabled = false;
                }
                yield return null;
            }
            notice.enabled = false;
            input.interactable = false;
            proceed = false;
            username = input.text;
            sparks.username = username;
            PlayerPrefs.SetString("Username", username);
            topText.GetComponent<Animator>().SetBool("sparked", true);
            yield return new WaitForSeconds(0.15f);
            input.GetComponent<Animator>().SetBool("active", false);
            yield return new WaitForSeconds(0.25f);
            bottomAnim.SetBool("active", false);
            yield return new WaitForSeconds(1f);
            topText.GetComponent<Animator>().SetBool("sparked", false);
            topText.text = "Score: " + Mathf.Floor(sparks.score);
        }
        isReady = false;
        LeaderboardCreator.UploadNewEntry(publicKey, username, score, ((msg) => {
            StartCoroutine(UpdateLeaderboard(true));
        }));
        isReady = true;
    }

    public void bottomClicked(){
        proceed = true;
    }

    public void keyboard(){
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}
