using UnityEngine;
using System.Collections;

public class rotatetext : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(GameControl.handle.player.SettingsOrientation_Player == 0)
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90.0f)); ;
        }
	}
}
