using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGroundEffects : MonoBehaviour
{

    public Vector3 rotationAxis = Vector3.up; // Axis around which the object will rotate
    public float rotationSpeed = 45f; // Rotation speed in degrees per second
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        // Rotate the GameObject around the specified axis at a fixed speed
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}


