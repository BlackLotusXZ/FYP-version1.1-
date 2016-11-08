using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PageController : MonoBehaviour
{
    // Public variables
    public RectTransform panel; // to hold the scroll panel (ScrollView)
    public RectTransform panel2; // to hold the scroll panel (ScrollView)
    public Button[] pages; // all the pages .. all of them are button now
    public RectTransform center; // center to compare the distance for each button
    public RectTransform center2; // center to compare the distance for each button
    public MouseController inputDragDirection; // info about dragging direction
    public float PageMovingSpeed = 5.0f;
    public float PageReactDragForce;// = 45.0f;
    //swap images for the navigation bar
    public Sprite HomeBlack;
    public Sprite HomeOrange;
    public Sprite ShopBlack;
    public Sprite ShopOrange;
    public Sprite SocialBlack;
    public Sprite SocialOrange;
    public Sprite StatisticsBlack;
    public Sprite StatisticsOrange;
    public GameObject HomePT;
    public GameObject ShopPT;
    public GameObject SocialPT;
    public GameObject StatisticsPT;
    public GameObject HomeLS;
    public GameObject ShopLS;
    public GameObject SocialLS;
    public GameObject StatisticsLS;
    // Private Variables
    private float[] distance; // all buttons distance to the center
    private bool dragging = false; // will be true when we drag the panel
    private int bttnDistance; // will hold the distance between the buttons
    private int CurrentPage = 0; // 0 -> MainMenu.. 1 -> Shop.. 2 -> Social.. 3 -> Stats
    public OrientationControl controller;

    public int getCurrentPage()
    {
        return CurrentPage;
    }

    public void setCurrentPage(int i)
    {
        this.CurrentPage = i;
        movenorder(i);
      
    }
    void movenorder( int i)
    {
        if (controller.isLandScape == true)
        {
            switch (i)
            {

                case 0:
                    HomeLS.GetComponent<Image>().sprite = HomeBlack;
                    ShopLS.GetComponent<Image>().sprite = ShopOrange;
                    SocialLS.GetComponent<Image>().sprite = SocialOrange;
                    StatisticsLS.GetComponent<Image>().sprite = StatisticsOrange;
                    break;
                case 1:
                    HomeLS.GetComponent<Image>().sprite = HomeOrange;
                    ShopLS.GetComponent<Image>().sprite = ShopBlack;
                    SocialLS.GetComponent<Image>().sprite = SocialOrange;
                    StatisticsLS.GetComponent<Image>().sprite = StatisticsOrange;
                    break;
                case 2:
                    HomeLS.GetComponent<Image>().sprite = HomeOrange;
                    ShopLS.GetComponent<Image>().sprite = ShopOrange;
                    SocialLS.GetComponent<Image>().sprite = SocialBlack;
                    StatisticsLS.GetComponent<Image>().sprite = StatisticsOrange;
                    break;
                case 3:
                    HomeLS.GetComponent<Image>().sprite = HomeOrange;
                    ShopLS.GetComponent<Image>().sprite = ShopOrange;
                    SocialLS.GetComponent<Image>().sprite = SocialOrange;
                    StatisticsLS.GetComponent<Image>().sprite = StatisticsBlack;
                    break;
            }
        }
        else
        {
            switch (i)
            {

                case 0:
                    HomePT.GetComponent<Image>().sprite = HomeBlack;
                    ShopPT.GetComponent<Image>().sprite = ShopOrange;
                    SocialPT.GetComponent<Image>().sprite = SocialOrange;
                    StatisticsPT.GetComponent<Image>().sprite = StatisticsOrange;
                    break;
                case 1:
                    HomePT.GetComponent<Image>().sprite = HomeOrange;
                    ShopPT.GetComponent<Image>().sprite = ShopBlack;
                    SocialPT.GetComponent<Image>().sprite = SocialOrange;
                    StatisticsPT.GetComponent<Image>().sprite = StatisticsOrange;
                    break;
                case 2:
                    HomePT.GetComponent<Image>().sprite = HomeOrange;
                    ShopPT.GetComponent<Image>().sprite = ShopOrange;
                    SocialPT.GetComponent<Image>().sprite = SocialBlack;
                    StatisticsPT.GetComponent<Image>().sprite = StatisticsOrange;
                    break;
                case 3:
                    HomePT.GetComponent<Image>().sprite = HomeOrange;
                    ShopPT.GetComponent<Image>().sprite = ShopOrange;
                    SocialPT.GetComponent<Image>().sprite = SocialOrange;
                    StatisticsPT.GetComponent<Image>().sprite = StatisticsBlack;
                    break;
            }

        }
    }
    void Start()
    {
        int bttnLength = pages.Length / 2;
        distance = new float[bttnLength];

        // Get distance between buttons
        bttnDistance = (int)Mathf.Abs(pages[1].GetComponent<RectTransform>().anchoredPosition.x -
            pages[0].GetComponent<RectTransform>().anchoredPosition.x);


    }

    void Update()
    {
        if (controller.isLandScape == false)
        {
            // if we are not dragging
            if (dragging)
            {
                for (int i = 0; i < 4; ++i)
                {
                    distance[i] = (int)Mathf.Abs(center.transform.position.x - pages[i].transform.position.x);
                }

                float minDistance = Mathf.Min(distance); // this will give me the min distance of the whole list

                for (int a = 0; a < 4; a++)
                {
                    // i need to find out who is the closest to the center
                    if (minDistance == distance[a])
                    {
                        CurrentPage = a;
                    }
                }
            }
            else
            {
                if (inputDragDirection.getHorizontalForce() > PageReactDragForce)
                {
                    if (CurrentPage < 4 - 1)
                        CurrentPage++;
                }
                else if (inputDragDirection.getHorizontalForce() < -PageReactDragForce)
                {
                    if (CurrentPage > 0)
                        CurrentPage--;
                }

                LerpToBttn(CurrentPage * -bttnDistance);
              //  movenorder(CurrentPage);
                inputDragDirection.setMouseDelta(Vector3.zero);
                inputDragDirection.setHorizontalForce(0.0f);
            }
        }
        else
        {
            if (dragging)
            {
                for (int i = 4; i < 8; ++i)
                {
                    distance[i] = (int)Mathf.Abs(center2.transform.position.x - pages[i].transform.position.x);
                }

                float minDistance = Mathf.Min(distance); // this will give me the min distance of the whole list

                for (int a = 4; a < 8; a++)
                {
                    // i need to find out who is the closest to the center
                    if (minDistance == distance[a])
                    {
                        CurrentPage = a;
                    }
                }
            }
            else
            {
                if (inputDragDirection.getHorizontalForce() > PageReactDragForce)
                {
                    if (CurrentPage < 8 - 1)
                        CurrentPage++;
                }
                else if (inputDragDirection.getHorizontalForce() < -PageReactDragForce)
                {
                    if (CurrentPage > 4)
                        CurrentPage--;
                }

                LerpToBttn(CurrentPage * -bttnDistance);
             //   movenorder(CurrentPage);
                inputDragDirection.setMouseDelta(Vector3.zero);
                inputDragDirection.setHorizontalForce(0.0f);
            }
        }
    }

    void LerpToBttn(int position)
    {
        if (controller.isLandScape == false)
        {
            float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10.0f);
            Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
            panel.anchoredPosition = newPosition;
        }
        else
        {
            float newX = Mathf.Lerp(panel2.anchoredPosition.x, position, Time.deltaTime * 10.0f);
            Vector2 newPosition = new Vector2(newX, panel2.anchoredPosition.y);

            panel2.anchoredPosition = newPosition;
        }
    }

    public void StartDrag()
    {
      //  dragging = true;
    }

    public void EndDrag()
    {
       // dragging = false;
      //  inputDragDirection.CheckDragHorizontal(); // after drag end liao .. check vertical or horizontal (no diagonal pls)
    }

}
