using UnityEngine;
using System.Collections;


/********************************************/
/**
*  \brief
*   - This script handles anything happening with the rigidbodies on the car, i.e its parts
***********************************************/
public class RigidBodyControl : MonoBehaviour {

    Rigidbody ri;
    private bool stopRepair = false;
    Transform OriginalTransform;

    bool destroyed = false;

    public bool IsDestroyed() // returns if the part is destroyed
    {
        return destroyed;
    }

    public void DestroyPart() // add rigidbody for physics and set destroyed to true
    {
        AddRigidB();
        destroyed = true;
        //this.GetComponent<Collider>().enabled = true;

        this.GetComponent<PartFallSound>().PlaySoundEffect();

    }

    public void RepairPart() // remove the rigidbody and turn off stoprepair , also set destroyed to false and start making parts go back
    {
        DestroyRigidB();
        stopRepair = false;
        destroyed = false;
        StartCoroutine(SnapPartBack());

        //this.GetComponent<Collider>().enabled = false;
    }

    public void setOriTransform(Transform ori) // set original transform to lerp to when repaired
    {
        OriginalTransform = ori;
    }

    void AddRigidB()
    {
        // stop putting it back to the car 
        stopRepair = true;
        
        StopCoroutine(SnapPartBack());

        // add the rigid body
        ri = gameObject.AddComponent<Rigidbody>();
        ri.mass = 1.0f;
        ri.interpolation = RigidbodyInterpolation.Interpolate;
        ri.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // add force so that when the parts are destroyed it has a force to move at a certain direction
        ri.AddForce(0,10.0f,-10.0f,ForceMode.Impulse);
    }

    void DestroyRigidB()
    {
        if(ri != null) // remove rigidbody component if it exists
        {
            Destroy(ri);
            ri = null;
        }

        transform.localPosition = new Vector3(0, 100.0f, 0); // set it to very high up so it can fall and lerp back
        
    }

    IEnumerator SnapPartBack()
    {
        while (!stopRepair) // lerp the position and rotation to the original transform 
        {
            this.transform.position = Vector3.Lerp(this.transform.position, OriginalTransform.position, Time.deltaTime * 5.0f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, OriginalTransform.rotation, Time.deltaTime * 5.0f);

            yield return null;
        }

        yield return null;
    }

}
