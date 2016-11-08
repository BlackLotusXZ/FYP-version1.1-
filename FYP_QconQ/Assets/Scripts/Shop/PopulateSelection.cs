using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopulateSelection : MonoBehaviour
{
    public GameObject Content;
    public GameObject PrefabButton;
    public GameObject ShowCasePreview;
    public RawImage Display;
    public bool isClearPlayerPref;

    //Mainly for badges
    public float totalNoOfItems, totalUnlocked, percentOfItemsUnlocked;
    //public float percentOfItemsUnlocked;

    // Use this for initialization
    void Start()
    {
        if (isClearPlayerPref == true)
        {
            PlayerPrefs.DeleteAll();
        }
        else
        {
            //If first time dont load
            if (!PlayerPrefs.HasKey("FirstTime"))
            {
                Debug.Log("Delete All");
                PlayerPrefs.DeleteAll();
                SaveContent();
            }
            //else load
            else
            {
                Debug.Log("content loaded");
                LoadContent();
                //PlayerPrefs.DeleteAll();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        EvaluateItemNumbers();
    }
    
    public void ReloadContent(int type)
    {
        //Remove Whatever Crap There Is In The ContentHolder
        foreach (Transform Child in Content.transform)
        {
            Destroy(Child.gameObject);
        }

        foreach (Transform Category in ShowCasePreview.transform)
        {
            if (Category.GetComponent<ItemInfo>().type == (ItemInfo.ShowCase)type)
            {
                //Repopulate ContentHolder
                foreach (Transform Child in Category.transform)
                {
                    // clone the button and position properly
                    GameObject clone = Instantiate(PrefabButton) as GameObject;
                   
                    //Assign Clone With ShowCaseItemCategoryChild's Name
                    clone.GetComponent<ItemInfo>().name = (Child.transform.name);

                    //Assign Clone With ShowCaseItemCategoryChild's Sprite
                    clone.GetComponent<Image>().sprite = Child.GetComponent<ItemInfo>().Icon;

                    //Assign Clone's ItemInfo Sprite With Child's ItemInfo's Sprite
                    clone.GetComponent<ItemInfo>().Icon = Child.GetComponent<ItemInfo>().Icon;

                    //Assign Clone's ItemInfo type with Child's ItemInfo's type
                    clone.GetComponent<ItemInfo>().type = Child.GetComponent<ItemInfo>().type;

                    //Assign ContentHodler As Clone's Parent
                    clone.transform.SetParent(Content.transform);

                    //Set Scale To Uniform 1 to Ease Calculation
                    clone.transform.localScale = new Vector3(1, 1, 1);

                    //Set Rotate -90 if LandScape
                    if (GameObject.Find("OrientationController").GetComponent<OrientationControl>().isLandScape == true)
                    {
                        clone.transform.rotation = new Quaternion(0, 0, 0, 0);
                    }
                 
                    if (Child.GetComponent<ItemInfo>().Selected == true)
                    {
                        clone.GetComponent<Image>().color = new Color(clone.GetComponent<Image>().color.r, clone.GetComponent<Image>().color.g, clone.GetComponent<Image>().color.b, 0.5f);
                    }

                    //Activate Clone
                    clone.SetActive(true);

                    //Set Function Call When clone's OnClick Triggered
                    clone.GetComponent<Button>().onClick.AddListener(() => ItemTapped(clone));

                    clone.GetComponent<Button>().onClick.AddListener(() => Display.GetComponent<DisplayControl>().ExpandDisplay());
                    clone.GetComponent<Button>().onClick.AddListener(() => Display.GetComponent<DisplayControl>().FadeOut());
                }
            }
        }
      
    }

    void ItemTapped(GameObject Clone)
    {
        //Loop Through ShowCasePreview
        foreach (Transform Child in ShowCasePreview.transform)
        {
            //Search For The Item's Category
            if (Child.GetComponent<ItemInfo>().type == Clone.GetComponent<ItemInfo>().type)
            {
                //Loop Through The Item's Category
                foreach (Transform item in Child.transform)
                {
                    //If Item Is The One That We Are Looking For
                    if (item.name == Clone.GetComponent<ItemInfo>().name)
                    {
                        //This Is Where All The Work Is Being Done

                        //Set Item's Active To True
                        item.gameObject.SetActive(true);

                        //Set Display's Name To Item's Name
                        Display.GetComponent<DisplayControl>().DisplayItemInfo(item.tag, item.name, item.GetComponent<ItemInfo>().Price);
                        //If Item Is Not Locked
                        if (item.GetComponent<ItemInfo>().Locked == false)
                        {
                            Display.GetComponent<DisplayControl>().UnlockDisplay();
                            Display.GetComponent<DisplayControl>().EnableSelect();
                        }
                        //If Item Is Locked
                        else
                        {
                            Display.GetComponent<DisplayControl>().LockDisplay();
                            Display.GetComponent<DisplayControl>().DisableSelect();
                        }
                    }
                    else
                    {
                        //Set Item's Active To False
                        item.gameObject.SetActive(false);
                    }
                }
            }
            else 
            {
                //Loop Through The Item's Category
                foreach (Transform item in Child.transform)
                {
                    //Set All Objects Active To False
                    item.gameObject.SetActive(false);
                }
            }
        }
    }

    public void EditGridLayoutRow(int NumberOfRow)
    {
        Content.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedRowCount;
        Content.GetComponent<GridLayoutGroup>().constraintCount = NumberOfRow;
    }

    public void EditGridLayoutColumn(int NumberOfColumn)
    {
        Content.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        Content.GetComponent<GridLayoutGroup>().constraintCount = NumberOfColumn;
    }

    public void EditCellSizeX(int CellSizeX)
    {
        Content.GetComponent<GridLayoutGroup>().cellSize = new Vector2(CellSizeX, Content.GetComponent<GridLayoutGroup>().cellSize.y);
    }

    public void EditCellSizeY(int CellSizeY)
    {
        Content.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Content.GetComponent<GridLayoutGroup>().cellSize.x, CellSizeY);
    }

    public void SaveContent()
    {
        PlayerPrefs.SetInt("FirstTime",0);
        Debug.Log(" Shop Items' Information Saved! :D");

        string SelectedKey = "";
        string LockedKey = "";

        //Loop Through Categories in ShowCasePreview
        foreach(Transform Category in ShowCasePreview.transform)
        {
            //Check Category Type & Set PlayerPrefKey
            switch (Category.GetComponent<ItemInfo>().type)
            {
                case ItemInfo.ShowCase.Gender:
                    {
                        SelectedKey = "GenderS ";
                        LockedKey = "GenderL ";
                        break;
                    }
                case ItemInfo.ShowCase.Car:
                    {
                        SelectedKey = "CarS ";
                        LockedKey = "CarL ";
                        break;
                    }
                case ItemInfo.ShowCase.Environment:
                    {
                        SelectedKey = "EnvironmentS ";
                        LockedKey = "EnvironmentL ";
                        break;
                    }
                case ItemInfo.ShowCase.Props:
                    {
                        SelectedKey = "PropsS ";
                        LockedKey = "PropsL ";
                        break;
                    }
            }

            foreach (Transform Child in Category)
            {
                //Check Child's Current Variables & Set PlayerPref's String As: Category + Child's ID

                if (Child.GetComponent<ItemInfo>().Selected == true)
                {
                    Debug.Log("***"+ Child.name);
                    PlayerPrefs.SetInt(SelectedKey + Child.name, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(SelectedKey + Child.name, 0);
                }

                if (Child.GetComponent<ItemInfo>().Locked == true)
                {
                    PlayerPrefs.SetInt(LockedKey + Child.name, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(LockedKey + Child.name, 0);
                }
            }
           
        }
    }

    public void LoadContent()
    {
        Debug.Log("Shop Items' Information Loaded :D");
        string SelectedKey = "";
        string LockedKey = "";

        foreach (Transform Category in ShowCasePreview.transform)
        {
             switch (Category.GetComponent<ItemInfo>().type)
            {
                case ItemInfo.ShowCase.Gender:
                    {
                        SelectedKey = "GenderS ";
                        LockedKey = "GenderL ";
                        break;
                    }
                case ItemInfo.ShowCase.Car:
                    {
                        SelectedKey = "CarS ";
                        LockedKey = "CarL ";
                        break;
                    }
                case ItemInfo.ShowCase.Environment:
                    {
                        SelectedKey = "EnvironmentS ";
                        LockedKey = "EnvironmentL ";
                        break;
                    }
                case ItemInfo.ShowCase.Props:
                    {
                        SelectedKey = "PropsS ";
                        LockedKey = "PropsL ";
                        break;
                    }
            }

            foreach(Transform Child in Category)
            {
                //Load PlayerPref For Select 
                if(PlayerPrefs.GetInt(SelectedKey + Child.name) == 1)
                {
                    Child.GetComponent<ItemInfo>().Selected = true;
                }
                else
                {
                    Child.GetComponent<ItemInfo>().Selected = false;
                }
                //Load PlayerPref For Locked
                if (PlayerPrefs.GetInt(LockedKey + Child.name) == 1)
                {
                    Child.GetComponent<ItemInfo>().Locked = true;
                }
                else
                {
                    Child.GetComponent<ItemInfo>().Locked = false;
                }
            }
        }
    }

    public void EvaluateItemNumbers()
    {
        totalNoOfItems = totalUnlocked = percentOfItemsUnlocked = 0;
        
        //Loop for each category
        foreach (Transform Category in ShowCasePreview.transform)
        {
            //Children in each category
            foreach (Transform Child in Category)
            {
                totalNoOfItems++;
                if (!Child.GetComponent<ItemInfo>().Locked)
                {
                    totalUnlocked++;
                }
            }
        }

        percentOfItemsUnlocked = totalUnlocked / totalNoOfItems * 100;
        GameControl.handle.player.percentOfItemsUnlocked = percentOfItemsUnlocked;
    }
}
