using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetary : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
