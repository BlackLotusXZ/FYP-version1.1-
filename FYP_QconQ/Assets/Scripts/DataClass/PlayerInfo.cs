using UnityEngine;
using System.Collections;


/********************************************/
/**
*  \brief
*   - This script includes most of the handling of playerprefs to save/load the information that the player can save/load
***********************************************/
[System.Serializable]
public class PlayerInfo 
{
    public string PlayerName_Player = "Default Name";
    public string DeviceUniqueID = "NONO";
    public int PlayerID_Player = 0;
    public int SettingsBGM_Player = 0;
    public int SettingsSE_Player = 0;
    public float TotalPlayTime_Player = 0.0f;
    public int SettingsOrientation_Player = 0;
    public int EnvironmentIn = 0;
    public int cumulativeScore = 0;
    

    public int Player_Money = 200;


    //Badges recurring info
    public int arcademodeAttempts = 0;
    public int numberOfStagesAllCorrect = 0;
    public int correctsInARow = 0;
    public int arcadeScore = 0;
    public int speedrunTimer = -1;
    public float totalPlaytime = 0;       //Carefull with this one
    public float percentOfItemsUnlocked = 0;

    public void Start()
    {

    }

	// save the player info 
    public void Save()
    {
        PlayerPrefs.SetString("PlayerName_Player", PlayerName_Player);
        PlayerPrefs.SetInt("PlayerID_Player", PlayerID_Player);

        PlayerPrefs.SetInt("SettingsBGM_Player", SettingsBGM_Player);
        PlayerPrefs.SetInt("SettingsSE_Player", SettingsSE_Player);
        PlayerPrefs.SetFloat("TotalPlayTime_Player", TotalPlayTime_Player);

        PlayerPrefs.SetInt("SettingsOrientation_Player", SettingsOrientation_Player);

        PlayerPrefs.SetInt("EnvironmentIn", EnvironmentIn);

        PlayerPrefs.SetInt("Player_Money", Player_Money);

        PlayerPrefs.SetInt("Cumulative_Score", cumulativeScore);

        
        //Badges
        PlayerPrefs.SetInt("B_Arcademode_Attempts", arcademodeAttempts);
        PlayerPrefs.SetInt("B_StagesAllCorrect", numberOfStagesAllCorrect);
        PlayerPrefs.SetInt("B_CorrectInARow", correctsInARow);
        PlayerPrefs.SetInt("B_ArcadeScore", arcadeScore);
        PlayerPrefs.SetInt("B_SpeedrunTimer", speedrunTimer);
        PlayerPrefs.SetFloat("B_TotalPlaytime", totalPlaytime);
        PlayerPrefs.SetFloat("B_ItemsPercent", percentOfItemsUnlocked);
    }

    public void Load()
    {
        PlayerName_Player = PlayerPrefs.GetString("PlayerName_Player");
        PlayerID_Player = PlayerPrefs.GetInt("PlayerID_Player");

        SettingsBGM_Player = PlayerPrefs.GetInt("SettingsBGM_Player");
        SettingsSE_Player = PlayerPrefs.GetInt("SettingsSE_Player");
        TotalPlayTime_Player = PlayerPrefs.GetFloat("TotalPlayTime_Player");

        SettingsOrientation_Player = PlayerPrefs.GetInt("SettingsOrientation_Player");

        EnvironmentIn = PlayerPrefs.GetInt("EnvironmentIn");

        Player_Money = PlayerPrefs.GetInt("Player_Money");
        if(Player_Money <= 0)
        {
            Player_Money = 500;
        }
        DeviceUniqueID = SystemInfo.deviceUniqueIdentifier;

        cumulativeScore = PlayerPrefs.GetInt("Cumulative_Score");


        //Badges
        arcademodeAttempts = PlayerPrefs.GetInt("B_Arcademode_Attempts");
        numberOfStagesAllCorrect = PlayerPrefs.GetInt("B_StagesAllCorrect");
        correctsInARow = PlayerPrefs.GetInt("B_CorrectInARow");
        arcadeScore = PlayerPrefs.GetInt("B_ArcadeScore");
        speedrunTimer = PlayerPrefs.GetInt("B_SpeedrunTimer");
        totalPlaytime = PlayerPrefs.GetFloat("B_TotalPlaytime");
        percentOfItemsUnlocked = PlayerPrefs.GetFloat("B_ItemsPercent");
    }

    public void Delete()
    {
        Debug.Log("delete");
        PlayerName_Player = "Default Name";

        SettingsBGM_Player = 0;
        SettingsSE_Player = 0;
        TotalPlayTime_Player = 0.0f;

        SettingsOrientation_Player = 0;

        EnvironmentIn = 0;

        Player_Money = 0;
        cumulativeScore = 0;
        

        //Badges
        arcademodeAttempts = 0;
        numberOfStagesAllCorrect = 0;
        correctsInARow = 0;
        arcadeScore = 0;
        speedrunTimer = -1;
        totalPlaytime = 0;
        percentOfItemsUnlocked = 0;
    }
}
