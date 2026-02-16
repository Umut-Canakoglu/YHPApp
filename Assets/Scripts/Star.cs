using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : CelestialBody
{
    
    public Star(GameObject starEntry, float starMass, float starRadius, float starSpeed)
        : base(starEntry, starMass * 10, starRadius * 3, starSpeed / 2, 0f)
    {//Stars don't orbit around objects
    }
    //These multiplications are to make sure that stars are heavier and larger

}
