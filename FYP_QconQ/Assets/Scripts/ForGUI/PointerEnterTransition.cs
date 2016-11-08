using UnityEngine;
using System.Collections;

public class PointerEnterTransition : MonoBehaviour {

    public Transform Button;

    private Vector3 Original = new Vector3();
    private Vector3 NextLerp = new Vector3();

	// Use this for initialization
	void Start ()
    {
        Original = Button.localScale;
        NextLerp = Button.localScale - new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void LerpScale()
    {
        Button.localScale = Vector3.Lerp(NextLerp, Button.localScale, Time.deltaTime);
    }

    public void LerpBack()
    {
        Button.localScale = Vector3.Lerp(Original, Button.localScale, Time.deltaTime);
    }
}
