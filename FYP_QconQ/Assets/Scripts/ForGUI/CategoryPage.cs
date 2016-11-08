using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CategoryPage : MonoBehaviour {

    public static CategoryPage handle;

    public Image[] percentageBar;
    public Text[] percentage;

	// Use this for initialization
	void Start () {
        handle = this;

        updateBar();
	}
	
	// Update is called once per frame
	public void updateBar() 
    {
        for (int i = 0; i < GameControl.handle.Modes[0].Categories.Length; i++)
        {
            float total = GameControl.handle.Modes[0].Categories[i].gettotalNoStage_Cat() * 10;
            float tried = GameControl.handle.Modes[0].Categories[i].getCorrectCount();

            percentageBar[i].fillAmount = tried / total;
            percentage[i].text = (tried / total * 100).ToString("F0") + "%";

            if (tried == 0)
            {
                percentageBar[i].fillAmount = 0;
                percentage[i].text = "0%";
            }
            if (tried / total * 100 > 100)
                percentage[i].text = "100%";

        }

	}
}
