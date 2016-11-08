using UnityEngine;
using System.Collections;

public class AndroidBackButtonCheck : MonoBehaviour {

    public LoadingScreenLoading load;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            load.StartLoadingScreenToMainMenu();
        
	}
}
