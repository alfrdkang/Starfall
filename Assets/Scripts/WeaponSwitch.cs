/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 24/06/2024
 * Date Modified: 25/06/2024
 * Description: Active weapon management and switching
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class WeaponSwitch : MonoBehaviour
{
    private GunAnimations animations;

    public StarterAssetsInputs _input;
    public GameObject primary;
    public GameObject secondary;

    private Gun primaryGun;
    private Gun secondaryGun;
    public BulletProjectile bullet;

    private void Awake()
    {
        animations = GetComponent<GunAnimations>();

        primaryGun = primary.GetComponent<Gun>();
        secondaryGun = secondary.GetComponent<Gun>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(_input.primary && !primaryGun.reloading && !secondaryGun.reloading)  
        {
            primary.SetActive(true);
            secondary.SetActive(false);
            bullet.damage = primaryGun.damage;

            animations.WeaponPullout();
            _input.primary = false;
        }
        if (_input.secondary && !primaryGun.reloading && !secondaryGun.reloading)
        {
            secondary.SetActive(true);
            primary.SetActive(false);
            bullet.damage = secondaryGun.damage;

            animations.WeaponPullout();
            _input.secondary = false;
        }
    }
}
