using UnityEngine;
using TMPro;
using StarterAssets;

public class Gun : MonoBehaviour
{
    // Gun Statistics
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowBtnHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    // Refs
    public Camera Cam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask Enemy;

    // Bullet
    [SerializeField] private Transform bullet;

    // Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;

    public CameraShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI ammoText;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
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

        ammoText.SetText(bulletsLeft.ToString() + "/" + magazineSize.ToString());
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Bullet Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = Cam.transform.forward + new Vector3(x, y, 0);

        Instantiate(bullet, attackPoint.position, Quaternion.LookRotation(direction, Vector3.up));

        //Camera Shake
        StartCoroutine(camShake.Shake(camShakeDuration, camShakeMagnitude));

        //Graphics
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

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
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
