using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LongRangeWeapon : MonoBehaviour
{

    public GameObject bullet;
    public Transform firePos;
    public float TimeBtwFire = 0.5f;
    public float bulletForce;

    private float timeBtwFire;



    // Update is called once per frame
    void Update()
    {
        RotateGun();
        timeBtwFire -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timeBtwFire < 0)
        {
            FireBullet();
        }
    }

    void RotateGun()
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
    }
}
