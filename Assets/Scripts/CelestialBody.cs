using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CelestialBody //Parent class for all objectd
{
    protected float mass; //I want child classes to also access these variables
    protected float radius;
    protected GameObject gameObject;
    protected Rigidbody rb;
    protected Transform transform;
    protected float rotation;
    protected float rotationSpeed;
    protected float orbitalSpeed;

    public CelestialBody(GameObject body, float bodyMass, float bodyRadius, float bodySpeed, float moveSpeed)
    {
        mass = bodyMass;
        radius = bodyRadius;
        gameObject = body;
        rotationSpeed = bodySpeed;
        orbitalSpeed = moveSpeed;

        rb = gameObject.GetComponent<Rigidbody>();//This is a class in UnityEngine library that is used for physics
        transform = gameObject.GetComponent<Transform>();//This is a class in UnityEngine library that is for placement
        transform.localScale = Vector3.one * radius;//Adjusting size according to radius
        rb.velocity = body.transform.forward * orbitalSpeed;//Adjusting speed according to initial speed
    }

    public Vector3 CalculateForce(Vector3 otherPosition, float objMass)
    {
        Vector3 difference = transform.position - otherPosition;
        float distance = Mathf.Sqrt(Mathf.Pow(otherPosition.x, 2) + Mathf.Pow(otherPosition.y, 2) + Mathf.Pow(otherPosition.z, 2));
        Vector3 normal = difference / distance;

        float forceMagnitude = (2f) * (objMass) * mass / (Mathf.Pow(distance, 2));
        //I didn't use the exact G-constant because in reality these objects have massive distances between them
        //and users wouldn't be able to traverse the simulation if I used those distance
        return normal * forceMagnitude;
    }

    public void RotateAmount(float timeDiff)
    {
        //This is for objects having a rotation around themselves
        rotation += timeDiff * rotationSpeed;
        if (rotation >= 360f)
        {
            rotation -= 360f;
        }
        rb.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public Dictionary<string, float> ObjectData()
    {
        //This for me to quickly acces the private information inside the class
        Dictionary<string, float> objectData = new Dictionary<string, float>();
        objectData.Add("mass", mass);
        objectData.Add("radius", radius);
        objectData.Add("rotationSpeed", rotationSpeed);
        objectData.Add("orbitalSpeed", orbitalSpeed);
        return objectData;
    }

    public void UpdateOrbital()
    {
        //This is to update the speed of the objects as they change when new objects are added
        orbitalSpeed = rb.velocity.magnitude;
    }
}

