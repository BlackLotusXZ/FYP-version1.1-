using UnityEngine;
using System.Collections;

public class AwakeLoadingPanel : MonoBehaviour {

    public GameObject LoadingPanel;

	void Awake()
    {
        LoadingPanel.SetActive(true);
    }
}
