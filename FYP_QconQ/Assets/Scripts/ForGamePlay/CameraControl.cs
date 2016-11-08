using UnityEngine;
using System.Collections;


/********************************************/
/**
*  \brief
*   This script is in charge of the in-game camera (zoom-in, zoom-out, look-at)
***********************************************/
[ExecuteInEditMode]
public class CameraControl : MonoBehaviour {

    //public GameObject CameraRotation;

    public float ZoomSpeed = 50;

    public float ZoomInDist = 20.0f;
    public float ZoomOutDist = 40.0f;

    //void Start()
    //{
    //    StartCoroutine(fixRotation());
    //}

    void Update()
    {
        this.transform.LookAt(transform.parent);
    }

    //IEnumerator fixRotation()
    //{
    //    while(true)
    //    {
    //        this.transform.rotation = CameraRotation.transform.rotation;
    //        yield return null;
    //    }
    //}

    public void ZoomIn()
    {
        stopZoom();
        StartCoroutine(ZoomIn(ZoomInDist));
    }

    public void ZoomOut()
    {
        stopZoom();
        StartCoroutine(ZoomOut(ZoomOutDist));
    }

    void stopZoom()
    {
        StopCoroutine(ZoomIn(1));
        StopCoroutine(ZoomOut(1));
    }

    IEnumerator ZoomIn(float length)
    {
        Vector3 dir = this.transform.parent.position - this.transform.position;

        while (dir.magnitude > length)
        {
            this.transform.position += dir * Time.deltaTime * ZoomSpeed;

            dir = this.transform.parent.position - this.transform.position;

            yield return null;
        }
    }

    IEnumerator ZoomOut(float length)
    {
        Vector3 dir = this.transform.parent.position - this.transform.position;

        while(dir.magnitude < length)
        {
            this.transform.position -= dir * Time.deltaTime * ZoomSpeed;

            dir = this.transform.parent.position - this.transform.position;

            yield return null;
        }


    }



}
