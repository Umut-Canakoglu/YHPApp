using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddObject : MonoBehaviour
{
    public GameObject selectionScreen;
    public GameObject setScreenPlanet;
    public GameObject setScreenStar;
    public GameObject planet;
    public GameObject star;
    public float mass;
    public float radius;
    public float rotational;
    public float orbital;
    public Vector3 mousePos;

    public void ActivatePlanetSet()
    //Open setting screen where you can determine the variables of a new object
    {
        setScreenPlanet.SetActive(true);
        setScreenStar.SetActive(false);
    }

    public void ActivateStarSet()
    {
        setScreenStar.SetActive(true);
        setScreenPlanet.SetActive(false);
    }

    public void SetPlanet()
    {
        //Create a new instance of the classes
        GameObject planetInst = Instantiate(planet, mousePos, transform.rotation);
        planetInst.GetComponent<PlanetMovement>().massPlanet = mass;
        planetInst.GetComponent<PlanetMovement>().radiusPlanet = radius;
        planetInst.GetComponent<PlanetMovement>().rotationPlanet = rotational;
        planetInst.GetComponent<PlanetMovement>().orbitPlanet = orbital;
        selectionScreen.SetActive(false);
        setScreenPlanet.SetActive(false);
    }

    public void SetStar()
    {
        GameObject starInst = Instantiate(star, mousePos, transform.rotation);
        starInst.GetComponent<StarMovement>().massStar = mass;
        starInst.GetComponent<StarMovement>().radiusStar = radius;
        starInst.GetComponent<StarMovement>().rotationStar = rotational;
        selectionScreen.SetActive(false);
        setScreenStar.SetActive(false);
    }

    public void SetMass(string s)
    //Taking inputs from fields
    {
        mass = float.Parse(s);
    }

    public void SetRadius(string s)
    {
        radius = float.Parse(s);
    }

    public void SetRotational(string s)
    {
        rotational = float.Parse(s);
    }

    public void SetOrbital(string s)
    {
        orbital = float.Parse(s);
    }

    public void CloseScreen()
    {
        //Closing all placement screens after the player thinks they are finished
        selectionScreen.SetActive(false);
        setScreenPlanet.SetActive(false);
        setScreenStar.SetActive(false);
    }
}
