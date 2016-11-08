using UnityEngine;
using System.Collections;

public class GenderControl : MonoBehaviour {

    private GameObject GenderObject;

    private GameObject Gender;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetGender(GameObject GenderObject)
    {
        this.GenderObject = GenderObject;
    }

    public void GenerateGender()
    {
        //Create Left Plane
        Gender = Instantiate(GenderObject);
        Gender.transform.SetParent(transform);
        Gender.transform.localPosition = new Vector3(0, 0, 0);
        Gender.transform.Rotate(new Vector3(0, 0, 0));
        Gender.transform.localScale = new Vector3(110, 110, 110);
    }
}
