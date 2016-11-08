using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/********************************************/
/**
*  \brief
*   - CURRENTLY UNSUSED
***********************************************/
public class fpsPrint : MonoBehaviour {

    public Text fpss;

    float deltaTime = 0.0f;

	// Update is called once per frame
	void Update () {
	    deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

		float fps = 1.0f / deltaTime;

        fpss.text = fps.ToString("F2");

	}
}
