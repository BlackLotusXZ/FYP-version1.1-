using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

    private Vector3 currentMousePos = new Vector3();
    private Vector3 prevMousePos = new Vector3();
    private Vector3 MousePosdelta = new Vector3();
    private Vector3 MouseWorldPos = new Vector3();
    private float HorizontalForce = 0.0f;
    public Camera cam;

    public void DragLiao()
    {
       // currentMousePos = Input.mousePosition;
      //  MousePosdelta = currentMousePos - prevMousePos;
      //  MouseWorldPos = cam.ScreenToWorldPoint(currentMousePos);
    }

    public void CheckDragHorizontal()
    {
        HorizontalForce = Vector3.Dot(MousePosdelta, Vector3.left);
    }

    void LateUpdate()
    {
        prevMousePos = currentMousePos;
        //MousePosdelta = Vector3.zero;
    }

    public void setMouseDelta(Vector3 v)
    {
        MousePosdelta = v;
    }

    public Vector3 getMouseDelta()
    {
        return MousePosdelta;
    }

    public Vector3 getMouseWorldPos()
    {
        return MouseWorldPos;
    }

    public float getHorizontalForce()
    {
        return HorizontalForce;
    }

    public void setHorizontalForce(float h)
    {
        HorizontalForce = h;
    }
}
