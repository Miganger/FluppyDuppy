using UnityEngine;
using UnityEngine.Pool;

public abstract class PooledObject : MonoBehaviour
{
    private IObjectPool<PooledObject> pool;

    public virtual void Setup(IObjectPool<PooledObject> pool)
    {
        this.pool = pool;
    }

    public void ReturnToPool()
    {
        pool?.Release(this);
    }
}