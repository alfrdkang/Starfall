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

/// <summary>
/// Controls the behavior and settings of a gun attached to a GameObject.
/// </summary>
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

    // References
    public Camera Cam; // Reference to the camera used for shooting
    public Transform attackPoint; // Point where bullets are spawned
    public RaycastHit rayHit; // Stores information about the hit object
    public LayerMask Enemy; // Layer mask to filter out enemies
    public BulletProjectile bulletScript; // Script handling bullet properties

    // Bullet
    [SerializeField] private Transform bullet; // Prefab of the bullet object

    // Graphics
    public GameObject muzzleFlash; // Muzzle flash effect
    public GameObject bulletHoleGraphic; // Bullet hole graphic for environment

    public CameraShake camShake; // Camera shake effect
    public float camShakeMagnitude, camShakeDuration; // Magnitude and duration of camera shake
    public TextMeshProUGUI ammoText; // UI text displaying ammo count
    public Slider ammoBar; // UI slider displaying ammo bar
    public GunAnimations animations; // Gun animation controller

    // Audio
    public AudioClip shootAudio; // Sound clip for shooting
    public AudioClip reloadAudio; // Sound clip for reloading
    private AudioSource audioSource; // Audio source component

    private void Awake()
    {
        bulletsLeft = magazineSize; // Initialize bullets left in magazine
        readyToShoot = true; // Gun is ready to shoot initially
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
        bulletScript.damage = damage; // Set bullet damage
        UpdateUI(); // Update UI elements
    }

    private void Update()
    {
        if (allowBtnHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0); // Check if shoot button is held down
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0); // Check if shoot button is pressed
        }

        // Handle shooting logic
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap; // Set number of bullets to shoot
            Shoot(); // Perform shooting
        }

        // Handle reloading logic
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload(); // Perform reloading
        }
    }

    private void Shoot()
    {
        readyToShoot = false; // Gun is not ready to shoot until cooldown

        // Apply bullet spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 direction = Cam.transform.forward + new Vector3(x, y, 0);

        animations.WeaponRecoil(); // Trigger recoil animation
        Instantiate(bullet, attackPoint.position, Quaternion.LookRotation(direction, Vector3.up)); // Spawn bullet

        // Play shooting audio
        audioSource.PlayOneShot(shootAudio);

        // Apply camera shake effect
        StartCoroutine(camShake.Shake(camShakeDuration, camShakeMagnitude));

        // Display muzzle flash
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--; // Reduce bullets left
        bulletsShot--;

        UpdateUI(); // Update UI elements
        Invoke("ResetShot", timeBetweenShooting); // Cooldown between shots

        // Continue shooting if more bullets are needed
        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true; // Gun is ready to shoot again
    }

    private void Reload()
    {
        reloading = true; // Gun is currently reloading

        // Play reload audio
        audioSource.PlayOneShot(reloadAudio);

        animations.WeaponReload(); // Trigger reload animation

        Invoke("ReloadFinished", reloadTime); // Finish reloading after specified time
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize; // Refill magazine
        UpdateUI(); // Update UI elements
        reloading = false; // Finish reloading
    }

    /// <summary>
    /// Updates the UI elements to display current ammo information.
    /// </summary>
    public void UpdateUI()
    {
        ammoText.SetText(bulletsLeft.ToString() + "/" + magazineSize.ToString()); // Update ammo text
        ammoBar.value = bulletsLeft; // Update ammo bar value
        ammoBar.maxValue = magazineSize; // Set max value of ammo bar
    }
}
