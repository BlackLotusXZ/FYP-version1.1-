using UnityEngine;
using System.Collections;

public class PartFallSound : MonoBehaviour
{

    public AudioClip SoundEffects;
    public float volume = 1.0f;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.volume = volume;
        source.clip = SoundEffects;
    }

    public void PlaySoundEffect()
    {
        if (GameControl.handle.player.SettingsSE_Player == 0)
        {
            source.PlayOneShot(SoundEffects);
        }
    }

}
