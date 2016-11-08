using UnityEngine;
using System.Collections;

public class DonDestroyThis : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        if(gameObject != null)
            DontDestroyOnLoad(gameObject);
	}

    public void NowDestroy()
    {
        Destroy(gameObject);
    }
}
