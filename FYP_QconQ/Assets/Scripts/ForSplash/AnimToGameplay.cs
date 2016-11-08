using UnityEngine;
using System.Collections;

public class AnimToGameplay : MonoBehaviour
{
    public RectTransform TopPanel;
    public RectTransform BottomPanel;
    public CanvasGroup WholeScene;
    public float TransitionSpeed = 2.0f;
    
    public void startToGoGameplay()
    {
        StartCoroutine(TwoPanelsMove());
        StartCoroutine(WholeSceneFade());
    }

    IEnumerator TwoPanelsMove()
    {
        while(true)
        {
            Vector3 up = new Vector3(0, Time.deltaTime * TransitionSpeed, 0);
            TopPanel.position       += up;
            BottomPanel.position    -= up;

            yield return null;
        }
    }

    IEnumerator WholeSceneFade()
    {
        WholeScene.blocksRaycasts = false;
        yield return new WaitForSeconds(0.2f);

        while (WholeScene.alpha > 0.0f)
        {
            WholeScene.alpha = Mathf.Lerp(WholeScene.alpha, -1.0f, Time.deltaTime * 3.0f);

            yield return null;
        }

        yield return new WaitForSeconds(0.25f);

        GameManager.instance.GotoScene("GamePlay");
    }
}
