using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    public GameObject gameManagerPrefab;
    private static bool hasBootstrapped = false;

    void Awake()
    {
        if (!hasBootstrapped)
        {
            Instantiate(gameManagerPrefab);
            hasBootstrapped = true;
        }
    }
}