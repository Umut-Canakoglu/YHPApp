using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovement : MonoBehaviour
{
    public Star starObject;
    public float massStar;
    public float radiusStar;
    public float rotationStar;

    void Start()
    {
        starObject = new Star(gameObject, massStar, radiusStar, rotationStar);
    }

    void Update()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject planet in planets)
        {
            Vector3 objPos = planet.transform.position;
            float objMass = planet.GetComponent<Rigidbody>().mass;
            planet.GetComponent<Rigidbody>().AddForce(starObject.CalculateForce(objPos, objMass));
        }
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids)
        {
            Vector3 objPos = asteroid.transform.position;
            float objMass = asteroid.GetComponent<Rigidbody>().mass * 0.5f;
            asteroid.GetComponent<Rigidbody>().AddForce(starObject.CalculateForce(objPos, objMass));
        }
        starObject.RotateAmount(Time.deltaTime);
    }
}
