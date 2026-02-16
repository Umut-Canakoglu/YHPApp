using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : CelestialBody
{

    public Planet(GameObject planetEntry, float planetMass, float planetRadius, float planetSpeed, float planetOrbSpeed)
        : base(planetEntry, planetMass, planetRadius, planetSpeed, planetOrbSpeed)
    {
    }
    
}
