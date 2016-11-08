using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour {

    public static Selection handle;

    void Start()
    {
        handle = this;
    }

    public void PressMode(int mode)
    {
        GameControl.handle.currentMode = mode;
    }

    public void PressCategory(int category)
    {
        GameControl.handle.currentCategory = category;
    }

    public void PressStage(int stage)
    {
        GameControl.handle.currentStageNo = stage;
    }

}
