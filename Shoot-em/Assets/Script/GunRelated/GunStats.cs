using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStats : MonoBehaviour
{
    public GameObject[] muzzles;
    public GameObject bullet;
    public float fireRate; // The rate of fire (in seconds per shot)
    public float spreadAngle = 0f; // Spread amount in degrees
    // public AudioClip fireSound;  // Audio clip to play when firing
    public AudioSource audioSource;
    
    public float damageMultipler;
    public float speedMultipler;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
