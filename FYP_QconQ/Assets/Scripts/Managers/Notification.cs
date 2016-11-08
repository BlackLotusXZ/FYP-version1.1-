using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Notification : MonoBehaviour {

    //Orientation Variables
    public GameObject OrientationController;
    public GameObject LandScapeParent;
    public GameObject PortraitParent;

    //Queue Order (First In First Out)
    private bool isDisplayingMessage;
    private List<GameObject> Queue;

    //Text 
    GameObject NotificationMessage;

    private int i = 0;
	// Use this for initialization
	void Start () {
        Queue = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (OrientationController.GetComponent<OrientationControl>().isLandScape == true)
        {
            transform.SetParent(LandScapeParent.transform);
            transform.localPosition = new Vector3(0, 0, 0);
        } 
        else
        {
            transform.SetParent(PortraitParent.transform);
            transform.localPosition = new Vector3(0, 0, 0);
        }
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            NotificationMessage = new GameObject("Toast Message" + i);

            //Add Text & Its Attribute
            NotificationMessage.AddComponent<Text>();
            NotificationMessage.GetComponent<Text>().fontSize = 60;
            NotificationMessage.GetComponent<Text>().font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            NotificationMessage.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            NotificationMessage.GetComponent<Text>().text = "testing" + i;
            ++i;

            NotificationMessage.AddComponent<Image>();

            NotificationMessage.transform.SetParent(transform);
            NotificationMessage.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 100);
            NotificationMessage.transform.localPosition = new Vector3(0, 0, 0);
            NotificationMessage.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            
            if (OrientationController.GetComponent<OrientationControl>().isLandScape == true)
            {
                NotificationMessage.transform.Rotate(0, 0, -90);
            }
            AddToQueue(NotificationMessage);
        }
	}

    public void AddToQueue(GameObject Badges)
    {
        Badges.SetActive(false);
        Queue.Add(Badges);

        if(isDisplayingMessage == false)
        {
            StartCoroutine(DisplayMessage(2, 0.004f, 0));
        }
    }

    IEnumerator DisplayMessage(float WaitTimeBeforeFade, float FadeSpeed, float MaxFadeOut)
    {
        //--------------------------To Be Done Later--------------------------
        isDisplayingMessage = true;
        float StartTime = 0;
        
        while (Queue.Count > 0)
        {
            Queue[0].SetActive(true);
            while (Queue[0].GetComponent<CanvasRenderer>().GetAlpha() > MaxFadeOut)
            {
            if (StartTime > WaitTimeBeforeFade)
            {
                Queue[0].GetComponent<CanvasRenderer>().SetAlpha(Queue[0].GetComponent<CanvasRenderer>().GetAlpha() - FadeSpeed);
            }
            else
            {
                Debug.Log(StartTime+" *");
                StartTime += Time.deltaTime;
            }
            yield return null;
            }
            Destroy(Queue[0]);
            Queue.Remove(Queue[0]);
            StartTime = 0;
        }
        Queue.Clear();
        isDisplayingMessage = false;
        //--------------------------To Be Done Later--------------------------
    }
}
