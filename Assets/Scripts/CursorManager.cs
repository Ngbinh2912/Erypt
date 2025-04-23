using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNormal;
    [SerializeField] private Texture2D cursorAttack;
    [SerializeField] private Texture2D cursorReload;
    private Vector2 hotspot = new Vector2 (16, 48);

    public float timedelay;
    private bool isReloading = false;

    private LongRangeWeapon weapon;

    void Start()
    {
        Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto);
        weapon = FindObjectOfType<LongRangeWeapon>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorAttack, hotspot, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto);
        }

        if (Input.GetMouseButtonDown(1) && !isReloading)
        {
            isReloading = true;
            timedelay = weapon.timeReload;
        }
        if (isReloading)
        {
            Cursor.SetCursor(cursorReload, hotspot, CursorMode.Auto);
            timedelay -= Time.deltaTime;
            if (timedelay < 0)
            {
                Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto);
                isReloading = false;
            }
        }
    }
}
