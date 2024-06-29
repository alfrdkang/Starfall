using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet"))
        {
            animator.Play("DoorsOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Bullet"))
        {
            animator.Play("DoorsClose");
        }
    }
}
