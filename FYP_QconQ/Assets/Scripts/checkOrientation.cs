using UnityEngine;
using System.Collections;

public class checkOrientation : MonoBehaviour {

    public GameObject[] gamecanvas;

    
	// Use this for initialization
	void Start () {

        switch (GameControl.handle.player.SettingsOrientation_Player)
        {
            case 0:
                gamecanvas[0].SetActive(true);
                gamecanvas[1].SetActive(false);
                break;
            case 1:
                gamecanvas[0].SetActive(false);
                gamecanvas[1].SetActive(true);
                break;
        }
	}

    void Update()
    {
       //GameControl.handle.player.TotalPlayTime_Player += Time.deltaTime;
    }
}
