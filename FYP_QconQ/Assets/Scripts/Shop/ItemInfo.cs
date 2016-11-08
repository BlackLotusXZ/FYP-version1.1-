using UnityEngine;
using System.Collections;

public class ItemInfo : MonoBehaviour {

    public enum ShowCase
    {
        Gender,
        Car,
        Environment,
        Props
    }
    
    public ShowCase type;
    public Sprite Icon;
    public int Price;
    public bool Locked;
    public bool Selected;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
