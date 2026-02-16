using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    public Planet planetObject;
    public float massPlanet;
    public float radiusPlanet;
    public float rotationPlanet;
    public float orbitPlanet;
    // Start is called before the first frame update
    void Start()
    {
        planetObject = new Planet(gameObject, massPlanet, radiusPlanet, rotationPlanet, orbitPlanet);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        planetObject.RotateAmount(Time.deltaTime);
        foreach (GameObject planet in planets)
        {
            if (gameObject == planet)
            {
                continue;
            }
            Vector3 objPos = planet.transform.position;
            float objMass = planet.GetComponent<Rigidbody>().mass;
            planet.GetComponent<Rigidbody>().AddForce(planetObject.CalculateForce(objPos, objMass));
        }
        planetObject.UpdateOrbital();
    }

}
