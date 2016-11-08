using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class overallshow : MonoBehaviour {

    float total = 0.0f;
	// Use this for initialization
	void Start () 
    {
        for (int i = 0; i < 5; i++)
        {
            float totallll = GameControl.handle.Modes[0].Categories[i].gettotalNoStage_Cat() * 10;
            float tried = GameControl.handle.Modes[0].Categories[i].getCorrectCount();

            total += (tried / totallll * 100.0f);
        }

        this.GetComponent<Text>().text = "Your Current Progress Is: " + (total / 5.0f).ToString() +" %";
	}
}
