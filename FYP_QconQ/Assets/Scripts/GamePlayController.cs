using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


/********************************************/
/**
*  \brief
*   Contains main gameplay logic and functions
***********************************************/
public class GamePlayController : MonoBehaviour 
{
    //Window Visuals
    public GameObject DisplayView;

    // game screen related
    public CanvasGroup[] AllImptPanels;
    //public CarControl carController;
    public CameraControl cameraController;

    // game play related
    public TimerController timerController;
    public QualityController qualityController;
    public ProgressBarController progressBarController;

    // need optimised in the future
    // from here --
    public tire_medalMat tireCreditController;
    public Text scoreText;
    public Text encouragingMessageText;
    public Text moneyGainedText;
    public MainGUIPanelColorLerp MainGUIcolor; // will change color when correct
    // till here --

    

    //private int ChosenAnswer = 10;
    //private bool chosen = false;

    private bool gameEnd = false; // rmb this is not stand for the whole game
    private int progressCurrent = 0; // current question attempt
    private int progressLimit = 10; // only up to 10 qns 
    private float score = 0.0f;
    const int BASE_SCORE_INCREMENT = 1;
    private int questionsRight = 0;
    private int winstreak = 0;
    private int losestreak = 0;
    private int tempwinstreak = 0;
    private int templosestreak = 0;
    private int ArcadeCoinCount = 0;

    public Arcade arcadeHandler;

    public ServerInGameController ServerController;
    private int temp_correctsInARow = 0;
    private int actualCorrectsInARow = 0;

    public QuestionAndAnswerManager QAndAManager;

    private string[] encouragingMessagesPresets = new string[] { "Try harder next time!", "You're getting there!", "You did great!" };

    private float temp_speedrunClocker = 0;

    private float efficiencyPercent = 0;

    enum GAME_PANELS
    {
        GAMEPLAY = 0,
        INSTRUCTION = 1,
        COUNTDOWN = 2,
        ENDRESULT = 3,
        END
    }

	// Use this for initialization
	void Start () 
    {
        for (int i = 0; i < AllImptPanels.Length; ++i)
        {
            AllImptPanels[i].gameObject.SetActive(false);
        }

        // by default instruction panel need to be active first
        AllImptPanels[(int)GAME_PANELS.INSTRUCTION].gameObject.SetActive(true);

        cameraController.ZoomIn(); // zoom in and look at the car first

        // these two just run first
        qualityController.StartQualityCount();
        progressBarController.StartProgress();

        ArcadeCoinCount = 0;

        QAndAManager.InitVariables();

        //defaultButtonColour = answerButtonsController.buttons[0].image.color;
	}
	
    public void CountDownPanelAppear()
    {
        AllImptPanels[(int)GAME_PANELS.INSTRUCTION].gameObject.SetActive(false);
        AllImptPanels[(int)GAME_PANELS.COUNTDOWN].gameObject.SetActive(true);

    }

	public void GamplayPanelAppear()
    {
        // instruction panel set to inactive
        AllImptPanels[(int)GAME_PANELS.COUNTDOWN].gameObject.SetActive(false);
        AllImptPanels[(int)GAME_PANELS.GAMEPLAY].gameObject.SetActive(true);

        //carController.StartCar(); // start the car 
        GamePlayStart();
    }


    public void GamePlayStart()
    {
        DisplayView.GetComponent<ViewControl>().CarHolder.GetComponent<CarControl>().StartCar();
        StartCoroutine(StartGameplay());
    }

    IEnumerator StartGameplay()
    {
        cameraController.ZoomOut(); // zoom out when player is trying to answer the quiz

        gameEnd = false;

        // rmb to get the timer running 
        timerController.StartTimer();

        QAndAManager.InitialiseQuestions();

        // if it is not time end .. then can continue playing
        // wait until timer is ended or answer is chosen
        while (timerController.isTimeEnd() == false && QAndAManager.chosen == false)
        {
            yield return null;
        }

        QAndAManager.StartQuestionProcessing();
        
        // need to do something here .. if is correct or wrong 
        // from here ..

        if (QAndAManager.isCorrectAnswer == true)
        {
    
            winstreak += 1;
            if (losestreak > templosestreak)
            {
                templosestreak = losestreak;         
            }
            losestreak = 0;
            float currentQuality = qualityController.getCurrentQuality();

            if (timerController.getCurrentTime() < 5)
            {
                questionsRight++;
                score += BASE_SCORE_INCREMENT * 4;
                if (GameControl.handle.currentMode == 1)
                    ArcadeCoinCount += 1 * 4;
            }
            else if (timerController.getCurrentTime() < 10 && timerController.getCurrentTime() > 5)
            {
                questionsRight++;
                score += BASE_SCORE_INCREMENT * 3;
                if (GameControl.handle.currentMode == 1)
                    ArcadeCoinCount += 1 * 3;
            }
            else if (timerController.getCurrentTime() < 15 && timerController.getCurrentTime() > 10)
            {
                questionsRight++;
                score += BASE_SCORE_INCREMENT * 2;
                if (GameControl.handle.currentMode == 1)
                    ArcadeCoinCount += 1 * 2;
            }
            else
            {
                questionsRight++;
                score += BASE_SCORE_INCREMENT;
                if (GameControl.handle.currentMode == 1)
                    ArcadeCoinCount += 1;
            }


            //Badge: corrects in a row
            temp_correctsInARow++;
            //Badge: speedrun
            temp_speedrunClocker += timerController.getCurrentTime();

            yield return new WaitForSeconds(1.0f);
            DisplayView.GetComponent<ViewControl>().CarHolder.GetComponent<CarControl>().RepairParts();

            yield return new WaitForSeconds(0.25f);

            qualityController.setNextQuality(currentQuality + 5.0f);

            MainGUIcolor.setColorTOLerp(new Color(0.0f, 5.0f, 0));

            QAndAManager.setColourChange();
            Debug.Log("winstreak" + winstreak);
 
        }
        else
        {
            losestreak += 1;
            if (winstreak > tempwinstreak)
            {
                tempwinstreak = winstreak;
            }
            winstreak = 0;
            Debug.Log("losestreak" + losestreak);
                //Badge: corrects in a row
            if (temp_correctsInARow > actualCorrectsInARow)
                actualCorrectsInARow = temp_correctsInARow;
            temp_correctsInARow = 0;

            //Badge: speedrun
            temp_speedrunClocker += timerController.getCurrentTime();

            float currentQuality = qualityController.getCurrentQuality();

            yield return new WaitForSeconds(1.0f);

            DisplayView.GetComponent<ViewControl>().CarHolder.GetComponent<CarControl>().DestroyParts();

            yield return new WaitForSeconds(0.25f);

            qualityController.setNextQuality(currentQuality - 5.0f);

            MainGUIcolor.setColorTOLerp(new Color(2.0f, 0, 0));

            QAndAManager.setColourChange();
            
        }

        // till here ..
        

        
        // reset everything to normal;
        QAndAManager.ChosenAnswer = 10;
        QAndAManager.chosen = false;

        yield return new WaitForSeconds(1.0f);

        // game end
        progressCurrent++; // increase the progress
        progressBarController.setNextProgress(((float)(progressCurrent)) / 10.0f);
        MainGUIcolor.setColorTOLerp(new Color(1.0f, 1.0f, 1.0f));
        QAndAManager.setOriginalColour();
        
        gameEnd = true;

        yield return null;

        yield return null;

    }

    public void setChosenAnswer(int i)
    {
        QAndAManager.setChosenAnswer(i);
        timerController.setTimeEnd(true);
        cameraController.ZoomIn();
    }


    public void OpenResultTab()
    {
        for (int i = 0; i < AllImptPanels.Length; ++i)
        {
            AllImptPanels[i].gameObject.SetActive(false);
        }

        // by default instruction panel need to be active first
        AllImptPanels[(int)GAME_PANELS.ENDRESULT].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd == true)
        {
    
            // prepare for the next round
            timerController.LerpBackTimer();

            // once ready .. start the next round 
            if (timerController.isTimeEnd() == false)
            {
                // for arcade mode 
                if(GameControl.handle.currentMode == 1)
                {
                    if (qualityController.getCurrentQuality() <= 0.0f)
                    {
                        // end game
                        // game really end
                        OpenResultTab();

                        StopAllCoroutines();
                        DisplayView.GetComponent<ViewControl>().CarHolder.GetComponent<CarControl>().StopCar();

                        //Process scores and UI text to display
                        EndScreenProcessing(1);
                        //Check what to display as the encouraging message
                        EncouragingMessages(1);
                        //Update the badges related to arcade
                        BadgeProcessing(1);

                        //Start up server post
                        ServerController.ServerPostStyle = 1;
                        if (!ServerController.shouldLoad)
                            ServerController.shouldLoad = true;

                        gameObject.SetActive(false); // set it to false 
                    }
                    if (progressCurrent >= progressLimit)
                    {
                        // reset it bitch
                        progressCurrent = 0;
                        progressBarController.setNextProgress(0.0f);
                    }

                    QAndAManager.answerButtonsController.AllButtonsResetInteractable();
                    GamePlayStart();
                }

                else
                {
                    // if the game is ended .. prepare for the next round
                    if (progressCurrent >= progressLimit)
                    {
                        // need to fill up before end the whole game
                        if (progressBarController.getCurrentProgress() >= 0.99999f)
                        {
                            // game really end
                            OpenResultTab();
                            StopAllCoroutines();

                            DisplayView.GetComponent<ViewControl>().CarHolder.GetComponent<CarControl>().StopCar();

                            //Process scores and UI text to display
                            EndScreenProcessing(0);
                            //Check what to display as the encouraging message
                            EncouragingMessages(0);
                            //Update the badges related to story
                            BadgeProcessing(0);

                            //Start up server post
                            ServerController.ServerPostStyle = 0;
                            if (!ServerController.shouldLoad)
                                ServerController.shouldLoad = true;

                            //Every time you start the Gameplay scene creates a GamePlayController, so we deactivate it here so that we dont spam create if the player restarts the level.
                            gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        QAndAManager.answerButtonsController.AllButtonsResetInteractable();
                        GamePlayStart();
                    }
                }

            }
        }
    }

    public void SetCumulativeScore(int score)
    {
        GameControl.handle.player.cumulativeScore += score;
    }

    public void SetArcadeAttempts()
    {
        GameControl.handle.player.arcademodeAttempts++;
    }
    
    public void EncouragingMessages(int mode)
    {
        switch (mode)
        {
            //Story
            case 0:
                if (questionsRight <= 3)
                    encouragingMessageText.text = encouragingMessagesPresets[0];
                else if (questionsRight <= 6 && questionsRight > 3)
                    encouragingMessageText.text = encouragingMessagesPresets[1];
                else
                    encouragingMessageText.text = encouragingMessagesPresets[2];
                break;

            //Arcade
            case 1:
                if (questionsRight <= 10)
                    encouragingMessageText.text = encouragingMessagesPresets[0];
                else if (questionsRight > 10 && questionsRight <= 15)
                    encouragingMessageText.text = encouragingMessagesPresets[1];
                else
                    encouragingMessageText.text = encouragingMessagesPresets[2];
                break;
        }
    }

    public void EndScreenProcessing(int mode)
    {
        switch (mode)
        {
            //STORY
            case 0:
                //Earnings
                     winstreak = tempwinstreak;
                 losestreak = templosestreak;
                GameControl.handle.getMode().Categories[GameControl.handle.currentCategory].setStage(score, questionsRight, winstreak,losestreak);
                moneyGainedText.text = "Earnings: " + score.ToString() + " Conti Coin(s)!";

                //Score and % of corrects
                efficiencyPercent = questionsRight * 10.0f;
                scoreText.text = "Score: " + score.ToString("F0") + "\n(" + efficiencyPercent.ToString("F0") + "% correct)";
                break;

            //ARCADE
            case 1:
                //Earnings
                winstreak = tempwinstreak;
                losestreak = templosestreak;
                GameControl.handle.getMode().m_Arcade.setStage(score, ArcadeCoinCount, winstreak, losestreak);
                moneyGainedText.text = "Earnings: " + ArcadeCoinCount.ToString() + " Conti Coin(s)!";

                //Score and no. of corrects
                efficiencyPercent = questionsRight * 10.0f;
                scoreText.text = "Score: " + score.ToString("F0") + "\n(" + questionsRight.ToString("F0") + " correct)";
                break;
        }

        //Final tire display material (i.e if the medal is bronze/silver/gold)
        if (efficiencyPercent >= 80.0f)
            tireCreditController.changeMaterials(2);
        else if (efficiencyPercent >= 49.0f)
            tireCreditController.changeMaterials(1);
        else
            tireCreditController.changeMaterials(0);
    }

    public void BadgeProcessing(int mode)
    {
        switch (mode)
        {
            //STORY
            case 0:
                //Badge: total score for story
                SetCumulativeScore((int)score);

                //Badge: All correct for a stage
                if (questionsRight == progressLimit)
                    GameControl.handle.player.numberOfStagesAllCorrect = GameControl.handle.player.numberOfStagesAllCorrect + 1;

                //Badge: Corrects in a row
                if (GameControl.handle.player.correctsInARow < actualCorrectsInARow)
                    GameControl.handle.player.correctsInARow = actualCorrectsInARow;
                    

                //Badge: Speedrun
                if (efficiencyPercent > 70.0f && temp_speedrunClocker > GameControl.handle.player.speedrunTimer)
                    GameControl.handle.player.speedrunTimer = (int)temp_speedrunClocker;

                break;

            //ARCADE
            case 1:
                //Badge: arcade attempts
                SetArcadeAttempts();
            
                //Badge: arcade score
                if (score > GameControl.handle.player.arcadeScore)
                    GameControl.handle.player.arcadeScore = (int)score;
                

                break;
        }
    }
}
