using UnityEngine;
using System.Collections;


/********************************************/
/**
*  \brief
*   -  This script sets the camera to do lookat functions
***********************************************/
[ExecuteInEditMode]
public class CameraLookAt : MonoBehaviour {

    public Transform objectToLookAt;

	// Use this for initialization
	void Start () 
    {
        transform.LookAt(objectToLookAt);
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.LookAt(objectToLookAt);
	}
}
