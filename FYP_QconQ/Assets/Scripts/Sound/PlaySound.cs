using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

    public AudioClip[] SoundEffects;
    public AudioClip[] Musics;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.volume = 1;
    }

    public void PlaySoundEffect(int whichSound)
    {
        if (GameControl.handle.player.SettingsSE_Player == 0)
        {
            source.clip = SoundEffects[whichSound];
            source.PlayOneShot(SoundEffects[whichSound]);
        }
    }

    public void PlayMusic(int whichMusic)
    {
        if (GameControl.handle.player.SettingsBGM_Player == 0)
        {
            source.clip = Musics[whichMusic];
            source.PlayOneShot(Musics[whichMusic]);
        }
    }

}
