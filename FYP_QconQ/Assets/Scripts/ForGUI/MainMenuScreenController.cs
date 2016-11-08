using UnityEngine;
using System.Collections;

public class MainMenuScreenController : MonoBehaviour
{
    // Public Variables
    public GameObject PreGamePlayUI; 
    public CanvasGroup MainDisplay_CG; // this is the main menu page
    public RectTransform MainBorder; // this the border of game mode choosing UI
    public CanvasGroup[] Panels; // 0: GameModeChoose, 1: ArcadeModeChoose, 2: CategoryChoose
    public RectTransform[] Buttons; // 0: Back button, 1: Exit button
    public RectTransform[] ButtPositionCompare; // 2 button position to lerp with for Exit button
    public PageController pages; // keep track of current pages that user currently in

    // Private Variables
    private enum State
    {
        GameModePanel = 0,
        ArcadeModePanel = 1,
        CategoryModePanel = 2,
        LevelChooseModePanel = 3,
        END
    }

    private int numswitchForButt = 0; // range is 0 to 1 only .. if current state is at GameModePanel, then == 0, if not then is == 1;
    private float ButtonSpeedSwitch = 10.0f; // lerping position speed 
    private float FadeSpeed = 3.0f; // panel fading speed
    private bool stopLoop = false; // coroutine stopping command 
    private State currentState = State.GameModePanel; // current state
    private State nextState = State.GameModePanel; // which state that the user want to lerp with

    void Awake()
    {
        PreGamePlayUI.SetActive(true);

        // Main display always come out first
        MainDisplay_CG.alpha = 1.0f;
        MainDisplay_CG.blocksRaycasts = true;
        MainDisplay_CG.interactable = true;


        // the pop up extra ui need to hide somewhere first
        CanvasGroup MainBorder_CG = MainBorder.GetComponent<CanvasGroup>();
        MainBorder_CG.alpha = 0.0f;
        MainBorder_CG.blocksRaycasts = false;
        MainBorder_CG.interactable = false;
        MainBorder_CG.transform.localScale = new Vector3(0, 0, 0);

        // always start with game mode choosing page in MainBorder
        currentState = State.GameModePanel;

        // all the state default setting
        for (int i = 0; i < (int)State.END; ++i)
        {
            // double make sure that the gameobject is active de
            Panels[i].gameObject.SetActive(true);

            // by default the gamemode is active
            if (i == (int)State.GameModePanel)
            {
                Panels[i].alpha = 1.0f;
                Panels[i].interactable = true;
                Panels[i].blocksRaycasts = true;
            }
            else
            {
                Panels[i].alpha = 0.0f;
                Panels[i].interactable = false;
                Panels[i].blocksRaycasts = false;
            }
        }

        StartCoroutine(load());
    }

    IEnumerator load()
    {
        MainBoderAppear();

        yield return new WaitForSeconds(0.5f);

        MainBorderDisappear();
    }


    // When user press the Play Game Button
    // The UI Main Border need to appear
    public void MainBoderAppear()
    {
        PreGamePlayUI.SetActive(true);

        // first the main menu disappear first
        MainDisplay_CG.alpha = 0.5f;
        MainDisplay_CG.blocksRaycasts = false;
        MainDisplay_CG.interactable = false;

        // just to make sure the currentstate is at choosing game mode page 
        nextState = State.GameModePanel;
        stopLoop = false; // prepare for the coroutine loop

        // coroutine go
        StartCoroutine(MainPanelGrowBig()); // the main panel will have animation for scaling
        StartCoroutine(CheckWhichPanelToAppear()); // this coroutine will constantly checking the current state and next state .. just to bring the correct page onto the screen
        StartCoroutine(BackEndCheckState()); // some checking and confirmation .. pls look at the function for more details
    }

    public void MainBorderDisappear()
    {
        // before it disappear .. make sure the state is at game mode again
        nextState = State.GameModePanel;

        // then ask this shit to fuck off
        StartCoroutine(MainPanelPopOut());
        
    }

    IEnumerator MainPanelGrowBig()
    {
        // this one will do the scaling animation .. scale big big de 

        CanvasGroup MainBorder_CG = MainBorder.GetComponent<CanvasGroup>();
        MainBorder_CG.alpha = 1.0f;

        while (MainBorder.transform.localScale.x < 1.0f)
        {
            MainBorder.transform.localScale = Vector3.Lerp(MainBorder.transform.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10.0f);
            yield return null;
        }

        MainBorder.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        MainBorder_CG.blocksRaycasts = true;
        MainBorder_CG.interactable = true;


        yield return null;


        StartCoroutine(ExitButtonPositionLerp());
    }

    IEnumerator MainPanelPopOut()
    {
        // wait for one frame to stop the looping of coroutines
        stopLoop = true;
        yield return null;

        // Disappear by scaling to zero
        MainBorder.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        CanvasGroup MainBorder_CG = MainBorder.GetComponent<CanvasGroup>();
        MainBorder_CG.alpha = 0.0f;
        MainBorder_CG.blocksRaycasts = false;
        MainBorder_CG.interactable = false;

        yield return null;

        MainDisplay_CG.blocksRaycasts = true;
        MainDisplay_CG.interactable = true;

        while (MainDisplay_CG.alpha < 1.0f)
        {
            MainDisplay_CG.alpha = Mathf.Lerp(MainDisplay_CG.alpha, 1.5f, Time.deltaTime * 2.0f);
            yield return null;
        }

        MainDisplay_CG.alpha = 1.0f;

        yield return null;

        PreGamePlayUI.SetActive(false); // set the whole panel to inactive
    }




    // Panels related
    IEnumerator CheckWhichPanelToAppear()
    {
        // this will constantly checking which panels to appear 
        while (currentState != State.END)
        {
            // if its not the same .. shit is getting real baby
            if (currentState != nextState)
            {
                // Firstly .. we will make sure the current page slowly fade out first
                while (Panels[(int)currentState].alpha > 0.0f)
                {
                    Panels[(int)currentState].alpha = Mathf.Lerp(Panels[(int)currentState].alpha, -1.0f,
                        Time.deltaTime * FadeSpeed);

                    yield return null;
                }

                Panels[(int)currentState].alpha = 0.0f;
                Panels[(int)currentState].interactable = false;
                Panels[(int)currentState].blocksRaycasts = false;

                // after fading out .. we need to make sure the currentstate is == next state
                currentState = nextState;
                yield return null;


                // Lastly .. make sure the next state is slowly fade to the screen
                while (Panels[(int)nextState].alpha < 1.0f)
                {
                    Panels[(int)nextState].alpha = Mathf.Lerp(Panels[(int)nextState].alpha, 2.0f,
                        Time.deltaTime * FadeSpeed);

                    yield return null;
                }

                Panels[(int)nextState].alpha = 1.0f;
                Panels[(int)nextState].interactable = true;
                Panels[(int)nextState].blocksRaycasts = true;

                // end of loop for this frame
                yield return null;

            }

            yield return null;
        }

    }




    // Buttons related
    // 0 == Back button, 1 == Exit Button <--> RMB THIS BITCH .. Dun say i never warn you
    
    IEnumerator ExitButtonPositionLerp()
    {
        // Exit button Lerping constantly
        while (!stopLoop)
        {
            Buttons[1].position = Vector3.Lerp(Buttons[1].position, ButtPositionCompare[numswitchForButt].position, Time.deltaTime * ButtonSpeedSwitch);
            yield return null;
        }

        Buttons[1].position = ButtPositionCompare[0].position;
        yield return null;
    }


    IEnumerator BackEndCheckState()
    {
        CanvasGroup BackButtons_CG = Buttons[0].GetComponent<CanvasGroup>();
        Vector3 oneone = new Vector3(1.0f, 1.0f, 1.0f);

        while (!stopLoop)
        {
            if (currentState != State.GameModePanel)
            {
                // the backbutton must be active 
                BackButtons_CG.gameObject.SetActive(true);
                BackButtons_CG.blocksRaycasts = true;
                BackButtons_CG.interactable = true;
                Buttons[0].localScale = oneone;

                numswitchForButt = 1;
            }
            else
            {
                BackButtons_CG.gameObject.SetActive(false);
                BackButtons_CG.blocksRaycasts = false;
                BackButtons_CG.interactable = false;
                BackButtons_CG.alpha = 1.0f;
                Buttons[0].localScale = oneone;

                numswitchForButt = 0;
            }

            yield return null;
        }
    }

    // for button clicking functions 

    public void setMode(int set)
    {
        nextState = (State)set;
    }

    public void StateBack()
    {
        switch(currentState)
        {
            case State.CategoryModePanel:
                nextState -= 2;
                break;
            case State.ArcadeModePanel:
                nextState -= 1;
                break;

            case State.GameModePanel:
                MainBorderDisappear();
                break;

            case State.LevelChooseModePanel:
                nextState -= 1;
                break;
        }
            
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { StateBack(); }
        if (pages.getCurrentPage() != 0) { MainBorderDisappear(); }
    }
}
