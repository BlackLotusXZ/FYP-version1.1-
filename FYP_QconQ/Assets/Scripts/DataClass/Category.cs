using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/********************************************/
/**
*  \brief
*   - This script handles the categories for story mode
***********************************************/
[System.Serializable]
public class Category
{
    public List<Stage> Stages_Cat; // all stages database
    public List<int> QtnsTried_Cat;

    private int totalNoStage_Cat; // how many stages that this category have
    private string Name_Cat = "NIL"; // name of this category 
    public float totalAttempted_Cat = 0.0f; // if got go inside the stage .. will be counted as attempted
    private float totalCorrect_Cat = 0.0f; // overall total correct from this category
    private float totalScore_Cat = 0.0f; // overall total score from this category
    private float MaximumScore_Cat = 0.0f; // overall maximum score from this category
    private int winstreak_Cat = 0;
    private int losestreak_Cat = 0;
    private int highestscore_Cat = 0;
    public int playedUntil_Cat = 0; // which stage that the player played until to .. 

    public void InitName(string CategoryName)
    {
        // set up the name of this category
        Name_Cat = CategoryName;

        // how many stages that this category hold
        // each stage maximum will hold 10 question
        totalNoStage_Cat = GameControl.handle.Database.GetCategorySize(Name_Cat);
        totalNoStage_Cat /= 10;
        // init all the available stages 
        for (int i = 0; i < totalNoStage_Cat; i++)
        {
            Stages_Cat.Add(new Stage());
        }

        // must have stages before adding new
        if (totalNoStage_Cat > 0)
        {
            int maxQtnNo = 10; // index for csv de
            int startNo = 0; // index for csv de

            // Stage Init
            for (int i = 0; i < totalNoStage_Cat; i++)
            {
                Stages_Cat[i].Init(i + 1, startNo, Name_Cat, maxQtnNo);
                startNo += maxQtnNo; // start of the stage qtn no
                //maxQtnNo += 1; // max qtn for each stage , incremental , harder stage = more qtns
            }
        }
    }

    public void Save(string ModeName)
    {

        for (int i = 0; i < totalNoStage_Cat; i++)
        {
            Stages_Cat[i].Save(ModeName, Name_Cat);
        }

        string path = ModeName + Name_Cat;

        PlayerPrefs.SetFloat(path + "totalAttempted", totalAttempted_Cat);
        PlayerPrefs.SetFloat(path + "totalCorrect", totalCorrect_Cat);
        PlayerPrefs.SetInt(path + "playedUntil", playedUntil_Cat);
        PlayerPrefs.SetFloat(path + "totalScore", totalScore_Cat);
        PlayerPrefs.SetInt(path + "winstreak", winstreak_Cat);
        PlayerPrefs.SetInt(path + "losestreak", losestreak_Cat);
        PlayerPrefs.SetInt(path + "highestscore", highestscore_Cat);
       // Debug.Log(totalScore_Cat);
        //    totalCorrect_Cat = 0;      
        //    totalCorrect_Cat = PlayerPrefs.GetFloat(path + "totalAttempted");
        //    Debug.Log(totalCorrect_Cat);
        int[] savingTried = new int[QtnsTried_Cat.Count];
        for (int i = 0; i < QtnsTried_Cat.Count; i++)
        {
            savingTried[i] = QtnsTried_Cat[i];
        }

        PlayerPrefsX.SetIntArray(path + "QtnsTried", savingTried);
    }

    public void Load(string ModeName)
    {
        // Load 10 stages
        for (int i = 0; i < totalNoStage_Cat; i++)
        {
            Stages_Cat[i].Load(ModeName, Name_Cat);
        }

        string path = ModeName + Name_Cat;

        totalAttempted_Cat = PlayerPrefs.GetFloat(path + "totalAttempted");
        totalCorrect_Cat = PlayerPrefs.GetFloat(path + "totalCorrect");
        playedUntil_Cat = PlayerPrefs.GetInt(path + "playedUntil");
        totalScore_Cat = PlayerPrefs.GetFloat(path + "totalScore");
        winstreak_Cat = PlayerPrefs.GetInt(path + "winstreak");
        losestreak_Cat = PlayerPrefs.GetInt(path + "losestreak");
        highestscore_Cat = PlayerPrefs.GetInt(path + "highestscore");
        Debug.Log(totalScore_Cat);
        int[] savingTried;

        savingTried = PlayerPrefsX.GetIntArray(path + "QtnsTried");

        for (int i = 0; i < savingTried.Length; i++)
        {
            QtnsTried_Cat.Add(savingTried[i]);
        }
        //Delete();
    }

    public List<string> getQtn()
    {
        return Stages_Cat[GameControl.handle.currentStageNo].FetchQuestion();
    }

    public bool pressAns(int ans)
    {
        //totalAttempted_Cat += 1;
        bool correctOrNot = Stages_Cat[GameControl.handle.currentStageNo].pressAns(ans);

        if (correctOrNot)
        {
            // totalAttempted_Cat += 1;
        }
        return correctOrNot;
    }

    public int setStage(float score, int noOfCorrects, int winstreak, int losestreak)
    {
        //if (GameControl.handle.currentStageNo == playedUntil_Cat)
        //  playedUntil_Cat++;
        // totalCorrect_Cat = 0;
        // totalAttempted_Cat = 0;
        totalAttempted_Cat += 10;
      //  Debug.Log(totalCorrect_Cat);
        totalCorrect_Cat += noOfCorrects;
     //   Debug.Log(totalCorrect_Cat);
      //  totalScore_Cat += Stages_Cat[GameControl.handle.currentStageNo].CompareScore((int)score);

        return Stages_Cat[GameControl.handle.currentStageNo].setStage(score, noOfCorrects, winstreak, losestreak);
    }

    public void Delete()
    {
        Debug.Log("delete");
        for (int i = 0; i < totalNoStage_Cat; i++)
        {
            Stages_Cat[i].Delete();
        }

        totalAttempted_Cat = 0;
        totalCorrect_Cat = 0;
        playedUntil_Cat = 0;
        totalScore_Cat = 0;
        winstreak_Cat = 0;
        losestreak_Cat = 0;
        highestscore_Cat = 0;
    }

    public void InitStage()
    {
        Stages_Cat[GameControl.handle.currentStageNo].InitStage();
    }

    public int getStars(int stage)
    {
        return Stages_Cat[stage].getStars();
    }

    public float getPercent()
    {
        return totalCorrect_Cat / (totalNoStage_Cat * 10) * 100;
    }

    public float getCorrectCount()
    {
        float correct = 0;

        for (int i = 0; i < Stages_Cat.Count; i++)
        {
            correct += Stages_Cat[i].getCorrectCount_Stage();
        }

        return correct;
    }

    public float getAvgQuality() // only get avg quality of stages that has been played before
    {
        float avg = 0;
        float minus = 0;
        for (int i = 0; i < Stages_Cat.Count; i++)
        {
            if (Stages_Cat[i].getPlayedBefore_Stage() > 0)
            {
                avg += Stages_Cat[i].getFinalQuality_Stage();
            }
            else
                minus++;
        }

        avg /= (Stages_Cat.Count - minus);
        if (avg > 0 == false)
            return 0;
        else
            return avg;
    }

    public int gettotalNoStage_Cat()
    {
        return totalNoStage_Cat;
    }

    public string getName_Cat()
    {
        return Name_Cat;
    }

    public float gettotalAttempted_Cat()
    {
        return totalAttempted_Cat;
    }

    public float gettotalScore_Cat()
    {
        //  Debug.Log(totalScore_Cat);  
        return totalScore_Cat;
    }

    public float gettotalCorrect_Cat()
    {
        return totalCorrect_Cat;
    }
    public float gethighestscore_Cat()
    {
        int temphiggestscore = 1;
        for (int i = 0; i < Stages_Cat.Count; i++)
        {
            if (temphiggestscore == 1)
            {
                highestscore_Cat = Stages_Cat[i].getScore_Stage();
            }
            temphiggestscore = Stages_Cat[i].getScore_Stage();
            if (temphiggestscore > losestreak_Cat)
            {
                highestscore_Cat = temphiggestscore;
            }
        }
        Debug.Log("highestscoreCat" + highestscore_Cat);
        return highestscore_Cat;
    }
    public int getwinstreak_Cat()
    {
        int tempwinstreak = 1;
        for (int i = 0; i < Stages_Cat.Count; i++)
        {
            if (tempwinstreak == 1)
            {
                winstreak_Cat = Stages_Cat[i].getwinstreak_Stage();
            }
            tempwinstreak = Stages_Cat[i].getwinstreak_Stage();
            if (tempwinstreak > losestreak_Cat)
            {
                winstreak_Cat = tempwinstreak;
            }
        }
        Debug.Log("winstreakcat" + winstreak_Cat);
        return winstreak_Cat;
    }
    public int getlosestreak_Cat()
    {
        int templosestreak = 1;
        for (int i = 0; i < Stages_Cat.Count; i++)
        {
            if (templosestreak == 1)
            {
                losestreak_Cat = Stages_Cat[i].getlosestreak_Stage();
            }
            templosestreak = Stages_Cat[i].getlosestreak_Stage();
            if (templosestreak > losestreak_Cat)
            {
                losestreak_Cat = templosestreak;
            }
        }
        Debug.Log("losestreakcat" + losestreak_Cat);
        return losestreak_Cat;
    }
    //public int getplayedUntil_Cat()
    //{
    //    //string path = "StoryMode" + Name_Cat;
    //    //PlayerPrefs.GetInt(path + "playedUntil_Cat");
    //    return playedUntil_Cat;
    //}

    //public void setplayedUntil_Cat(int playuntil)
    //{
    //    //string path = "StoryMode" + Name_Cat;
    //    playedUntil_Cat = playuntil;
    //    //PlayerPrefs.SetInt(path + "playedUntil", playuntil);
    //}
}
