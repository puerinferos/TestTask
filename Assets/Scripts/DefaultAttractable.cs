using DefaultNamespace;
using UnityEngine;

public class DefaultAttractable : MonoBehaviour, IAttractable
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Attract(Transform objectAttractTo, float force)
    {
        rb.useGravity = false;
        rb.AddForce(objectAttractTo.position - transform.position);
    }

    public void StopAttract()
    {
        rb.useGravity = true;
    }
}