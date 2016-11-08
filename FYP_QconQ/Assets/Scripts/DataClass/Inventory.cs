using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;


/********************************************/
/**
*  \brief
*   - This script handles the game's "inventory". i.e it contains lists for the unlocked customisation options
*   - Also contains shop information and info on equipping items
***********************************************/
public class Inventory : MonoBehaviour 
{
    public GameObject ShowCasePreview;
    void Start()
    {
    }

    void Update() 
    {
    }


    public void AddItem()
    {
        ItemInfo.ShowCase ItemType = new ItemInfo.ShowCase();

        

       
    }

    public void RemoveItem(GameObject Item)
    {
        foreach (Transform Child in transform)
        {
            if (Child.name == Item.name)
            {
                Destroy(Child);
            }
        }
    }

    public void SaveInventory()
    {
        foreach (Transform Category in ShowCasePreview.transform)
        {
            foreach (Transform Child in Category)
                if (Child.GetComponent<ItemInfo>().Selected == true)
                {
                }
        }
    }

    public void LoadInventory()
    {

    }

//    private List<GameObject> car_Inventory = new List<GameObject>();
//    private List<GameObject> environment_Inventory = new List<GameObject>();

//    private List<GameObject> carButton = new List<GameObject>();
//    private List<GameObject> environmentButton = new List<GameObject>();

//    private List<GameObject> carButton2 = new List<GameObject>(); // for landscape canvas
//    private List<GameObject> environmentButton2 = new List<GameObject>(); // for landscape canvas

//    private Vector2[] originalContent = new Vector2[2];

//    public GameObject[] panels; // panels 0,1 is portrait , 2,3 is landscape

//    public GameObject[] contentCanvas; // 0 is portrait , 1 is landscape

//    public GameObject prefabButton;

//    static bool onCarTab = true;

//    private float dist = 200;

//    public Text[] DisplayName;

//    public AudioClip[] pressSFX;
//    private AudioSource source { get { return GetComponent<AudioSource>(); } }

//    public GameObject[] ChangeenvironmentButtons;
//    public GameObject[] ChangeenvironmentButtons2;
//    private Color originalColor;


//    void Start()
//    {
//        foreach (Transform child in this.GetComponentInChildren<Transform>())
//        {
//            if (child.tag == "CarPart")
//            {
//                foreach (Transform children in child.GetComponentInChildren<Transform>())
//                {
//                    car_Inventory.Add(children.gameObject);
//                }
//            }
//            if (child.tag == "EnvironmentPart")
//            {
//                foreach (Transform children in child.GetComponentInChildren<Transform>())
//                {
//                    environment_Inventory.Add(children.gameObject);
//                }
//            }
//        }

//        inventory_Load();
//        SpawnShop(false); // spawn for portrait 
//        SpawnShop(true); // spawn for landscape

//        contentCanvas[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
//        contentCanvas[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);

//        gameObject.AddComponent<AudioSource>();
//        source.playOnAwake = false;
//        source.volume = 1;

//        originalColor = ChangeenvironmentButtons[0].GetComponent<Image>().color;

//        for (int i = 0; i < ChangeenvironmentButtons.Length; i++)
//        {
//            ChangeenvironmentButtons[i].SetActive(false);
//            ChangeenvironmentButtons2[i].SetActive(false);
//        }

//        pressEnvironmentButton(GameControl.handle.player.EnvironmentIn);

//    }

//    void inventory_Save()
//    {
//        save(car_Inventory, "CarPart");
//        save(environment_Inventory, "EnvironmentPart");
//    }
//    void save(List<GameObject> inventory, string saveword)
//    {
//        for (int i = 0; i < inventory.Count; i++)
//        {
//            PlayerPrefs.SetInt(saveword + inventory[i].GetComponent<Item>().item_Name, inventory[i].GetComponent<Item>().getState());
//        }
//    }

//    public void inventory_Load()
//    {
//        load(car_Inventory, "CarPart");
//        load(environment_Inventory, "EnvironmentPart");
//    }
//    void load(List<GameObject> inventory, string saveword)
//    {
//        for (int i = 0; i < inventory.Count; i++)
//        {
//            inventory[i].GetComponent<Item>().setState(PlayerPrefs.GetInt(saveword + inventory[i].GetComponent<Item>().item_Name));
//        }
//    }

//    void OnApplicationPause()
//    {
//        inventory_Save();
//    }
//    void OnDisable()
//    {
//        inventory_Save();
//    }

//    public void selectItem(int position) // if press , select the item and set it active in front of the raw image camera
//    {
//        setAllFalse();
//        if (onCarTab)
//        {
//            car_Inventory[position].SetActive(true);
//            for (int i = 0; i < DisplayName.Length;i++ )
//                DisplayName[i].text = car_Inventory[position].GetComponent<Item>().item_Name;
//        }
//        else
//        {
//            environment_Inventory[position].SetActive(true);
//            for (int i = 0; i < DisplayName.Length; i++)
//                DisplayName[i].text = environment_Inventory[position].GetComponent<Item>().item_Name;
//        }

//        this.transform.rotation = Quaternion.Euler(0, 0, 0);
//    }
//    void setAllFalse()
//    {
//        for (int i = 0; i < environment_Inventory.Count; i++)
//            environment_Inventory[i].SetActive(false);
//        for (int i = 0; i < car_Inventory.Count; i++)
//            car_Inventory[i].SetActive(false);
//    }

//    public void ClickOnButton(int position)
//    {
//        if (onCarTab)
//        {
//            if (car_Inventory[position].GetComponent<Item>().getState() == (int)Item.ItemState.nothing && GameControl.handle.player.Player_Money >= car_Inventory[position].GetComponent<Item>().item_Price)
//            {
//                carButton[position].GetComponent<Text>().text = "Equip"; // change button in 2 diff canvas at same time
//                carButton2[position].GetComponent<Text>().text = "Equip";
//                car_Inventory[position].GetComponent<Item>().setState((int)Item.ItemState.bought);

//                GameControl.handle.player.Player_Money -= car_Inventory[position].GetComponent<Item>().item_Price;

//                if (GameControl.handle.player.SettingsSE_Player == 0)
//                {
//                    source.PlayOneShot(pressSFX[0]);
//                }
//            }
//            else if (car_Inventory[position].GetComponent<Item>().getState() == (int)Item.ItemState.bought)
//            {
//                carButton[position].GetComponent<Text>().text = "Equipped";
//                carButton2[position].GetComponent<Text>().text = "Equipped";
//                car_Inventory[position].GetComponent<Item>().setState((int)Item.ItemState.equipped);

//                if (GameControl.handle.player.SettingsSE_Player == 0)
//                {
//                    source.PlayOneShot(pressSFX[1]);
//                }
//            }
//        }
//        else
//        {
//            if (environment_Inventory[position].GetComponent<Item>().getState() == (int)Item.ItemState.nothing && GameControl.handle.player.Player_Money >= environment_Inventory[position].GetComponent<Item>().item_Price)
//            {
//                environmentButton[position].GetComponent<Text>().text = "Equip";
//                environmentButton2[position].GetComponent<Text>().text = "Equip";
//                environment_Inventory[position].GetComponent<Item>().setState((int)Item.ItemState.bought);

//                GameControl.handle.player.Player_Money -= environment_Inventory[position].GetComponent<Item>().item_Price;

//                if (GameControl.handle.player.SettingsSE_Player == 0)
//                {
//                    source.PlayOneShot(pressSFX[0]);
//                }
//            }
//            else if (environment_Inventory[position].GetComponent<Item>().getState() == (int)Item.ItemState.bought)
//            {
//                environmentButton[position].GetComponent<Text>().text = "Equipped";
//                environmentButton2[position].GetComponent<Text>().text = "Equipped";
//                environment_Inventory[position].GetComponent<Item>().setState((int)Item.ItemState.equipped);

//                if (GameControl.handle.player.SettingsSE_Player == 0)
//                {
//                    source.PlayOneShot(pressSFX[1]);
//                }
//            }
//        }

//        inventory_Save();

//    }

//    public void PressTab(int tab)
//    {
//        if ((tab == 0 && onCarTab == false) || (tab == 1 && onCarTab == true))
//            setAllFalse();

//        if (tab == 0) // pressing tab sets for both orientation canvas so it is consistent ( tho alot of hardcode)
//        {
//            onCarTab = true;
//            panels[0].SetActive(true);
//            panels[1].SetActive(false);
//            panels[2].SetActive(true);
//            panels[3].SetActive(false);
//            for (int i = 0; i < contentCanvas.Length; i++)
//            {
//                contentCanvas[i].GetComponent<RectTransform>().offsetMin = originalContent[0];
//                contentCanvas[i].GetComponent<RectTransform>().offsetMax = originalContent[1];

//                contentCanvas[i].GetComponent<RectTransform>().offsetMin = new Vector2(contentCanvas[i].GetComponent<RectTransform>().offsetMin.x, -car_Inventory.Count * dist);
//            }

//            for (int i = 0; i < ChangeenvironmentButtons.Length; i++)
//            {
//                ChangeenvironmentButtons[i].SetActive(false);
//                ChangeenvironmentButtons2[i].SetActive(false);
//            }
//        }
//        else
//        {
//            onCarTab = false;
//            panels[1].SetActive(true);
//            panels[0].SetActive(false);
//            panels[3].SetActive(true);
//            panels[2].SetActive(false);
//            for (int i = 0; i < contentCanvas.Length; i++)
//            {
//                contentCanvas[i].GetComponent<RectTransform>().offsetMin = originalContent[0];
//                contentCanvas[i].GetComponent<RectTransform>().offsetMax = originalContent[1];

//                contentCanvas[i].GetComponent<RectTransform>().offsetMin = new Vector2(contentCanvas[i].GetComponent<RectTransform>().offsetMin.x, -environment_Inventory.Count * dist);
//            }

//            for (int i = 0; i < ChangeenvironmentButtons.Length; i++)
//            {
//                ChangeenvironmentButtons[i].SetActive(true);
//                ChangeenvironmentButtons2[i].SetActive(true);
//            }
            
//        }

//        contentCanvas[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
//        contentCanvas[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

//    }

//    public void delete()
//    {
//        for(int i = 0 ; i < car_Inventory.Count; i++)
//        {
//            car_Inventory[i].GetComponent<Item>().setState((int)Item.ItemState.nothing);
//        }
//        for (int i = 0; i < environment_Inventory.Count; i++)
//        {
//            environment_Inventory[i].GetComponent<Item>().setState((int)Item.ItemState.nothing);
//        }
//    }

//    List<string> getCPartInventory()
//    {
//        List<string> data = new List<string>();

//        for (int i = 0; i < car_Inventory.Count; i++)
//        {
//            if (car_Inventory[i].GetComponent<Item>().getState() == (int)Item.ItemState.equipped)
//            {
//                data.Add(car_Inventory[i].name);
//            }
//        }

//        return data;
//    }
//    List<string> getEPartInventory()
//    {
//        List<string> data = new List<string>();

//        for (int i = 0; i < environment_Inventory.Count; i++)
//        {
//            if (environment_Inventory[i].GetComponent<Item>().getState() == (int)Item.ItemState.equipped)
//            {
//                data.Add(environment_Inventory[i].name);
//            }
//        }

//        // essential environment part 
//        data.Add("base");

//        return data;
//    }

//    // populate the shop thingy
//    void SpawnShop(bool isLandscape)
//    {
//        if(!isLandscape)
//            contentCanvas[0].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
//        else
//            contentCanvas[1].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

//        if (isLandscape) // rotate if landscape
//        {
//            contentCanvas[1].transform.rotation = Quaternion.Euler(0, 0, 0);
//        }

//        if (isLandscape)
//        {
//            spawner(car_Inventory, 2, carButton2);
//            spawner(environment_Inventory, 3, environmentButton2);
//        }
//        else
//        {
//            spawner(car_Inventory, 0, carButton);
//            spawner(environment_Inventory, 1, environmentButton);
//        }

//        for (int i = 1; i < panels.Length; i++)
//            panels[i].SetActive(false);
//        panels[2].SetActive(true);

//        if (!isLandscape)
//        {
//            originalContent[0] = contentCanvas[0].GetComponent<RectTransform>().offsetMin;
//            originalContent[1] = contentCanvas[0].GetComponent<RectTransform>().offsetMax;
//        }

//        if(!isLandscape)
//            contentCanvas[0].GetComponent<RectTransform>().offsetMin = new Vector2(contentCanvas[0].GetComponent<RectTransform>().offsetMin.x, -car_Inventory.Count* dist);
//        else 
//            contentCanvas[1].GetComponent<RectTransform>().offsetMin = new Vector2(contentCanvas[1].GetComponent<RectTransform>().offsetMin.x, -car_Inventory.Count* dist);


//        if (isLandscape)
//        {
//            contentCanvas[1].transform.rotation = Quaternion.Euler(0, 0, 270);
//        }
//    }

//    void spawner(List<GameObject> inventory , int which, List<GameObject> button)
//    {
//        Vector3 startpos = new Vector3(0, 95, 0);

//        for (int i = 0; i < inventory.Count; i++)
//        {
//            // clone the button and position properly
//            GameObject clone = Instantiate<GameObject>(prefabButton);
//            clone.transform.SetParent(panels[which].transform);
//            clone.transform.localPosition = startpos;
//            clone.transform.localScale = new Vector3(1, 1, 1);
//            RectTransform rectTransform =  clone.GetComponent<RectTransform>();
//            rectTransform.offsetMin = new Vector2(-7.5f, rectTransform.offsetMin.y);
//            rectTransform.offsetMax = new Vector2(7.5f, rectTransform.offsetMax.y);

//            startpos.y -= 200;
//            int x = i;
//            clone.GetComponent<Button>().onClick.AddListener(() => { selectItem(x); });

//            // copy data from inventory to the button clone
//            Item cloneItem = inventory[i].GetComponent<Item>();
//            foreach (Transform child in clone.GetComponentInChildren<Transform>())
//            {
//                if (child.tag == "Name")
//                {
//                    child.gameObject.GetComponent<Text>().text = cloneItem.item_Name;
//                }
//                if (child.tag == "Icon")
//                {
//                    child.gameObject.GetComponent<Image>().sprite = cloneItem.item_Icon;
//                }
//                if (child.tag == "InButton")
//                {
//                    foreach (Transform children in child.GetComponentInChildren<Transform>())
//                    {
//                        if (children.tag == "Price")
//                        {
//                            if(cloneItem.getState() == (int)Item.ItemState.nothing)
//                                children.gameObject.GetComponent<Text>().text = cloneItem.item_Price.ToString();
//                            if (cloneItem.getState() == (int)Item.ItemState.bought)
//                                children.gameObject.GetComponent<Text>().text = "Equip";
//                            if (cloneItem.getState() == (int)Item.ItemState.equipped)
//                                children.gameObject.GetComponent<Text>().text = "Equipped";

//                            button.Add(children.gameObject);

//                        }
//                    }

//                    child.GetComponent<Button>().onClick.AddListener(() => { ClickOnButton(x); });
//                }
//            }
//        }
//    }

//    public void setControl()
//    {
//        GameControl.handle.carparts = getCPartInventory();
//        GameControl.handle.environmentparts = getEPartInventory();
//        inventory_Save();
//    }

//    public void pressEnvironmentButton(int a)
//    {
//        GameControl.handle.player.EnvironmentIn = a;

//        for(int i = 0 ; i < ChangeenvironmentButtons.Length;i++)
//        {
//            if (i == a)
//            {
//                ChangeenvironmentButtons[i].GetComponent<Image>().color = new Color(1, 0, 0);
//                ChangeenvironmentButtons2[i].GetComponent<Image>().color = new Color(1, 0, 0);
//            }
//            else
//            {
//                ChangeenvironmentButtons[i].GetComponent<Image>().color = originalColor;
//                ChangeenvironmentButtons2[i].GetComponent<Image>().color = originalColor;
//            }
//        }

//    }

}