/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 22/06/2024
 * Date Modified: 25/06/2024
 * Description: Script Attached to Bullet Prefab
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of a bullet projectile fired by the player.
/// </summary>
public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen; // Visual effect for hitting an enemy.
    [SerializeField] private Transform vfxHitRed;   // Visual effect for hitting other objects.

    private Rigidbody rb;       // Rigidbody component of the bullet.
    private EnemyAI enemy;      // Reference to the EnemyAI script of the enemy hit.
    public int damage;          // Amount of damage this bullet deals.

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component.
        Destroy(gameObject, 5);         // Destroy the bullet after 5 seconds to prevent leaks.
    }

    private void Start()
    {
        float speed = 40f;               // Speed of the bullet.
        rb.velocity = transform.forward * speed; // Set the initial velocity of the bullet.
    }

    /// <summary>
    /// Triggered when the bullet collides with another collider.
    /// </summary>
    /// <param name="other">The collider the bullet collided with.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("TriggerZone")) // Check if the collider is not a trigger zone.
        {
            if (other.CompareTag("Enemy"))  // If the collider is an enemy.
            {
                enemy = other.gameObject.GetComponent<EnemyAI>(); // Get the EnemyAI component of the enemy.
                enemy.TakeDamage(damage);   // Deal damage to the enemy.
                Debug.Log(other.gameObject.name + " took " + damage + " points of damage!");

                Instantiate(vfxHitGreen, transform.position, Quaternion.identity); // Instantiate green hit VFX.
            }
            else
            {
                Debug.Log(other.gameObject.name);
                Instantiate(vfxHitRed, transform.position, Quaternion.identity); // Instantiate red hit VFX for other objects.
            }
            Destroy(gameObject); // Destroy the bullet after hitting something.
        }
    }
}
