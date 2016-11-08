using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class printDefaultName : MonoBehaviour {

	// Use this for initialization
	void Start () 
{
        GetComponent<InputField>().text = GameControl.handle.player.PlayerName_Player;
	}
}
