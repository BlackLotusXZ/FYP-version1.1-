using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChosenBorder : MonoBehaviour {

    public RectTransform HeightBorder;
    public RectTransform Width;
    public Transform[] SnapPosition;
    public PageController pagecontroller;    
    
    private Vector2 size;
    private RectTransform own;

	// Use this for initialization
	void Start()
    {
        size.x = Width.sizeDelta.x;
        size.y = HeightBorder.sizeDelta.y;

        own = this.GetComponent<RectTransform>();
        own.sizeDelta = size;
    }

    // when the window size has changed .. rmb to change the border too
    void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            size.x = Width.sizeDelta.x;
            size.y = HeightBorder.sizeDelta.y;

            own = this.GetComponent<RectTransform>();
            own.sizeDelta = size;

            own.position = SnapPosition[pagecontroller.getCurrentPage()].position;
        }
        else
        {
            size.x = Width.sizeDelta.x;
            size.y = HeightBorder.sizeDelta.y;

            own.sizeDelta = size;
            own.position = Vector3.Lerp(own.position, SnapPosition[pagecontroller.getCurrentPage()].position, Time.deltaTime * pagecontroller.PageMovingSpeed);
        }
    }

}
