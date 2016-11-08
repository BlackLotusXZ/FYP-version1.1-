using UnityEngine;
using System.Collections;

public class ShopCameraPreview : MonoBehaviour {

    enum CameraMovement
    { 
        ShowCased,
        Zoom,
        Move
    }

    CameraMovement CurrentCameraMovement;

    public GameObject ShowCasePreview;

    //General Variable
    public float CameraSpeed;

    //ShowCase Variable
    private Vector3 LookAt;
    private Vector3 RotateAroundAxis;

    //Zoom Variable
    public float ZoomSpeed;

	// Use this for initialization
	void Start () {
        LookAt = ShowCasePreview.transform.position;
        RotateAroundAxis.Set(0, 1, 0);

        ZoomSpeed = 10;

        CurrentCameraMovement = CameraMovement.ShowCased;
	}
	
	// Update is called once per frame
	void Update () {
        switch (CurrentCameraMovement)
        {
            case CameraMovement.ShowCased:
                {
                    GetComponent<Camera>().transform.LookAt(LookAt);
                    GetComponent<Camera>().transform.RotateAround(LookAt, RotateAroundAxis, Time.deltaTime * CameraSpeed);
                    break;
                }
            case CameraMovement.Zoom:
                {
                    GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, ZoomSpeed, Time.deltaTime * CameraSpeed);
                    break;
                }
            case CameraMovement.Move:
                {
                    break;
                }
        }
	}

    public void ShowCase(Vector3 LookAt, Vector3 RotateAroundAxis)
    {
        //Set Camera Movement
        CurrentCameraMovement = CameraMovement.ShowCased;

        //Set Its Required Variables To Work
        this.LookAt = LookAt;
        this.RotateAroundAxis = RotateAroundAxis;
    }

    public void ZoomIn(float ZoomSpeed)
    {
        //Set Camera Movement
        CurrentCameraMovement = CameraMovement.ShowCased;

        this.ZoomSpeed = ZoomSpeed;
    }
}
