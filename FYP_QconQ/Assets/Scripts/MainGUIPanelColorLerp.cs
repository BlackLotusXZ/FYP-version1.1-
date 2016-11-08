using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainGUIPanelColorLerp : MonoBehaviour {

    private Color newColor = new Color(1.0f, 1.0f, 1.0f);
    private Image im;
	// Use this for initialization
	void Start () 
    {

        im = this.GetComponent<Image>();
        StartCoroutine(lerpColor());
	}
	
    IEnumerator lerpColor()
    {
        while(true)
        {
            im.color = Color.Lerp(im.color, newColor, Time.deltaTime * 5.0f);
            yield return null;
        }
    }

    public void setColorTOLerp(Color c)
    {
        newColor = c;
    }

    public Color getColor()
    {
        return newColor;
    }

}
