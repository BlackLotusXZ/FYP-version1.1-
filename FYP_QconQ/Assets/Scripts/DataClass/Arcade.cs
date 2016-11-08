using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/********************************************/
/**
*  \brief
*   - Data class for all game logic for arcade mode
***********************************************/
[System.Serializable]
public class Arcade 
{
    public csvReader.Row currentQtn = new csvReader.Row();
    public List<int> qtnIDs_Arcade = new List<int>();

    private float totalCorrect_Arcade = 0; // how many the player have answer it correctly
    private float totalSize_Arcade = 0; // The total number of questions that the player can answer
    private float totalAttempted_Arcade = 0; // player attempted how many of it
    private int winstreak_Arcade = 0;
    private int losestreak_Arcade = 0;
    private int highestscore_Arcade = 0;
    private int highestearning_Arcade = 0;
    private string[] correctAns_Arcade = new string[3]; // now is only 1 correct answer .. the rest will be nil
    List<string> Data = new List<string>();

    public void Init()
    {
        qtnIDs_Arcade.Clear();
        totalSize_Arcade = GameControl.handle.Database.GetRowList().Count;

        for (int i = 0; i < totalSize_Arcade; i++)
        {
            qtnIDs_Arcade.Add(i);
        }
    }

    

    public void Save(string ModeName)
    {
        string path = ModeName;

        PlayerPrefs.SetFloat(path + "totalCorrect_Arcade", totalCorrect_Arcade);

        //int count = 0;

        //int[] qtnsTried = new int[(int)totalSize_Arcade - qtnIDs_Arcade.Count];
        //for (int i = 0; i < totalSize_Arcade; i++)
        //{
        //    if (!qtnIDs_Arcade.Contains(i))
        //    {
        //        qtnsTried[count] = i;
        //        count++;
        //    }
        //}
        
        //PlayerPrefsX.SetIntArray(path + "qtnIDs_Arcade", qtnsTried);
        //Debug.Log(qtnsTried.Length);
        PlayerPrefs.SetFloat("totalAttempted_Arcade", totalAttempted_Arcade);
        PlayerPrefs.SetInt("winstreak_Arcade", winstreak_Arcade);
        PlayerPrefs.SetInt("losestreak_Arcade", losestreak_Arcade);
        PlayerPrefs.SetInt("highestscore_Arcade", highestscore_Arcade);
        PlayerPrefs.SetInt("highestearning_Arcade", highestearning_Arcade);

    }

    public void Load(string ModeName)
    {
        string path = ModeName;

        totalCorrect_Arcade = PlayerPrefs.GetFloat(path + "totalCorrect_Arcade");
        totalAttempted_Arcade = PlayerPrefs.GetFloat("totalAttempted_Arcade");
        winstreak_Arcade = PlayerPrefs.GetInt("winstreak_Arcade");
        losestreak_Arcade = PlayerPrefs.GetInt("losestreak_Arcade");
        highestscore_Arcade = PlayerPrefs.GetInt("highestscore_Arcade");
        highestearning_Arcade = PlayerPrefs.GetInt("highestearning_Arcade");
        //int[] qtnsTried;

        //PlayerPrefsX.SetIntArray(path + "qtnIDs_Arcade", 0);
        //qtnsTried = PlayerPrefsX.GetIntArray(path + "qtnIDs_Arcade");
        //Debug.Log(qtnsTried.Length);

        qtnIDs_Arcade = new List<int>();

        //checker = new List<int>();
        
        //Add questions tried into a list called checker
        //for (int i = 0; i < qtnsTried.Length; i++ )
        //{
        //    checker.Add(qtnsTried[i]);
        //}

        //Add questions qtnIDs_Arcade which is the array of arcade questions
        for (int i = 0; i < totalSize_Arcade; i++)
        {
            qtnIDs_Arcade.Add(i);
        }

    }
    public int setStage(float score, int money, int winstreak, int losestreak)
    {
        GameControl.handle.player.Player_Money += money;
        if (winstreak > winstreak_Arcade)
        {
            winstreak_Arcade = winstreak;
            Debug.Log("winstreakarcade" + winstreak_Arcade);
        }
        if (losestreak > losestreak_Arcade)
        {
            losestreak_Arcade = losestreak;
            Debug.Log("losestreakarcade" + losestreak_Arcade);
        }
        if (score > highestscore_Arcade)
        {
            highestscore_Arcade = (int)score;
        }
        if (money > highestearning_Arcade)
        {
            highestearning_Arcade = money;
        }
        Debug.Log(highestscore_Arcade);
        return (int)score;
    }
    public List<string> getQtn()
    {
        if (qtnIDs_Arcade.Count > 1)
            currentQtn = GameControl.handle.Database.FetchArcadeQtns(qtnIDs_Arcade);
        else
        {
            Init();

            currentQtn = GameControl.handle.Database.FetchArcadeQtns(qtnIDs_Arcade);
        }
        
        Data.Clear();
        Data.Add(currentQtn.Question);
        //Debug.Log(qtnIDs_Arcade.Count)

        for (int i = 0; i < 10; i++)
            Data.Add(currentQtn.Answer[i]);

        for (int i = 0; i < correctAns_Arcade.Length; i++)
            correctAns_Arcade[i] = currentQtn.CorrectAns[i];

        return Data;
    }

    public bool pressAns(int ans)
    {
        totalAttempted_Arcade++;
        Debug.Log("total attempted" + totalAttempted_Arcade);
        qtnIDs_Arcade.Remove(int.Parse(currentQtn.ID));

        for (int i = 0; i < correctAns_Arcade.Length; i++)
        {
            if (currentQtn.Answer[ans] == correctAns_Arcade[i])
            {
                totalCorrect_Arcade++;
                Debug.Log("arcade correct" + totalCorrect_Arcade);
               // GameControl.handle.player.Player_Money += 1;
                return true;
            }
        }
        return false;
    }

    public void Delete()
    {
        Debug.Log("delete");
        totalCorrect_Arcade = 0;
        totalAttempted_Arcade = 0;
        winstreak_Arcade = 0;
        losestreak_Arcade = 0;
        highestearning_Arcade = 0;
        highestscore_Arcade = 0;
        qtnIDs_Arcade.Clear();

        for (int i = 0; i < totalSize_Arcade; i++)
        {
            qtnIDs_Arcade.Add(i);
        }
    }

    public float GetTotalSize()
    {
        return totalSize_Arcade;
    }

    public float GetTotalAttempted()
    {
        return totalAttempted_Arcade;
    }

    public int Gethighestscore()
    {
        return highestscore_Arcade;
    }
    public int Gethighestearning()
    {
        return highestearning_Arcade;
    }
    public int GetWinstreak()
    {
        return winstreak_Arcade;
    }
    public int Getlosestreak()
    {
        return losestreak_Arcade;
    }
    public float GetTotalCorrect()
    {
        return totalCorrect_Arcade;
    }

}
