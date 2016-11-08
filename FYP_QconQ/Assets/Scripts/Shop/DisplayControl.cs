using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DisplayControl : MonoBehaviour
{
    //-----Animation-----
    //Flag For Scaling Animation
    private bool isExpanding;
    private bool isCompressing;

    //Max & Min Scale For Scaling Animations
    public Vector3 MaxScale;
    public Vector3 MinScale;

    //Flags For Alpha Fading Animation
    private bool isFadingIn;
    private bool isFadingOut;

    //Max & Min Alpha For Fading Animation
    public float MaxFadeIn;
    public float MinFadeOut;

    //Black Quad For Alpha Fade
    public GameObject AlphaQuad;

    //alpha in and out for the preview of gameobject
    public GameObject BackgroundPreview;
    //How Fast Scaling Animation Is
    public float ScaleSpeed;

    //How Fast Fading Animation Is
    public float FadeSpeed;

    //To What Point Will Animation Stop & Snap To Targetted Destination 
    public float SnappingPoint;

    //-----In-Game GameObjects(Can Be Found In Scene Hierarchy)-----
    public GameObject LockButton;
    public GameObject SelectButton;
    public GameObject ContentScrollView;
    public GameObject ShowCasePreview;
    public GameObject OrientationController;

    //Others
    //ItemInfo Holder
    public GameObject ItemInfoHolder;

    //ListOfSelectedItems
    public List<GameObject> ListOfSelectedItems;

    public GameObject PageController;

    //Sprite
    public Sprite RandoBox;

    public GameObject Toast;
    private Text toastText;

    // Use this for initialization
    void Start()
    {
        //Initialization
        isExpanding = false;
        isCompressing = false;

        isFadingIn = false;
        isFadingOut = false;

        //Link SelectButton On Click Function
        LockButton.GetComponent<Button>().onClick.AddListener(() => ContentScrollView.GetComponent<PopulateSelection>().SaveContent());
        SelectButton.GetComponent<Button>().onClick.AddListener(() => Selected());
        SelectButton.GetComponent<Button>().onClick.AddListener(() => ContentScrollView.GetComponent<PopulateSelection>().SaveContent());

        //Toast.SetActive(false);
        
        //PlayerPrefs.DeleteAll()
    }

    // Update is called once per frame
    void Update()
    {
        if(PageController.GetComponent<PageController>().getCurrentPage() != 1)
        {
            CompressDisplay();
            FadeIn();
        }

        //If Expanding Animation On Going
        if (isExpanding == true)
        {

            if ((this.transform.localScale - MaxScale).magnitude > SnappingPoint)
            {
                this.transform.localScale = new Vector3(Mathf.Lerp(this.transform.localScale.x, MaxScale.x, Time.deltaTime * ScaleSpeed), Mathf.Lerp(this.transform.localScale.y, MaxScale.y, Time.deltaTime * ScaleSpeed), this.transform.localScale.z);

                //Debug.Log("Expanding");
            }
            else
            {
                this.transform.localScale = MaxScale;
                isExpanding = false;

                //Debug.Log("StopExpanding");
            }
        }

        //If Compressing Animation On Going
        if (isCompressing == true)
        {
            if ((this.transform.localScale - MinScale).magnitude > SnappingPoint)
            {
                this.transform.localScale = new Vector3(Mathf.Lerp(this.transform.localScale.x, MinScale.x, Time.deltaTime * ScaleSpeed), Mathf.Lerp(this.transform.localScale.y, MinScale.y, Time.deltaTime * ScaleSpeed), this.transform.localScale.z);
            }
            else
            {
                this.transform.localScale = MinScale;
                isCompressing = false;
            }
        }

        //If Fading Out Animation On-Going
        if (isFadingOut == true)
        {
            AlphaQuad.GetComponent<Image>().raycastTarget = true;
            if (AlphaQuad.GetComponent<Image>().color.a < MinFadeOut)
            {
                AlphaQuad.GetComponent<Image>().color = new Color(AlphaQuad.GetComponent<Image>().color.r, AlphaQuad.GetComponent<Image>().color.g, AlphaQuad.GetComponent<Image>().color.b, Mathf.Lerp(AlphaQuad.GetComponent<Image>().color.a, MinFadeOut, Time.deltaTime * FadeSpeed));
                BackgroundPreview.GetComponent<Image>().color = new Color(BackgroundPreview.GetComponent<Image>().color.r, BackgroundPreview.GetComponent<Image>().color.g, BackgroundPreview.GetComponent<Image>().color.b, Mathf.Lerp(BackgroundPreview.GetComponent<Image>().color.a, MinFadeOut, Time.deltaTime * FadeSpeed));
            }
            else
            {
                AlphaQuad.GetComponent<Image>().color = new Color(AlphaQuad.GetComponent<Image>().color.r, AlphaQuad.GetComponent<Image>().color.g, AlphaQuad.GetComponent<Image>().color.b, MinFadeOut);
                BackgroundPreview.GetComponent<Image>().color = new Color(BackgroundPreview.GetComponent<Image>().color.r, BackgroundPreview.GetComponent<Image>().color.g, BackgroundPreview.GetComponent<Image>().color.b, MinFadeOut);
                isFadingOut = false;
            }
        }

        //If Fading In Animation On-Going
        if (isFadingIn == true)
        {
            AlphaQuad.GetComponent<Image>().raycastTarget = false;
            if (AlphaQuad.GetComponent<Image>().color.a > MaxFadeIn)
            {
                AlphaQuad.GetComponent<Image>().color = new Color(AlphaQuad.GetComponent<Image>().color.r, AlphaQuad.GetComponent<Image>().color.g, AlphaQuad.GetComponent<Image>().color.b, Mathf.Lerp(AlphaQuad.GetComponent<Image>().color.a, MaxFadeIn, Time.deltaTime * FadeSpeed));
                BackgroundPreview.GetComponent<Image>().color = new Color(BackgroundPreview.GetComponent<Image>().color.r, BackgroundPreview.GetComponent<Image>().color.g, BackgroundPreview.GetComponent<Image>().color.b, Mathf.Lerp(BackgroundPreview.GetComponent<Image>().color.a, MaxFadeIn, Time.deltaTime * FadeSpeed));

                //Debug.Log("FadingIn");
            }
            else
            {
                AlphaQuad.GetComponent<Image>().color = new Color(AlphaQuad.GetComponent<Image>().color.r, AlphaQuad.GetComponent<Image>().color.g, AlphaQuad.GetComponent<Image>().color.b, MaxFadeIn);
                BackgroundPreview.GetComponent<Image>().color = new Color(BackgroundPreview.GetComponent<Image>().color.r, BackgroundPreview.GetComponent<Image>().color.g, BackgroundPreview.GetComponent<Image>().color.b, MaxFadeIn);

                isFadingIn = false;
                //Debug.Log("StopFading");
            }
        }
    }

    //Save All Unlocked & Selected Items
    public void Save_Inventory()
    { 
        //foreach(Transform Category in ShowCasePreview.transform)
        //{
        //    foreach(Transform Child in Category)
        //    {
        //        switch(Child.GetComponent<ItemInfo>().Selected)
        //        {
        //            case true:
        //                {
        //                    PlayerPrefs.SetInt("S " + Child.name, 1);
        //                    break;
        //                }
        //            case false:
        //                {
        //                    PlayerPrefs.SetInt("S " + Child.name, 0);
        //                    break;
        //                }
        //        }
        //        switch (Child.GetComponent<ItemInfo>().Locked)
        //        {
        //            case true:
        //                {
        //                    PlayerPrefs.SetInt("L " + Child.name, 1);
        //                    break;
        //                }
        //            case false:
        //                {
        //                    PlayerPrefs.SetInt("L " + Child.name, 0);
        //                    break;
        //                }
        //        }
        //    }
        //}
    }

    //Load All Unlocked & Saved Items
    public void Load_Inventory()
    {
        foreach (Transform Category in ShowCasePreview.transform)
        {
            foreach (Transform Child in Category)
            {
                Child.GetComponent<ItemInfo>().Locked = true;
                //switch (PlayerPrefs.GetInt("L " + Child.name))
                //{
                //    case 1:
                //        {
                //            Child.GetComponent<ItemInfo>().Locked = true;
                //            break;
                //        }
                //    case 0:
                //        {
                //            Child.GetComponent<ItemInfo>().Locked = false;
                //            break;
                //        }
                //}
                //switch (PlayerPrefs.GetInt("S " + Child.name))
                //{
                //    case 1:
                //        {
                //            Child.GetComponent<ItemInfo>().Selected = true;
                //            break;
                //        }
                //    case 0:
                //        {
                //            Child.GetComponent<ItemInfo>().Selected = false;
                //            break;
                //        }
                //}
            }

        }
    }

    //Hide Lock Button & Set Item's Lock Variable in ItemInfo to False
    public void UnlockDisplay()
    {
        LockButton.SetActive(false);
        getCurrentDisplayItem().GetComponent<ItemInfo>().Locked = false;
    }

    //UnHide Lock Button & Set Item's Lock Variable in ItemInfo to true
    public void LockDisplay()
    {
        LockButton.SetActive(true);
        getCurrentDisplayItem().GetComponent<ItemInfo>().Locked = true;
    }

    //Enable Select Button
    public void EnableSelect()
    {
        SelectButton.GetComponent<Button>().interactable = true;
    }

    //Disable Select BUtton
    public void DisableSelect()
    {
        SelectButton.GetComponent<Button>().interactable = false;
    }

    public void DisplayItemInfo(string Type, string Name, int Price)
    {
        foreach (Transform Child in ItemInfoHolder.transform)
        {
            switch (Child.name)
            {
                case "ItemName":
                    {
                        Child.GetComponent<Text>().text = Name;
                        break;
                    }
                case "ItemPrice":
                    {
                        Child.GetComponent<Text>().text = Price.ToString();
                        break;
                    }
                case "ItemType":
                    {
                        Child.GetComponent<Text>().text = Type;
                        break;
                    }
            }
        }
    }

    //Show Preview Window Using Scaling Animation
    public void ExpandDisplay()
    {
        isExpanding = true;
        isCompressing = false;
    }

    //Hide Preview Window Using Scaling Animation
    public void CompressDisplay()
    {
        isCompressing = true;
        isExpanding = false;
    }

    //Bring Focus Onto Preview Window Display Using FadeOut Animation
    public void FadeOut()
    {
        isFadingOut = true;
        isFadingIn = false;
    }

    //Scatter Focus Of Preview Window Display Using FadeIn Animation
    public void FadeIn()
    {
        isFadingIn = true;
        isFadingOut = false;
    }

    //Get The Current Item In The Preview Window Display
    public GameObject getCurrentDisplayItem()
    {
        //Loop Through ShowCase
        foreach (Transform Category in ShowCasePreview.transform)
        {
            //Loop Through Category
            foreach (Transform Child in Category)
            {
                //If Child is the one we selected and Child does not exist in GameObjectNameList 
                if (Child.gameObject.activeInHierarchy == true)
                {
                    return Child.gameObject;
                }
            }
        }
        return null;
    }

    //Prompt/Hint Player When Player Attempts To Unlock & Item
    public void CheckUnlockable()
    {
        DisplayMessage();
    }

    //Display Prompt/Hint
    public void DisplayMessage()
    {
        toastText = Toast.GetComponentInChildren<Text>();
        if (!Toast.activeInHierarchy)
        {
            Toast.GetComponent<CanvasRenderer>().SetAlpha(1);
            Toast.SetActive(true);
        }

        if (GameControl.handle.player.Player_Money >= getCurrentDisplayItem().GetComponent<ItemInfo>().Price)
        {
            GameControl.handle.player.Player_Money -= getCurrentDisplayItem().GetComponent<ItemInfo>().Price;
            toastText.text = "You Have Unlocked: " + getCurrentDisplayItem().name;
            UnlockDisplay();
            EnableSelect();
            StartCoroutine(FadeOut(Toast, 0.005f, 0));
        }
        else
        {
            toastText.text = "You Don't Have Enough Money To Unlock This Item";
            LockDisplay();
            DisableSelect();
            StartCoroutine(FadeOut(Toast, 0.007f, 0));
        }

        //StartCoroutine(FadeOut(Toast, 0.002f, 0));

        //GameObject ToastFrame;
        ////Spawn ToastFrame
        //ToastFrame = new GameObject("ToastFrame");

        ////Add Sprite & Its Attribute
        //ToastFrame.AddComponent<Image>();
        //ToastFrame.GetComponent<Image>().sprite = RandoBox;
        //ToastFrame.transform.SetParent((RectTransform)transform);
        //ToastFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 30);
        //ToastFrame.transform.localPosition = new Vector3(0, 0, 0);
        //ToastFrame.transform.localScale = new Vector3(1, 1, 1);

        //if (OrientationController.GetComponent<OrientationControl>().isLandScape == true)
        //{
        //    ToastFrame.transform.Rotate(0, 0, -90);
        //    ToastFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(2000, 100);
        //}
        //StartCoroutine(FadeOut(ToastFrame, 0.002f, 0));

        //GameObject Toast;
        ////Spawn Toast
        //Toast = new GameObject("Toast Message");

        //    //Add Text & Its Attribute
        //    Toast.AddComponent<Text>();
        //    Toast.GetComponent<Text>().fontSize = 60;
        //    Toast.GetComponent<Text>().font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        //    Toast.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        //    Toast.transform.SetParent(transform);

        //    Toast.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 100);
        //    Toast.transform.localPosition = new Vector3(0, 0, 0);
        //    Toast.transform.localScale = new Vector3(0.10f, 0.10f, 1);

        //if (OrientationController.GetComponent<OrientationControl>().isLandScape == true)
        //{
        //    Toast.transform.Rotate(0, 0, -90);
        //    Toast.GetComponent<RectTransform>().sizeDelta = new Vector2(2000, 100);
        //}

        
    }

    //Something Like A Thread To Handle Another Process Which In This Case Is To Fade The Promt/Hint Message For The Player
    IEnumerator FadeOut(GameObject Toast, float FadeSpeed, float MaxFadeOut)
    {
        while (Toast.GetComponent<CanvasRenderer>().GetAlpha() > MaxFadeOut)
        {
            Toast.GetComponent<CanvasRenderer>().SetAlpha(Toast.GetComponent<CanvasRenderer>().GetAlpha() - FadeSpeed);
            yield return null;
        }

        if (Toast.activeInHierarchy)
            Toast.SetActive(false);
            
        //Destroy(Toast);
    }

    //Update The Newest Version Of The ContentScrollView
    public void Selected()
    {
        //Loop Through ShowCase
        foreach (Transform Category in ShowCasePreview.transform)
        {
            //Loop Through Category
            foreach (Transform Child in Category)
            {
                //If Child is the one we selected and Child does not exist in GameObjectNameList 
                if (Child.gameObject.activeInHierarchy == true)
                {
                    if (Child.GetComponent<ItemInfo>().type != ItemInfo.ShowCase.Props)
                    {
                        foreach (Transform Item in Category)
                        {
                            //Set Child's Selected To True
                            Item.GetComponent<ItemInfo>().Selected = false;
                        }

                    }
                    //Set Child's Selected To True
                    Child.GetComponent<ItemInfo>().Selected = true;

                }
            }
        }

        string toDebug = "";
        foreach (Transform Category in ShowCasePreview.transform)
        {
            foreach (Transform Child in Category)
                if (Child.GetComponent<ItemInfo>().Selected == true)
                {
                    toDebug += Child.name + ", ";
                }
        }
        Debug.Log(toDebug);

        ContentScrollView.GetComponent<PopulateSelection>().ReloadContent((int)getCurrentDisplayItem().GetComponent<ItemInfo>().type);


        Save_Inventory();
    }
}
