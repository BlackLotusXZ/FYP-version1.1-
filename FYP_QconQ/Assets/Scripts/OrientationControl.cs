using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class OrientationControl : MonoBehaviour {

    public GameObject[] cc;
    public GameObject ContentScrollView;
    public GameObject ContentScrollView2;
    public bool isLandScape;
    void Start()
    {
        for (int i = 0; i < cc.Length; i++)
            cc[i].SetActive(true);

        if (isLandScape)
        {
            ActivateLandScape();

        }
        else
        {
            ActivatePortrait();
        }
    }

	// Update is called once per frame
	void Update () 
    {
        switch(Input.deviceOrientation)
        {
           
            case DeviceOrientation.Portrait:
                {
                    ActivatePortrait();
                    break;
                }
            case DeviceOrientation.LandscapeLeft:
                {
                    ActivateLandScape();
                    break;
                }
            case DeviceOrientation.LandscapeRight:
                {
                    ActivateLandScape();
                    break;
                }
        }
       
        // do the orientation code here
	}

    public void ActivatePortrait()
    {
        isLandScape = false;
      //  ContentScrollView.GetComponent<PopulateSelection>().ReloadContent(0);
      //  ContentScrollView2.GetComponent<PopulateSelection>().ReloadContent(0);
       
        StartCoroutine(portraitActive());
    }

    IEnumerator portraitActive()
    {
        CanvasGroup cgroup = cc[0].GetComponent<CanvasGroup>();
        cgroup.alpha = 1.0f;
        cgroup.interactable = true;
        
        
        cgroup = cc[1].GetComponent<CanvasGroup>();
        cgroup.alpha = 0;
        cgroup.interactable = false;
        cgroup.blocksRaycasts = false;

        yield return new WaitForSeconds(1.0f);

        GameControl.handle.player.SettingsOrientation_Player = 0;
        cc[0].SetActive(true);
        cc[1].SetActive(false);
        cgroup.blocksRaycasts = true;
      

        yield return null;
    }

    public void ActivateLandScape()
    {
        isLandScape = true;
       // ContentScrollView.GetComponent<PopulateSelection>().ReloadContent(0);
      //  ContentScrollView2.GetComponent<PopulateSelection>().ReloadContent(0);
        StartCoroutine(landscapeActive());
    }

    IEnumerator landscapeActive()
    {
        CanvasGroup cgroup = cc[0].GetComponent<CanvasGroup>();
        cgroup.alpha = 0;
        cgroup.interactable = false;
        cgroup.blocksRaycasts = false;
      
        cgroup = cc[1].GetComponent<CanvasGroup>();
        cgroup.alpha = 1.0f;
        cgroup.interactable = true;
        cgroup.blocksRaycasts = true;

        yield return new WaitForSeconds(1.0f);

        GameControl.handle.player.SettingsOrientation_Player = 1;
        cc[0].SetActive(false);
        cc[1].SetActive(true);
       
        yield return null;
    }

}
