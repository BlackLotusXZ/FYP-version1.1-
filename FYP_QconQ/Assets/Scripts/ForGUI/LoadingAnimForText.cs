using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LoadingAnimForText : MonoBehaviour {

    public bool LoadFinish = false;
    
    public void LoadingTextAnim()
    {
        StartCoroutine(textChange());
    }

    IEnumerator textChange()
    {

        Text text = GetComponent<Text>();
        text.text = "Loading . . .";

        WaitForSeconds waitTime = new WaitForSeconds(0.5f);

        while (!LoadFinish)
        {
            text.text = "Loading ";

            yield return waitTime;

            text.text = "Loading .";

            yield return waitTime;

            text.text = "Loading . .";

            yield return waitTime;

            text.text = "Loading . . .";

            yield return waitTime;
        }

        text.text = "Loading . . .";

        yield return null;
    }
}
