using UnityEngine;

public class GiantSword : MonoBehaviour
{
    private Animator animator;
    private Transform parentTransform;

    void Start()
    {
        animator = GetComponent<Animator>();
        parentTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        RotateWeapon();
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    void RotateWeapon()
    {
        //lay vi tri chuot
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;

        if (transform.eulerAngles.z >= 90 && transform.eulerAngles.z < 270)
        {
            //thay doi huong xoay vu khi
            parentTransform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            parentTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
