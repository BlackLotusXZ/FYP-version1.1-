using UnityEngine;
using System.Collections;

public class ExitApplicationCheck : MonoBehaviour {

    public PageController page;
    public GameObject PreGamePlayUI;
    public PopUpScreenController Setting;
    public GameObject settingPanel;

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && page.getCurrentPage() != 0)
            page.setCurrentPage(0);
        else if (Input.GetKeyDown(KeyCode.Escape) && settingPanel.activeSelf == true)
            Setting.MainBoderAppear();
        else if (Input.GetKeyDown(KeyCode.Escape) && page.getCurrentPage() == 0 && PreGamePlayUI.activeSelf == false)
            Application.Quit();
	}
}
