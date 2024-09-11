using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGroundSC : MonoBehaviour
{

    public Vector3 rotationAxis = Vector3.up; // Axis around which the object will rotate
    public float rotationSpeed = 45f; // Rotation speed in degrees per second

    public GameObject[] gunPrefabs;
    public GameObject gunPrefab;
    // Position to spawn the gun
    public Transform spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomGun();
    }

    // Method to spawn a random gun from the list
    void SpawnRandomGun()
    {
        // Check if there are any guns in the list
        if (gunPrefabs.Length == 0)
        {
            Debug.LogWarning("No gun prefabs assigned in the inspector!");
            return;
        }

        // Select a random index from the list
        int randomIndex = Random.Range(0, gunPrefabs.Length);

        // Instantiate the selected gun at the desired position and rotation
         gunPrefab = Instantiate(gunPrefabs[randomIndex], spawnPosition.transform);
    }

    public void GetPickedUp(){
        Destroy(gameObject);
    }

    void Update()
    {
        // Rotate the GameObject around the specified axis at a fixed speed
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);


    }
}
