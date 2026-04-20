using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public Asteroid asteroidObject;
    public float massAsteroid;
    public float radiusAsteroid;
    public float orbitAsteroid;
    public TrailRenderer trailRenderer;
    public ParticleSystem[] explosionSystems;
    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        asteroidObject = new Asteroid(gameObject, massAsteroid, radiusAsteroid * 0.1f, orbitAsteroid);
        //Creating an instance of the planet class
        trailRenderer.widthMultiplier = radiusAsteroid * 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        asteroidObject.RotateAmount(Time.deltaTime);//Rotation       
        asteroidObject.UpdateOrbital();//Orbit
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planet" || collision.gameObject.tag == "Star")
        {
            foreach (ParticleSystem particle in explosionSystems)
            {
                ParticleSystem instantExp = Instantiate(particle, transform.position, Quaternion.identity);
                ParticleSystem.ShapeModule pshape = instantExp.shape;
                pshape.radius = radiusAsteroid * 0.5f;
                instantExp.Play();
                Destroy(instantExp, 1f);
            }
            Destroy(gameObject);
        }

    }
}
