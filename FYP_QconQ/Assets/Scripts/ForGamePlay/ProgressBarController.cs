using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/********************************************/
/**
*  \brief
*   This script is in-charge of all things related to the in-game progress bar 
***********************************************/
public class ProgressBarController : MonoBehaviour {

    public Image progress;
    private float currentProgress = 0.0f;
    private float nextProgress = 0.0f;

	// Use this for initialization
	public void StartProgress () 
    {
        StartCoroutine(lerpProgress());
	}
	
    IEnumerator lerpProgress()
    {
        while(true)
        {
            currentProgress = Mathf.Lerp(currentProgress, nextProgress, Time.deltaTime * 5.0f);
            yield return null;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        progress.fillAmount = currentProgress;
	}

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void setNextProgress(float next)
    {
        nextProgress = next;
    }

    public float getNextProgress()
    {
        return nextProgress;
    }

    public float getCurrentProgress()
    {
        return currentProgress;
    }

   
}
