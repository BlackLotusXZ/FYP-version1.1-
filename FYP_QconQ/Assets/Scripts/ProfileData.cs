using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class ProfileData : MonoBehaviour {

    public Text profileName;
    

	// Use this for initialization
	void Start () {
        profileName.text = GameControl.handle.player.PlayerName_Player;
	}
}
