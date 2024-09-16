using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSC : MonoBehaviour
{
    public float speed = 20f; // Speed of the bullet
    public float damage = 10f;

    private Rigidbody rb; // Reference to the Rigidbody component
    public PlayerStats owner; // Player that own that projectile

    void Start()
    {
        // Get the Rigidbody component attached to the bullet
        rb = GetComponent<Rigidbody>();

        // Set the initial velocity of the bullet
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Trigger event when the bullet collides with another collider
    void OnCollisionEnter(Collision collision)
    {
        GameObject objectCollided = collision.gameObject;

        // Ignore collision if the other object is a bullet
        if (objectCollided.CompareTag("Bullet"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return; // Exit to prevent further logic for this collision
        }

        if (objectCollided.CompareTag("Player"))
        {
            PlayerStats collidedPlayerStats = objectCollided.GetComponent<PlayerStats>();
            collidedPlayerStats.takeDamage(damage, owner);
        }

        // Destroy the bullet
        Destroy(gameObject);
    }
}
