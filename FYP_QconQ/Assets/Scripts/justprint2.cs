using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class justprint2 : MonoBehaviour {

    private Text namem;
    // Use this for initialization
    void Start ()
    {
        namem = GetComponent<Text>(); 
        namem.text = GameControl.handle.player.PlayerName_Player;
    }
	
	// Update is called once per frame
	void Update ()
    {
       // namem.text = GameControl.handle.player.PlayerName_Player;

        namem.text = Screen.orientation.ToString();
    }
}
