using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestionAndAnswerManager : MonoBehaviour {

    
    public Text QuestionPost;
    public Text[] AnswersText;

    private Color defaultButtonColor;

    public bool isCorrectAnswer;
    public bool chosen = false;
    public int ChosenAnswer = 10;
    int temp_chosenAnswer;
    int actualCorrectAnswer;

    public AnswerButtonsController answerButtonsController;
	// Use this for initialization
	void Start () {
        //for (int i = 0; i < answerButtonsController.buttons.Length; ++i)
        //    AnswersText[i] = answerButtonsController.buttons[i].GetComponentInChildren<Text>();
        
	}

    public void InitVariables()
    {
        defaultButtonColor = answerButtonsController.buttons[0].image.color;
    }

    public void InitialiseQuestions()
    {
        List<string> qtn = new List<string>();
        qtn = GameControl.handle.getMode().getQtn();

        // update visually for the client
        QuestionPost.text = qtn[0];
        for (int i = 0; i < AnswersText.Length; i++)
        {
            AnswersText[i].text = qtn[i + 1];
        }
    }

    public void StartQuestionProcessing()
    {
        temp_chosenAnswer = 0;
        actualCorrectAnswer = 0;

        isCorrectAnswer = false;

        CheckIfCorrect();
    }

    public void setColourChange()
    {
        if (isCorrectAnswer)
        {
            answerButtonsController.buttons[ChosenAnswer].image.color = new Color(0.0f, 5.0f, 0.0f);
            temp_chosenAnswer = ChosenAnswer;
        }
        else
        {
            if (ChosenAnswer < 4)
            {
                answerButtonsController.buttons[ChosenAnswer].image.color = new Color(2.0f, 0.0f, 0.0f);
                if (actualCorrectAnswer != -1)
                    answerButtonsController.buttons[actualCorrectAnswer].image.color = new Color(0.0f, 5.0f, 0.0f);
                temp_chosenAnswer = ChosenAnswer;
            }
            
        }
    }

    public void setOriginalColour()
    {
        answerButtonsController.buttons[temp_chosenAnswer].image.color = defaultButtonColor;
        if (actualCorrectAnswer != -1)
            answerButtonsController.buttons[actualCorrectAnswer].image.color = defaultButtonColor;
        temp_chosenAnswer = 0;
        actualCorrectAnswer = 0;
    }

    bool CheckIfCorrect()
    {
        //Check if answer is correct
        if (ChosenAnswer < 4) // if have choosen any answer
        {
            isCorrectAnswer = GameControl.handle.getMode().pressAns(ChosenAnswer);
            actualCorrectAnswer = CheckCorrectIfPickedWrong();
        }

        return isCorrectAnswer;
    }

    private int CheckCorrectIfPickedWrong()
    {
        for (int i = 0; i < answerButtonsController.buttons.Length; ++i)
        {
            if (GameControl.handle.getMode().pressAns(i))
                return i;
        }
        return -1;
    }

    public void setChosenAnswer(int i)
    {
        ChosenAnswer = i;
        chosen = true;
        answerButtonsController.OneButtonInteractable(i);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
