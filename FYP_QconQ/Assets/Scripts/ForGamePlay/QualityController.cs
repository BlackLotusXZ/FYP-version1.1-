using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/********************************************/
/**
*  \brief
*   This script is in-charge of all things related to the in-game quality 
***********************************************/
public class QualityController : MonoBehaviour {

    public Text qualityText;
    public Image QualityRingFill;

    private float TotalQuality = 100.0f;
    private float nextQuality = 50.0f; // this one is used to lerp
    private float currentQuality = 0.0f;

    private Color red = new Color(1.0f, 0, 0);
    private Color white = new Color(1.0f, 1.0f, 1.0f);

    public void StartQualityCount()
    {
        StartCoroutine(lerpQualityNum());
    }

    IEnumerator lerpQualityNum()
    {
        while(true)
        {
            if (currentQuality <= 30.9f)
            {
                QualityRingFill.color = Color.Lerp(QualityRingFill.color, red, Time.deltaTime * 10.0f);
            }
            else
            {
                QualityRingFill.color = Color.Lerp(QualityRingFill.color, white, Time.deltaTime * 5.0f);
            }

            currentQuality = Mathf.Lerp(currentQuality, nextQuality, Time.deltaTime * 4.0f);
            yield return null;
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        // constantly update this two visually 
        qualityText.text = currentQuality.ToString("F0");
        QualityRingFill.fillAmount = (currentQuality / TotalQuality);
	}

    public float getCurrentQuality()
    {
        currentQuality = nextQuality;
        return currentQuality;
    }

    public float getNextQuality()
    {
        return nextQuality;
    }

    public void setNextQuality(float s)
    {
        nextQuality = s;
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
