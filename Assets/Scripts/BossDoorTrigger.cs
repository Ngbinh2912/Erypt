using UnityEngine;

public class BossDoorTrigger : DoorTrigger
{
    protected override void OnPlayerEnterRoom()
    {
        base.OnPlayerEnterRoom();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBossMusic();
        }

        Debug.Log("BossDoorTrigger activated: Boss music started");
    }

    public override void UnlockDoors()
    {
        base.UnlockDoors();
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayDefaultMusic();
        }
    }
}
