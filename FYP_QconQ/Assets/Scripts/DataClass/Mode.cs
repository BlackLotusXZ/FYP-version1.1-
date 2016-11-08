using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/********************************************/
/**
*  \brief
*   - This script handles the calling of the functions from category and arcade (this is basically the "main" function; this script HAS A category and HAS AN arcade)
***********************************************/
[System.Serializable]
public class Mode 
{
    public string Name = "NIL";
    public Category[] Categories = new Category[5];
    public Arcade m_Arcade = new Arcade();

    public void StoryNameInit()
    {
        Name = "StoryMode";

        Categories[0].InitName("PEOPLE & ORGANIZATION");
        Categories[1].InitName("PRODUCTION & DELIVERY");
        Categories[2].InitName("PRODUCT & PRODUCTION PROCESS DEVELOPMENT");
        Categories[3].InitName("MANAGEMENT & SUPPORT PROCESS");
        Categories[4].InitName("SUPPLIER MANAGEMENT");
    }

    public void ArcadeInit()
    {
        Name = "ArcadeMode";

        m_Arcade.Init();
    }

    public void Save()
    {
        if (Name == "StoryMode")
        {
            for (int i = 0; i < Categories.Length ; i++)
            {
                Categories[i].Save(Name);
            }
        }
        else if(Name == "ArcadeMode")
        {
            m_Arcade.Save(Name);
        }
    }

    public void Load()
    {
        if (Name == "StoryMode")
        {
            for (int i = 0; i < Categories.Length; i++)
            {
                Categories[i].Load(Name);
            }
        }
        else if(Name == "ArcadeMode")
        {
            m_Arcade.Load(Name);
        }
    }

    public List<string> getQtn()
    {
        if (Name == "StoryMode")
            return Categories[GameControl.handle.currentCategory].getQtn();
        else
            return m_Arcade.getQtn();
    }

    public bool pressAns(int ans)
    {
        if (Name == "StoryMode")
            return Categories[GameControl.handle.currentCategory].pressAns(ans);
        else
            return m_Arcade.pressAns(ans);
    }

    public void Delete()
    {
        Debug.Log("delete");
        if (Name == "StoryMode")
        {
            for (int i = 0; i < Categories.Length; i++)
            {
                Categories[i].Delete();
            }
        }
        else if (Name == "ArcadeMode")
        {
            m_Arcade.Delete();
        }
    }

    public float getCatPercent(int category)
    {
        return Categories[category].getPercent();
    }

}
