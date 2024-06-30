/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 22/06/2024
 * Date Modified: 22/06/2024
 * Description: Gun Animator Script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the animation states of a gun using an Animator component.
/// </summary>
public class GunAnimations : MonoBehaviour
{
    private Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get the Animator component on awake
    }

    /// <summary>
    /// Plays the recoil animation of the gun.
    /// </summary>
    public void WeaponRecoil()
    {
        animator.Play("Recoil"); // Play the recoil animation
    }

    /// <summary>
    /// Plays the reload animation of the gun.
    /// </summary>
    public void WeaponReload()
    {
        animator.Play("ReloadPlus"); // Play the reload animation
    }

    /// <summary>
    /// Plays the pullout animation of the gun.
    /// </summary>
    public void WeaponPullout()
    {
        animator.Play("Pullout"); // Play the pullout animation
    }
}
