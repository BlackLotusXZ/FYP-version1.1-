using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/********************************************/
/**
*  \brief
*   This script is in-charge of all things related to the car
***********************************************/

public class CarControl : MonoBehaviour
{
    public int StartDistance; // Distance to Offset When EndDistance Is Reached
    public int EndDistance;

    public int Acceleration;
    public int MaxVelocity;
    public int RepairSpeed;
    public int WheelSpeed;

    public GameObject EnvironmentHolder;

    private GameObject Car;
    private GameObject CarFrame;
    private List<GameObject> CarTires;
    private SphereCollider BoundingSphere;
    private Rigidbody Body;

    private List<GameObject> DestroyableParts;
    private List<GameObject> DestroyedParts;

    private bool isMoving;

    //Sound
    public float volume;

    public AudioClip RepairSound;
    public AudioClip StartSound; 
    public AudioClip DestroySound;
    public AudioClip DrivingSound;

    public AudioSource source { get { return GetComponent<AudioSource>(); } }


    // Use this for initialization
    void Start()
    {
        CarTires = new List<GameObject>();
        DestroyableParts = new List<GameObject>();
        DestroyedParts = new List<GameObject>();

        gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        if (GameControl.handle.player.SettingsSE_Player == 0)
            source.volume = volume;
        else
            source.volume = 0;
    }

    void Update()
    {
        foreach (GameObject Tire in CarTires)
        {
            Tire.GetComponent<Rigidbody>().angularVelocity = new Vector3(Body.velocity.z, 0, 0);
        }
    }

    //Set .Obj Asset to Instantiate Car With
    public void SetCar(GameObject CarFrame)
    {
        this.CarFrame = CarFrame;
    }

    //Get A Hold of All The Car Tires In The Game
    public void SetWheels()
    {
        foreach (Transform Child in Car.transform)
        {
            if (Child.gameObject.tag == "Wheel")
            {
                CarTires.Add(Child.gameObject);

                Rigidbody Body;
                Body = Child.gameObject.AddComponent<Rigidbody>();
                Body.useGravity = false;
                Body.mass = 5;
                Body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }
    //Get A Hold Of All The Car Parts Under The Car .Obj 
    public void SetDestroyableParts()
    {
        for (int i = 0; i < Car.transform.childCount; ++i)
        {
            if (Car.transform.GetChild(i).tag == "Destroyable")
            {
                DestroyableParts.Add(Car.transform.GetChild(i).gameObject);
            }
        }

    }

    void ResetCarPos()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, EnvironmentHolder.GetComponent<EnvironmentControl>().GetSegmentManager()[EnvironmentHolder.GetComponent<EnvironmentControl>().StartSegment].transform.localPosition.z);
    }

    IEnumerator CarMove()
    {
        while (isMoving)
        {
            if (Body.velocity.magnitude < MaxVelocity)
                Body.AddForce(transform.forward * Acceleration);


            if (transform.position.z > EnvironmentHolder.GetComponent<EnvironmentControl>().GetSegmentManager()[EnvironmentHolder.GetComponent<EnvironmentControl>().EndSegment].transform.localPosition.z)
            {
                ResetCarPos();
                EnvironmentHolder.GetComponent<EnvironmentControl>().SwapSegments(EnvironmentHolder.GetComponent<EnvironmentControl>().StartSegment, EnvironmentHolder.GetComponent<EnvironmentControl>().EndSegment);
                EnvironmentHolder.GetComponent<EnvironmentControl>().SwapSegments(EnvironmentHolder.GetComponent<EnvironmentControl>().StartSegment - 1, EnvironmentHolder.GetComponent<EnvironmentControl>().EndSegment - 1);
                EnvironmentHolder.GetComponent<EnvironmentControl>().SwapSegments(EnvironmentHolder.GetComponent<EnvironmentControl>().StartSegment - 2, EnvironmentHolder.GetComponent<EnvironmentControl>().EndSegment - 2);
                EnvironmentHolder.GetComponent<EnvironmentControl>().SwapSegments(EnvironmentHolder.GetComponent<EnvironmentControl>().StartSegment - 3, EnvironmentHolder.GetComponent<EnvironmentControl>().EndSegment - 3);
            }
            yield return null;
        }
    }

    public void RepairParts()
    {
        source.PlayOneShot(RepairSound);
        if (DestroyedParts.Count > 0)
        {
            int PartToRepair = Random.Range(0, DestroyedParts.Count - 1);

            Debug.Log(DestroyedParts[PartToRepair].name);

            Destroy(DestroyedParts[PartToRepair].GetComponent<Rigidbody>());
            Destroy(DestroyedParts[PartToRepair].GetComponent<BoxCollider>());

            StartCoroutine(LerpBackToDefaultPos(DestroyedParts[PartToRepair]));

            DestroyableParts.Add(DestroyedParts[PartToRepair]);
            DestroyedParts.Remove(DestroyedParts[PartToRepair]);
        }
    }

    IEnumerator LerpBackToDefaultPos(GameObject PartToLerpBack)
    {
        bool isBackToOrigin = false;

        Vector3 Origin = new Vector3(0, 0, 0);

        float newX, newY, newZ;
        PartToLerpBack.transform.localPosition = new Vector3(0, 200, 0);
        PartToLerpBack.transform.rotation = Quaternion.identity;

        while (isBackToOrigin == false)
        {
            Debug.Log("Repairing");
            if ((PartToLerpBack.transform.localPosition - Origin).magnitude > 1)
            {
                newX = Mathf.Lerp(PartToLerpBack.transform.localPosition.x, Origin.x, Time.deltaTime * RepairSpeed);
                newY = Mathf.Lerp(PartToLerpBack.transform.localPosition.y, Origin.y, Time.deltaTime * RepairSpeed);
                newZ = Mathf.Lerp(PartToLerpBack.transform.localPosition.z, Origin.z, Time.deltaTime * RepairSpeed);

                PartToLerpBack.transform.localPosition = new Vector3(newX, newY, newZ);
            }
            else
            {
                isBackToOrigin = true;
                PartToLerpBack.transform.localPosition = Origin;
            }
            yield return null;
        }

    }

    public void DestroyParts()
    {
        source.PlayOneShot(DestroySound);
        if (DestroyableParts.Count > 0)
        {
            int PartToDestroy = Random.Range(0, DestroyableParts.Count - 1);
            Rigidbody BodyPart;
            BodyPart = DestroyableParts[PartToDestroy].AddComponent<Rigidbody>();
            BodyPart.mass = 50f;
            BodyPart.AddForce(0, 2000, 0);
            BodyPart.AddTorque(100, 100, 0);

            BoxCollider BoundingBox;
            BoundingBox = DestroyableParts[PartToDestroy].gameObject.AddComponent<BoxCollider>(); // Add the rigidbody.
            BoundingBox.center = new Vector3(0, 5, 0);
            BoundingBox.size = new Vector3(3, 3, 3);

            DestroyedParts.Add(DestroyableParts[PartToDestroy]);
            DestroyableParts.Remove(DestroyableParts[PartToDestroy]);
        }
    }

    public void StopCar()
    {
        isMoving = false;
    }

    public void StartCar()
    {
        if (isMoving == false)
        {
            source.PlayOneShot(StartSound);
        }
        isMoving = true;
        StartCoroutine(CarMove());
    }

    public void GenerateCar()
    {
        //Create Left Plane
        Car = Instantiate(CarFrame);
        Car.transform.SetParent(transform);
        Car.transform.localPosition = new Vector3(0, 0, 0);
        Car.transform.Rotate(new Vector3(0, 0, 0));
        Car.transform.localScale = new Vector3(3, 3, 3);

        BoundingSphere = transform.gameObject.AddComponent<SphereCollider>(); // Add the rigidbody.
        BoundingSphere.center = new Vector3(0, 0, 0);
        BoundingSphere.radius = 6.5f;

        Body = transform.gameObject.AddComponent<Rigidbody>(); // Add the rigidbody.
        Body.mass = 5; // Set the GO's mass

        Body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        
        ResetCarPos();
        SetWheels();
        SetDestroyableParts();
    }
}