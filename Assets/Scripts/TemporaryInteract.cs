using UnityEngine;

public class TemporaryInteract : MonoPoolable, IInterectable
{
    public void Interact()
    {
        PlayerInfo.TemporaryCollected += 1;
        ReturnToPool();
        Debug.Log($"collected permanent booster!");
    }
}