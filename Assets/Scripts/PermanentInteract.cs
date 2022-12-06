using UnityEngine;

public class PermanentInteract : MonoPoolable, IInterectable
{
    public void Interact()
    {
        PlayerInfo.PermanentCollected += 1;
        ReturnToPool();

        Debug.Log($"collected permanent booster!");
    }
}