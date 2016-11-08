using UnityEngine;
using System.Collections;

public class ServerDataPost : MonoBehaviour {

    //URLs
    public string URL;
    public GamePlayController gameplayController;

	// Use this for initialization
	void Start () {

        //Create a form
        WWWForm form = new WWWForm();
        //Create a field for device ID, Nickname, and score
        form.AddField("deviceID", GameControl.handle.player.DeviceUniqueID);
        if (GameControl.handle.player.PlayerName_Player == "")
            GameControl.handle.player.PlayerName_Player = "DEFAULT NAME";
        form.AddField("nickname", GameControl.handle.player.PlayerName_Player);
        //GameControl.handle.player.PlayerName_Player
        if (URL.Contains("/storystatspost"))
            form.AddField("score", GetCumulativeScore());
        else if (URL.Contains("/arcadestatspost"))
        {
            Debug.Log(GetArcadeAttempts());
            form.AddField("attempts", GetArcadeAttempts());
        }
        else
            Debug.Log("ERROR!!! UNITY DOESNT KNOW WHERE TO POST");
        byte[] rawData = form.data;

        //Create a download object
        WWW www = new WWW(URL, rawData);

        StartCoroutine(SendDataToServer(www));
	}

    IEnumerator SendDataToServer(WWW www)
    {
        yield return www;

        if (www.error == null)
            Debug.Log("NO ERRORS - POST SUCCESSFUL");
        else
        {
            Debug.Log("THERE WAS AN ERROR WITH THE POST");
            Debug.Log(www.error);
        }
            
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    private int GetCumulativeScore()
    {
        return GameControl.handle.player.cumulativeScore;
    }

    private int GetArcadeAttempts()
    {
        return GameControl.handle.player.arcademodeAttempts;
    }
}
