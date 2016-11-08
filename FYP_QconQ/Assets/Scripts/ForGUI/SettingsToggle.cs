using UnityEngine;
using System.Collections;

public class SettingsToggle : MonoBehaviour
{

    public GameObject[] Music;
    public GameObject[] SE;

    public int environment = 0;

    // Use this for initialization
    void Start()
    {
        ChangedOptions();
    }

    public void toggleMusic(int a)
    {
        GameControl.handle.player.SettingsBGM_Player = a;
        ChangedOptions();
    }

    public void toggleSE(int a)
    {
        GameControl.handle.player.SettingsSE_Player = a;
        ChangedOptions();
    }

    public void ChangedOptions()
    {
        for (int i = 0; i < Music.Length; i++)
        {
            if (i != GameControl.handle.player.SettingsBGM_Player)
                Music[i].SetActive(false);
            else
                Music[i].SetActive(true);
        }

        for (int i = 0; i < SE.Length; i++)
        {
            if (i != GameControl.handle.player.SettingsSE_Player)
                SE[i].SetActive(false);
            else
                SE[i].SetActive(true);
        }
    }

    public void DefaultSettings()
    {
        GameControl.handle.player.SettingsBGM_Player = 0;
        GameControl.handle.player.SettingsSE_Player = 0;
        ChangedOptions();
    }

}
