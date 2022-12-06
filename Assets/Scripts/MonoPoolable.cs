using UnityEngine;

public class MonoPoolable : MonoBehaviour
{
    private SimpleObjectPool _objectPool;

    public void PoolInitialize(SimpleObjectPool objectPool) =>
        _objectPool = objectPool;

    public void ReturnToPool() =>
        _objectPool.ReturnToPool(this);
}