using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/********************************************/
/**
*  \brief
*   - This script handles the interactions with the answers in the game
***********************************************/
public class AnswerButtonsController : MonoBehaviour {

    public Button []buttons;

    public void OneButtonInteractable(int which)
    {
        buttons[which].interactable = true;

        for (int i = 0; i < 4; ++i)
        {
            if (i == which)
                continue;

            buttons[i].interactable = false;
        }
    }

    public void AllButtonsResetInteractable()
    {
        for (int i = 0; i < 4; ++i)
        {
            buttons[i].interactable = true;
        }
    }
}
