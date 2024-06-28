/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 22/06/2024
 * Date Modified: 24/06/2024
 * Description: Script Attached to Guns, Handles events and gun settings
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    // Gun Statistics
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowBtnHold;
    int bulletsLeft, bulletsShot;

    public bool shooting, readyToShoot, reloading;

    // Refs
    public Camera Cam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask Enemy;
    public BulletProjectile bulletScript;

    // Bullet
    [SerializeField] private Transform bullet;

    // Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;

    public CameraShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI ammoText;
    public Slider ammoBar;
    public GunAnimations animations;

    // Audio
    public AudioClip shootAudio;
    public AudioClip reloadAudio;
    private AudioSource audioSource;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        audioSource = GetComponent<AudioSource>();
        bulletScript.damage = damage;
        UpdateUI();
    }

    private void Update()
    {
        if (allowBtnHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Bullet Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = Cam.transform.forward + new Vector3(x, y, 0);

        animations.WeaponRecoil();
        Instantiate(bullet, attackPoint.position, Quaternion.LookRotation(direction, Vector3.up));

        //Audio
        audioSource.PlayOneShot(shootAudio);

        //Camera Shake
        StartCoroutine(camShake.Shake(camShakeDuration, camShakeMagnitude));

        //Graphics
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        UpdateUI();

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;

        //Audio
        audioSource.PlayOneShot(reloadAudio);

        animations.WeaponReload();

        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        UpdateUI();
        reloading = false;
    }

    public void UpdateUI()
    {
        ammoText.SetText(bulletsLeft.ToString() + "/" + magazineSize.ToString());
        ammoBar.value = bulletsLeft;
        ammoBar.maxValue = magazineSize;
    }
}
