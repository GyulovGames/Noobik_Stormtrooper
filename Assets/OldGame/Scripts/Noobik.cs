using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using YG;

public class Noobik : MonoBehaviour
{
    [SerializeField] private float neckMaxAngle;
    [SerializeField] private float sholderMaxAngle;
    [SerializeField] private float spineMaxAngle;
    [Space(25)]
    [SerializeField] private Transform neckTransform;
    [SerializeField] private Transform sholderTransform;
    [SerializeField] private Transform spineTransform;
    [SerializeField] private Transform sleeveSpawnerTransform;
    [SerializeField] private Transform rifleBarrelTransform;
    [SerializeField] private Transform leftArmTransform;
    [SerializeField] private Transform handTransform;
    [Space(25)]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject sleeve;
    [SerializeField] private GameObject bullet;
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private EventSystem eventSystem;

    [SerializeField] public static UnityEvent ShootEvent = new UnityEvent(); 

    private Camera mainCamera;

    private bool paused;
    private float fireRate = 1.1f;
    private float nextFire = 0.0f;
    private int bulletsNumber = 10;


    private void Start()
    {
        mainCamera = Camera.main.GetComponent<Camera>();

        GameObject canvasEventSystem = GameObject.FindGameObjectWithTag("EventSystem");
        eventSystem = canvasEventSystem.GetComponent<EventSystem>();

        GameCanvas.pauseEvent.AddListener(Pause);

        if (!YandexGame.savesData.sounds)
        {
            audioSource.volume = 0.0f;
        }
    }

    private void Update()
    {
        if(!paused)
        {
            if (!eventSystem.IsPointerOverGameObject())
            {               
                Aiming();
                
                if (bulletsNumber > 0 && Time.time > nextFire)
                {
                    if (Input.GetMouseButton(0))
                    {
                        animator.SetBool("Aiming", true);
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        ShootEvent.Invoke();
                        nextFire = Time.time + fireRate;
                        bulletsNumber--;
                        SpawnBullet();
                    }
                }              
            }

            LeftArmRotation();
        }             
    }

    public void Aiming()
    {
        Vector2 mousePosition = Input.mousePosition;
        sholderTransform.rotation = SholderRotationAngle(mousePosition);
        neckTransform.rotation = NeckRotationAngle(mousePosition);
        spineTransform.rotation = SpineRotationAngle(mousePosition);
    }

    public void SpawnBullet()
    {
        GameObject bulletObject = Instantiate(bullet, rifleBarrelTransform.position, rifleBarrelTransform.rotation);
        Rigidbody2D bulletRigid = bulletObject.GetComponent<Rigidbody2D>();
        bulletRigid.velocity = rifleBarrelTransform.right * 60f;

        audioSource.Play();
        muzzleEffect.Play();
        animator.SetBool("Aiming", false);
    }

    public void SpawnSleeve() // Called from noob shooting animation.
    {
       Instantiate(sleeve, sleeveSpawnerTransform.position, sleeveSpawnerTransform.rotation);       // There also will be a sleevers pool.
    }

    private Quaternion NeckRotationAngle(Vector2 mousePos)
    {
        Vector2 neckPosition = mainCamera.WorldToScreenPoint(neckTransform.position);
        Vector2 direction = mousePos - neckPosition;

        float rawAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(rawAngle, -neckMaxAngle, neckMaxAngle);
        return  Quaternion.Euler(new Vector3(0, 0, clampedAngle));       
    }
    private Quaternion SholderRotationAngle(Vector3 mousePos)
    {
        Vector3 sholderPosition = mainCamera.WorldToScreenPoint(sholderTransform.position);
        Vector2 direction = mousePos - sholderPosition;

        float rawAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(rawAngle, -sholderMaxAngle, sholderMaxAngle);
        return Quaternion.Euler(new Vector3(0, 0, clampedAngle));
    }
    private Quaternion SpineRotationAngle(Vector3 mousePos)
    {
        Vector3 spinePosition = mainCamera.WorldToScreenPoint(spineTransform.position);
        Vector2 direction = mousePos - spinePosition;

        float rawAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(rawAngle, -spineMaxAngle, spineMaxAngle);
        return Quaternion.Euler(new Vector3(0, 0, clampedAngle));
    }
    private void LeftArmRotation()
    {
        Vector3 direction = handTransform.position - leftArmTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        leftArmTransform.rotation = rotation;
    }


    private void Pause()
    {
        if(!paused)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }
    }
}