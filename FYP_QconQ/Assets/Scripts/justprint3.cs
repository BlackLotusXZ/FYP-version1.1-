using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class justprint3 : MonoBehaviour
{
	public void ChangeName(string inp)
    {
        GameControl.handle.player.PlayerName_Player = inp;
    }
}
