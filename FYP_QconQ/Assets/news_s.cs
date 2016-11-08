using UnityEngine;
using System.Collections;

public class news_s : MonoBehaviour 
{
    public CanvasGroup[] allnewspanel;
    int currentNewsPage = 0;
    float timer = 0.0f;
	// Use this for initialization
	void Start () 
    {
        allnewspanel[0].alpha = 1.0f;
	    for(int i = 1; i < allnewspanel.Length; ++i)
        {
            allnewspanel[i].alpha = 0.0f;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;

        if(timer > 5.0f)
        {
            allnewspanel[currentNewsPage].alpha = 0.0f;
            currentNewsPage+=1;
            if (currentNewsPage > 3)
                currentNewsPage = 0;

            allnewspanel[currentNewsPage].alpha = 1.0f;
            timer = 0.0f;
        }
        
	}
}
