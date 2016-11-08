using UnityEngine;
using System.Collections;


/********************************************/
/**
*  \brief
*   - This script is in charge of just spinning the end-game medal
***********************************************/
public class GearRotate : MonoBehaviour
{

    public float RotateSpeed = 10.0f;
    public float rotateClockwise = 1;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(rotatePls());
    }

    IEnumerator rotatePls()
    {
        while (true) // rotate based on clockwise/anticlockwise
        {
            transform.Rotate(0, RotateSpeed * Time.deltaTime * 20.0f * rotateClockwise, 0);
            yield return null;
        }
    }
}