using System.Collections;
using UnityEngine;

public class LongRangeWeapon : MonoBehaviour
{
    public GameObject bullet;               // prefab dan
    public Transform firePos;               // vi tri ban
    public float TimeBtwFire = 0.2f;        // thoi gian delay ban
    public float bulletForce;               // luc ban dan

    public int currentAmmo;                 // so dan hien tai
    private int maxAmmo = 12;               // dan toi da

    private float timeBtwFire;

    public float timeReload = 2f;           // thoi gian nap dan
    private float timedelay;
    private bool isReloading = false;

    public int bulletCount = 1;             // so vien dan ban lien tiep
    public float timeBetweenShots = 0.1f;   // khoang cach thoi gian giua cac vien dan

    [SerializeField] private AudioManager audioManager;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        RotateWeapon();
        timeBtwFire -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timeBtwFire < 0 && currentAmmo > 0 && !isReloading)
        {
            StartCoroutine(ShootBullets()); // ban nhieu dan lien tiep
        }

        ReloadAmmo();
    }

    void RotateWeapon()
    {
        // lay vi tri chuot
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            // lat vu khi khi xoay nguoc
            transform.localScale = new Vector3(1, -1, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
    }

    IEnumerator ShootBullets()
    {
        timeBtwFire = TimeBtwFire;

        for (int i = 0; i < bulletCount && currentAmmo > 0; i++)
        {
            // tao dan
            GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);

            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

            audioManager.PlayShootSound(); // phat am thanh ban dan

            yield return new WaitForSeconds(timeBetweenShots); // delay giua cac vien dan
        }

        currentAmmo--;
    }

    void ReloadAmmo()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo && !isReloading)
        {
            isReloading = true;
            timedelay = timeReload;
            audioManager.PlayReloadSound(); // phat am thanh nap dan
        }
        if (isReloading)
        {
            timedelay -= Time.deltaTime;
            if (timedelay < 0)
            {
                currentAmmo = maxAmmo;
                isReloading = false;
            }
        }
    }

    // ham duoc goi boi PowerUpBullet de tang so vien dan
    public void IncreaseBulletCount(int amount)
    {
        bulletCount += amount;
        audioManager.PlayBuffSound(); // phat am thanh buff
    }
}
