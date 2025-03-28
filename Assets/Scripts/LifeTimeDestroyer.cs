using UnityEngine;

public class LifeTimeDestroyer : MonoBehaviour
{

    public float lifeTime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
