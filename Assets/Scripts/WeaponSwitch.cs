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
            animations.WeaponPullout();
            _input.primary = false;
        }
        if (_input.secondary && !primaryGun.reloading && !secondaryGun.reloading)
        {
            secondary.SetActive(true);
            primary.SetActive(false);
            animations.WeaponPullout();
            _input.secondary = false;
        }
    }
}
