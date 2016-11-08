using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour {

    public AudioClip sound;

    private Button button { get { return GetComponent<Button>();  } }

    private AudioSource source { get { return GetComponent<AudioSource>();  } }

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        source.volume = 1;
        button.onClick.AddListener(() => PlaySound());
	}
	
	void PlaySound()
    {
        if(GameControl.handle.player.SettingsSE_Player == 0)
            source.PlayOneShot(sound);
    }

}
