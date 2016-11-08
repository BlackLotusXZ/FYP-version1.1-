using UnityEngine;
using System.Collections;

public class LoadingAnim : MonoBehaviour {

    public bool stopLoading = false;
    public float RotateSpeed = 50.0f;
    private Vector3 r = new Vector3();

    public void RotateRing()
    {
        StartCoroutine(rotatethering());
    }

    IEnumerator rotatethering()
    {
        while (!stopLoading)
        {
            r.z = Time.deltaTime * RotateSpeed;
            transform.Rotate(r);

            yield return null;
        }

        yield return null;
    }
}
