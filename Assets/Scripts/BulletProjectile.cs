using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 40f;
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        } else
        {
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
