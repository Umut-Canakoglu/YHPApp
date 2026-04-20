using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selections : MonoBehaviour
{
    public TextMeshProUGUI massText;
    public TextMeshProUGUI radiusText;
    public TextMeshProUGUI rotationSpeedText;
    public TextMeshProUGUI orbitalSpeedText;

    public void ActivateScreen(Planet planet)
    //Using overload to take different objects as parameters
    {
        Dictionary<string, float> allData = planet.ObjectData();
        FixedActivate(allData);
        rotationSpeedText.text = "Rotational Speed: " + allData["rotationSpeed"];
        orbitalSpeedText.text = "Orbital Speed: " + allData["orbitalSpeed"];
        //Pulling data from the object to put it on the screen
    }

    public void ActivateScreen(Star star)
    {
        Dictionary<string, float> allData = star.ObjectData();
        FixedActivate(allData);
        rotationSpeedText.text = "Rotational Speed: " + allData["rotationSpeed"];
        orbitalSpeedText.text = string.Empty;
    }

    public void ActivateScreen(Asteroid asteroid)
    {
        Dictionary<string, float> allData = asteroid.ObjectData();
        FixedActivate(allData);
        rotationSpeedText.text = "Orbital Speed: " + allData["orbitalSpeed"];
        rotationSpeedText.text = string.Empty;
    }

    public void FixedActivate(Dictionary<string, float> entryData)
    {
        //The change on mass and radius are applicable to every object.
        massText.text = "Mass: " + entryData["mass"];
        radiusText.text = "Radius: " + entryData["radius"];
    }

}
