using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using System.Collections.Generic;


/********************************************/
/**
*  \brief
*   - Triggers init functions for story/arcade mode
*   - Contains load/save game, deletesave, onDisable and onApplicationPause methods
***********************************************/
public class GameControl : MonoBehaviour {

    public static GameControl handle;

    // all the mode name is here 
    enum ModeName
    {
        Story = 0,
        Arcade = 1,
        END
    }

    public Mode[] Modes = new Mode[(int)ModeName.END]; // there are only 2 modes .. Arcade and Story

    public int currentMode = 0; // which mode that player is in
    public int currentCategory = 0; // which category that player is playing
    public int currentStageNo = 0; // which stage that the player is playing with in the category
    public TextAsset QtnData; // dunno what is this yet
    public csvReader Database = new csvReader();  // all the question data are here
    

    // player related info
    public PlayerInfo player = new PlayerInfo();

    public List<string> carparts = new List<string>();
    public List<string> environmentparts = new List<string>();

    void Awake() // Before start
    {
        // something like singleton but not rly singleton
        if (handle == null)
        {
            // do not destroy this gameObject .. throughout the whole game de
            DontDestroyOnLoad(gameObject);
            handle = this;

            // load all the questions from csv pls
            if (Database.IsLoaded() == false)
            {
                Database.Load(QtnData);
            }

            Modes[(int)ModeName.Story].StoryNameInit(); // story mode init
            Modes[(int)ModeName.Arcade].ArcadeInit(); // arcade mode init

            LoadGame(); // load the game data from system
        }

        else if (handle != this)
        {
            Destroy(gameObject); // prevent multiple type of this obj
        }
        
    }

    public Mode getMode()
    {
        return Modes[currentMode];
    }

    public float getCatPercent(int category)
    {
        return Modes[(int)ModeName.Story].getCatPercent(category);
    }

    void OnDisable()
    {
        SaveGame();
    }

    void OnApplicationPause()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            SaveGame();
        }
    }

    public void SaveGame()
    {
        // save all the data to player pref
        player.Save();

        for (int i = 0; i < 2; i++)
            Modes[i].Save();
    }

    public void LoadGame()
    {
        // load all the old data from system
        player.Load();

        for (int i = 0; i < 2; i++)
            Modes[i].Load();
    }

    public void DeleteSave()
    {
        Debug.Log("delete");
        player.Delete();

        for (int i = 0; i < 2; i++)
            Modes[i].Delete();
    }

    void Update()
    {
        player.totalPlaytime += Time.deltaTime;
        //Debug.Log(player.totalPlaytime);
    }

}