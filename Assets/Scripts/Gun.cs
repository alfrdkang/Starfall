using UnityEngine;
using TMPro;
using StarterAssets;

public class Gun : MonoBehaviour
{
    public GameObject player;
    private FirstPersonController firstPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    // Gun Statistics
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowBtnHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    private Vector3 initialPos;
    private Quaternion initialRot;

    // Refs
    public Camera Cam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask Enemy;

    // Bullet
    [SerializeField] private Transform bullet;

    // Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    //public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI ammoText;

    [Header("Settings")]
    public bool sway;
    public bool swayRotation;
    public bool bobOffset;
    public bool bobSway;

    [Header("Sway")]
    public float step = 0.01f; //multiply by value from mouse for one frame
    public float maxStepDist = 0.05f; //max dist from local origin
    Vector3 swayPos;

    [Header("Sway Rotation")]
    public float rotationStep = 4f; //multiply by value from mouse for one frame
    public float maxRotationStep = 5f; //max rotation from local identity rotation
    Vector3 swayEulerRot;

    [Header("Bobbing")]
    public float speedCurve;
    /// <summary>
    /// Curve Sin
    /// </summary>
    private float curveSin { get => Mathf.Sin(speedCurve); }
    /// <summary>
    /// Curve Cos
    /// </summary>
    private float curveCos { get => Mathf.Cos(speedCurve); }
    /// <summary>
    /// max mouseinput travel limit
    /// </summary>
    public Vector3 travelLimit = Vector3.one * 0.025f;
    /// <summary>
    /// bob over time travel limit
    /// </summary>
    public Vector3 bobLimit = Vector3.one * 0.01f;
    Vector3 bobPosition;

    [Header("Bob Rotation")]
    public Vector3 multiplier;
    Vector3 bobEulerRotation;


    private float smooth = 10f;
    private float smoothRot = 12f;
    private void CompositePositionRotation()
    {
        //position
        transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos + bobPosition, Time.deltaTime * smooth);

        //rotation
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRot) * Quaternion.Euler(bobEulerRotation), Time.deltaTime * smoothRot);
    }

    /// <summary>
    /// x,y,z pos change on player move
    /// </summary>
    private void BobOffset()
    {
        speedCurve += Time.deltaTime * (firstPersonController.Grounded ? firstPersonController._rotationVelocity : 1f) + 0.01f; //generate waves

        if (bobOffset == false)
        {
            bobPosition = Vector3.zero; return;
        }

        bobPosition.x = (curveCos * bobLimit.x * (firstPersonController.Grounded ? 1 : 0)) - starterAssetsInputs.move.x * travelLimit.x;
        bobPosition.y = (curveSin * bobLimit.y) - (firstPersonController._verticalVelocity * travelLimit.y);
        bobPosition.z = -(starterAssetsInputs.move.y * travelLimit.z);
    }

    /// <summary>
    /// roll,pitch,yaw on player move
    /// </summary>
    private void BobRotation()
    {
        if (bobSway == false)
        {
            bobEulerRotation = Vector3.zero; return;
        }

        bobEulerRotation.x = (starterAssetsInputs.move != Vector2.zero ? multiplier.x * Mathf.Sin(2*speedCurve) : multiplier.x * (Mathf.Sin(2 * speedCurve) / 2));
        bobEulerRotation.y = (starterAssetsInputs.move != Vector2.zero ? multiplier.y * curveCos : 0);
        bobEulerRotation.z = (starterAssetsInputs.move != Vector2.zero ? multiplier.z * curveCos * starterAssetsInputs.move.x : 0);
    }

    private void Awake()
    {
        firstPersonController = player.GetComponent<FirstPersonController>();
        starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();

        initialPos = new Vector3 (transform.position.x, -0.3f, transform.position.z);
        initialRot = transform.rotation;

        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        Sway();
        SwayRotation();
        BobOffset();
        BobRotation();

        CompositePositionRotation();

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

    /// <summary>
    /// x,y,z pos change as player moves mouse
    /// </summary>
    void Sway()
    {
        if(sway == false)
        {
            swayPos = Vector3.zero; return;
        }
        Vector3 invertLook = starterAssetsInputs.look * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDist, maxStepDist);
        invertLook.x = Mathf.Clamp(invertLook.y, -maxStepDist, maxStepDist);

        swayPos = invertLook + initialPos;
    }

    /// <summary>
    /// roll,pitch,yaw change as player moves mouse
    /// </summary>
    private void SwayRotation()
    {
        if (swayRotation == false)
        {
            swayEulerRot = Vector3.zero; return;
        }
        Vector2 invertLook = starterAssetsInputs.look * -rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
        invertLook.x = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);

        swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x) + initialRot.eulerAngles;
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
        //camShake.Shake(camShakeDuration, camShakeMagnitude);

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
