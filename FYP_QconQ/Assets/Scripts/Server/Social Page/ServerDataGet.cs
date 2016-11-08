using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ServerDataGet : MonoBehaviour {

    //private Text[] UItext;
    private ServerTextAndScore[] NamesAndScores;
    //public GameObject NamesGO, ScoresGO;
    //private Text[] Names, Scores;
    public string url;
    private WWW www;

    //Regular expression for "anything within square brackets"
    string regex = @"\[(.*?)\]";
    //Regular expression for "anything within brackets"
    //string regex2 = @"\((.*?)\)";
    
    string temp_regex = @"\[(.+?),";
    string temp_regex2 = @",(.+?)\]";
    //string temp_regex3 = @"\((.*?)\)";

    string regex_true = @"\+(.*?)\+";

    private string temp_string;

	// Use this for initialization
	void Start () {
        
        NamesAndScores = GetComponentsInChildren<ServerTextAndScore>();

        www = new WWW(url);

        noWifiPlaceholders();
        if (Application.internetReachability != NetworkReachability.NotReachable)
            StartCoroutine(WaitForDownload(www));
	}

    IEnumerator WaitForDownload(WWW www)
    {
        yield return www;
        string temp_text = www.text;

        if (www.isDone)
        {
            //Check whether to reset stats
            if (Regex.Match(temp_text, regex_true).Success)
            {
                if (url.Contains("story"))
                    GameControl.handle.player.cumulativeScore = 0;
                if (url.Contains("arcade"))
                    GameControl.handle.player.arcademodeAttempts = 0;
            }

            //For Names
            for (int i = 0; i < Regex.Matches(temp_text, regex).Count; ++i)
            {
                NamesAndScores[i].Name.text = Regex.Matches(temp_text, temp_regex)[i].Groups[1].Value;
                //Names[i].text = Names[i].text.Replace(",", "");
                NamesAndScores[i].Name.text = NamesAndScores[i].Name.text.Replace("Nickname:", "");
            }

            //For Scores
            for (int i = 0; i < Regex.Matches(temp_text, regex).Count; ++i)
            {
                NamesAndScores[i].Score.text = Regex.Matches(temp_text, temp_regex2)[i].Groups[1].Value;
                if (NamesAndScores[i].Score.text.Contains("Score"))
                    NamesAndScores[i].Score.text = NamesAndScores[i].Score.text.Replace("Score:", "");
                else
                    NamesAndScores[i].Score.text = NamesAndScores[i].Score.text.Replace("Attempts:", "");
            }

            for (int i = 0; i < NamesAndScores.Length; ++i)
            {
                if (NamesAndScores[i].Name.text == "" && NamesAndScores[i].Score.text == "")
                    NamesAndScores[i].Name.text = "--Empty--";
            }
        }
        else if (Regex.Matches(temp_text, regex).Count <= 0)
        {
            for (int i = 0; i < NamesAndScores.Length; ++i)
                NamesAndScores[i].Name.text = "--Empty--";
        }

        else
        {
            noWifiPlaceholders();
        }

        
    }
	
	// Update is called once per frame
	void Update () {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            noWifiPlaceholders();
    }

    private void noWifiPlaceholders()
    {
        for (int i = 0; i < NamesAndScores.Length; ++i)
            NamesAndScores[i].Name.text = "--Empty--";
    }
}
