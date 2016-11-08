using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/********************************************/
/**
*  \brief
*   This script is in charge of all things related to the in-game timer
***********************************************/
public class TimerController : MonoBehaviour {

    public Text timerText;
    public Image timerRingFill;
    
    private float timeLimit = 25.0f;
    private bool timeEnd = false;
    private float currentTime = 0.0f;

    private Color red = new Color(1.0f, 0, 0);
    private Color white = new Color(1.0f, 1.0f, 1.0f);

    // run the timer .. timer will stop if reach the time limit
    public void StartTimer()
    {
        StartCoroutine(runTimer());
    }

    // current time will do the lerping animation
    public void LerpBackTimer()
    {
        StartCoroutine(CurrentTimeLerpBack());
    }

    IEnumerator runTimer()
    {
        // will keep on increase the time if is not reach the time limit
        while(timeEnd == false)
        {
            currentTime += Time.deltaTime;

            if ((timeLimit - currentTime) <= 5.0f)
            {
                timerRingFill.color = Color.Lerp(timerRingFill.color, red, Time.deltaTime * 10.0f);
            }
            else
            {
                timerRingFill.color = Color.Lerp(timerRingFill.color, white, Time.deltaTime * 5.0f);
            }

            if (currentTime > timeLimit)
            {
                // indicate that the time has ended
                timeEnd = true;
            }

            yield return null;
        }

        yield return null;
    }

    IEnumerator CurrentTimeLerpBack()
    {
        // time must be ended before lerping 
        while (timeEnd == true)
        {
            currentTime = Mathf.Lerp(currentTime, 0.0f, Time.deltaTime * 1.0f);
            
            // time is not ended now
            if ((timeLimit - currentTime).ToString("F0") == timeLimit.ToString("F0"))
            {
                currentTime = 0.0f;
                timerRingFill.fillAmount = 1.0f;
                timeEnd = false;
                break;
            }

            yield return null;
        }

        yield return null;
    }

    void Update()
    {
        // constantly update this two visually 
        timerText.text = getCountDownTime().ToString("F0");
        timerRingFill.fillAmount = (getCountDownTime() / timeLimit);
    }

    // set the time limit
    public void setTimeLimit(float t)
    {
        this.timeLimit = t;
    }

    // get the time limit 
    public float getTimeLimit()
    {
        return timeLimit;
    }
    
    // get the increasingly number 
    public float getCurrentTime()
    {
        return currentTime;
    }

    // get count down timer (big to small num)
    public float getCountDownTime()
    {
        return timeLimit - currentTime;
    }

    // return time end or not
    public bool isTimeEnd()
    {
        return timeEnd;
    }

    // need to stop the time mah
    public void setTimeEnd(bool t)
    {
        timeEnd = t;
    }
}
