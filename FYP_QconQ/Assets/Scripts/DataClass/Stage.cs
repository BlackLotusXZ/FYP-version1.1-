using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/********************************************/
/**
*  \brief
*   - This script handles the stages in each category of story mode (thus the category class HAS MULTIPLE stage classes)
***********************************************/
[System.Serializable]
public class Stage  
{
    // Set of questions the stage has.
    public csvReader.Row[] Questions;

    private int Medal_Stage = 0; // medals reward
    private string Name_Stage = "NIL"; // name of this stage
    private float FastestCompletedTime_Stage = 0.0f; // the fastest timing for player to complete this stage
    private float currentCompletedTime_Stage = 0.0f; // the timing when player finish the stage
    private float finalQuality_Stage = 0.0f; // when completed .. i want to know the final quality of the car
    public int[] rangeRanking_Stage = new int[3]; // for comparison purpose
    public float correctCount_Stage = 0.0f;  // current correct number 
    private int currentQtn_Stage = 0;
    private int size_Stage = 0;
    private int MaxQtns_Stage = 10;
    private int playedBefore_Stage = 0;
    private int score_stage = 0;
    private int winstreak_stage = 0;
    private int losestreak_stage = 0;
    private List<int> qtnsOut = new List<int>();
    private string[] correctAns = new string[3];

    public bool isNextStage = false;
    public bool isDone = false;         //Check if stage is done

    public void Init(int stageNo, int startNo, string categoryName, int maxQtnNo)
    {
        MaxQtns_Stage = maxQtnNo;
        Name_Stage = "Stage" + stageNo;

        // Marking scheme to determine number of stars for the stage
        //for (int i = 0; i < rangeRanking.Length; i++)
        rangeRanking_Stage[0] = 40;
        rangeRanking_Stage[1] = 60;
        rangeRanking_Stage[2] = 80;

        Questions = new csvReader.Row[MaxQtns_Stage];

        for (int i = 0; i < MaxQtns_Stage; i++)
        {
            qtnsOut.Add(i);
        }

        //assuming every category has 100 qtns
        for (int i = 0; i < MaxQtns_Stage; i++)
        {
            Questions[i] = GameControl.handle.Database.FetchStageQtns(startNo + i, categoryName);
        }

    }

    public void InitStage()
    {
        qtnsOut.Clear();

        for (int i = 0; i < MaxQtns_Stage; i++)
        {
            qtnsOut.Add(i);
        }
    }

    public List<string> FetchQuestion()
    {
        // a random element to use the number as a question
        int random = Random.Range(0, qtnsOut.Count);

        currentQtn_Stage = qtnsOut[random];

        List<string> Data = new List<string>();
        Data.Add(Questions[currentQtn_Stage].Question);
        for (int i = 0; i < 10; i++)
            Data.Add(Questions[currentQtn_Stage].Answer[i]);

        for (int i = 0; i < correctAns.Length; i++)
            correctAns[i] = Questions[currentQtn_Stage].CorrectAns[i];

        qtnsOut.RemoveAt(random); // remove the element so no repeat questions


        Data.Add(qtnsOut.Count.ToString()); // return how many questions left that is not repeated

        if(qtnsOut.Count == 0) // refill the list so there are questions to use next time (replay)
        {
            for (int i = 0; i < MaxQtns_Stage; i++)
            {
                qtnsOut.Add(i);
            }
        }

        return Data; // return the list of data for 1 question
    }

    public bool pressAns(int ans) 
    {
        if (!GameControl.handle.Modes[0].Categories[GameControl.handle.currentCategory].QtnsTried_Cat.Contains(int.Parse(Questions[currentQtn_Stage].ID)))
        {
            GameControl.handle.Modes[0].Categories[GameControl.handle.currentCategory].QtnsTried_Cat.Add(int.Parse(Questions[currentQtn_Stage].ID));
        }

        // returns true if correct 
        for (int i = 0; i < correctAns.Length; i++)
        {
            if (Questions[currentQtn_Stage].Answer[ans] == correctAns[i])
            {
                return true;
            }
        }
        return false;
    }

    public void Save(string ModeName, string CategoryName)
    {
        string path = ModeName + CategoryName + Name_Stage;
        //score_stage = 0;
        PlayerPrefs.SetInt(path + "Medal_Stage", Medal_Stage);
        PlayerPrefs.SetFloat(path + "FastestCompletedTime_Stage", FastestCompletedTime_Stage);
        PlayerPrefs.SetFloat(path + "finalQuality_Stage", finalQuality_Stage);
        PlayerPrefs.SetFloat(path + "correctCount_Stage", correctCount_Stage);
        PlayerPrefs.SetInt(path + "playedBefore_Stage", playedBefore_Stage);
        PlayerPrefs.SetInt(path + "score_Stage", score_stage);
        PlayerPrefs.SetInt(path + "winstreak_Stage", winstreak_stage);
        PlayerPrefs.SetInt(path + "losestreak_Stage", losestreak_stage);
    }

    public void Load(string ModeName, string CategoryName)
    {
        string path = ModeName + CategoryName + Name_Stage;

        Medal_Stage = PlayerPrefs.GetInt(path + "Medal_Stage");
        FastestCompletedTime_Stage = PlayerPrefs.GetFloat(path + "FastestCompletedTime_Stage");
        finalQuality_Stage = PlayerPrefs.GetFloat(path + "finalQuality_Stage");
        correctCount_Stage = PlayerPrefs.GetFloat(path + "correctCount_Stage");
        playedBefore_Stage = PlayerPrefs.GetInt(path + "playedBefore_Stage");
        score_stage = PlayerPrefs.GetInt(path + "score_Stage");
        winstreak_stage = PlayerPrefs.GetInt(path + "winstreak_Stage");
        losestreak_stage = PlayerPrefs.GetInt(path + "losestreak_Stage");

    }

    public int setStage(float score, int noOfCorrects, int winstreak,int losestreak)
    {
        // if the score is better .. then we will store 
        if (noOfCorrects > correctCount_Stage)
        {
            correctCount_Stage = noOfCorrects;
            float percentage = correctCount_Stage / MaxQtns_Stage * 100;

            for (int i = 0; i < rangeRanking_Stage.Length; i++)
            {
                if (percentage >= rangeRanking_Stage[i])
                    Medal_Stage = i + 1;
            }
        }
        if (winstreak > winstreak_stage)
        {
            winstreak_stage = winstreak;
            Debug.Log("winstreakstage" + winstreak_stage);
        }
        if (losestreak > losestreak_stage)
        {
            losestreak_stage = losestreak;
            Debug.Log("losestreakstage" + losestreak_stage);
        }

        /*
        // only get the highest quality
        if (quality > finalQuality_Stage)
            finalQuality_Stage = quality;
        */
        playedBefore_Stage += 1;

        GameControl.handle.player.Player_Money += (int)score;

        Category tempCat = GameControl.handle.Modes[0].Categories[GameControl.handle.currentCategory];

        //Check here
        
        if (GameControl.handle.currentStageNo == tempCat.playedUntil_Cat && Medal_Stage > 0)
            tempCat.playedUntil_Cat += 1;

        score_stage = (int)score;
        Debug.Log(score_stage);
        return (int)score;
    }
    public int CompareScore(int score)
    {
        Debug.Log(score_stage);
        if (score > score_stage)
        {
            return score - score_stage;
        }
        else if (score_stage > score)
        {
            return 0;
        }
        else if(score == score_stage)
        {
            return 0;
        }
        else
        return 0;
       
    }
    public void Delete()
    {
        Debug.Log("delete");
        //Medal_Stage = 0;
        //FastestCompletedTime_Stage = 0;
        //finalQuality_Stage = 0;
        //correctCount_Stage = 0;

        Medal_Stage = 0; // medals reward
        Name_Stage = "NIL"; // name of this st
        FastestCompletedTime_Stage = 0.0f; 
        currentCompletedTime_Stage = 0.0f; 
        finalQuality_Stage = 0.0f; 
        correctCount_Stage = 0.0f;
        playedBefore_Stage = 0;
        winstreak_stage = 0;
        losestreak_stage = 0;
    }

    public bool checkIfLevelIsDone()
    {
        if (correctCount_Stage * 10 < rangeRanking_Stage[0])
            return false;
        else
            return true;
    }

    public int getStars()
    {
        return Medal_Stage;
    }
    public string getName_Stage()
    {
        return Name_Stage;
    }
    public float getFastestCompletedTime_Stage()
    {
        return FastestCompletedTime_Stage;
    }
    public float getCurrentCompletedTime_Stage()
    {
        return currentCompletedTime_Stage;
    }
    public float getFinalQuality_Stage()
    {
        return finalQuality_Stage;
    }
    public float getCorrectCount_Stage()
    {
        return correctCount_Stage;
    }
    public int getCurrentQtn_Stage()
    {
        return currentQtn_Stage;
    }
    public int getSize_Stage()
    {
        return size_Stage;
    }
    public int getMaxQtns_Stage()
    {
        return MaxQtns_Stage;
    }
    public int getPlayedBefore_Stage()
    {
        return playedBefore_Stage;
    }
    public int getwinstreak_Stage()
    {
        return winstreak_stage;
    }
    public int getlosestreak_Stage()
    {
        return losestreak_stage;
    }
    public int getScore_Stage()
    {
        return score_stage;
    }
    

}
