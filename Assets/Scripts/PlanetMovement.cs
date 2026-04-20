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
    public ParticleSystem[] explosionSystems;
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
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids)
        {
            Vector3 objPos = asteroid.transform.position;
            float objMass = asteroid.GetComponent<Rigidbody>().mass * 0.5f;
            asteroid.GetComponent<Rigidbody>().AddForce(planetObject.CalculateForce(objPos, objMass));
        }
        //Force applied on each planet
        planetObject.UpdateOrbital();//Orbit
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Star")
        {
            foreach (ParticleSystem particle in explosionSystems)
            {
                ParticleSystem instantExp = Instantiate(particle, transform.position, Quaternion.identity);
                ParticleSystem.ShapeModule pshape = instantExp.shape;
                pshape.radius = radiusPlanet * 0.5f;
                instantExp.Play();
                Destroy(instantExp, 1f);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Planet")
        {
            foreach (ParticleSystem particle in explosionSystems)
            {
                ParticleSystem instantExp = Instantiate(particle, transform.position, Quaternion.identity);
                ParticleSystem.ShapeModule pshape = instantExp.shape;
                pshape.radius = radiusPlanet * 0.5f;
                instantExp.Play();
                Destroy(instantExp, 1f);
            }
            if (collision.gameObject.GetComponent<PlanetMovement>().massPlanet > massPlanet)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
            //Only the heavier planet explodes in collision
        }
    }

}
