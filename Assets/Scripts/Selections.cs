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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateScreen(Planet planet)
    {
        Dictionary<string, float> allData = planet.ObjectData();
        FixedActivate(allData);
        rotationSpeedText.text = "Rotational Speed: " + allData["rotationSpeed"];
        orbitalSpeedText.text = "Orbital Speed: " + allData["orbitalSpeed"];
    } 

    public void ActivateScreen(Star star)
    {
        Dictionary<string, float> allData = star.ObjectData();
        FixedActivate(allData);
        rotationSpeedText.text = "Rotational Speed: " + allData["rotationSpeed"];
        orbitalSpeedText.text = string.Empty;
    }

    public void FixedActivate(Dictionary<string, float> entryData)
    {
        massText.text = "Mass: " + entryData["mass"];
        radiusText.text = "Radius: " + entryData["radius"];
    }

}
