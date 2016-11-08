using UnityEngine;
using System.Collections;


/********************************************/
/**
*  \brief
*   - This script handles the rotation of the object in the display in the shop
***********************************************/
public class ObjRotate : MonoBehaviour
{

    public float speed = 300.0f;
    // Update is called once per frame
    public void RotateBasedOnInput()
    {
        float y = Input.GetAxis("Mouse Y");
        float x = -Input.GetAxis("Mouse X");
        if (Mathf.Abs(y) > Mathf.Abs(x))
            x = 0.0f;
        else
            y = 0.0f;
        transform.Rotate(new Vector3(y, x, 0) * Time.deltaTime * speed, Space.World);

    }
}
