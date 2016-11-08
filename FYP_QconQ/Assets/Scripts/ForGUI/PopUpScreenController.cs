using UnityEngine;
using System.Collections;

public class PopUpScreenController : MonoBehaviour {

    public GameObject PopUpGameObject;
    public CanvasGroup MainDisplay_CG;// this is the main menu page
    public RectTransform MainBorder; // this the border of choosing UI
    public PageController pages; // keep track of current pages that user currently in
    public creditAppear creditpage;

    private bool stopLoop = false; // coroutine stopping command 

    public void Start()
    {
        // setting 

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

        PopUpGameObject.SetActive(false);
    }

    // When user press the Play Game Button
    // The UI Main Border need to appear
    public void MainBoderAppear()
    {
        PopUpGameObject.SetActive(true);

        // first the main menu disappear first
        MainDisplay_CG.alpha = 0.5f;
        MainDisplay_CG.blocksRaycasts = false;
        MainDisplay_CG.interactable = false;

        // just to make sure the currentstate is at choosing game mode page 
        stopLoop = false; // prepare for the coroutine loop

        // coroutine go
        StartCoroutine(MainPanelGrowBig()); // the main panel will have animation for scaling
        
    }

    public void MainBorderDisappear()
    {
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
    }

    IEnumerator MainPanelPopOut()
    {
        // wait for one frame to stop the looping of coroutines
        stopLoop = true;
        yield return null;

        creditpage.creditDissappearSS();
        creditpage.gameObject.SetActive(false);
        
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

        PopUpGameObject.SetActive(false); // set the whole panel to inactive
    }

    void Update()
    {
        if (pages.getCurrentPage() != 0 || Input.GetKeyDown(KeyCode.Escape)) { MainBorderDisappear(); }
    }
}
