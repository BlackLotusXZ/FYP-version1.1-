using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class Statistics : MonoBehaviour {

    int currentCat = 0;

    public Text catName;
    public Text totalPlaytime;

    public GameObject[] GraphBars;


    float[] numbers = new float[6]; // each bar number for current cat
	// Use this for initialization
	void Start () 
    {
        float sec = GameControl.handle.player.totalPlaytime;
        float min = (int)(sec / 60.0f);
        float hour = (int)(min / 60.0f);

        min = min % 60;

        if (hour > 0)
            totalPlaytime.text = hour + " hr " + min + " min";
        else
            totalPlaytime.text = min + " min";

       // totalPlaytime.text = GameControl.handle.player.TotalPlayTime_Player.ToString();

	    changeCat();
	}

    void Update()
    {
        for (int i = 0; i < GraphBars.Length; i++)
        {
         //   float fillHowMuch = GraphBars[i].GetComponent<Image>().fillAmount;

         //   GraphBars[i].GetComponent<Image>().fillAmount = Mathf.Lerp(fillHowMuch, lerpAmount[i], Time.deltaTime * lerpSpeed);
           
            foreach (Transform child in GraphBars[i].GetComponentInChildren<Transform>())
            {
           //     child.GetComponent<RectTransform>().localPosition = new Vector2(0, (fillHowMuch * (textPos[1].localPosition.y - textPos[0].localPosition.y) + textPos[0].localPosition.y));

                foreach (Transform children in child.GetComponentInChildren<Transform>())
                {
                  //  if (children.tag == "Percentage")
                 //   {
                 //       children.GetComponentInChildren<Text>().text = (fillHowMuch * 100).ToString("F2") + "%";
                //    }
                    if (children.tag == "Score")
                    {
                        children.GetComponentInChildren<Text>().text = numbers[i].ToString();
                    }
                }
            }
            /*
            GraphBars[0].GetComponent<Image>().color = lerpColor[2];
            GraphBars[1].GetComponent<Image>().color = lerpColor[0];

            if (i > 1)
            {
                if ((lerpAmount[i] - fillHowMuch) < 0.0001f)
                {
                    if (fillHowMuch < 0.33f)
                        GraphBars[i].GetComponent<Image>().color = Color.Lerp(GraphBars[i].GetComponent<Image>().color, lerpColor[0], Time.deltaTime * lerpSpeed);
                    else if (fillHowMuch < 0.66f)
                        GraphBars[i].GetComponent<Image>().color = Color.Lerp(GraphBars[i].GetComponent<Image>().color, lerpColor[1], Time.deltaTime * lerpSpeed);
                    else
                        GraphBars[i].GetComponent<Image>().color = Color.Lerp(GraphBars[i].GetComponent<Image>().color, lerpColor[2], Time.deltaTime * lerpSpeed);
                }
            }*/
        }
    }

    public void pressRight()
    {
        currentCat = (currentCat + 1) % 6;

        changeCat();
    }

    public void PressLeft()
    {
        currentCat -= 1;

        if (currentCat < 0)
            currentCat = 5;

        changeCat();
    }

    void changeCat()
    {
        // Correct , wrong , win streak , lose streak , highest score , hightest earning
        if(currentCat < 5)
        {
            catName.text = GameControl.handle.Modes[0].Categories[currentCat].getName_Cat();

            float Correct = GameControl.handle.Modes[0].Categories[currentCat].gettotalCorrect_Cat();
            float attempted = GameControl.handle.Modes[0].Categories[currentCat].gettotalAttempted_Cat();
            float winstreak = GameControl.handle.Modes[0].Categories[currentCat].getwinstreak_Cat();
            float losestreak = GameControl.handle.Modes[0].Categories[currentCat].getlosestreak_Cat();
            float highestscore = GameControl.handle.Modes[0].Categories[currentCat].gethighestscore_Cat();
            float highestearning = GameControl.handle.Modes[0].Categories[currentCat].gethighestscore_Cat();

            numbers[0] = Correct;//correct
            
            numbers[1] = (attempted - Correct);//wrong

            numbers[2] = winstreak;//win streak

            numbers[3] = losestreak;//lose streak

            numbers[4] = highestscore;//highest score

            numbers[5] = highestearning;//highest earning

        }

        if (currentCat == 5)
        {
            catName.text = "ARCADE";

            float Correct = GameControl.handle.Modes[1].m_Arcade.GetTotalCorrect();
            float attempted = GameControl.handle.Modes[1].m_Arcade.GetTotalAttempted();
            float winstreak = GameControl.handle.Modes[1].m_Arcade.GetWinstreak();
            float losestreak = GameControl.handle.Modes[1].m_Arcade.Getlosestreak();
            float highestscore = GameControl.handle.Modes[1].m_Arcade.Gethighestscore();
            float highesteaning = GameControl.handle.Modes[1].m_Arcade.Gethighestearning();
            numbers[0] = Correct;//correct

            numbers[1] = (attempted - Correct);//wrong

            numbers[2] = winstreak;//win streak

            numbers[3] = losestreak;//lose streak

            numbers[4] = highestscore;//highest score

            numbers[5] = highesteaning;//highest earning

        }
    }

}