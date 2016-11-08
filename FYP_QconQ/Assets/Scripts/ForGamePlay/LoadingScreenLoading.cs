using UnityEngine;
using System.Collections;


/********************************************/
/**
*  \brief
*   - This script handles the fade-in/fade-out that happens during page switching.
***********************************************/
public class LoadingScreenLoading : MonoBehaviour
{
    public LoadingAnim loadingRing;
    public LoadingAnimForText loadingText;
    public GameObject GameplayScene; // for disabling when go back to main menu

    public float loadingTime = 3.0f;

    void Awake()
    {
        StartLoadingScreen();
    }

    public void StartLoadingScreen()
    {
        loadingRing.stopLoading = false;
        loadingText.LoadFinish = false;

        loadingRing.RotateRing();
        loadingText.LoadingTextAnim();
        StartCoroutine(FadeOut());
    }

    public void StartLoadingScreenToMainMenu()
    {
        loadingRing.stopLoading = false;
        loadingText.LoadFinish = false;

        loadingRing.RotateRing();
        loadingText.LoadingTextAnim();
        StartCoroutine(FadeOutToMainMenu());
    }

    public void StartLoadingScreenToGameplay()
    {
        loadingRing.stopLoading = false;
        loadingText.LoadFinish = false;

        loadingRing.RotateRing();
        loadingText.LoadingTextAnim();
        StartCoroutine(FadeOutToGameplay());
    }
	
    IEnumerator FadeOut()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 1.0f; // before fade out .. make sure got alpha pls

        yield return new WaitForSeconds(loadingTime);

        while (cg.alpha > 0.0f)
        {
            cg.alpha = Mathf.Lerp(cg.alpha, -1.0f, Time.deltaTime * 1.0f);

            yield return null;
        }

        loadingRing.stopLoading = true;
        loadingText.LoadFinish = true;

        yield return null;
    }

    IEnumerator FadeOutToMainMenu()
    {
        GameManager.instance.GotoScene("MainScene");

        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 1.0f; // before fade out .. make sure got alpha pls

        GameplayScene.SetActive(false);

        cg = this.transform.parent.GetComponent<CanvasGroup>();

        yield return new WaitForSeconds(1.25f);

        while (cg.alpha > 0.0f)
        {
            cg.alpha = Mathf.Lerp(cg.alpha, -1.0f, Time.deltaTime * 1.0f);

            yield return null;
        }

        loadingRing.stopLoading = true;
        loadingText.LoadFinish = true;

        yield return null;

        transform.parent.GetComponent<DonDestroyThis>().NowDestroy();
    }

    IEnumerator FadeOutToGameplay()
    {
        GameManager.instance.GotoScene("GamePlay");

        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 1.0f; // before fade out .. make sure got alpha pls

        GameplayScene.SetActive(false);

        cg = this.transform.parent.GetComponent<CanvasGroup>();

        yield return new WaitForSeconds(1.25f);

        while (cg.alpha > 0.0f)
        {
            cg.alpha = Mathf.Lerp(cg.alpha, -1.0f, Time.deltaTime * 1.0f);

            yield return null;
        }

        loadingRing.stopLoading = true;
        loadingText.LoadFinish = true;

        yield return null;

        transform.parent.GetComponent<DonDestroyThis>().NowDestroy();
    }
}
