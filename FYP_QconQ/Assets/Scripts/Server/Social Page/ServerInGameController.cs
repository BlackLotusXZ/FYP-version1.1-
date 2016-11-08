using UnityEngine;
using System.Collections;

public class ServerInGameController : MonoBehaviour {

    //ServerPostStyle: 0 for story mode, 1 for arcade mode
    public int ServerPostStyle;
    private GameObject StoryModeManager, ArcadeModeManager;
    public bool shouldLoad = false;

	// Use this for initialization
	void Start () {
        StoryModeManager = transform.Find("SDataPost").gameObject;
        ArcadeModeManager = transform.Find("ADataPost").gameObject;
	}

    void SetManagerActive()
    {
        if (ServerPostStyle == 0)
        {
            if (!StoryModeManager.activeSelf)
                StoryModeManager.SetActive(true);
        }
        else if (ServerPostStyle == 1)
        {
            if (!ArcadeModeManager.activeSelf)
                ArcadeModeManager.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (shouldLoad)
            SetManagerActive();
	}
}
