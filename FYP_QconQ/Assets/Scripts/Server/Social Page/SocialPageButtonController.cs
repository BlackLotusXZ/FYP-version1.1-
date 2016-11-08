using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SocialPageButtonController : MonoBehaviour {

    public GameObject storystatspanel, arcadestatspanel;
    //public GameObject storyHeader, arcadeHeader;
    //private Button storystatsbutton, arcadestatsbutton;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadStoryPanel()
    {
        //if (storystatsbutton.onClick())
        if (!storystatspanel.activeInHierarchy)
        {
            storystatspanel.SetActive(true);
            //storyHeader.SetActive(true);
            arcadestatspanel.SetActive(false);
            //arcadeHeader.SetActive(false);
        }
    }

    public void LoadArcadePanel()
    {
        if (!arcadestatspanel.activeInHierarchy)
        {
            arcadestatspanel.SetActive(true);
            //arcadeHeader.SetActive(true);
            storystatspanel.SetActive(false);
            //storyHeader.SetActive(false);
        }        
    }
}
