using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PropControl : MonoBehaviour {

    public List<GameObject> ListOfUsableProps;
    public Camera Camera;
    public Vector3 MinArea;
    public Vector3 MaxArea;
    public int RoadLength;
    public int NumberOfProps;

    private List<GameObject> PropsInGame;
    private int i = 0;
	// Use this for initialization
	void Start () {
        PropsInGame = new List<GameObject>();
        ListOfUsableProps = new List<GameObject>();
	}

    public void SetProp(List<GameObject> Props)
    {
        this.ListOfUsableProps = Props;
    }

	// Update is called once per frame
	void Update () {
        
	}

    public void GenerateProps()
    {
        if(PropsInGame.Count > 0 )
        {
            foreach (GameObject Prop in PropsInGame)
            {
                Destroy(Prop);
            }
        }
        

        for (int i = 0; i < NumberOfProps; ++i)
        {
            //Create Left Plane
            GameObject Prop = Instantiate(ListOfUsableProps[(int)Random.Range(0, ListOfUsableProps.Count - 1)]);
           
            int RandX = (int)Random.Range(MinArea.x, MaxArea.x);
            int RandZ = (int)Random.Range(MinArea.z, MaxArea.z);

            Prop.transform.SetParent(transform);
            Prop.transform.localPosition = new Vector3(RoadLength * 1 + RandX / 1 + Mathf.Abs(RandX) + RandX, 0, RandZ);
            Prop.SetActive(true);
            Prop.transform.localScale = new Vector3(500, 500, 500);
            PropsInGame.Add(Prop);
        }
    }
}
