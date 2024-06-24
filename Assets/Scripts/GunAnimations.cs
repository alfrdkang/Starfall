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
}
