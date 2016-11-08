using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class justprint : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        GetComponent<Text>().text = GameControl.handle.player.DeviceUniqueID;
	}
}
