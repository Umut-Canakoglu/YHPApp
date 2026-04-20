using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : CelestialBody
{

    public Asteroid(GameObject asteroidEntry, float asteroidMass, float asteroidRadius, float asteroidSpeed)
        : base(asteroidEntry, asteroidMass, asteroidRadius, 0f, asteroidSpeed)
    {
    }

}
