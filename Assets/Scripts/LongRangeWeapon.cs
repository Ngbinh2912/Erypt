using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LongRangeWeapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public float TimeBtwFire = 0.5f;
    public float bulletForce;

    public int currentAmmo;
    private int maxAmmo = 5;

    private float timeBtwFire;

    public float timeReload = 2f;
    private float timedelay;
    private bool isReloading = false;


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
            FireBullet();
        }
        ReloadAmmo();
    }

    void RotateWeapon()
    {
        //lay vi tri chuot
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            //thay doi huong xoay vu khi
            transform.localScale = new Vector3(1, -1, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
    }

    void FireBullet()
    {
        timeBtwFire = TimeBtwFire;

        //tao ra vien dan
        GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);

        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
        currentAmmo--;
    }

    void ReloadAmmo()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo && !isReloading)
        {
            isReloading = true;
            timedelay = timeReload;
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
}
