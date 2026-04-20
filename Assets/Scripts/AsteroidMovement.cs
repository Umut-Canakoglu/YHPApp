using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public Asteroid asteroidObject;
    public float massAsteroid;
    public float radiusAsteroid;
    public float orbitAsteroid;
    public TrailRenderer trailRenderer;//A trail for the asteroid
    public ParticleSystem[] explosionSystems;
    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        asteroidObject = new Asteroid(gameObject, massAsteroid, radiusAsteroid * 0.1f, orbitAsteroid);
        //Creating an instance of the asteroid class
        trailRenderer.widthMultiplier = radiusAsteroid * 0.15f;//The trail is affected by the asteroid's size
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
            //Explosions are simulated by using four parts: spark, rocks, flash, and fire
            foreach (ParticleSystem particle in explosionSystems)
            {
                //Using a for loop to iterate through each particle system
                ParticleSystem instantExp = Instantiate(particle, transform.position, Quaternion.identity);
                ParticleSystem.ShapeModule pshape = instantExp.shape;
                pshape.radius = radiusAsteroid * 0.5f;//The size of the collisions are affected by the object's size
                instantExp.Play();
                Destroy(instantExp, 1f);
                //Destroying the particle effects after a certain time (1s)
            }
            Destroy(gameObject);
            //Destroying the object after collision
        }

    }
}
