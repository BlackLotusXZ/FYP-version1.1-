using UnityEngine;
using System.Collections;

using System.Collections.Generic;


/********************************************/
/**
*  \brief
*   - This script handles the environment of the game. Switching environments is done here
***********************************************/
public class EnvironmentControl : MonoBehaviour
{

    public int NumberOfSegments;
    public int StartSegment;
    public int EndSegment;

    public GameObject CitySegment;
    public GameObject DesertSegment;
    public GameObject WinterSegment;

    private int ToSpawnOrNot;

    private GameObject Environment;

    private List<GameObject> PropList;
    private GameObject Segment;
    private List<GameObject> SegmentManager; // A Segment Consist Of A Base & 1 Prop or More

    void Start()
    {
        ToSpawnOrNot = 0;

        PropList = new List<GameObject>();
        Segment = new GameObject();
        SegmentManager = new List<GameObject>();
    }

    public List<GameObject> GetSegmentManager()
    {
        return SegmentManager;
    }

    public void SetEnvironment(GameObject Environment)
    {
        this.Environment = Environment;
    }

    public void SetProp(List<GameObject> Props)
    {
        this.PropList = Props;
    }

    //Randomize Props In A Specified Segment
    public void RandomizePropsInSegment(GameObject Segment)
    {
        //Loop Through Segment and Get The Flooring/Base
        for (int i = 0; i < Segment.transform.childCount; ++i)
        {
            //Loop Through Flooring/Base To Get Props Under Its Hierarchy
            for (int j = 0; j < Segment.transform.GetChild(i).childCount; ++j)
            {
                if (BoughtPropBefore(Segment.transform.GetChild(i).transform.GetChild(j).name) == true)
                {
                    ToSpawnOrNot = Random.Range(-1, 2);
                    if (ToSpawnOrNot > 0 || Segment.transform.GetChild(i).transform.GetChild(j).tag == "Consecutive")
                    {
                        Segment.transform.GetChild(i).transform.GetChild(j).gameObject.SetActive(true);
                    }
                    else
                    {
                        Segment.transform.GetChild(i).transform.GetChild(j).gameObject.SetActive(false);
                    }
                }
                else
                {
                    Segment.transform.GetChild(i).transform.GetChild(j).gameObject.SetActive(false);
                }
            }
        }

    }

    public void GenerateEnvironment()
    {
        int SegmentsGenerated = 0;
        int SpaceToMoveBackZ = 0;
        Debug.Log(Environment.name);
        while (SegmentsGenerated < NumberOfSegments)
        {
            switch (Environment.name)
            {
                case "City Base":
                    {
                        //Create Left Plane
                        Segment = Instantiate(CitySegment);
                        Segment.transform.SetParent(transform);
                        Segment.transform.localPosition = new Vector3(0, -1, SpaceToMoveBackZ);
                        Segment.transform.Rotate(new Vector3(0, 0, 0));
                        Segment.transform.localScale = new Vector3(1, 1, 1);
                        RandomizePropsInSegment(Segment);
                        break;
                    }
                case "Desert Base":
                    {
                        //Create Left Plane
                        Segment = Instantiate(DesertSegment);
                        Segment.transform.SetParent(transform);
                        Segment.transform.localPosition = new Vector3(0, -1, SpaceToMoveBackZ);
                        Segment.transform.Rotate(new Vector3(0, 0, 0));
                        Segment.transform.localScale = new Vector3(1, 1, 1);
                        RandomizePropsInSegment(Segment);
                        break;
                    }
                case "Winter Base":
                    {
                        //Create Left Plane
                        Segment = Instantiate(WinterSegment);
                        Segment.transform.SetParent(transform);
                        Segment.transform.localPosition = new Vector3(0, -1, SpaceToMoveBackZ);
                        Segment.transform.Rotate(new Vector3(0, 0, 0));
                        Segment.transform.localScale = new Vector3(1, 1, 1);
                        RandomizePropsInSegment(Segment);
                        break;
                    }
            }
            SegmentManager.Add(Segment);
            SegmentsGenerated++;
            SpaceToMoveBackZ += 90;
        }
    }

    public void SwapSegments(int FirstSegment, int SecondSegment)
    {
        GameObject FirstSegmentFlooringL = SegmentManager[FirstSegment].transform.GetChild(0).gameObject;
        GameObject FirstSegmentFlooringR = SegmentManager[FirstSegment].transform.GetChild(1).gameObject;

        GameObject SecondSegmentFlooringL = SegmentManager[SecondSegment].transform.GetChild(0).gameObject;
        GameObject SecondSegmentFlooringR = SegmentManager[SecondSegment].transform.GetChild(1).gameObject;

        FirstSegmentFlooringL.transform.SetParent(SegmentManager[SecondSegment].transform);
        FirstSegmentFlooringL.transform.localPosition = new Vector3(FirstSegmentFlooringL.transform.localPosition.x, FirstSegmentFlooringL.transform.localPosition.y, 0);

        FirstSegmentFlooringR.transform.SetParent(SegmentManager[SecondSegment].transform);
        FirstSegmentFlooringR.transform.localPosition = new Vector3(FirstSegmentFlooringR.transform.localPosition.x, FirstSegmentFlooringR.transform.localPosition.y, 0);

        SecondSegmentFlooringL.transform.SetParent(SegmentManager[FirstSegment].transform);
        SecondSegmentFlooringL.transform.localPosition = new Vector3(SecondSegmentFlooringL.transform.localPosition.x, SecondSegmentFlooringL.transform.localPosition.y, 0);

        SecondSegmentFlooringR.transform.SetParent(SegmentManager[FirstSegment].transform);
        SecondSegmentFlooringR.transform.localPosition = new Vector3(SecondSegmentFlooringR.transform.localPosition.x, SecondSegmentFlooringR.transform.localPosition.y, 0);
        //string FlooringName = "";
        //foreach (Transform Flooring in SegmentManager[FirstSegment].transform)
        //{
        //    FlooringName = Flooring.name;
        //    Flooring.SetParent(SegmentManager[SecondSegment].transform);
        //    Flooring.localPosition = new Vector3(Flooring.localPosition.x, Flooring.localPosition.y, 0);
        //}

        //foreach (Transform Flooring in SegmentManager[SecondSegment].transform)
        //{
        //    if (Flooring.name != "SwapInProgress")
        //    {
        //        Flooring.SetParent(SegmentManager[FirstSegment].transform);
        //        Flooring.localPosition = new Vector3(Flooring.localPosition.x, Flooring.localPosition.y, 0);
        //    }
        //    else
        //    {
        //        Flooring.name = FlooringName;
        //    }
    }



    //Check If Prop Is Present In PlayerPref/If Player Bought Prop Before
    public bool BoughtPropBefore(string PropName)
    {
        foreach (GameObject Prop in PropList)
        {
            if (PropName.Contains(Prop.name))
            {
                return true;
            }
        }
        return false;
    }
}