using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopNavigation : MonoBehaviour {

    //List Of ShopTabs In Game
    public GameObject[] ListOfTabs;

    //Scrolling Thingy In Shop
    public GameObject Selection;

    public GameObject Display;

    //That Badass 3D Window In Shop
    //public GameObject GamePreview;

    private enum Tab
    {

        Gender,
        Car,
        Environment,
        Props
    }

    private Tab CurrentTab = Tab.Gender;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void NavigateTo(int CurrentTab)
    {
        //See Which Tab U At Now
        switch ((Tab)CurrentTab)
        {
            case Tab.Gender:
                {
                    //Set Environment Tab Render In Front
                    ListOfTabs[0].GetComponent<RectTransform>().SetAsLastSibling();
                    break;
                }
            //Car Tab
            case Tab.Car:
                {
                    //Set Environment Tab Render In Front
                    ListOfTabs[1].GetComponent<RectTransform>().SetAsLastSibling();
                    break;
                }
            //Environment Tab
            case Tab.Environment:
                {
                    //Set Environment Tab Render In Front
                    ListOfTabs[2].GetComponent<RectTransform>().SetAsLastSibling();
                    break;
                }
            //Environment Tab
            case Tab.Props:
                {
                    //Set Environment Tab Render In Front
                    ListOfTabs[3].GetComponent<RectTransform>().SetAsLastSibling();
                    //Set Selection Box In Front In Front
                    //Selection.GetComponent<RectTransform>().SetAsLastSibling();
                    break;
                }
        }

        //Set Selection Box In Front In Front
        Selection.GetComponent<RectTransform>().SetAsLastSibling();

        //Set This Badass Window In The Most Bloody Front So Nobody Can Block 
        Display.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public int NavigatedTo()
    {
        return (int)CurrentTab;
    }

}
