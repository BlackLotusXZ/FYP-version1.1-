using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashLoading : MonoBehaviour {

    public CanvasGroup[] splashGroup;
    public float TransitionSpeed = 1.0f;
    public float fadeSpeed = 1.0f;
    private int c = 0; // this is to keep track of the current splash screen : 0 -> continental splash, 1 -> nyp de, 2 -> team name
    private Text slogan;
    private int sloganNumber = 0;
    private string[] possibleSlogans = 
        new string[]{
        "You think learning is boring? \n\nStart our QUALITY quiz and be convinced of how much FUN learning can be!",
        "Is Learning tiring you out? \n\nStart our QUALITY quiz and find out how much FUN it can be!",
        "Becoming a quality wizard and having fun at the same time.",
        "A quality quiz that lets you have fun while learning!",
        "Lets you have fun & makes you an ace in quality!",
        };

    // Use this for initialization
    void Start ()
    {

        for (int i = 0; i < 3; ++i)
        {
            splashGroup[i].alpha = 0.0f;
            splashGroup[i].gameObject.SetActive(false);
        }

        slogan = splashGroup[2].gameObject.GetComponentInChildren<Text>();
        sloganNumber = Random.Range(0, 5);
        slogan.text = possibleSlogans[sloganNumber];
       

        StartSplash();
    }

    void StartSplash()
    {
        StartCoroutine(splashStartLoop());
    }

    IEnumerator splashStartLoop()
    {
        c = 0;

        while (c < 3)
        {
            splashGroup[c].gameObject.SetActive(true);

            while (splashGroup[c].alpha < 1.0f)
            {
                splashGroup[c].alpha = Mathf.Lerp(splashGroup[c].alpha, 1.5f, Time.deltaTime* TransitionSpeed);
                yield return null;
            }

            if (c == 2)
            {
                yield return new WaitForSeconds(1.25f);
                break;
            }
                
            else
                yield return new WaitForSeconds(0.5f);

            while (splashGroup[c].alpha > 0.0f)
            {
                splashGroup[c].alpha = Mathf.Lerp(splashGroup[c].alpha, -0.2f, Time.deltaTime* fadeSpeed);
                yield return null;
            }

            splashGroup[c].alpha = 0.0f;
            splashGroup[c].gameObject.SetActive(false);

            c++;

            yield return new WaitForSeconds(0.2f);

        }

        GameManager.instance.GotoScene("MainScene");
        yield return new WaitForSeconds(1.0f); // for loading screen

        // final effect ... when finish fading shld see the entire main menu scene behind
        c = 2;
        while (splashGroup[c].alpha > 0.0f)
        {
            splashGroup[c].alpha = Mathf.Lerp(splashGroup[c].alpha, -0.2f, Time.deltaTime * 3.0f);
            yield return null;
        }

        yield return null;

        // destroy the whole canvas now !!
        transform.parent.GetComponent<DonDestroyThis>().NowDestroy();
    }
}
