using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDownText : MonoBehaviour {

	// Use this for initialization
    public GamePlayController gamplayController;

	public void StartCountDown () 
    {
        StartCoroutine(StartToCountDown());
	}

    IEnumerator StartToCountDown()
    {
        Text text = GetComponent<Text>();
        text.text = "3";

        Vector3 bscale = new Vector3(1.5f, 1.5f, 1.5f);
        Color color = new Color();

        color = text.color;

        float speed = 0.75f;

        while (color.a > 0.0f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, bscale, Time.deltaTime * speed);
            color.a = Mathf.Lerp(color.a, -1.0f, Time.deltaTime * speed);
            text.color = color;

            yield return null;
        }

        // restart again 
        text.text = "2";
        color.a = 1.0f;
        text.color = color;
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        while (color.a > 0.0f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, bscale, Time.deltaTime * speed);
            color.a = Mathf.Lerp(color.a, -1.0f, Time.deltaTime * speed);
            text.color = color;

            yield return null;
        }

        // restart again 
        text.text = "1";
        color.a = 1.0f;
        text.color = color;
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        while (color.a > 0.0f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, bscale, Time.deltaTime * speed);
            color.a = Mathf.Lerp(color.a, -1.0f, Time.deltaTime * speed);
            text.color = color;

            yield return null;
        }

        // restart again 
        text.text = "GO !";
        color.a = 1.0f;
        text.color = color;
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        while (color.a > 0.0f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, bscale, Time.deltaTime * speed);
            color.a = Mathf.Lerp(color.a, -1.0f, Time.deltaTime * speed);
            text.color = color;

            yield return null;
        }

        yield return null;

        // start the gameplay
        gamplayController.GamplayPanelAppear();

        yield return null;
    }
}
