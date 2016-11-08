using UnityEngine;
using System.Collections;

/********************************************/
/**
*  \brief
*  - CURRENTLY UNUSED
***********************************************/
public class UIPanelController : MonoBehaviour {

    public CanvasGroup []UIPanels;

    enum UINUM
    {
        UPPER = 0,
        LOWER,
        INSTRUCTION,
        END,
        TOTAL
    };

	// Use this for initialization
	void Start () 
    {
        for (int i = 0; i < 4; ++i)
            UIPanels[i].gameObject.SetActive(true);

        // instruction screen always appear first
        PanelsHelper((int)UINUM.UPPER, 0.2f, false, false);
        PanelsHelper((int)UINUM.LOWER, 0.2f, false, false);
        PanelsHelper((int)UINUM.END, 0.0f, false, false);
        PanelsHelper((int)UINUM.INSTRUCTION, 1.0f, true, true);
	}

    void PanelsHelper(int which, float v, bool interactable, bool blocksRaycasts)
    {
        UIPanels[which].alpha = v;
        UIPanels[which].interactable = interactable;
        UIPanels[which].blocksRaycasts = blocksRaycasts;
    }
	
    public void CloseInstructionTab()
    {
        // gameplay start here
        PanelsHelper((int)UINUM.UPPER, 1.0f, true, true);
        PanelsHelper((int)UINUM.LOWER, 1.0f, true, true);
        PanelsHelper((int)UINUM.END, 0.0f, false, false);
        PanelsHelper((int)UINUM.INSTRUCTION, 0.0f, false, false);
    }

    public void OpenResultTab()
    {
        // end gameplay and show result 
        PanelsHelper((int)UINUM.UPPER, 0.2f, false, false);
        PanelsHelper((int)UINUM.LOWER, 0.2f, false, false);
        PanelsHelper((int)UINUM.END, 1.0f, true, true);
        PanelsHelper((int)UINUM.INSTRUCTION, 0.0f, false, false);
    }
}
