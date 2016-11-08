using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class UpdateMoney : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = GameControl.handle.player.Player_Money.ToString();
	}
}
