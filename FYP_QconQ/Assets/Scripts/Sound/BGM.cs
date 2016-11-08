using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour {

    public AudioClip sound;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.loop = true;
        source.volume = 1.2f;
    }

    void Update()
    {
        if (GameControl.handle.player.SettingsBGM_Player == 0 && source.isPlaying == false)
            source.Play();
        else if (GameControl.handle.player.SettingsBGM_Player == 1)
            source.Stop();
    }

}
