using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoFocus : MonoBehaviour {

    public GameObject ContentHolder;
    public float InertiaSpeed;

    public bool isPortrait;
    public bool isLandScape;

    private GameObject ObjectClosestToScrollRect;

    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        //If Screen Detects No Touches
        if(Input.touchCount == 0 && ContentHolder.transform.childCount != 0)
        {
            //Find The Nearest GameObject And Lerp It To Scroll View's Position
            LerpToScrollView(FindNearest(gameObject));
            FocusOnNearest();
        }
	}

    public GameObject getObjectClosestToScrollRect()
    {
        return ObjectClosestToScrollRect;
    }

    GameObject FindNearest(GameObject ScrollRect)
    { 
        //If The Content Holder Is Not Empty
        if (ContentHolder.transform.childCount > 0)
        {
            ObjectClosestToScrollRect = ContentHolder.transform.GetChild(0).gameObject;

            string ObjectDistances = "";
            //Loop Through ContentHolder
            foreach (Transform Child in ContentHolder.transform)
            {
                //Check For The Object With The Closest Distance Between Itself And The Scroll Rect
                if ((Child.position - transform.position).magnitude < (ObjectClosestToScrollRect.transform.position - transform.position).magnitude)
                {
                    ObjectClosestToScrollRect = Child.gameObject;
                }
                ObjectDistances += Child.name + ": " + (Child.position - transform.position).magnitude + ", ";
            }

            return ObjectClosestToScrollRect;
        }

        return null;
    }

    void LerpToScrollView(GameObject ObjectClosestToScrollRect)
    {
        if (isPortrait == true)
        {
            ContentHolder.transform.position = new Vector3(Mathf.Lerp(ContentHolder.transform.position.x, ContentHolder.transform.position.x + (transform.position.x - ObjectClosestToScrollRect.transform.position.x), Time.deltaTime * InertiaSpeed), transform.position.y, transform.position.z);
        }
        if (isLandScape == true) 
        {
            ContentHolder.transform.position = new Vector3(transform.position.x, Mathf.Lerp(ContentHolder.transform.position.y, ContentHolder.transform.position.y + (transform.position.y - ObjectClosestToScrollRect.transform.position.y), Time.deltaTime * InertiaSpeed), transform.position.z);
        }
      }

    void FocusOnNearest()
    {
        foreach (Transform Child in ContentHolder.transform)
        {
            if (Child.gameObject == ObjectClosestToScrollRect)
            {
                Color AlphaChange = new Color(Child.GetComponent<Image>().color.r, Child.GetComponent<Image>().color.g, Child.GetComponent<Image>().color.b, 1);
                Child.GetComponent<Image>().color = AlphaChange;
            }
            else 
            {
                Color AlphaChange = new Color(Child.GetComponent<Image>().color.r, Child.GetComponent<Image>().color.g, Child.GetComponent<Image>().color.b, 0.5f);
                Child.GetComponent<Image>().color = AlphaChange;
            }
        }
    }
}
