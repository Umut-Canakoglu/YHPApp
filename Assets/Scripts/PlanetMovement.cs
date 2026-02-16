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
        //Creating an instance of the planet class
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        planetObject.RotateAmount(Time.deltaTime);//Rotation
        foreach (GameObject planet in planets)
        {
            if (gameObject == planet)
            {
                continue;//An object shouldn't apply force on itself
            }
            Vector3 objPos = planet.transform.position;
            float objMass = planet.GetComponent<Rigidbody>().mass;
            planet.GetComponent<Rigidbody>().AddForce(planetObject.CalculateForce(objPos, objMass));
        }
        //Force applied on each planet
        planetObject.UpdateOrbital();//Orbit
    }

}
