using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CelestialBody
{
    protected float mass;
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

        rb = gameObject.GetComponent<Rigidbody>();
        transform = gameObject.GetComponent<Transform>();
        transform.localScale = Vector3.one * radius;
        rb.velocity = body.transform.forward * orbitalSpeed;
    }

    public Vector3 CalculateForce(Vector3 otherPosition, float objMass)
    {
        Vector3 difference = transform.position - otherPosition;
        float distance = Mathf.Sqrt(Mathf.Pow(otherPosition.x, 2) + Mathf.Pow(otherPosition.y, 2) + Mathf.Pow(otherPosition.z, 2));
        Vector3 normal = difference / distance;

        float forceMagnitude = (2f) * (objMass) * mass / (Mathf.Pow(distance, 2));
        return normal * forceMagnitude;
    }

    public void RotateAmount(float timeDiff)
    {
        rotation += timeDiff * rotationSpeed;
        if (rotation >= 360f)
        {
            rotation -= 360f;
        }
        rb.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public Dictionary<string, float> ObjectData()
    {
        Dictionary<string, float> objectData = new Dictionary<string, float>();
        objectData.Add("mass", mass);
        objectData.Add("radius", radius);
        objectData.Add("rotationSpeed", rotationSpeed);
        objectData.Add("orbitalSpeed", orbitalSpeed);
        return objectData;
    }

    public void UpdateOrbital()
    {
        orbitalSpeed = rb.velocity.magnitude;
    }
}

