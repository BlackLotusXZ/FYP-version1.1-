using UnityEngine;
using System.Collections;

public class CheckNewsFeed : MonoBehaviour {

    private float CentrePointX;
    private float CentrePointY;

    private GameObject NearestNewsFeed;
    public GameObject[] ListOfNewsFeed;

    public string NewsFeedContentHolderName;

    public float InertiaSpeed;

    public bool isPortrait;

	// Use this for initialization
	void Start () {
        CentrePointX = GameObject.Find(NewsFeedContentHolderName).transform.position.x;
        CentrePointY = GameObject.Find(NewsFeedContentHolderName).transform.position.y;
        NearestNewsFeed = ListOfNewsFeed[0];
	}

	// Update is called once per frame
	void Update () {
        if(Input.touchCount == 0)
        {
            LerpToNearestNewsFeed(FindNearest());
        }
      // Debug.Log(GameObject.Find(NewsFeedContentHolderName).transform.position.x);
	}

    GameObject FindNearest()
    {
        switch (isPortrait)
        {
            case true:
                {
                    for (int i = 0; i < ListOfNewsFeed.Length; ++i)
                    {
                        if (Mathf.Abs(CentrePointX - ListOfNewsFeed[i].transform.position.x) < Mathf.Abs(CentrePointX - NearestNewsFeed.transform.position.x))
                        {
                            NearestNewsFeed = ListOfNewsFeed[i];
                        }
                    }
                    break;
                }
            case false:
                {
                    for (int i = 0; i < ListOfNewsFeed.Length; ++i)
                    {
                        if (Mathf.Abs(CentrePointY - ListOfNewsFeed[i].transform.position.y) < Mathf.Abs(CentrePointY - NearestNewsFeed.transform.position.y))
                        {
                            NearestNewsFeed = ListOfNewsFeed[i];
                        }
                    }
                    break;
                }
        }

        return NearestNewsFeed;
    }

    void LerpToNearestNewsFeed(GameObject Content)
    {
        switch (isPortrait)
        {
            case true:
                {
                    float newX = Mathf.Lerp(this.transform.position.x, this.transform.position.x + (CentrePointX - Content.transform.position.x), Time.deltaTime * InertiaSpeed);
                    this.transform.position = new Vector3(newX, CentrePointY, transform.position.z);
                    break;
                }
            case false:
                {
                  
                    float newY = Mathf.Lerp(this.transform.position.y, this.transform.position.y + (CentrePointY - Content.transform.position.y), Time.deltaTime * InertiaSpeed);
                    this.transform.position = new Vector3(CentrePointX, newY, transform.position.z);

                    break;
                }
        }
      }
}
