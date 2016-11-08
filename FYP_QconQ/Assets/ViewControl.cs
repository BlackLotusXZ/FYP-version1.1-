using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using System.Collections.Generic;

public class ViewControl : MonoBehaviour {

    public GameObject GameAssets;

    public GameObject GenderHolder;
    public GameObject CarHolder;
    public GameObject EnvironmentHolder;

	// Use this for initialization
	void Start () {
        InitGameAssets();

        GenderHolder.GetComponent<GenderControl>().GenerateGender();
        EnvironmentHolder.GetComponent<EnvironmentControl>().GenerateEnvironment();
        CarHolder.GetComponent<CarControl>().GenerateCar();
	}
	
	// Update is called once per frame
	void Update () {
    }

    //Init GameObjects You Can Play With Using PlayerPrefs From Shop
    void InitGameAssets()
    {
        List<GameObject> Props = new List<GameObject>();
        GameObject Environment = new GameObject();
        GameObject Gender = new GameObject();
        GameObject Car = new GameObject();

        string SelectedKey = "";

        foreach (Transform Category in GameAssets.transform)
        {
            switch (Category.GetComponent<ItemInfo>().type)
            {
                case ItemInfo.ShowCase.Gender:
                    {
                        SelectedKey = "GenderS ";
                        break;
                    }
                case ItemInfo.ShowCase.Car:
                    {
                        SelectedKey = "CarS ";
                        break;
                    }
                case ItemInfo.ShowCase.Environment:
                    {
                        SelectedKey = "EnvironmentS ";
                        break;
                    }
                case ItemInfo.ShowCase.Props:
                    {
                        SelectedKey = "PropsS ";
                        break;
                    }
            }

            foreach (Transform Child in Category)
            {
                if (PlayerPrefs.GetInt(SelectedKey + Child.name) == 1)
                {
                        switch(Child.GetComponent<ItemInfo>().type)
                        {
                            case ItemInfo.ShowCase.Gender:
                                {
                                    Debug.Log(Gender.name);
                                    Gender = Child.gameObject;
                                    break;
                                }
                            case ItemInfo.ShowCase.Car:
                                {
                                    Car = Child.gameObject;
                                    break;
                                }
                            case ItemInfo.ShowCase.Environment:
                                {
                                    Environment = Child.gameObject;
                                    break;
                                }
                            case ItemInfo.ShowCase.Props:
                                {
                                    Props.Add(Child.gameObject);
                                    break;
                                }
                        }
                }
                else
                {
                    Child.gameObject.SetActive(false);
                }
            }
        }

        GenderHolder.GetComponent<GenderControl>().SetGender(Gender);
        EnvironmentHolder.GetComponent<EnvironmentControl>().SetEnvironment(Environment);
        EnvironmentHolder.GetComponent<EnvironmentControl>().SetProp(Props);
        CarHolder.GetComponent<CarControl>().SetCar(Car);
    }
}
