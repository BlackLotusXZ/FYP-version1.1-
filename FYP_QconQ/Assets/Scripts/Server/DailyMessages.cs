using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DailyMessages : MonoBehaviour {

    private Text[] text;
    string url;
    private WWW www;
    Regex regex;
    float total = 0.0f;
    
    private string[] placeholderText = new string[]{
        "Welcome to QconQ!",
        "Press the play game button to play!",
        "Buy some stuff from the shop!",
        "Check out your statistics" };

	// Use this for initialization
	void Start () {
        text = GetComponentsInChildren<Text>();
        url = "https://continental-hr.appspot.com/";
        www = new WWW(url);
        regex = new Regex(@"\[(.*?)\]");

        noWifiPlaceholders();
        if (Application.internetReachability != NetworkReachability.NotReachable)
            StartCoroutine(WaitForLoad(www));
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            noWifiPlaceholders();
    }

    IEnumerator WaitForLoad(WWW www)
    {
        yield return www;
        if (www.isDone)
        {
            string temp_text = www.text;
            for (int i = 0; i < Regex.Matches(temp_text, @"\[(.*?)\]").Count; ++i)
            {
                text[i].text = Regex.Matches(temp_text, @"\[(.*?)\]")[i].Groups[1].Value;
            }
        }
        else
        {
            noWifiPlaceholders();
        }
    }

    private void CurrentPlayerProgressDailyMessage()
    {
        for (int i = 0; i < 5; i++)
        {
            float temp_total = GameControl.handle.Modes[0].Categories[i].gettotalNoStage_Cat() * 10;
            float tried = GameControl.handle.Modes[0].Categories[i].getCorrectCount();

            total += (tried / temp_total * 100.0f);
        }

        text[3].text = "Your Current Progress Is: " + (total / 5.0f).ToString("F0") + " %";
    }

    private void noWifiPlaceholders()
    {
        for (int i = 0; i < text.Length - 1; ++i)
            text[i].text = placeholderText[i];
        CurrentPlayerProgressDailyMessage();
    }
}
