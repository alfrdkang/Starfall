/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 22/06/2024
 * Date Modified: 22/06/2024
 * Description: Gun Animator Script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimations : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void WeaponRecoil()
    {
        animator.Play("Recoil");   
    }

    public void WeaponReload()
    {
        animator.Play("ReloadPlus");
    }

    public void WeaponPullout()
    {
        animator.Play("Pullout");
    }
}
